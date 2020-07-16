using System;
using System.Runtime.InteropServices;

namespace SodaDungeon2Tool.Utils
{
    public static class Window
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowTitle);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool ShowWindow(IntPtr hWnd, ShowWindowEnum flags);

        [DllImport("user32.dll")]
        private static extern int SetForegroundWindow(IntPtr hwnd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowPlacement(IntPtr hWnd, ref Windowplacement lpwndpl);

        private enum ShowWindowEnum
        {
            Hide = 0,
            ShowNormal = 1, ShowMinimized = 2, ShowMaximized = 3,
            Maximize = 3, ShowNormalNoActivate = 4, Show = 5,
            Minimize = 6, ShowMinNoActivate = 7, ShowNoActivate = 8,
            Restore = 9, ShowDefault = 10, ForceMinimized = 11
        };

        private struct Windowplacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }

        /// <summary>
        /// Restores a minimized window without setting any focus to it
        /// </summary>
        /// <param name="wdwIntPtr">The window-Handler</param>
        public static void Restore(IntPtr wdwIntPtr)
        {
            ShowWindow(wdwIntPtr, ShowWindowEnum.ShowNormalNoActivate);
        }

        /// <summary>
        /// Minimizes a window
        /// </summary>
        /// <param name="wdwIntPtr">The window-Handler</param>
        public static void Minimize(IntPtr wdwIntPtr)
        {
            ShowWindow(wdwIntPtr, ShowWindowEnum.Minimize);
        }

        /// <summary>
        /// Checks whether a given window is currently minimized
        /// </summary>
        /// <param name="wdwIntPtr">The window-Handler</param>
        /// <returns>true if the givven window is minimized</returns>
        public static bool IsMinimized(IntPtr wdwIntPtr)
        {
            Windowplacement placement = new Windowplacement();
            GetWindowPlacement(wdwIntPtr, ref placement);

            // Check if window is minimized
            if (placement.showCmd == 2)
            {
                return true;
            }
            return false;
        }
    }
}
