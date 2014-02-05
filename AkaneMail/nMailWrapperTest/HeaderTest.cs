using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MiniMailTest
{
    [TestClass]
    public class HeaderTest
    {
        [TestMethod]
        public void GetUidl()
        {
            var expect = "hogehogehogehoge";
            var actual = new MiniMail.Header { { "uidl", expect } };
            Assert.AreEqual(expect, actual.Uidl);
        }

        [TestMethod]
        public void AreEqualWhenSameUidl()
        {
            var uidl = "hogehogehogehoge";
            var expect = new MiniMail.Header { { "uidl", uidl } };
            var actual = new MiniMail.Header { { "uidl", uidl } };
            Assert.AreEqual(expect, actual);
        }
    }

    [TestClass]
    public class HeaderFailureTest
    {
        [TestMethod]
        public void UnsetUidl()
        {
            var actual = new MiniMail.Header();
            Assert.IsNull(actual.Uidl);

        }
    }
}
