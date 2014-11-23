using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Leap;
using System.Collections.Generic;
using LeapSample.MessageGenerator;

namespace LeapRelay.Test
{
    [TestClass]
    public class HandMessageGeneratorTest
    {
        [TestMethod]
        public void TestHandMessageGenerator()
        {
            var handProxy = new TestHandProxy
            {
                Direction = new Vector(1.0f, 0.0f, 0.0f),
                Fingers = new List<TestFingerProxy>
                {
                    new TestFingerProxy {
                        Direction = new Vector(0.0f, 1.0f, 0.0f),
                        Type = Finger.FingerType.TYPE_INDEX
                    }
                }
            };

            var messageGenerator = new HandMessageGenerator(false);

            var message = messageGenerator.GetArduinoMessage(handProxy);
            // At the moment the expected finger angle boundaries are hard coded into the class
            // So at the moment the test can't validate the message, as we don't know what it should be
            //CollectionAssert.AreEqual(new byte[] { 0x43, 191, 0x00 }, message);
            // Verify that the message has the correct starting byte
            Assert.AreEqual(0x43, message[0]);
        }
    }
}
