using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Leap;
using LeapSample;
using System.Linq;
using System.Collections.Generic;

namespace LeapRelay.Test
{

    [TestClass]
    public class FingerModelTest
    {
        [TestMethod]
        public void TestGetBytes()
        {
            var handVector = new Vector(1.0f, 0.0f, 0.0f);
            var fingerVector = new Vector(0.0f, 1.0f, 0.0f);
            var fingerModel = new FingerModel(Finger.FingerType.TYPE_INDEX, 0, 0.0f, 90.0f);

            var resultBytes = fingerModel.GetBytes(fingerVector, handVector);

            CollectionAssert.AreEqual(new byte[] { 0xFF, 0x00 }, resultBytes);
        }

        [TestMethod]
        public void Test1()
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

            var fingerMotionThing = new FingerMotionThing();

            var message = fingerMotionThing.GetArduinoMessage(handProxy);
            // At the moment the expected finger angle boundaries are hard coded into the class
            // So at the moment the test can't validate the message, as we don't know what it should be
            //CollectionAssert.AreEqual(new byte[] { 0x43, 191, 0x00 }, message);
            // Verify that the message has the correct starting byte
            Assert.AreEqual(0x43, message[0]);
        }
    }
}
