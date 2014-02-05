using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMail;
using Microsoft.QualityTools.Testing.Fakes;
using nMail.Fakes;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using System.Collections;

namespace MiniMailTest
{
    [TestClass]
    public class Pop3LoadHeaderTest
    {
        private Pop3 pop3;
        string collectId = "";
        string collectPass = "";

        [TestInitialize]
        public void Initialize()
        {
            pop3 = new Pop3();

        }

        [TestMethod]
        public void LoadUidlSuccess()
        {
            using (ShimsContext.Create()) {
                bool called = false;
                ShimPop3.AllInstances.Connect = p => called = true;
                ShimPop3.AllInstances.AuthenticateStringString = (p, a1, a2) => called &= true;
                ShimPop3.AllInstances.UidlGet = _ => "1 hohohohohohohoh\n2 yoyoyoyoyoyooyoyo";
                ShimPop3.AllInstances.GetUidlInt32 = (p, i) => called &= true;
                pop3.Connect("127.0.0.1", 8080);
                var po = new PrivateObject(pop3);
                var expect = po.Invoke("ParseUidls", null) as Dictionary<int, string>;
                Assert.IsTrue(called);
                CollectionAssert.AllItemsAreUnique(expect);
            }
        }
    }

    [TestClass]
    public class Pop3LoadHeaderFailureTest
    {
        private Pop3 pop3;
        string collectId = "";
        string collectPass = "";

        [TestInitialize]
        public void Initialize()
        {
            pop3 = new Pop3();
        }

        [TestMethod]
        public void LoadNoUIdls()
        {
            using (ShimsContext.Create()) {
                bool called = false;
                ShimPop3.AllInstances.Connect = p => called = true;
                ShimPop3.AllInstances.AuthenticateStringString = (p, a1, a2) => called &= true;
                ShimPop3.AllInstances.UidlGet = _ => "";
                ShimPop3.AllInstances.GetUidlInt32 = (p, i) => called &= true;
                pop3.Connect("127.0.0.1", 8080);
                var po = new PrivateObject(pop3);
                var actual = po.Invoke("ParseUidls", null) as Dictionary<int, string>;
                Assert.IsTrue(called);
                Assert.AreEqual(0, actual.Count);
            }
        }

    }
}
