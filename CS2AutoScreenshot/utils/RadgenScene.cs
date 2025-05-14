using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Numerics;
using ValveResourceFormat.Utils;

// radgen am i right? hah
namespace RadGenCore.Components
{
    public class RadgenScene
    {
        public static Transforms DefaultTransforms => new(Vector3.Zero, Vector3.Zero, Vector3.One);

        public Transforms SceneTransform;

        public RadgenScene(Transforms? transforms = null)
        {
            if (transforms == null)
            {
                SceneTransform = DefaultTransforms;
            }
            else
            {
                SceneTransform = transforms;
            }
        }

        #region Transforms class

        public class Transforms
        {
            public Matrix4x4 TransformMatrix;

            public static Transforms operator *(Transforms a, Transforms b)
            {
                return new Transforms(a.TransformMatrix * b.TransformMatrix);
            }

            public (Vector3 Angles, Vector3 Origin, Vector3 Scales) Decompose()
            {
                Matrix4x4.Decompose(TransformMatrix, out var scale, out var rotation, out var translation);

                return (QuaterionToAngles(rotation), translation, scale);
            }

            public Transforms(Matrix4x4 transformMatrix)
            {
                TransformMatrix = transformMatrix;
            }

            public Transforms(Vector3 angles, Vector3 origin, Vector3 scale)
            {
                TransformMatrix = CreateModelMatrix(origin, angles, scale);
            }

            public Transforms(Quaternion angles, Vector3 origin, Vector3 scale)
            {
                TransformMatrix = CreateModelMatrix(origin, angles, scale);
            }

            public static Transforms GetDeltaTransforms(Transforms transforms1, Transforms transforms2)
            {
                Matrix4x4.Decompose(transforms2.TransformMatrix, out Vector3 transforms2Scale, out Quaternion transforms2Angles, out Vector3 transforms2Origin);
                Matrix4x4.Decompose(transforms1.TransformMatrix, out Vector3 transforms1Scale, out Quaternion transforms1Angles, out Vector3 transforms1Origin);

                var deltaTransformsPosition = transforms1Origin - transforms2Origin;
                var deltaTransformsScale = transforms1Scale / transforms2Scale;
                var deltaAngle = transforms1Angles * Quaternion.Inverse(transforms2Angles);

                return new Transforms(deltaAngle, deltaTransformsPosition, deltaTransformsScale);
            }
        }

        #endregion

        abstract public class SceneNode
        {
            public Transforms Transforms;

            public SceneNode(Transforms? transforms = null)
            {
                if (transforms == null)
                {
                    Transforms = DefaultTransforms;
                }
                else
                {
                    Transforms = transforms;
                }
            }
        }

        public class Mesh : SceneNode
        {
            public float[] Vertices;
            public float[] Normals;
            public Dictionary<VmapParser.RadGenType, List<int>> TriangleLists { get; private set; } = [];

            public Vector4 RenderColor { get; set; }

            public Mesh(IEnumerable<Vector3> vertices, IEnumerable<Vector3> normals, Transforms? transforms = null) : base(transforms)
            {
                var vertexList = vertices.ToList();
                var normalList = normals.ToList();

                Vertices = new float[vertexList.Count * 3];
                Normals = new float[normalList.Count * 3];

                for (var i = 0; i < vertexList.Count; i++)
                {
                    var newMeshVerticesIndex = i * 3;

                    var vertex = vertexList[i];
                    Vertices[newMeshVerticesIndex] = vertex.X;
                    Vertices[newMeshVerticesIndex + 1] = vertex.Y;
                    Vertices[newMeshVerticesIndex + 2] = vertex.Z;
                }

                for (var i = 0; i < normalList.Count; i++)
                {
                    var newMeshVerticesIndex = i * 3;

                    var normal = normalList[i];
                    Normals[newMeshVerticesIndex] = normal.X;
                    Normals[newMeshVerticesIndex + 1] = normal.Y;
                    Normals[newMeshVerticesIndex + 2] = normal.Z;
                }
            }

            public void AddTriangles(VmapParser.RadGenType type, List<int> triangles)
            {
                if (triangles.Count % 3 != 0)
                {
                    throw new InvalidDataException($"Trying to add an invalid amount of indices to mesh with type '{type}', amount: '{triangles.Count}', needs to be divisible by 3 to form valid triangles!");
                }

                var typeEntryExists = TriangleLists.TryGetValue(type, out var triangleList);
                if (!typeEntryExists || triangleList == null)
                {
                    triangleList = new List<int>();
                    TriangleLists.Add(type, triangleList);
                }

                triangleList.AddRange(triangles);
            }
        }

        public class KeyValues
        {
            private Dictionary<string, string> _keyValues = [];

            public bool AddKey(string key, string value)
            {
                return _keyValues.TryAdd(key, value);
            }

            public bool HasHey(string key)
            {
                return _keyValues.ContainsKey(key);
            }

            public T? GetValue<T>(string key)
            {
                bool containsValue = _keyValues.TryGetValue(key, out var value);

                if (containsValue == false || value == null)
                {
                    return default;
                }

                var type = typeof(T);

                if (Nullable.GetUnderlyingType(type) != null)
                {
                    type = Nullable.GetUnderlyingType(type);
                }

                if (type == typeof(Single))
                {
                    return (T?)(object)Single.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (type == typeof(Double))
                {
                    return (T?)(object)Double.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (type == typeof(Int32))
                {
                    return (T?)(object)Int32.Parse(value);
                }
                else if (type == typeof(Int64))
                {
                    return (T?)(object)Int64.Parse(value);
                }
                else if (type == typeof(Decimal))
                {
                    return (T?)(object)Decimal.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (type == typeof(String))
                {
                    return (T?)(object)value;
                }

                return default;
            }

            public static Vector4 ParseVector(string input)
            {
                if (string.IsNullOrEmpty(input))
                {
                    return default;
                }
                var split = input.Split(' ');


                if (split.Length == 3)
                {
                    return new Vector4(
                    float.Parse(split[0], CultureInfo.InvariantCulture),
                    float.Parse(split[1], CultureInfo.InvariantCulture),
                    float.Parse(split[2], CultureInfo.InvariantCulture),
                    0);
                }
                else if (split.Length == 4)
                {
                    return new Vector4(
                    float.Parse(split[0], CultureInfo.InvariantCulture),
                    float.Parse(split[1], CultureInfo.InvariantCulture),
                    float.Parse(split[2], CultureInfo.InvariantCulture),
                    float.Parse(split[3], CultureInfo.InvariantCulture));
                }

                return default;
            }
        }

        public class Entity : SceneNode
        {
            public KeyValues KeyValues { get; private set; } = new();

            public List<Mesh> Meshes { get; private set; } = [];

            public bool IsMeshEntity { get; private set; } = false;
            public bool IsPointEntity { get; private set; } = false;

            public Entity(Transforms? transforms = null, bool isMeshEntity = false, KeyValues? keyValues = null, bool isPointEntity = false) : base(transforms)
            {
                if (keyValues != null)
                {
                    KeyValues = keyValues;
                }

                IsMeshEntity = isMeshEntity;
                IsPointEntity = isPointEntity;
            }
        }

        public class Instance : SceneNode
        {
            public RadgenScene InstanceScene { get; private set; }

            public Instance(RadgenScene scene, Transforms? transforms = null) : base(transforms)
            {
                InstanceScene = scene;
            }
        }

        public List<Mesh> Meshes = [];

        public void AddMesh(Mesh mesh)
        {
            Meshes.Add(mesh);

            if (SceneTransform.TransformMatrix != DefaultTransforms.TransformMatrix)
            {
                mesh.Transforms *= SceneTransform;
            }
        }

        public List<Entity> Entities = [];

        public Entity? GetEntityByClassname(string classname)
        {
            foreach (var entity in Entities)
            {
                var entityClassname = entity.KeyValues.GetValue<string>(classname);

                if (entityClassname != null)
                {
                    if (entityClassname == classname)
                    {
                        return entity;
                    }
                }
            }

            return null;
        }

        public List<Entity> GetEntitiesByClassname(string classname)
        {
            var entities = new List<Entity>();

            foreach (var entity in Entities)
            {
                var entityClassname = entity.KeyValues.GetValue<string>(classname);

                if (entityClassname != null)
                {
                    if (entityClassname == classname)
                    {
                        entities.Add(entity);
                    }
                }
            }

            return entities;
        }

        public void AddEntity(Entity entity)
        {
            Entities.Add(entity);

            if (SceneTransform.TransformMatrix != DefaultTransforms.TransformMatrix)
            {
                // needs special treatment because for mesh entities the actual entity wont have transforms, it will only be the meshes
                // because hammer is fucking weird
                if (entity.IsMeshEntity)
                {
                    foreach (var mesh in entity.Meshes)
                    {
                        mesh.Transforms *= SceneTransform;
                    }
                }
                else
                {
                    entity.Transforms *= SceneTransform;
                }
            }
        }

        public List<Instance> Instances = [];

        public void AddInstance(Instance instance)
        {
            if (SceneTransform.TransformMatrix != DefaultTransforms.TransformMatrix)
            {
                instance.Transforms *= SceneTransform;
            }

            Instances.Add(instance);
        }

        public List<RadgenScene> ChildScenes = [];

        public void AddScene(RadgenScene scene)
        {
            ChildScenes.Add(scene);
        }

        #region Utils

        public static Vector3 QuaterionToAngles(Quaternion quat)
        {
            var angles = new Vector3();

            var m11 = (2.0 * quat.W * quat.W) + (2.0 * quat.X * quat.X) - 1.0;
            var m12 = (2.0 * quat.X * quat.Y) + (2.0 * quat.W * quat.Z);
            var m13 = (2.0 * quat.X * quat.Z) - (2.0 * quat.W * quat.Y);
            var m23 = (2.0 * quat.Y * quat.Z) + (2.0 * quat.W * quat.X);
            var m33 = (2.0 * quat.W * quat.W) + (2.0 * quat.Z * quat.Z) - 1.0;

            // FIXME: this code has a singularity near PITCH +-90
            angles.Y = (float)(57.295779513 * Math.Atan2(m12, m11));
            angles.X = (float)(57.295779513 * Math.Asin(-m13));
            angles.Z = (float)(57.295779513 * Math.Atan2(m23, m33));

            return angles;
        }

        public static Matrix4x4 CreateModelMatrix(Vector3 position, Vector3 rotation, Vector3 scale)
        {
            // create translation and scale matrices
            Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(position);
            Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale);

            float pitch = rotation.X * (MathF.PI / 180f); // rotation around Y-axis (pitch)
            float yaw = rotation.Y * (MathF.PI / 180f);   // rotation around Z-axis (yaw)
            float roll = rotation.Z * (MathF.PI / 180f);  // rotation around X-axis (roll)

            Matrix4x4 rotationX = Matrix4x4.CreateRotationY(pitch);  // roll: X-axis rotation
            Matrix4x4 rotationY = Matrix4x4.CreateRotationX(roll); // pitch: Y-axis rotation
            Matrix4x4 rotationZ = Matrix4x4.CreateRotationZ(yaw);   // yaw: Z-axis rotation

            // combine rotations: roll (X) -> pitch (Y) -> yaw (Z)
            var rotationMatrix = rotationY * rotationX * rotationZ;

            // combine the transformations: scale -> rotate -> translate.
            return scaleMatrix * rotationMatrix * translationMatrix;
        }

        public static Matrix4x4 CreateModelMatrix(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            // create translation and scale matrices
            Matrix4x4 translationMatrix = Matrix4x4.CreateTranslation(position);
            Matrix4x4 scaleMatrix = Matrix4x4.CreateScale(scale);
            Matrix4x4 rotationMatrix = Matrix4x4.CreateFromQuaternion(rotation);

            // combine the transformations: scale -> rotate -> translate.
            return scaleMatrix * rotationMatrix * translationMatrix;
        }

        public static Vector3 CalculateEulerAngleDelta(Vector3 fromEuler, Vector3 toEuler)
        {
            Vector3 delta = new Vector3();

            delta.X = DeltaAngle(fromEuler.X, toEuler.X);

            delta.Y = DeltaAngle(fromEuler.Y, toEuler.Y);

            delta.Z = DeltaAngle(fromEuler.Z, toEuler.Z);

            return delta;
        }

        private static float DeltaAngle(float current, float target)
        {
            float delta = target - current;

            // Normalize the delta to be between -180 and 180 degrees
            delta = ((delta + 180) % 360) - 180;

            // Handle the special case for -180 vs 180
            if (delta == -180)
            {
                delta = 180;
            }

            return delta;
        }

        #endregion
    }
}
