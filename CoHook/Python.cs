using System;
using System.Diagnostics;
using COAPI;
using Microsoft.Scripting.Hosting;

namespace CoHook
{
    public class Python
    {
        private readonly ScriptRuntime scriptRuntime;
        private readonly string scriptsPath;
        private readonly Conquer conquer;

        public Python(string scriptsPath)
        {
            conquer = new Conquer(Process.GetProcessesByName("Conquer")[0]);
            this.scriptsPath = scriptsPath;
            scriptRuntime = IronPython.Hosting.Python.CreateRuntime();
        }

        public dynamic Execute(string script)
        {
            dynamic pythonScript = scriptRuntime.UseFile(scriptsPath + "/" + script);
            try
            {
                return pythonScript.Execute(conquer);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return ex;
            }
        }
    }
}