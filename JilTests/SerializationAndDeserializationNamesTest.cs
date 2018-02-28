using JilFork;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JilTests
{
    [TestClass]
    public class SerializationAndDeserializationNamesTest
    {
        [TestMethod]
        public void SerializationAndDeserializationNamesAreSet()
        {
            var b = new ClassB(){B = "Some data"};
            var bjson = JSON.Serialize(b);
            var deserializedBC = JSON.Deserialize<SerializeAtoCDeserializeCtoA>(bjson);
            var jsonCA = JSON.Serialize(deserializedBC);
            var a = JSON.Deserialize<ClassA>(jsonCA);
            Assert.IsTrue(b.B == a.A);

        }
        [TestMethod]
        public void SerializeDeserializeWithMapping()
        {
            var a= new ClassA(){A = "Some data"};
            var ajson = JSON.Serialize(a);
            var deserializedAC = JSON.Deserialize<DeserializeAtoC>(ajson);
            var mappedACCB = new SerializeCtoB() {C = deserializedAC.C};
            var jsonCA = JSON.Serialize(mappedACCB);
            var b = JSON.Deserialize<ClassB>(jsonCA);
            Assert.IsTrue(b.B == a.A);

        }

        [TestMethod]
        public void SerializeNameIsNotUsedDuringDeserialization()
        {
            var abc = new ClassABC() { A = "A", B = "B", C = "C" };
            var abcJson = JSON.Serialize(abc);
            var deserialized = JSON.Deserialize<SerializeCtoB>(abcJson);
            Assert.IsTrue(deserialized.C == abc.C);

        }
        [TestMethod]
        public void DeserializeNameIsNotUsedDuringSerialization()
        {
            var o = new DeserializeAtoC(){C = "C"};
            var json = JSON.Serialize(o);

            var deserialized = JSON.Deserialize<ClassABC>(json);
            Assert.IsTrue(deserialized.C == o.C);
        }

        public class ClassA
        {
            public string A { get; set; }
        }

        public class ClassABC
        {
            public string A { get; set; }
            public string B { get; set; }
            public string C { get; set; }
        }

        public class ClassB
        {
            public string B { get; set; }
        }

        public class SerializeCtoB
        {
            [JilDirective(SerializationName = "B")]
            public string C { get; set; }
        }
        public class DeserializeAtoC
        {
            [JilDirective(DeserializationName = "A")]
            public string C { get; set; }
        }
        public class SerializeAtoCDeserializeCtoA
        {
            [JilDirective(DeserializationName = "B", SerializationName = "A")]
            public string C { get; set; }
        }
    }
}