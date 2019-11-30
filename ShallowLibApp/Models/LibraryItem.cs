using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallowLibApp.Models
{
    public class LibraryItem
    {
        public int ID { get; set; }
        public string TypeofMedia { get; set; }

        public string Title { get; set; }

        public int AutorId { get; set; }
        public string AutorName { get; set; }

        public string Year { get; set; }

        public DateTimeOffset? DateRent { get; set; }

        public string Renter { get; set; }
        public string Lender { get; set; }

        public string BlobID { get; set; }

        public bool State { get; set; }

        public IEnumerable<AuthorsItem> Iauthors { get; set; }
        public IEnumerable<Media> Itemsmedia { get; set; }
    }
}
