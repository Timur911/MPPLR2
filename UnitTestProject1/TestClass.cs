using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTest
{

    public class TestClass
    {
        public TestClass()
        {
            UnitTest1.IsDied = false;
        }

        public void EventHandler(int a, double a1, int a2)
        {
            Console.Write("{0}, {1}, {2} ", a, a1, a2);
        }
        public void EventHandler(int a, double a1)
        {
            Console.Write("{0}, {1} ", a, a1);
        }
        public void EventHandler(int a)
        {
            Console.Write("{0}", a);
        }
        public void EventHandler(int a, int a1, int a2, int a3)
        {
            Console.Write("{0}, {1}, {2}, {3} ", a, a1, a2, a3);
        }
        ~TestClass()
        {
            UnitTest1.IsDied = true;
        }
    }
    public class Test_Source
    {
        public event Action<int> Completed;
        public event Action<int, double> Completed1;
        public event Action<int, double, int> Completed2;
        public event Action<int, int, int, int> Completed3;

    }
}
