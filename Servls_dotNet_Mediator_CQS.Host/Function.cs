using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using MediatR;
using Servls_dotNet_Mediator_CQS.Domain.Entities;
using Servls_dotNet_Mediator_CQS.Domain.Queries;
using Servls_dotNet_Mediator_CQS.Host;
using System.Text.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Basic_Serverless_dotNet_Media_CQS;

public class Functions
{
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    public Functions()
    {
    }

    private readonly IMediator mediator;
    public Functions(IMediator mediator)
    {
        this.mediator = mediator;
    }


    public async Task<APIGatewayProxyResponse> GetBook(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var bookId = request.GetParameterValue("bookId");
        var blog = await mediator.Send(new GetBookQuery { bookId = bookId });
        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = JsonSerializer.Serialize(blog),
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        return response;
    }

    public async Task<APIGatewayProxyResponse> GetBooks(APIGatewayProxyRequest request, ILambdaContext context)
    {
        var books = await mediator.Send(new GetBooksQuery());
        var response = new APIGatewayProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = JsonSerializer.Serialize(books),
            Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
        };

        return response;
    }

}