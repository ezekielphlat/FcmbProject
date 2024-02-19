using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Application.Common.Interfaces.Services
{
    public interface IReferenceNumberGenerator
    {
        string GetReferenceNumber(string seedValue);
    }
}
