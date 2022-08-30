using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Servls_dotNet_Mediator_CQS.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servls_dotNet_Mediator_CQS.Infrastructure
{
    public class DynamoDbRepository<T> : IRepository<T>
    {
        private readonly IDynamoDBContext dynamoDBContext;

        public DynamoDbRepository(IDynamoDBContext dynamoDBContext)
        {
            this.dynamoDBContext = dynamoDBContext;
        }

        public Task Create(T data)
        {
           return dynamoDBContext.SaveAsync(data);
        }

        public Task<List<T>> GetAll()
        {
            return dynamoDBContext.ScanAsync<T>(null).GetNextSetAsync();
        }

        public Task<T> GetById(string id)
        {
            return dynamoDBContext.LoadAsync<T>(id);
        }
    }
}
