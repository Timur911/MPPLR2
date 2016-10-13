using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConsoleApplication1;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private TestClass listner;
        private Test_Source source;
        public static bool IsDied { get; set; }


        [TestMethod]
        public void Test_WeakLink_IsDead()
        {
            source = new Test_Source();
            listner = new TestClass();
            WeakDelegate weakDelegate = new WeakDelegate((Action<int>)listner.EventHandler);
            source.Completed += (Action<int>)weakDelegate.Weak;
            Assert.IsTrue(weakDelegate.IsAlive);
            listner = null;
            source = null;
            GC.Collect();
            Assert.IsFalse(weakDelegate.IsAlive);

        }


        [TestMethod]
        public void Test_StrongLink_NotDead()
        {
            source = new Test_Source();
            listner = new TestClass();
            source.Completed += listner.EventHandler;
            listner = null;
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Assert.IsFalse(IsDied);

        }
    }

}
