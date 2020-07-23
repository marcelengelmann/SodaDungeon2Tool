using SodaDungeon2Tool.Model;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SodaDungeon2Tool.Util
{
    public static class Logic
    {
        private static System.Windows.Media.MediaPlayer player = new System.Windows.Media.MediaPlayer();
        private static bool soundIsPlaying = false;
        private static int remainingNotifications;

        /// <summary>
        /// Get the Game-Handler to be able to capture the screen of the correct process
        /// </summary>
        /// <returns>The Game Handler of The Soda Dungeon 2 Process</returns>
        public static IntPtr? GetGameHandler()
        {
            string ProcessName = "SodaDungeon2";
            if (Process.GetProcessesByName(ProcessName).Length < 1)
            {
                return null;
            }

            return Process.GetProcessesByName(ProcessName)[0].MainWindowHandle;
        }

        /// <summary>
        /// Checks whether the game currently shows the Exit Button, that is seen at the end of a run
        /// </summary>
        /// <param name="image">The Captured Screenshot to check for the Exit Button</param>
        /// <returns>Whether the Exit-Button has been found</returns>
        public static bool HasExitButton(Bitmap image)
        {
            //[A=255, R=44, G=86, B=153] 524, 640
            Color[] colorFields = new Color[4];
            Color target = Color.FromArgb(255, 44, 86, 153);

            colorFields[0] = image.GetPixel(524, 640);
            colorFields[1] = image.GetPixel(750, 640);
            colorFields[2] = image.GetPixel(750, 660);
            colorFields[3] = image.GetPixel(524, 660);

            for (int i = 0; i < 4; i++)
            {
                if (colorFields[i].ToArgb() != target.ToArgb())
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Captures the current screen of the game
        /// </summary>
        /// <param name="sodaGame">The Game-Handler of the Soda Dungeon 2 Process</param>
        /// <returns></returns>
        public static Bitmap TakeScreenshot(IntPtr sodaGame)
        {
            bool wasMinimized = Window.IsMinimized(sodaGame);
            Window.Restore(sodaGame);
            Bitmap image = ScreenCapture.CaptureWindow(sodaGame);
            if (wasMinimized == true) Window.Minimize(sodaGame);
            return ResizeImage(image, 1264, 720);
        }

        /// <summary>
        /// Resize an image to a specific resolution
        /// </summary>
        /// <param name="image">The image that should be resized</param>
        /// <param name="width">The target width of the result image</param>
        /// <param name="height">The target height of the result image</param>
        /// <returns>The resized image</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void ExitButtonFound(Bitmap image, Configuration config)
        {
            if (config.notifyOnFinish == true)
            {
                new Thread(() => NotifyOnFinish(config.notificationSoundFileLocation, config.numberOfNotifications, config.notificationSoundVolume));
            }
            if(config.saveLastScreenshot == true)
            {
                image.Save("LastRunEndScreen.jpg", ImageFormat.Jpeg);
            }
            if (config.shutdownOnFinish == true)
            {
                Process.Start("shutdown", "/s /t 0");
            }
        }

        public static void NotifyOnFinish(string soundFilePath, int numberOfNotifications, int volume)
        {
            if(soundIsPlaying)
                return;
            try
            {
                player.Open(new Uri(soundFilePath));
            }
            catch (UriFormatException) //file not found
            {
                string outputPath = Directory.GetCurrentDirectory() + "\\defaultNotification.mp3";
                File.WriteAllBytes(outputPath, Properties.Resources.defaultNotification);
                player.Open(new Uri(outputPath));
            }
            player.Volume = volume / 100.0f;
            remainingNotifications = numberOfNotifications;
            player.MediaEnded += (sender, e) => MediaEnded();
            soundIsPlaying = true;
            player.Play();
        }

        private static void MediaEnded()
        {
            if(remainingNotifications > 0)
            {
                remainingNotifications--;
                player.Play();
            }
            else
            {
                soundIsPlaying = false;
                player.Close();
            }
        }
    }
}