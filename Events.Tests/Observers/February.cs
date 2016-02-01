using Events.Tests.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Observers
{
    public class February : IEventObserver<HelloWorldEvent>
    {
        public void On(HelloWorldEvent value)
        {
            value.Data += "February, ";
        }
    }
}
