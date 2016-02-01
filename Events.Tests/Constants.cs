using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Events.Tests
{
    static class Constants
    {
        public static readonly Regex ExpectedResultForHelloWorldEvent = new Regex("January, |February, |March");
    }
}
