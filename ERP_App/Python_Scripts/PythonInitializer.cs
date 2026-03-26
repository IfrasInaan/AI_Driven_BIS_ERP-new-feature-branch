using Python.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP_App.Python_Scripts
{
    public static class PythonInitializer
    {
        private static bool _initialized = false;

        public static void Initialize()
        {
            if (!_initialized)
            {
                Runtime.PythonDLL = @"C:\Users\Risha\AppData\Local\Programs\Python\Python310\python310.dll";
                Environment.SetEnvironmentVariable("PYTHONHOME", @"C:\Users\Risha\AppData\Local\Programs\Python\Python310");
                PythonEngine.Initialize();
                _initialized = true;
            }
        }

        public static void Shutdown()
        {
            if (_initialized)
            {
                PythonEngine.Shutdown();
                _initialized = false;
            }
        }


    }
}