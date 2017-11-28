using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JilFork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JilTests
{
    [TestClass]
    public class UnionArrayTest
    {
        [TestMethod]
        public void CasDeserializeEmptyUnionWithArrayAsCase()
        {
            var json = "{\"Data\":[]}";
            var res = JSON.Deserialize<_UnionObjectOrStringArray>(json);
            Assert.IsTrue(res.DataArr.Length == 0);
            Assert.IsTrue(res.DataObj == null);

        }
        [TestMethod]
        public void CasDeserializeEmptyUnionArray()
        {
            var json = "{\"Data\":[]}";
            var res = JSON.Deserialize<_UnionArrayOfStringsOrObjects>(json);
            Assert.IsTrue(res.DataArr == null);
            Assert.IsTrue(res.DataObj == null);

        }
        [TestMethod]
        public void CanDeserializeUnionArray()
        {
            var json = "{\"id\":1,\"test\":[\"\"]}";
            var json2 = "{\"id\":1,\"test\":[{\"name\":\"t1\"},{\"name\":\"t2\"}] }";

            var uu = JSON.Deserialize<Data>(json);
            var uu1 = JSON.Deserialize<Data>(json2);
        }

        public class Data
        {
            public int id { get; set; }
            [JilDirective(IsUnion = true, Name = "test")]
            public string[] testStr { get; set; }
            [JilDirective(IsUnion = true, Name = "test")]
            public DataSub[] testArray { get; set; }
        }

        public class DataSub
        {
            public string name { get; set; }
        }
        public class _UnionObjectOrStringArray
        {
            [JilDirective(Name = "Data", IsUnion = true)]
            public SubClass DataObj { get; set; }

            [JilDirective(Name = "Data", IsUnion = true)]
            public string[] DataArr { get; set; }
            public class SubClass
            {

            }
        }
        public class _UnionArrayOfStringsOrObjects
        {
            [JilDirective(Name = "Data", IsUnion = true)]
            public SubClass[] DataObj { get; set; }

            [JilDirective(Name = "Data", IsUnion = true)]
            public string[] DataArr { get; set; }
            public class SubClass
            {

            }
        }
    }
}
