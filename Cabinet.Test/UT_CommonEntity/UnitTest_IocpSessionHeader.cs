using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Cabinet.Bridge.Tcp.Session;
using Cabinet.Utility;

namespace Cabinet.UnitTest.CommonEntity
{
    [TestClass]
    public class UnitTest_IocpSessionHeader
    {
        [TestMethod]
        public void TestHeader()
        {
            IocpSessionFrameHeader hd = new IocpSessionFrameHeader();
            hd.payloadLength = 432474321;
            hd.version = 2000;
            byte[] b = hd.serialize();
            IocpSessionFrameHeader hd2 = IocpSessionFrameHeader.deserialize(b);
            Assert.AreEqual(hd.payloadLength, hd2.payloadLength);
            Assert.AreEqual(hd.version, hd2.version);
        }

        [TestMethod]
        public void TestAppend()
        {
            byte[] a = {1,2,3};
            DescriptorBuffer buf = DescriptorBuffer.create(100);
            buf.append(a, 0, 3);
            Assert.AreEqual(buf.des[0], 1);
        }
    }
}
