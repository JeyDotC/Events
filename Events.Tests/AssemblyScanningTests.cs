using Events.Tests.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Events.Tests
{
    [TestClass]
    public class AssemblyScanningTests
    {
        private static readonly IObserverStorage _observers = new AssemblyScanningOberverStorage("Events.Tests");

        [TestMethod]
        public void SimpleTest()
        {
            var emitter = new EventEmitter(_observers);

            var evnt = new HelloWorldEvent();

            emitter.Emit(evnt);

            Assert.IsTrue(Constants.ExpectedResultForHelloWorldEvent.IsMatch(evnt.Data));

        }

        [TestMethod]
        public void ParallelsSimpleTest()
        {
            var emitter = new EventEmitter(_observers, new ParallelsObserverInvoker());

            var evnt = new HelloWorldEvent();

            emitter.Emit(evnt);

            Assert.IsTrue(Constants.ExpectedResultForHelloWorldEvent.IsMatch(evnt.Data));
        }
    }
}
