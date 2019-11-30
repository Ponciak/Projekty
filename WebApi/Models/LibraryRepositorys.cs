using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class LibraryRepositorys
    {
        public int ID { get; set; }
        public string TypeofMedia { get; set; }

        public string Title { get; set; }

        public int AutorId { get; set; }
        public string AutorName { get; set; }

        public string Year { get; set; }

        public DateTime? DateRent { get; set; }

        public string Renter { get; set; }
        public string Lender { get; set; }

        public string BlobID { get; set; }

        public bool State { get; set; }
    }
}
