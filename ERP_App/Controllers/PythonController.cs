using ERP_App.Python_Scripts;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace ERP_App.Controllers
{
    public class PythonController : Controller
    {
        // GET: Python
        public ActionResult Index()
        {
            /*
            // Create the IronPython scripting engine
            ScriptEngine engine = Python.CreateEngine();
            ScriptScope scope = engine.CreateScope();

            // Define Python code (properly indented)
            string pythonCode = @"
print('Hello! It is Python code in ASP.NET MVC')
result = 'This is the result from Python'
";

            // Execute the Python code
            engine.Execute(pythonCode, scope);

            // Get the result from the Python scope
            dynamic result = scope.GetVariable("result");

            // Pass the result to the view
            ViewBag.Message = result; 
            */
            return View();
 
        }

        public ActionResult Index2() {
            /*
            try
            {
                // Create the IronPython scripting engine
                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.CreateScope();

                // Get the path to the Python script
                string scriptPath = Server.MapPath("~/Python_Scripts/script1.py");

                // Check if the file exists
                if (!System.IO.File.Exists(scriptPath))
                {
                    ViewBag.Error = $"Python script not found at: {scriptPath}";
                    return View();
                }

                // Load and execute the Python script
                engine.ExecuteFile(scriptPath, scope);

                // Get the result from the Python scope
                dynamic result = scope.GetVariable("result");

                // Pass the result to the view
                ViewBag.Message = result;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error executing Python script: {ex.Message}";
            }
            */
            return View();
        }

        public ActionResult TrainModel1()
        {
            /*
            try
            {
                // Create the IronPython scripting engine
                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.CreateScope();

                // Get the path to the Python script
                string scriptPath = Server.MapPath("~/Python_Scripts/train_model1.py");

                // Check if the file exists
                if (!System.IO.File.Exists(scriptPath))
                {
                    ViewBag.Error = $"Python script not found at: {scriptPath}";
                    return View();
                }

                // Execute the Python script
                engine.ExecuteFile(scriptPath, scope);

                // Pass the result to the view
                ViewBag.Message = "Model trained and saved successfully!";
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error training model: {ex.Message}";
            }
            */
            return View();
        }

        public ActionResult Predict1()
        {
            /*
            try
            {
                // Create the IronPython scripting engine
                ScriptEngine engine = Python.CreateEngine();
                ScriptScope scope = engine.CreateScope();

                // Get the path to the Python script for inference
                string scriptPath = Server.MapPath("~/Python_Scripts/predict_1.py");

                // Check if the file exists
                if (!System.IO.File.Exists(scriptPath))
                {
                    ViewBag.Error = $"Python script not found at: {scriptPath}";
                    return View();
                }

                // Execute the Python script
                engine.ExecuteFile(scriptPath, scope);

                // Call the predict function
                dynamic predictFunction = scope.GetVariable("predict");
                dynamic result = predictFunction(new double[] { 5.1, 3.5, 1.4, 0.2 });

                // Pass the result to the view
                ViewBag.Prediction = result;
            }
            catch (Exception ex)
            {
                ViewBag.Error = $"Error making prediction: {ex.Message}";
            }
            */
            return View();
        }
        public ActionResult RunPython()
        {
            if (string.IsNullOrEmpty(Convert.ToString(Session["UserName"])))
            {
                return RedirectToAction("Login", "Home");
            }
            var userid = 0;
            var usertypeid = 0;
            var companyid = 0;
            var branchid = 0;
            var branchtypeid = 0;
            int.TryParse(Convert.ToString(Session["UserID"]), out userid);
            int.TryParse(Convert.ToString(Session["UserTypeID"]), out usertypeid);
            int.TryParse(Convert.ToString(Session["CompanyID"]), out companyid);
            int.TryParse(Convert.ToString(Session["BranchID"]), out branchid);
            int.TryParse(Convert.ToString(Session["BranchTypeID"]), out branchtypeid);

            string output = PythonProgram.RunScript(); // Modified to return string
            ViewBag.PythonOutput = output;
            return View();
        }

    }
}