using Python.Runtime;
using System;
using System.IO;
using System.Runtime.InteropServices;


namespace ERP_App.Python_Scripts
{
    public class PythonProgram
    {
        public static string RunScript()
        {
            Runtime.PythonDLL = @"C:\Users\Risha\AppData\Local\Programs\Python\Python310\python310.dll";

            Environment.SetEnvironmentVariable("PYTHONHOME", @"C:\Users\Risha\AppData\Local\Programs\Python\Python310");
            Environment.SetEnvironmentVariable("PATH", Environment.GetEnvironmentVariable("PATH") + @";C:\Users\Risha\AppData\Local\Programs\Python\Python310");

            PythonEngine.Initialize();

            string output = "";

            using (Py.GIL())
            {
                string scriptPath = @"C:\Users\Risha\Desktop\IAST Details\2024 Summer Internship\Application\ERP_Solution\ERP_App\Python_Scripts\";
                dynamic sys = Py.Import("sys");
                sys.path.append(scriptPath);

                dynamic script = Py.Import("mypythonScripts");

                string message = "Hello from ASP.NET MVC";
                dynamic result = script.test(message);

                output = result.ToString();
            }

            PythonEngine.Shutdown();
            return output;
        }

    }
}