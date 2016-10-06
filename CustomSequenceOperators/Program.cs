using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace CustomSequenceOperators
{

    public static class CustomSequenceOperators
    {
        public static IEnumerable<S> Combine<S>(this IEnumerable<DataRow> first, IEnumerable<DataRow> second, Func<DataRow, DataRow, S> func)
        {
            using (IEnumerator<DataRow> e1 = first.GetEnumerator(), e2 = second.GetEnumerator())
            {
                while (e1.MoveNext() && e2.MoveNext())
                {
                    yield return func(e1.Current, e2.Current);
                }
            }
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            LinqSample objTEST = new LinqSample();
            objTEST.DataSetLinq98();
        }
    }


    public class LinqSample
    {
        private DataSet testDS;

        public LinqSample()
        {
            testDS = TestHelper.CreateTestDataset();
        }


        public void DataSetLinq98()
        {
            var numberA = testDS.Tables["NumbersA"].AsEnumerable();
            var numberB = testDS.Tables["NumbersB"].AsEnumerable();

            int dotProduct = numberA.Combine<int>(numberB, (a, b)=> a.Field<int>("number") * b.Field<int>("number")).Sum();

            //IEnumerable<int> TEST11 = numberA.Combine<int>(numberB, (a, b) => a.Field<int>("number") * b.Field<int>("number"));

            IEnumerable<string> TEST11 = numberA.Combine<string>(numberB, (a, b) => a.Field<int>("number").ToString() + "~" + b.Field<int>("number").ToString());

            foreach (var a in TEST11)
            {
                Console.WriteLine("{0}", a);
            }

            Console.WriteLine("Dot product: {0}", dotProduct);
        }

    }
}
