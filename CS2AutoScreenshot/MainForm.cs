using RadGenCore.Components;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace CS2AutoScreenshot
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            codeTextBox1.Text = InitialText;
        }

        const string InitialText =
            "This tool will scan your map for any 'point_camera' entities and construct a console command which will make the game take a screenshot from the perspective of each camera.\n\n" +
            "After selecting a VMAP, copy the generated commands from here to your console and hit enter (you should only do this in -tools mode).\n\n" +
            "You can click 'Regenerate Commands' to manually regenerate the commands after tweaking settings!\n\n" +
            "If you edit the 'point_camera' entities in your vmap, you will need to reload it using the 'Select VMAP' button.";

        const string LoadingScreenModeText =
            "Loading screen mode enabled!\n\n" +
            "When this is enabled, instead of saving the screenshots to the usual folder, they will be moved to 'panorama\\images\\map_icons\\screenshots'\n\n" +
            "However, in order to accomplish this, you must first click 'Regenerate Commands', copy the commands, paste them into the console and hit enter, after it finishes, go " +
            "to the right of the 'Loading screen mode' setting, and hit the 'Finalise Images' button!\n\n" +
            "This will actually copy the images to the right folder, and generate the required .vtex files for the game to load them!\n\n" +
            "Only the first 10 screenshots will be used, this is the limit CS2 sets.";

        const string VtexTemplate =
            "<!-- dmx encoding keyvalues2_noids 1 format vtex 1 -->\r\n\"" +
            "CDmeVtex\"\r\n" +
            "{\r\n" +
            "    \"m_inputTextureArray\" \"element_array\"\r\n" +
            "    [\r\n" +
            "        \"CDmeInputTexture\"\r\n" +
            "        {\r\n" +
            "            \"m_name\" \"string\" \"InputTexture0\"\r\n" +
            "            \"m_fileName\" \"string\" \"IMAGE_PATH\"\r\n" +
            "            \"m_colorSpace\" \"string\" \"srgb\"\r\n" +
            "            \"m_typeString\" \"string\" \"2D\"\r\n" +
            "            \"m_imageProcessorArray\" \"element_array\"\r\n" +
            "            [\r\n" +
            "                \"CDmeImageProcessor\"\r\n" +
            "                {\r\n" +
            "                    \"m_algorithm\" \"string\" \"None\"\r\n" +
            "                    \"m_stringArg\" \"string\" \"\"\r\n" +
            "                    \"m_vFloat4Arg\" \"vector4\" \"0 0 0 0\"\r\n" +
            "                }\r\n" +
            "            ]\r\n" +
            "        }\r\n" +
            "    ]\r\n" +
            "    \"m_outputTypeString\" \"string\" \"2D\"\r\n" +
            "    \"m_outputFormat\" \"string\" \"BGRA8888\"\r\n" +
            "    \"m_outputClearColor\" \"vector4\" \"0 0 0 0\"\r\n" +
            "    \"m_nOutputMinDimension\" \"int\" \"0\"\r\n" +
            "    \"m_nOutputMaxDimension\" \"int\" \"0\"\r\n" +
            "    \"m_textureOutputChannelArray\" \"element_array\"\r\n" +
            "    [\r\n" +
            "        \"CDmeTextureOutputChannel\"\r\n" +
            "        {\r\n" +
            "            \"m_inputTextureArray\" \"string_array\" [ \"InputTexture0\" ]\r\n" +
            "            \"m_srcChannels\" \"string\" \"rgba\"\r\n" +
            "            \"m_dstChannels\" \"string\" \"rgba\"\r\n" +
            "            \"m_mipAlgorithm\" \"CDmeImageProcessor\"\r\n" +
            "            {\r\n" +
            "                \"m_algorithm\" \"string\" \"None\"\r\n" +
            "                \"m_stringArg\" \"string\" \"\"\r\n" +
            "                \"m_vFloat4Arg\" \"vector4\" \"0 0 0 0\"\r\n" +
            "            }\r\n" +
            "            \"m_outputColorSpace\" \"string\" \"srgb\"\r\n" +
            "        }\r\n" +
            "    ]\r\n" +
            "    \"m_vClamp\" \"vector3\" \"0 0 0\"\r\n " +
            "   \"m_bNoLod\" \"bool\" \"0\"\r\n" +
            "}";

        const string TempFolderName = "_tmp_loading_imgs";

        const string LoadingImagesPath = "panorama\\images\\map_icons\\screenshots\\1080p";

        List<Camera>? CurrentCameras = new();
        string? VmapFilePath = null;

        private List<Camera>? GetCameras()
        {
            List<Camera> pointCameras = new();

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "VMAP Files|*.vmap";
                openFileDialog.Title = "Open VMAP File";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    VmapFilePath = openFileDialog.FileName;
                }
            }

            button1.Text = "Reading VMAP...";
            Application.DoEvents();

            if (!string.IsNullOrEmpty(VmapFilePath))
            {
                var context = new VmapParser.VmapParserContext
                {
                    OnlyParsePointEntities = true,
                };

                var parsedScene = VmapParser.ParseVmap(VmapFilePath, context);

                if (parsedScene != null)
                {
                    pointCameras = GetPointCamerasFromScene(parsedScene, RadgenScene.DefaultTransforms);
                }
            }
            else
            {
                return null;
            }

            button1.Text = "Select VMAP";
            return pointCameras;
        }

        private void GenerateCommands(List<Camera> pointCameras, string vmapFilePath)
        {
            //make it file system safe
            string currentDate = DateTime.Now.ToString(CultureInfo.InstalledUICulture);
            foreach (var c in System.IO.Path.GetInvalidFileNameChars())
            {
                currentDate = currentDate.Replace(c, '.');
            }

            string vmapName = Path.GetFileNameWithoutExtension(vmapFilePath);
            string screenshotPath = GetOutputPath(vmapFilePath, currentDate);

            float tick = 1f / 64;

            //kills previous servercommand just incase user ran this before
            string finalOutputCommand = "sv_cheats 1;noclip 1;ent_fire cmd kill;ent_create point_servercommand {targetname cmd};" +
                $"screenshot_subdir {screenshotPath};";

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

            codeTextBox1.Text = finalOutputCommand;


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

                    if (entity.KeyValues.HasHey("FOV"))
                    {
                        fov = entity.KeyValues.GetValue<float>("FOV");
                    }

                    string? name = null;

                    if (entity.KeyValues.HasHey("targetname"))
                    {
                        name = entity.KeyValues.GetValue<string>("targetname");
                    }

                    returnList.Add(new Camera(objTransforms, fov, name ?? string.Empty));
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

        private void FinaliseLoadingScreenImages(string vmapPath)
        {
            var tempPath = GetTempLoadingImagesPath(vmapPath);
            var loadingScreenImagesPath = GetLoadingScreenImagesPath(vmapPath);

            if (string.IsNullOrEmpty(tempPath) || !Path.Exists(tempPath))
            {
                codeTextBox1.Text =
                    "Failed to find the temporary screenshot folder! This likely means you did not copy and run the commands before clicking the button!\n\n" +
                    "Try again.";
                return;
            }

            if (string.IsNullOrEmpty(loadingScreenImagesPath))
            {
                codeTextBox1.Text = "Failed to get final loading images folder!";
                return;
            }

            DeleteFolderSafer(loadingScreenImagesPath, new string[] { ".png", ".vtex" });
            Directory.CreateDirectory(loadingScreenImagesPath);

            var screenshots = Directory.GetFiles(tempPath, "*.png");

            int skippedImages = 0;
            for (int i = 0; i < screenshots.Length; i++)
            {
                if (i > 9)
                {
                    skippedImages++;
                    continue;
                }

                var vtexFileName = string.Empty;
                var imageFileName = string.Empty;
                var vmapName = Path.GetFileNameWithoutExtension(VmapFilePath!);

                // follow funny valve naming convention
                if (i == 0)
                {
                    vtexFileName = $"{vmapName}_png.vtex";
                    imageFileName = $"{vmapName}.png";
                }
                else
                {
                    vtexFileName = $"{vmapName}_{i}_png.vtex";
                    imageFileName = $"{vmapName}_{i}.png";
                }

                var vtexPngPath = Path.Combine(LoadingImagesPath, imageFileName).Replace("\\", "\\\\");
                var vtexFile = VtexTemplate.Replace("IMAGE_PATH", vtexPngPath);

                var finalImagePath = Path.Combine(loadingScreenImagesPath, imageFileName);

                if (TargetnameAsPlacename_Checkbox.Checked)
                {
                    using Image screenshotImage = Image.FromFile(screenshots[i]);

                    if (CurrentCameras![i].Name != string.Empty)
                    {
                        var text = CurrentCameras![i].Name;

                        using Graphics g = Graphics.FromImage(screenshotImage);

                        g.DrawImage(screenshotImage, 0, 0);

                        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias | System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
                        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                        g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

                        float fontSize = screenshotImage.Height * 0.027f;

                        float marginX = screenshotImage.Width * 0.022f;
                        float marginY = screenshotImage.Height * 0.05f;

                        using Font font = new Font("Bahnschrift Light Condensed", fontSize, FontStyle.Regular);
                        using SolidBrush brush = new SolidBrush(Color.White);

                        SizeF textSize = g.MeasureString(text, font);

                        float x = marginX;
                        float y = screenshotImage.Height - textSize.Height - marginY;

                        g.DrawString(text, font, brush, x, y);
                    }

                    screenshotImage.Save(finalImagePath);
                }
                else
                {
                    File.Copy(screenshots[i], finalImagePath, true);
                }

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(loadingScreenImagesPath, vtexFileName)))
                {
                    outputFile.Write(vtexFile);
                }
            }

            if (skippedImages > 0)
            {
                codeTextBox1.Text = $"Loading image limit of 10 reached! skipped {skippedImages} image(s).";
            }

            DeleteFolderSafer(tempPath, new string[] { ".png" });
        }

        private class Camera
        {
            public RadgenScene.Transforms Transforms { private set; get; }
            public float FOV { private set; get; }
            public string Name { private set; get; }

            public Camera(RadgenScene.Transforms transforms, float fOV, string name)
            {
                Transforms = transforms;
                FOV = fOV;
                Name = name;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CurrentCameras = GetCameras();

            if (string.IsNullOrEmpty(VmapFilePath))
            {
                codeTextBox1.Text = "Failed to load VMAP file!";
                OnFailure();
                return;
            }

            if (CurrentCameras == null)
            {
                codeTextBox1.Text = "Failed to parse VMAP!";
                OnFailure();
                return;
            }

            if (CurrentCameras.Count == 0)
            {
                codeTextBox1.Text = "Selected vmap has no point_camera entities!";
                OnFailure();
                return;
            }

            button2.Enabled = true;
            betterCheckBox1.Enabled = true;
            label1.Text = $"Selected VMAP: {GetShortenedVMAPPath(VmapFilePath!)}";
            SetOutputPathText();

            GenerateCommands(CurrentCameras!, VmapFilePath!);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            GenerateCommands(CurrentCameras!, VmapFilePath!);

            if (betterCheckBox1.Checked)
            {
                betterButton1.Enabled = true;
            }
        }

        private void OnFailure()
        {
            button2.Enabled = false;
            betterCheckBox1.Enabled = false;
            betterButton1.Enabled = false;
            label1.Text = "Selected VMAP: ";
            label1.Text = "Output Path: ";
        }

        private void SetOutputPathText()
        {
            if (!betterCheckBox1.Checked)
            {
                label3.Text = $"Output Path: {Path.Join("game", "csgo_addons", "'ADDON_NAME'", GetOutputPath(VmapFilePath!, "'CURRENT_DATE'"), "'FILE_NAME'")}";
            }
            else
            {
                label3.Text = $"Output Path: {Path.Join("content", "csgo_addons", "'ADDON_NAME'", LoadingImagesPath, "'FILE_NAME'")}";
            }
        }

        private string GetShortenedVMAPPath(string vmapPath)
        {
            int csgoIndex = vmapPath.IndexOf("Counter-Strike Global Offensive");
            if (csgoIndex != -1)
            {
                string afterCsgo = vmapPath.Substring(csgoIndex + "Counter-Strike Global Offensive".Length);
                int contentIndex = afterCsgo.IndexOf("content");
                if (contentIndex != -1)
                {
                    return afterCsgo.Substring(contentIndex);
                }
            }

            // if we fail, just return the full path
            return vmapPath;
        }

        private string? GetTempLoadingImagesPath(string vmapPath)
        {
            string[] parts = vmapPath.Split(Path.DirectorySeparatorChar);

            string newPath = "";

            var separatingChar = Path.DirectorySeparatorChar.ToString();

            for (int i = 0; i < parts.Length; i++)
            {
                if (parts[i] == "content")
                    parts[i] = "game";

                if (i > 0)
                {
                    newPath = $"{newPath}{Path.DirectorySeparatorChar}{parts[i]}";

                    if (parts[i - 1] == "csgo_addons")
                    {
                        return Path.Combine(newPath, TempFolderName);
                    }
                }
                else
                {
                    newPath = parts[i];
                }
            }

            return null;
        }

        private string? GetLoadingScreenImagesPath(string vmapPath)
        {
            string[] parts = vmapPath.Split(Path.DirectorySeparatorChar);

            string newPath = "";

            var separatingChar = Path.DirectorySeparatorChar.ToString();

            for (int i = 0; i < parts.Length; i++)
            {

                if (i > 0)
                {
                    newPath = $"{newPath}{Path.DirectorySeparatorChar}{parts[i]}";

                    if (parts[i - 1] == "csgo_addons")
                    {
                        return Path.Combine(newPath, LoadingImagesPath);
                    }
                }
                else
                {
                    newPath = parts[i];
                }
            }

            return null;
        }

        // this might seem a little funky, but i really would rather not accidentally delete people's stuff
        // so first gonna empty the folder of the target files, and only delete it if it's empty, this at least somewhat
        // ensured we're targeting the right folder.
        private bool DeleteFolderSafer(string folderPath, string[] filesExtensionsToDeleteFirst)
        {
            foreach (var fileExtension in filesExtensionsToDeleteFirst)
            {
                foreach (string file in Directory.GetFiles(folderPath, $"*{fileExtension}"))
                {
                    File.Delete(file);
                }
            }

            if (Directory.GetFiles(folderPath).Length == 0 && Directory.GetDirectories(folderPath).Length == 0)
            {
                Directory.Delete(folderPath);
                return true;
            }

            return false;
        }

        private string GetOutputPath(string vmapPath, string currentDate)
        {
            string vmapName = Path.GetFileNameWithoutExtension(vmapPath);

            if (!betterCheckBox1.Checked)
            {
                return $"screenshots/CS2AutoScreenshot/{vmapName} - {currentDate}";
            }
            else
            {
                return TempFolderName;
            }
        }

        private void betterCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (betterCheckBox1.Checked)
            {
                codeTextBox1.Text = LoadingScreenModeText;
                TargetnameAsPlacename_Checkbox.Enabled = true;
                SetOutputPathText();

            }
            else
            {
                codeTextBox1.Text = InitialText;
                betterButton1.Enabled = false;
                TargetnameAsPlacename_Checkbox.Enabled = false;
                SetOutputPathText();
            }
        }

        private void betterButton1_Click(object sender, EventArgs e)
        {
            FinaliseLoadingScreenImages(VmapFilePath!);
        }
    }
}
