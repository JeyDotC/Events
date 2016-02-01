using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events.Tests.Events
{
    public class HelloWorldEvent
    {
        public string Data { get; set; }

        public HelloWorldEvent()
        {
            Data = "";
        }
    }
}
