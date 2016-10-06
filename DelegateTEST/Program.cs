using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateTEST
{
    class Program
    {
        public delegate void Del(string Message);

        public event Del DelEvent;

        static void DoSomething(string strMessage)
        {
            Console.WriteLine(strMessage);
        }

        static void Main(string[] args)
        {
            /*
            Del del2 = DelegateMethod;
            del2("TEST");

            Del del3 = delegate(string strMessage){
                Console.WriteLine(strMessage);;

            //pub.DoSomething();

            //Func<int, bool> myFunc = x => x == 5;

            };

            
            
            objTEST.DelEvent("TEST");

            del3("TEST#####");

            //del4("TEST!!@!@!@!@!@!@!");
             * 
             * 
             * /
             */
            Program objTEST = new Program();
            //objTEST.DelEvent += delegate(string ss)
            //{
            //    Console.WriteLine(ss);
            //};

            //objTEST.DelEvent = DoSomething;

            objTEST.DelEvent = x => Console.WriteLine(x);

            Del handler1 = objTEST.DelEvent;


            handler1("TEST111111");

            //Publisher pub = new Publisher();
            //Susscriber sb1 = new Susscriber("sub1", pub);
            //Susscriber sb2 = new Susscriber("sub2", pub)
            //Func<int, bool> myFunc = delegate(int x)
            //{
            //    return x == 5;
            //};

            Func<int, bool> myFunc = BoolDecide;


            bool result = myFunc(4);

            Console.WriteLine(result.ToString());
             
        }


        private static bool BoolDecide(int a)
        {
            return a == 5;
        }

        public static void DelegateMethod(string strMessage)
        {
            Console.WriteLine(strMessage);
        }
    }
}
