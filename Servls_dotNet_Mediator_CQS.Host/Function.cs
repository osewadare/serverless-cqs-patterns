using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using MediatR;
using Servls_dotNet_Mediator_CQS.Domain.Entities;
using Servls_dotNet_Mediator_CQS.Domain.Queries;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Servls_dotNet_Mediator_CQS.Host;

public class Functions
{
    /// <summary>
    /// Default constructor that Lambda will invoke.
    /// </summary>
    private readonly IMediator mediator;
    public Functions()
    {
        this.mediator = Startup.ConfigureServices().GetService<IMediator>();
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