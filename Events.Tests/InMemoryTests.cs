using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Events.Tests.Observers;
using Events.Tests.Events;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Events.Tests
{
    [TestClass]
    public class InMemoryTests
    {
        private static readonly IObserverStorage _observers = new MemoryObserverStorage(
            new January(),
            new February(),
            new March(),
            new April() // This one will not listen to the HelloWorldEvent.
        );

        [TestMethod]
        public void SimpleTest()
        {
            var emitter = new EventEmitter(_observers);

            var evnt = new HelloWorldEvent();

            emitter.Emit(evnt);

            Assert.AreEqual("January, February, March", evnt.Data);

        }

        [TestMethod]
        public void ParallelsSimpleTest()
        {
            var emitter = new EventEmitter(_observers, new ParallelsObserverInvoker());

            var evnt = new HelloWorldEvent();

            emitter.Emit(evnt);

            Assert.IsTrue(Constants.ExpectedResultForHelloWorldEvent.IsMatch(evnt.Data));
        }

        [TestMethod]
        public void ContravariantTest()
        {
            var emitter = new EventEmitter(_observers);

            var evnt = new HelloOceanEvent();

            emitter.Emit(evnt);

            Assert.AreEqual("January, February, March, April", evnt.Data);
        }
    }
}
