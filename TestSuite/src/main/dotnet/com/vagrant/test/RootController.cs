using core;
using helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace test
{
    [TestClass]
    public class RootController: BaseController
    {
        [AssemblyInitialize]
        public static void AssemblyInitialize(TestContext context)
        {
            Trace.WriteLine("AssemblyInitialize");
            BaseHelper.PreSetup();
        }
       
        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            Trace.WriteLine("AssemblyCleanup");
            BaseHelper.PostSetup();
        }

   
    }
}
