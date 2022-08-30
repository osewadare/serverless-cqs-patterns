using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Runtime;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Servls_dotNet_Mediator_CQS.Domain.Entities;
using Servls_dotNet_Mediator_CQS.Domain.Queries;
using Servls_dotNet_Mediator_CQS.Domain.Repository;
using Servls_dotNet_Mediator_CQS.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Servls_dotNet_Mediator_CQS.Host
{
    public static class Startup
    {
        public static ServiceProvider ConfigureServices()
        {
            var serviceCollection = new ServiceCollection();

            //Add all handlers in the assembly to the service collection
            serviceCollection.AddMediatR(typeof(GetBookQuery).GetTypeInfo().Assembly);

            serviceCollection.AddScoped(typeof(IRepository<>), typeof(DynamoDbRepository<>));

            RegisterDynamoDb(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void RegisterDynamoDb(ServiceCollection serviceCollection)
        {
            var credentials = new BasicAWSCredentials("AKIA46A2QM6E2HJCEZVN", "dGCH2/m3U90XLwK1uW641Eo2QGgDdnA0tQFA4lXd");
            var dbconfig = new AmazonDynamoDBConfig()
            {
                RegionEndpoint = RegionEndpoint.USEast2
            };
            var tableName = "Book";
            if (!string.IsNullOrEmpty(tableName))
            {
                AWSConfigsDynamoDB.Context.TypeMappings[typeof(Book)] = new Amazon.Util.TypeMapping(typeof(Book), tableName);
            }
            var config = new DynamoDBContextConfig { Conversion = DynamoDBEntryConversion.V2 };
            var dbContext = new DynamoDBContext(new AmazonDynamoDBClient(credentials, dbconfig), config);
            serviceCollection.AddSingleton<IDynamoDBContext>(dbContext);

        }
    }
}
