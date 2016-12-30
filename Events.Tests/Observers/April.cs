using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Events.Tests.Events;

namespace Events.Tests.Observers
{
    public class April : IEventObserver<HelloOceanEvent>
    {
        public void On(HelloOceanEvent evnt)
        {
            evnt.Data += ", April";
        }
    }
}
