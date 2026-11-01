using System;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace InternalInmate.Forms
{
    public static class ControlPerformanceExtensions
    {
        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, bool wParam, int lParam);

        private const int WM_SETREDRAW = 0x000B;

        public static void EnableDoubleBuffering(this Control control)
        {
            if (control == null) return;
            try
            {
                typeof(Control).InvokeMember(
                    "DoubleBuffered",
                    BindingFlags.SetProperty | BindingFlags.Instance | BindingFlags.NonPublic,
                    null,
                    control,
                    new object[] { true });
            }
            catch
            {
                // Bỏ qua nếu không hỗ trợ reflection
            }
        }

        public static void OptimizeGrid(this DataGridView grid)
        {
            if (grid == null) return;
            grid.EnableDoubleBuffering();
            grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        }

        public static void BeginUpdateOptimized(this Control control)
        {
            if (control == null || !control.IsHandleCreated) return;
            try
            {
                SendMessage(control.Handle, WM_SETREDRAW, false, 0);
            }
            catch { }
        }

        public static void EndUpdateOptimized(this Control control)
        {
            if (control == null || !control.IsHandleCreated) return;
            try
            {
                SendMessage(control.Handle, WM_SETREDRAW, true, 0);
                control.Refresh();
            }
            catch { }
        }
    }
}
