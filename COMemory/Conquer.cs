using System;
using System.Diagnostics;

namespace COAPI
{
    public class Conquer
    {
        public Process Process { get; private set; }
        public IntPtr ProcessPtr { get; private set; }
        public int BaseAddress { get { return Process.MainModule.BaseAddress.ToInt32(); } }

        public Memory Memory { get; private set; }

        public Conquer(Process process)
        {
            Process = process;
            ProcessPtr = Process.Handle;
            Memory = new Memory(this);
        }

        public void SuspendProcess()
        {
            Process process = Process.GetProcessById(Process.Id);

            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = NativeMethods.OpenThread(NativeMethods.ThreadAccess.SUSPEND_RESUME,
                    false, (uint) pT.Id);

                if (pOpenThread == IntPtr.Zero)
                    continue;

                NativeMethods.SuspendThread(pOpenThread);
            }
        }

        public void ResumeProcess()
        {
            var process = Process.GetProcessById(Process.Id);

            if (process.ProcessName == string.Empty)
                return;

            foreach (ProcessThread pT in process.Threads)
            {
                IntPtr pOpenThread = NativeMethods.OpenThread(NativeMethods.ThreadAccess.SUSPEND_RESUME, false,
                    (uint) pT.Id);

                if (pOpenThread == IntPtr.Zero)
                    continue;

                int suspendCount;
                do
                {
                    suspendCount = NativeMethods.ResumeThread(pOpenThread);
                } while (suspendCount > 0);
            }
        }
    }
}