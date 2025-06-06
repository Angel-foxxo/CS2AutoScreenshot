using Datamodel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using ValveResourceFormat;

// stolen from radgen and cut down to only parse entities
// in the future i will make a proper app agnostic library for parsing vmaps
// but today is not the day
namespace RadGenCore.Components
{
    public static class VmapParser
    {
        // all of this is just for logging
        // name of the vmap being processed currently, used in error messages
        private static string CurrentVmapName = "";

        static VmapParser()
        {
        }

        // this exists simply because we need to pass this data around to basically everything and otherwise every function would need
        // all of these as parameters
        // the reason this isnt just created once and referenced by everything directly is because this way every function can be static and have no sideeffect due to
        // global state, it means we can also easily unit test this code
        public struct VmapParserContext
        {
            public bool OnlyParsePointEntities;
            public RadgenScene.Transforms VmapTransforms;
            public Dictionary<string, Resource> ModelResourceDict;
            public Dictionary<Guid, RadgenScene> ProcessedInstanceContents;
            public Dictionary<Guid, int> VisibilityDict;
            public string InitialVmapFolder;
        }

        public static RadgenScene? ParseVmap(string VmapFilePath, VmapParserContext vmapParserContext, bool calledForPrefab = false)
        {
            string normalizedPath = VmapFilePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            int index = normalizedPath.IndexOf("maps" + Path.DirectorySeparatorChar);
            if (index >= 0)
            {
                CurrentVmapName = normalizedPath.Substring(index + "maps".Length + 1);
            }

            var localVmapName = CurrentVmapName;

            if (!File.Exists(VmapFilePath))
            {
                return null;
            }

            if (!DirectoryAndFileHelpers.CheckFileIsNotLocked(VmapFilePath))
            {
                throw new InvalidOperationException($"Vmap '{localVmapName}' is locked: '{VmapFilePath}'");
            }

            var vmapFolder = Path.GetDirectoryName(VmapFilePath);

            if (vmapFolder == null)
            {
                throw new InvalidOperationException("Failed to get vmap folder");
            }

            vmapParserContext.InitialVmapFolder = vmapFolder;

            var parsedRadgenScene = new RadgenScene(vmapParserContext.VmapTransforms);

            using FileStream fs = File.Open(VmapFilePath, FileMode.Open);
            var vmap = Datamodel.Datamodel.Load(fs);

            if (vmapParserContext.ModelResourceDict == null)
            {
                vmapParserContext.ModelResourceDict = new();
            }

            if (vmapParserContext.ProcessedInstanceContents == null)
            {
                vmapParserContext.ProcessedInstanceContents = new();
            }

            // this is the main object that everything else is referenced in, things might not directly be a child of it but just have a GUID reference in it
            // because in kv2 when an object needs to be referenced in multiple places, its usually put in the Root of the datamodel, and everything else just
            // uses its GUID for referencing, its therefor important we dont just loop every element in the datamodel because that makes no sense, we
            // need to follow the references inside of `world`
            var world = (Element)vmap.Root["world"];

            // GUESS WHAT? THEY FIXED THE MISSPELLING!
            // now we have to check for both in case someone has an old vmap!
            Element? visibility = null;
            try
            {
                // visbility - yes its actually written like that in the vmaps, it took hours to debug
                visibility = (Element)vmap.Root["visbility"];
            }
            catch
            {
                visibility = (Element)vmap.Root["visibility"];
            }

            var visibilityChildren = (ElementArray)visibility["nodes"];
            Dictionary<Guid, int> visibilityDict = new();

            var hiddenFlags = (IntArray)visibility["hiddenFlags"];

            for (int i = 0; i < visibilityChildren.Count; i++)
            {
                visibilityDict.Add(visibilityChildren[i].ID, hiddenFlags[i]);
            }

            vmapParserContext.VisibilityDict = visibilityDict;

            // because of how stupidly instances are stored, sadly we need to loop every element in the vmap here
            // check if its an instance, and preprocess that, this needs to be done so later when we process groups
            // we dont accidentally process an instance group, we can "pretend" that the original instance contents
            // are a scene, this maps nicely to how we treat prefabs
            foreach (var child in vmap.AllElements)
            {
                if (child.ClassName == "CMapInstance")
                {
                    CacheInstanceGroup(child, vmapParserContext);
                }
            }

            // give it the scene and let it parse, we can treat the world as a group because it seems to be
            // inhereted from it with no change other than classname
            // when this is done the scene we passed into it should be filled out
            ParseGroup(world, ref parsedRadgenScene, vmapParserContext);

            return parsedRadgenScene;
        }

        private static void ParseGroup(Element group, ref RadgenScene scene, VmapParserContext vmapParserContext)
        {
            // dont process groups which come from instances
            if (vmapParserContext.ProcessedInstanceContents.ContainsKey(group.ID))
            {
                return;
            }

            var groupChildren = (ElementArray)group["children"];
            foreach (var child in groupChildren)
            {
                if (child.ClassName == "CMapGroup")
                {
                    ParseGroup(child, ref scene, vmapParserContext);
                }

                if (child.ClassName == "CMapPrefab")
                {
                    ParseCMapPrefabElement(child, ref scene, vmapParserContext);
                }

                if (child.ClassName == "CMapInstance")
                {
                    var instance = ParseInstance(child, vmapParserContext);

                    if (instance != null)
                    {
                        scene.AddInstance(instance);
                    }
                }

                if (child.ClassName == "CMapEntity")
                {
                    ParseCMapEntity(child, ref scene, vmapParserContext);
                }
            }
        }

        private static void ParseCMapPrefabElement(Element prefab, ref RadgenScene scene, VmapParserContext vmapParserContext)
        {
            if (!IsVisible(prefab.ID, vmapParserContext.VisibilityDict))
            {
                return;
            }

            var prefabVmapName = (string)prefab["targetMapPath"];

            var transforms = new RadgenScene.Transforms((QAngle)prefab["angles"], (Vector3)prefab["origin"], (Vector3)prefab["scales"]);

            transforms *= scene.SceneTransform;

            vmapParserContext.VmapTransforms = transforms;

            var addonFolder = Directory.GetParent(vmapParserContext.InitialVmapFolder);

            if(addonFolder == null)
            {
                return;
            }

            var diskVmapPath = Path.Combine(addonFolder.FullName, prefabVmapName);

            var prefabScene = ParseVmap(diskVmapPath, vmapParserContext, true);

            if (prefabScene != null)
            {
                scene.AddScene(prefabScene);
            }
        }


        private static void ParseCMapEntity(Element cMapEntity, ref RadgenScene scene, VmapParserContext vmapParserContext)
        {
            if (!IsVisible(cMapEntity.ID, vmapParserContext.VisibilityDict))
            {
                return;
            }

            var entityProperties = (Element)cMapEntity["entity_properties"];

            var entityKeyValues = new RadgenScene.KeyValues();
            foreach (var kv in entityProperties)
            {
                if(kv.Value is string)
                {
                    entityKeyValues.AddKey(kv.Key, (string)kv.Value);
                }
            }

            var classname = (string)entityProperties["classname"];

            var pointEntity = ParseRecognizedEntity(cMapEntity, entityKeyValues, classname, vmapParserContext);

            if (pointEntity != null)
            {
                scene.AddEntity(pointEntity);
            }
        }

        private static RadgenScene.Entity ParsePointEntity(Element entityElement, RadgenScene.KeyValues entityKv)
        {
            var newEntity = new RadgenScene.Entity(new RadgenScene.Transforms((QAngle)entityElement["angles"], (Vector3)entityElement["origin"], (Vector3)entityElement["scales"]), false, entityKv, true);

            return newEntity;
        }

        private static RadgenScene.Entity? ParseRecognizedEntity(Element entity, RadgenScene.KeyValues entityKv, string classname, VmapParserContext vmapParserContext)
        {
            switch (classname)
            {
                case "point_camera":
                    return ParsePointEntity(entity, entityKv);
            }

            return null;
        }

        private static void CacheInstanceGroup(Element instance, VmapParserContext vmapParserContext)
        {
            var target = (Element)instance["target"];

            var instanceScene = new RadgenScene(RadgenScene.DefaultTransforms);
            ParseGroup(target, ref instanceScene, vmapParserContext);

            if (!vmapParserContext.ProcessedInstanceContents.ContainsKey(target.ID))
            {
                vmapParserContext.ProcessedInstanceContents.Add(target.ID, instanceScene);

            }
        }

        private static RadgenScene.Instance? ParseInstance(Element instance, VmapParserContext vmapParserContext)
        {
            if (!IsVisible(instance.ID, vmapParserContext.VisibilityDict))
            {
                return null;
            }

            var target = (Element)instance["target"];

            var instanceTransforms = new RadgenScene.Transforms((QAngle)instance["angles"], (Vector3)instance["origin"], (Vector3)instance["scales"]);
            var targetGroupTransforms = new RadgenScene.Transforms((QAngle)target["angles"], (Vector3)target["origin"], (Vector3)target["scales"]);

            var isInstanceCached = vmapParserContext.ProcessedInstanceContents.TryGetValue(target.ID, out RadgenScene? cachedScene);

            // first cache miss, try to cache it again using this group, this can happen with nested instances if we try to cache an instance group
            // which also contains an instance
            if (isInstanceCached == false || cachedScene == null)
            {
                CacheInstanceGroup(instance, vmapParserContext);
                vmapParserContext.ProcessedInstanceContents.TryGetValue(target.ID, out cachedScene);
            }

            if (cachedScene == null)
            {
                throw new InvalidDataException("Attempting to process an instance whose base contents we have not cached!");
            }

            var newInstance = new RadgenScene.Instance(cachedScene, RadgenScene.Transforms.GetDeltaTransforms(instanceTransforms, targetGroupTransforms));


            return newInstance;
        }

        private static bool IsVisible(Guid elementID, Dictionary<Guid, int> visibilityDict)
        {
            bool containedInVisibilityDict = visibilityDict.TryGetValue(elementID, out var visibilityDictValue);

            if (containedInVisibilityDict == false || visibilityDictValue == 0)
            {
                return true;
            }

            return false;

        }
    }
}
