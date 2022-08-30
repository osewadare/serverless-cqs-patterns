using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servls_dotNet_Mediator_CQS.Domain.Repository
{
    public interface IRepository<T>
    {
        Task<T> GetById(string id);

        Task<List<T>> GetAll();

        Task Create(T data);
    }
}
