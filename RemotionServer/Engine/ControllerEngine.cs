using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using RemotionServer.Properties;

namespace Engine
{
    /// <summary>
    /// Инкапсуляция контроллера для любых системных действий
    /// </summary>
    internal static class ControllerEngine
    {
        #region Variables
        private static Form form = new Form();
        private const int APPCOMMAND_VOLUME_MUTE = 0x80000; //Замьютить
        private const int APPCOMMAND_VOLUME_UP = 0xA0000;
        private const int APPCOMMAND_VOLUME_DOWN = 0x90000;
        private const int WM_APPCOMMAND = 0x319;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x008;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_WHEEL = 0x0800;
        private const int WM_COMMAND = 0x111;
        private const int MIN_ALL = 419; //Свернуть
        private const int MIN_ALL_UNDO = 416; //Развернуть
        private const int APPCOMMAND_BROWSER_BACKWARD = 1; //Браузер - назад
        private const int APPCOMMAND_BROWSER_FORWARD = 2; //
        private const int APPCOMMAND_BROWSER_REFRESH = 3;//
        private const int SW_SHOWMINIMIZED = 2;//
        private const int APPCOMMAND_PASTE = 38;//
        private const int APPCOMMAND_COPY = 36;//
        private const int APPCOMMAND_CUT = 37;//
        public static int _mouseWheelSpeed;

        public static int MouseWheelSpeed
        {
            get
            {
                return Settings.Default.MouseWheelSpeed;
            }
            set
            {
                Settings.Default.MouseWheelSpeed = value;
                Settings.Default.Save();
            }
        }

        public static double TouchpadSens
        {
            get
            {
                return Settings.Default.TouchpadSpeed;
            }
            set
            {
                Settings.Default.TouchpadSpeed = value;
                Settings.Default.Save();
            }
        }
        #endregion

        static ControllerEngine()
        {
            _mouseWheelSpeed = MouseWheelSpeed;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageW(IntPtr hWnd, int Msg,
            IntPtr wParam, IntPtr lParam);

        public static void Mute()
        {
            SendMessageW(form.Handle, WM_APPCOMMAND, form.Handle,
                (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        public static void VolDown()
        {
            SendMessageW(form.Handle, WM_APPCOMMAND, form.Handle,
                (IntPtr)APPCOMMAND_VOLUME_DOWN);
        }

        public static void VolUp()
        {
            SendMessageW(form.Handle, WM_APPCOMMAND, form.Handle,
                (IntPtr)APPCOMMAND_VOLUME_UP);
        }

        public static void DoLeftButtonMouseClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public static void DoEnterPress()
        {
            SendKeys.SendWait("{ENTER}");
        }

        public static void ZoomIn()
        {
            SendKeys.SendWait("^{+}");
        }

        public static void ZoomOut()
        {
            SendKeys.SendWait("^{-}");
        }

        public static void TypeCustomMessage(string message)
        {
            string result = Regex.Replace(message, @"(\+|\^|%|~|\(|\)|\[|]|\{|})", "{$1}");
            SendKeys.SendWait(result);
        }

        public static void DoLeftButtonMouseDown()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN, X, Y, 0, 0);
        }

        public static void DoLeftButtonMouseUp()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }

        public static void DoRightButtonMouseDown()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
        }

        public static void DoRightButtonMouseUp()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        public static void DoRightButtonMouseClick()
        {
            uint X = (uint)Cursor.Position.X;
            uint Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN, X, Y, 0, 0);
            mouse_event(MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }

        public static void MouseWheelUp()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, _mouseWheelSpeed, 0);
        }

        public static void MouseWheelDown()
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, -_mouseWheelSpeed, 0);
        }

        public static void CloseActiveWindow()
        {
            Task.Factory.StartNew(() =>
            {
                IntPtr handle = GetForegroundWindow();
                Process process = null;
                try
                {
                    process = Process.GetProcesses().Single(
                        p => p.Id != 0 && p.MainWindowHandle == handle);
                }
                catch (InvalidOperationException)
                {
                    return;
                }

                bool success = false;
                try
                {
                    success = process.CloseMainWindow();
                }
                catch (InvalidOperationException)
                {
                    //Process already finished
                }
                process.Refresh();

                Thread.Sleep(1000);

                if (!success)
                {
                    process.Kill();
                    process.Close();
                }
            });
        }

        public static void HideActiveWindow()
        {
            Task.Factory.StartNew(() =>
            {
                IntPtr handle = GetForegroundWindow();
                if (!handle.Equals(IntPtr.Zero))
                {
                    // SW_SHOWMAXIMIZED to maximize the window
                    // SW_SHOWMINIMIZED to minimize the window
                    // SW_SHOWNORMAL to make the window be normal size
                    ShowWindowAsync(handle, SW_SHOWMINIMIZED);
                }
            });
        }

        public static void MinimizeAll()
        {
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL, IntPtr.Zero);
        }

        public static void ExpandAll()
        {
            IntPtr lHwnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(lHwnd, WM_COMMAND, (IntPtr)MIN_ALL_UNDO, IntPtr.Zero);
        }

        public static void BrowserBackward()
        {
            IntPtr handle = GetForegroundWindow();
            SendMessage(handle, WM_COMMAND, (IntPtr)APPCOMMAND_BROWSER_BACKWARD, IntPtr.Zero);
        }

        public static void BrowserForward()
        {
            IntPtr handle = GetForegroundWindow();
            SendMessage(handle, WM_COMMAND, (IntPtr)APPCOMMAND_BROWSER_FORWARD, IntPtr.Zero);
        }

        public static void RefreshPage()
        {
            IntPtr handle = GetForegroundWindow();
            SendMessage(handle, WM_COMMAND, (IntPtr)APPCOMMAND_BROWSER_REFRESH, IntPtr.Zero);
        }

        public static void CopySelection()
        {
            IntPtr handle = GetForegroundWindow();
            SendMessage(handle, WM_COMMAND, (IntPtr)APPCOMMAND_COPY, IntPtr.Zero);
        }

        public static void Paste()
        {
            IntPtr handle = GetForegroundWindow();
            SendMessage(handle, WM_COMMAND, (IntPtr)APPCOMMAND_PASTE, IntPtr.Zero);
        }

        public static void Cut()
        {
            IntPtr handle = GetForegroundWindow();
            SendMessage(handle, WM_COMMAND, (IntPtr)APPCOMMAND_CUT, IntPtr.Zero);
        }

        public static void MuteSound()
        {
            SendMessageW(form.Handle, WM_APPCOMMAND, form.Handle, (IntPtr)APPCOMMAND_VOLUME_MUTE);
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        static extern IntPtr SendMessage(IntPtr hWnd, Int32 Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, int cButtons, uint dwExtraInfo);
    }
}
