using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyContact.Instrumentation;

namespace MyContact.Tests
{
    [TestClass]
    public class TestInstrumentation
    {
        [TestMethod]
        public void LogWarning_Test()
        {
            //ILogWriter log = new LogWriter();
            LogWriter.Instance.LogInformation("test..............");
            LogWriter.Instance.LogWarning("testatrrrr");
        }
    }
}
