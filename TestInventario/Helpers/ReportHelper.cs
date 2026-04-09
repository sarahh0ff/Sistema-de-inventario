using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace TestInventario.Helpers
{
    public static class ReportHelper
    {
        private static ExtentReports extent;
        private static ExtentTest test;

        public static void InitializeReport()
        {
            string reportPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "..", "..", "..",
                "Reports",
                $"TestReport_{DateTime.Now:yyyyMMdd_HHmmss}.html"
            );

            Directory.CreateDirectory(Path.GetDirectoryName(reportPath)!);

            var htmlReporter = new ExtentSparkReporter(reportPath);
            htmlReporter.Config.DocumentTitle = "InventarioCore - Test Report";
            htmlReporter.Config.ReportName = "Resultados de Pruebas Automatizadas";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Dark;

            extent = new ExtentReports();
            extent.AttachReporter(htmlReporter);
            extent.AddSystemInfo("Aplicación", "InventarioCore");
            extent.AddSystemInfo("Ambiente", "Testing");
            extent.AddSystemInfo("Tester", "Sarah Heischly Figuereo Morillo");
        }

        public static void CreateTest(string testName, string description)
        {
            test = extent.CreateTest(testName, description);
        }

        public static void LogPass(string message) => test.Pass(message);
        public static void LogFail(string message) => test.Fail(message);
        public static void LogInfo(string message) => test.Info(message);

        public static void AttachScreenshot(string screenshotPath, string title = "Screenshot")
        {
            test.AddScreenCaptureFromPath(screenshotPath, title);
        }

        public static void FlushReport()
        {
            extent.Flush();
        }
    }
}