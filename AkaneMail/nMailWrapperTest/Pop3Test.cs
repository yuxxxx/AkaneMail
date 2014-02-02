using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMail;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MiniMailTest
{
    [TestClass]
    public class Pop3Test
    {
        private MiniMail.Pop3 pop3;
        string collectHost = "";
        int collectPort = 0;

        [TestInitialize]
        public void Initialize()
        {
            pop3 = new Pop3();
        }

        [TestMethod]
        public void CanConnectNormally()
        {
            using (ShimsContext.Create()) {
                nMail.Fakes.ShimPop3.AllInstances.Connect = (p) => { };
                bool actual = pop3.Connect(collectHost, collectPort);
                Assert.IsTrue(actual);
            }
        }

        [TestMethod]
        public void CanConnectOptional()
        {
            using (ShimsContext.Create()) {
                bool expect = true;
                var option = new MiniMail.MailOption() { UseApop = expect } ;
                nMail.Fakes.ShimPop3.AllInstances.Connect = (p) => { if (!p.APop) throw new System.PlatformNotSupportedException(); };
                bool actual = pop3.Connect(collectHost, collectPort, option);
                Assert.IsTrue(actual);
            }
        }

        [TestMethod]
        public void CanSetSSL()
        {
            using (ShimsContext.Create()) {
                var option = new MiniMail.MailOption { UseSSL = true };
                nMail.Fakes.ShimPop3.AllInstances.Connect = (p) => { if (p.SSL != nMail.Pop3.SSL3) throw new nMail.nMailException("error", 0); };
                bool actual = pop3.Connect(collectHost, collectPort, option);
                Assert.IsTrue(actual);
            }
        }


        [TestCleanup]
        public void Cleanup()
        {
            
        }
    }

    [TestClass]
    public class Pop3FailureTest
    {
        private MiniMail.Pop3 pop3;
        string invalidHost = "";
        int collectPort = 0;

        [TestInitialize]
        public void Initialize()
        {
            pop3 = new Pop3();
        }

        [TestMethod]
        public void CanConnectIlleagal()
        {
            using (ShimsContext.Create()) {
                try {
                    nMail.Fakes.ShimPop3.AllInstances.Connect = (p) => { throw new nMail.nMailException("error", 0); };
                    bool actual = pop3.Connect(invalidHost, collectPort);
                }
                catch (MiniMailException ex) {
                    Assert.AreEqual("error", ex.InnerException.Message);
                }
            }
        }

        [TestMethod]
        public void CannotConnectLackOption()
        {
            using (ShimsContext.Create()) {
                try {
                    nMail.Fakes.ShimPop3.AllInstances.Connect = (p) => { if(p.SSL!= nMail.Pop3.SSL3) throw new nMail.nMailException("error", 0); };
                    bool actual = pop3.Connect(invalidHost, collectPort);
                }
                catch (MiniMailException ex) {
                    Assert.AreEqual("error", ex.InnerException.Message);
                }
            }
        }
    }
}
