using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MiniMail;
using Microsoft.QualityTools.Testing.Fakes;
using nMail.Fakes;
using System.Linq;

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
        public void LoadHeaderSuccess()
        {
            var methods = System.Reflection.RuntimeReflectionExtensions.GetRuntimeMethods(pop3.GetType());// .Where(s => s.IsPrivate);
            using (ShimsContext.Create()) {
                bool called = false;
                ShimPop3.AllInstances.Connect = p => { };
                nMail.Fakes.ShimPop3.AllInstances.UidlGet = _ => "1 hohohohohohohoh\n2 yoyoyoyoyoyooyoyo";

                ShimPop3.AllInstances.GetUidlInt32 = (p, i) => called = true;
                pop3.Connect("127.0.0.1", 8080);
                pop3.LoadHeader(collectId, collectPass);
                
            }
        }
    }

    [TestClass]
    public class Pop3LoadHeaderFailureTest
    {

    }
}
