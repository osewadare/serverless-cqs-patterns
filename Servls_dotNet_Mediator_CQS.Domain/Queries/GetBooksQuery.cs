
using MediatR;
using Servls_dotNet_Mediator_CQS.Domain.Entities;
using Servls_dotNet_Mediator_CQS.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servls_dotNet_Mediator_CQS.Domain.Queries
{
    public class GetBooksQuery: IRequest<List<Book>>
    {

    }

    public class GetBooksQueryHandler : IRequestHandler<GetBooksQuery, List<Book>>
    {
        private IRepository<Book> bookRepository;

        public GetBooksQueryHandler(IRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository;   
        }

        public Task<List<Book>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = bookRepository.GetAll();
            return books;
        }
    }
}
