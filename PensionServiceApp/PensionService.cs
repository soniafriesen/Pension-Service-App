using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PensionServiceApp
{
    public class PensionService: Pension.PensionBase
    {
        private readonly ILogger<PensionService> _logger;
        public PensionService(ILogger<PensionService> logger)
        {
            _logger = logger;
        }

        //override methods
        public override Task<PensionResponse> GetFullPensionAmount(PensionRequest request, ServerCallContext context)
        {
            PensionResponse response = new PensionResponse();
            response.BaseAmount= 0.0;
            response.BridgeAmount = 0.0;
            //get the below and above AMYPE
            int below = BelowAYMPE(request);
            int above = AboveAYMPE(request);

            //calcuate pension
            if(request.Age >= 65 || request.Age + request.NumYears >= 85)
            {
                response.BaseAmount = (0.013 * below + 0.02 * above) * request.NumYears;
            }
            if(response.BaseAmount > 0 && request.Age < 65)
            {
                response.BridgeAmount = 0.007 * below * request.NumYears;
            }
            return Task.FromResult(response);
        }

        //helper methods
        private int BelowAYMPE(PensionRequest request)
        {
            int salary_below_aympe = Math.Min(55000, request.Salary);
            return salary_below_aympe;
        }

        private int AboveAYMPE(PensionRequest request)
        {
            int salary_above_aympe = request.Salary - 55000;
            if (salary_above_aympe < 0)
            {
                return 0;
            }
            else
                return salary_above_aympe;
        }
    }
}
