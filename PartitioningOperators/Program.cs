using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PartitioningOperators
{
    public class Order
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal Total { get; set; }
    }

    public class Customer
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public Order[] Orders { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            LinqSamples objTEST = new LinqSamples();
            //objTEST.Linq20();
            //objTEST.Linq21();
            //objTEST.Linq22();
            //objTEST.Linq23();
            //objTEST.Linq24();
            objTEST.Linq25();
        }
    }

    class LinqSamples
    {
        private List<Customer> customerList;

        public void Linq20()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var first3Numers = numbers.Take(3);

            foreach(var n in first3Numers)
            {
                Console.WriteLine(n);
            }

        }


        public void Linq21()
        {
            List<Customer> customers = GetCustomerList();

            var first3WAOrders = (
                from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select new { cust.CustomerID, order.OrderID, order.OrderDate, cust.Region }).Take(3);

            Console.WriteLine("First 3 orders in WA:");
            foreach (var order in first3WAOrders)
            {
                ObjectDumper.Write(order);
            }
        }

        public void Linq22()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var allButFirst4Numbers = numbers.Skip(4);


            //앞의 4개의 Record를 skip하고 출력
            Console.WriteLine("All but first 4 numbers:");
            foreach (var n in allButFirst4Numbers)
            {
                Console.WriteLine(n);
            }
        }

        public void Linq23()
        {
            List<Customer> customers = GetCustomerList();

            var waOrders =
                from cust in customers
                from order in cust.Orders
                where cust.Region == "WA"
                select new { cust.CustomerID, order.OrderID, order.OrderDate, cust.Region };

            var allButFirst2Orders = waOrders.Skip(2);

            Console.WriteLine("All but first 2 orders in WA:");
            foreach (var order in allButFirst2Orders)
            {
                ObjectDumper.Write(order);
            }

        }

        public void Linq24()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            IEnumerable<int> objTE11 = numbers;

            var firstNumbersLessThan6 = objTE11.TakeWhile<int>(n => n < 6);

            Console.WriteLine("First numbers less than 6:");
            foreach (var num in firstNumbersLessThan6)
            {
                Console.WriteLine(num);
            }
        }

        public void Linq25()
        {
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            //var firstSmallNumbers = numbers.TakeWhile((n, index) => n >= 9);

            var firstSmallNumbers = numbers.SkipWhile((n, index) => n >= index);

            Console.WriteLine("First numbers not less than their position:");
            foreach (var n in firstSmallNumbers)
            {
                Console.WriteLine(n);
            }

        }
        public List<Customer> GetCustomerList()
        {
            if (customerList == null)
                createLists();

            return customerList;
        }

        private void createLists()
        {
            // Customer/Order data read into memory from XML file using XLinq:
            customerList = (
                from e in XDocument.Load("Customers.xml").
                          Root.Elements("customer")
                select new Customer
                {
                    CustomerID = (string)e.Element("id"),
                    CompanyName = (string)e.Element("name"),
                    Address = (string)e.Element("address"),
                    City = (string)e.Element("city"),
                    Region = (string)e.Element("region"),
                    PostalCode = (string)e.Element("postalcode"),
                    Country = (string)e.Element("country"),
                    Phone = (string)e.Element("phone"),
                    Fax = (string)e.Element("fax"),
                    Orders = (
                        from o in e.Elements("orders").Elements("order")
                        select new Order
                        {
                            OrderID = (int)o.Element("id"),
                            OrderDate = (DateTime)o.Element("orderdate"),
                            Total = (decimal)o.Element("total")
                        })
                        .ToArray()
                })
                .ToList();
        }
    }
}
