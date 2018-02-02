using DeepEqual.Syntax;
using JilFork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JilTests
{
    [TestClass]
    public class UnionTest
    {
        [TestMethod]
        public void TypeWithUnion_CanBeSerializedAndDeserialized()
        {
            //var json = "{\"unionprop\":\"Test\",\"abc\":\"text\"}";

            //var data = JSON.Deserialize(json, typeof(UnionSubTest));
            //var str1 = JSON.Serialize(data);

            //data = new UnionSubTest
            //{
            //    abc = "text",
            //    unionString = "Test"
            //};
            //var str2 = JSON.Serialize(data);

            //Assert.AreEqual(json, str1);

            var obj = new UnionSubTest
            {
                abc = "text",
                unionString = "Test"
            };
            SerializeDeserializeTest(obj);
        }
        [TestMethod]
        public void TypeWithoutUnion_CanBeSerializedAdDeserialized()
        {
            var obj = new NoUnion() {a = "Test1", b = "Test2"};
            SerializeDeserializeTest(obj);
            //var json = JSON.Serialize(obj);
            //var deserialized = JSON.Deserialize<NoUnion>(json);
            //obj.ShouldDeepEqual(deserialized);       
            //Assert.AreEqual(obj.a, deserialized.a);
            //Assert.AreEqual(obj.b, deserialized.b);
        }

        [TestMethod]
        public void TypeWithUnionInTheMiddle_CanBeSerializedAdDeserialized()
        {
            var obj1 = new UnionInTheMiddle() {a = "aaa", b = "bbbbb", d = "dddddd"};
            var obj2 = new UnionInTheMiddle() {a = "aaa", c = 5, d = "dddddd"};
            SerializeDeserializeTest(obj1);
            SerializeDeserializeTest(obj2);
        }

        private void SerializeDeserializeTest<T>(T obj)
        {
            var json = JSON.Serialize(obj);
            var deserialized = JSON.Deserialize<T>(json);
            obj.ShouldDeepEqual(deserialized);
        }

        public class NoUnion
        {
            public string a { get; set; }
            public string b { get; set; }
        }
        public class UnionInTheMiddle
        {
            public string a { get; set; }
            [JilDirective(Name = "unionprop", IsUnion = true)]
            public string b { get; set; }
            [JilDirective(Name = "unionprop", IsUnion = true)]
            public int? c { get; set; }            
            public string d { get; set; }
        }

        public class UnionSubTest
        {
            public string abc
            {
                get;
                set;
            }

            [JilDirective(Name = "unionprop", IsUnion = true)]
            public string unionString
            {
                get;
                set;
            }

            [JilDirective(Name = "unionprop", IsUnion = true)]
            public int unionInt
            {
                get;
                set;
            }
        }
    }
}