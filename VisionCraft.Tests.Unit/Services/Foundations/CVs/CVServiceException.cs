using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xeptions;

namespace VisionCraft.Tests.Unit.Services.Foundations.CVs
{
    internal class CVServiceException : Xeption
    {
        public CVServiceException(Xeption innerException)
            :base("CV service error occured, contact support.",
                 innerException)
        { }
    }
}
