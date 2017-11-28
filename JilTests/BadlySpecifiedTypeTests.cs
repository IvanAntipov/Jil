using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using JilFork;

namespace JilTests
{
    [TestClass]
    public class BadlySpecifiedTypeTests
    {
        [TestMethod]
        public void SerializeString()
        {
            object obj = new { Foo = 123, Bar = "abc" };
            string s = JSON.Serialize(obj), t = JSON.SerializeDynamic(obj);
            Assert.AreEqual(t, s);
        }

        [TestMethod]
        public void SerializeWriter()
        {
            object obj = new { Foo = 123, Bar = "abc" };
            StringWriter s = new StringWriter(), t = new StringWriter();
            JSON.Serialize(obj, s);
            JSON.SerializeDynamic(obj, t);
            Assert.AreEqual(t.ToString(), s.ToString());
        }

        [TestMethod]
        public void DeserializeString()
        {
            string json = JSON.SerializeDynamic(new { Foo = 123, Bar = "abc" });
            dynamic s = JSON.Deserialize<object>(json),
                t = JSON.DeserializeDynamic(json);
            Assert.AreEqual((int)t.Foo, (int)s.Foo);
            Assert.AreEqual((string)t.Bar, (string)s.Bar);
        }

        [TestMethod]
        public void DeserializeReader()
        {
            string json = JSON.SerializeDynamic(new { Foo = 123, Bar = "abc" });
            dynamic s = JSON.Deserialize<object>(new StringReader(json)),
                t = JSON.DeserializeDynamic(new StringReader(json));
            Assert.AreEqual((int)t.Foo, (int)s.Foo);
            Assert.AreEqual((string)t.Bar, (string)s.Bar);
        }
    }
}
