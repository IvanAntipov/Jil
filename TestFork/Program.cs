using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using JilFork;

namespace TestFork
{
    class Program
    {
        static void Main(string[] args)
        {
            var parsed = JSON.Deserialize<Items>("{\"data\":[{\"id\":\"1\", \"additional\":90}, {\"id\":\"2\"}]}");
            Console.WriteLine("Done" + parsed.ToString());
        }
    }

    public class Items
    {
        [JilDirective(Name = "data")]
        public Item[] Data { get; set; }
    }
    
    [JilClassDirective("Raw")]
    public class Item
    {
        [JilDirective(Name = "id")]
        public string Id { get; set; }
        [JilDirective(Ignore = true)]
        public string Raw { get; set; }
    }

}
