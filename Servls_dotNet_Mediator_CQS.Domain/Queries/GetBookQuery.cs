using LMCD.Domain.Entities;
using LMCD.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servls_dotNet_Mediator_CQS.Domain.Queries
{
    public class GetBookQuery : IRequest<Book>
    {
        public string bookId;
    }

    public class GetBookQueryHandler : IRequestHandler<GetBookQuery, Book>
    {
        private IRepository<Book> bookRepository;
        public GetBookQueryHandler(IRepository<Book> bookRepository)
        {
            this.bookRepository = bookRepository; 
        }

        public Task<Book> Handle(GetBookQuery request, CancellationToken cancellationToken)
        {
            var book = bookRepository.GetById(request.bookId);
            return book;
        }
    }
}
