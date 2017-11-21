using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Apprenda.AuditEventForwarder.Syslog.Test
{
    public class CallsiteMapContruction
    {
        [Fact]
        public void Version81CallsiteMapConstructable()
        {
            var map = new Apprenda81CallsiteMap();
            map.Should().NotBeNull();
        }
    }
}
