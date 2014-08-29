using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utility
{
    public class Utility
    {
        #region Minimize, Memory Release
        [DllImport("KERNEL32.DLL", EntryPoint = "SetProcessWorkingSetSize", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern bool SetProcessWorkingSetSize(IntPtr pProcess, int dwMinimumWorkingSetSize, int dwMaximumWorkingSetSize);

        [DllImport("KERNEL32.DLL", EntryPoint = "GetCurrentProcess", SetLastError = true, CallingConvention = CallingConvention.StdCall)]
        internal static extern IntPtr GetCurrentProcess();
       
        public static void MinimizeRelease()
        {
            IntPtr pHandle = GetCurrentProcess();
            SetProcessWorkingSetSize(pHandle, -1, -1);


            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }
        #endregion
    }
}
