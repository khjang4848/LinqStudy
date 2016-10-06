using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; 

namespace GroupOperators
{
    class Program
    {
        static void Main(string[] args)
        {
            LinqSamples objTEST = new LinqSamples();

            //objTEST.DataSetLinq40();
            //objTEST.DataSetLinq41();
            //objTEST.DataSetLinq42();
            //objTEST.DataSetLinq43();
            //objTEST.DataSetLinq44();
            objTEST.DataSetLinq45();
        }
    }


    public class LinqSamples
    {
        private DataSet testDS;

        public LinqSamples()
        {
            testDS = TestHelper.CreateTestDataset();
        }


        #region DataSetLinq40()
        public void DataSetLinq40()
        {
            var numbers = testDS.Tables["Numbers"].AsEnumerable();

            //int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var numberGroups =
                from n in numbers
                group n by n.Field<int>("number") % 5 into g
                select new { Remainder = g.Key, numbers = g };

            foreach (var g in numberGroups)
            {
                Console.WriteLine("Numbers with a remainers of {0} when divided by 5:", g.Remainder);

                foreach (var n in g.numbers)
                {
                    Console.WriteLine(n.Field<int>("number"));
                }
            }
        }
        #endregion

        #region DataSetLinq41
        public void DataSetLinq41()
        {
            //string[] words = { "blueberry", "chimpanzee", "abacus", "banana", "apple", "cheese" };
            var words4 = testDS.Tables["Words4"].AsEnumerable();

            var wordGroups =
                from w in words4
                group w by w.Field<string>("word")[0] into g
                select new { FirstLetters = g.Key, Words = g };

            foreach (var g in wordGroups)
            {
                Console.WriteLine("Words that start with the letter '{0}'", g.FirstLetters);
                foreach (var w in g.Words)
                {
                    Console.WriteLine(w.Field<string>("word"));
                }
            }
        }
#endregion

        #region DataSetLinq42()
        public void DataSetLinq42()
        {
            var products = testDS.Tables["Products"].AsEnumerable();

            var productGroups =
                from p in products
                group p by p.Field<string>("Category") into g
                select new { Category = g.Key, Prodcts = g };

            foreach (var g in productGroups)
            {
                Console.WriteLine("Category : {0}", g.Category);
                foreach (var w in g.Prodcts)
                {
                    Console.WriteLine("\t " + w.Field<string>("ProductName"));
                }
            }
        }
        #endregion

        #region DataSetLinq43()
        public void DataSetLinq43()
        {
            var customers = testDS.Tables["Customers"].AsEnumerable();

            var customerOrderGroups =
                from c in customers
                select
                    new
                    {
                        CompanyName = c.Field<string>("CompanyName"),
                        YearsGroup =
                            from o in c.GetChildRows("CustomersOrders")
                            group o by o.Field<DateTime>("OrderDate").Year into yg
                            select
                            new
                            {
                                Year = yg.Key,
                                MonthGroups =
                                    from o in yg
                                    group o by o.Field<DateTime>("OrderDate").Month into mg
                                    select new { Month = mg.Key, Orders = mg }
                            }
                    };

            foreach (var cog in customerOrderGroups)
            {
                Console.WriteLine("CompanyName={0}", cog.CompanyName);
                foreach (var yg in cog.YearsGroup)
                {
                    Console.WriteLine("\t Year={0}", yg.Year);
                    foreach (var mg in yg.MonthGroups)
                    {
                        Console.WriteLine("\t\t Month={0}", mg.Month);
                        foreach (var order in mg.Orders)
                        {
                            Console.WriteLine("\t\t\t OrderID = {0}", order.Field<int>("OrderID"));
                            Console.WriteLine("\t\t\t OrderDate = {0}", order.Field<DateTime>("OrderDate"));
                        }
                    }

                }

            }


        }
        #endregion


        #region DataSetLinq44()
        public void DataSetLinq44()
        {
            //string[] anagrams = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var anagrams = testDS.Tables["Anagrams"].AsEnumerable();

            var orderGroups = anagrams.GroupBy(w => w.Field<string>("anagram").Trim(), new AnagramEqualityComparer());

            foreach (var g in orderGroups)
            {
                Console.WriteLine("Key : {0}", g.Key);
                foreach (var w in g)
                {
                    Console.WriteLine("\t" + w.Field<string>("anagram"));
                }
            }


        }
        #endregion


        #region DataSetLinq45()
        public void DataSetLinq45()
        {
            var anagrams = testDS.Tables["Anagrams"].AsEnumerable();

            var orderGroups = anagrams.GroupBy(w => w.Field<string>("anagram").Trim(),
                                          a => a.Field<string>("anagram").ToUpper(),
                                          new AnagramEqualityComparer());

            foreach (var g in orderGroups)
            {
                Console.WriteLine("Key : {0}", g.Key);
                foreach (var w in g)
                {
                    Console.WriteLine("\t" + w);
                }
            }
        }
 
        #endregion
       
    }
}
