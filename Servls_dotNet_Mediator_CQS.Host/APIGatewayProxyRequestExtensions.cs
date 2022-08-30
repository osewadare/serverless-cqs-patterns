using Amazon.Lambda.APIGatewayEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servls_dotNet_Mediator_CQS.Host
{
    public static class APIGatewayProxyRequestExtensions
    {
        public static string GetParameterValue(this APIGatewayProxyRequest request, string parameterName)
        {
            string parameterValue = string.Empty;
            if (request.PathParameters != null && request.PathParameters.ContainsKey(parameterName))
                parameterValue = request.PathParameters[parameterName];
            else if (request.QueryStringParameters != null && request.QueryStringParameters.ContainsKey(parameterName))
                parameterValue = request.QueryStringParameters[parameterName];

            return parameterValue;
        }
    }
}
