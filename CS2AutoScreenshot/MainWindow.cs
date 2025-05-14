using Gdk;
using Gtk;
using RadGenCore.Components;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using UI = Gtk.Builder.ObjectAttribute;

namespace CS2AutoScreenshot
{
    class MainWindow : Gtk.Window
    {
        [UI]
        Button? SelectVMAP = null;

        [UI]
        TextBuffer? MainTextBuffer = null;

        public MainWindow() : this(new Builder("MainWindow.glade")) { }

        private class Camera
        {
            public RadgenScene.Transforms Transforms { private set; get; }
            public float FOV { private set; get; }

            public Camera(RadgenScene.Transforms transforms, float fOV)
            {
                Transforms = transforms;
                FOV = fOV;
            }
        }

        private MainWindow(Builder builder) : base(builder.GetRawOwnedObject("MainWindow"))
        {
            builder.Autoconnect(this);

            try
            {
                Assembly assembly = Assembly.GetExecutingAssembly();
                string resourceName = "CS2AutoScreenshot.misc.screenshot.png";
                var resource = assembly.GetManifestResourceStream(resourceName);
                if (resource != null)
                {
                    using (Stream iconStream = resource)
                    {
                        if (iconStream != null)
                        {
                            Pixbuf originalIcon = new Pixbuf(iconStream);

                            int[] sizes = new int[] { 16, 32, 48, 64, 128, 256 };
                            List<Pixbuf> iconList = new List<Pixbuf>();

                            foreach (int size in sizes)
                            {
                                Pixbuf scaledIcon = originalIcon.ScaleSimple(
                                    size, size, InterpType.Hyper);
                                iconList.Add(scaledIcon);
                            }

                            DefaultIconList = iconList.ToArray();
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            SelectVMAP!.Clicked += MainWindow_Clicked;

            DeleteEvent += Window_DeleteEvent;
        }

        private void MainWindow_Clicked(object? sender, EventArgs e)
        {
            NativeFileDialogs.Net.Nfd.OpenDialog(out string? vmapFilePath, new Dictionary<string, string> { { "VMAP Files", "vmap" } });

            if(System.IO.Path.GetExtension(vmapFilePath) != ".vmap")
            {
                MainTextBuffer!.Text = "Selected file is not a vmap!";
                return;
            }

            if (!string.IsNullOrEmpty(vmapFilePath))
            {
                var context = new VmapParser.VmapParserContext
                {
                    OnlyParsePointEntities = true,
                };

                var parsedScene = VmapParser.ParseVmap(vmapFilePath, context);

                List<Camera> pointCameras = new();

                if (parsedScene != null)
                {
                    pointCameras = GetPointCamerasFromScene(parsedScene, RadgenScene.DefaultTransforms);
                }

                //make it file system safe
                string currentDate = DateTime.Now.ToString(CultureInfo.InstalledUICulture);
                foreach (var c in System.IO.Path.GetInvalidFileNameChars())
                {
                    currentDate = currentDate.Replace(c, '.');
                }

                string vmapName = System.IO.Path.GetFileNameWithoutExtension(vmapFilePath);
                string screenshotPath = $"screenshots/CS2AutoScreenshot/{vmapName} - {currentDate};";

                float tick = 1f / 64;

                //kills previous servercommand just incase user ran this before
                string finalOutputCommand = "sv_cheats 1;noclip 1;ent_fire cmd kill;ent_create point_servercommand {targetname cmd};" +
                    $"screenshot_subdir {screenshotPath}";

                if(pointCameras.Count == 0)
                {
                    MainTextBuffer!.Text = "Selected vmap has no point_camera entities!";
                    return;
                }

                for (int i = 0; i < pointCameras.Count; i++)
                {
                    var camera = pointCameras[i];
                    var cameraTransforms = camera.Transforms.Decompose();
                    
                    //make each screenshot take 10 ticks, just to make sure IO has time to process everything
                    //+ 1 second delay so the first screenshot doesn't start with 0 delay, thats too early, the point_servercommmand won't exist yet
                    //also i like the initial delay because the first pic wont just strobe away
                    var delay = i * tick * 10 + 0.1;

                    finalOutputCommand += $"ent_fire worldent addoutput \"OnUser1>cmd>command>setpos {cameraTransforms.Origin.X} {cameraTransforms.Origin.Y} {cameraTransforms.Origin.Z - 64}>{delay}>1\";";
                    finalOutputCommand += $"ent_fire worldent addoutput \"OnUser1>cmd>command>setang {cameraTransforms.Angles.X} {cameraTransforms.Angles.Y} 0>{delay}>1\";";
                    finalOutputCommand += $"ent_fire worldent addoutput \"OnUser1>cmd>command>fov_cs_debug {camera.FOV}>{delay}>1\";";
                    //add extra tick delay just to make sure the screenshot is taken after the camera is in place
                    finalOutputCommand += $"ent_fire worldent addoutput \"OnUser1>cmd>command>png_screenshot {vmapName}>{delay + (tick * 2)}>1\";";
                }

                var finalCommandDelay = (pointCameras.Count - 1) * tick * 10 + 1;
                finalOutputCommand += $"ent_fire worldent addoutput \"OnUser1>cmd>command>r_drawviewmodel 1;cl_drawhud 1;r_drawpanorama 1;noclip 0>{finalCommandDelay}>1\";";

                finalOutputCommand += "r_drawviewmodel 0;cl_drawhud 0;r_drawpanorama 0;ent_fire worldent FireUser1";

                MainTextBuffer!.Text = finalOutputCommand;
            }
        }

        private List<Camera> GetPointCamerasFromScene(RadgenScene scene, RadgenScene.Transforms transforms)
        {
            var returnList = new List<Camera>();
            foreach (var entity in scene.Entities)
            {
                var classname = entity.KeyValues.GetValue<string>("classname");

                if (classname == "point_camera")
                {
                    var objTransforms = entity.Transforms;

                    objTransforms *= transforms;

                    float fov = 90;

                    if (entity.KeyValues.HasHey("fov"))
                    {
                        fov = entity.KeyValues.GetValue<float>("fov");
                    }

                    returnList.Add(new Camera(objTransforms, fov));
                }
            }

            foreach (var prefab in scene.ChildScenes)
            {
                returnList.AddRange(GetPointCamerasFromScene(prefab, scene.SceneTransform));
            }

            foreach (var instance in scene.Instances)
            {
                var childInstanceTransforms = instance.Transforms;
                childInstanceTransforms *= transforms;

                returnList.AddRange(GetPointCamerasFromScene(instance.InstanceScene, childInstanceTransforms));
            }

            return returnList;
        }

        private void Window_DeleteEvent(object sender, DeleteEventArgs a)
        {
            Gtk.Application.Quit();
        }
    }
}
