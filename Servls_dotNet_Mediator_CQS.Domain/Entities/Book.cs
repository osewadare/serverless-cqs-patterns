using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Servls_dotNet_Mediator_CQS.Domain.Entities
{

    public class Book
    {
        public string BookId { get; set; }
        public string Name  { get; set; }
        public DateTime CreatedDate { get; set; }

        public Book()
        {

        }

        public Book(string Name)
        {
            this.Name = Name;
            CreatedDate = DateTime.Now;
        }
            
    }
}
