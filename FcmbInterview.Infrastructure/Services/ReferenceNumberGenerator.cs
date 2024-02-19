using FcmbInterview.Application.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FcmbInterview.Infrastructure.Services
{
    public class ReferenceNumberGenerator : IReferenceNumberGenerator
    {
        private List<string> randomNumberList;
        Random randGenerator;
        public ReferenceNumberGenerator()
        {
            randomNumberList = new List<string>();
            randGenerator = new Random();
        }
        public string GetReferenceNumber(string seedValue)
        {
            var randNum = seedValue + DateTime.Now.ToString("yyMMddhhmmssffff") + randGenerator.Next(1, 9999999);
            if (randomNumberList.Contains(randNum))
            {
                GetReferenceNumber(seedValue);
            }
            //Console.WriteLine($"Reference number ==> {randNum}");
            randomNumberList.Add(randNum);
            return randNum;
        }
    }
}
