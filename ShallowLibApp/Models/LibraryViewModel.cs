using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallowLibApp.Models
{
    public class LibraryViewModel
    {
       
        public IEnumerable<LibraryItem> Items { get; set; }
        public LibraryItem Items2 { get; set; }
        public IEnumerable<Media> Itemsmedia { get; set; }
        public IEnumerable<AuthorsItem> Iauthors { get; set; }
        public AuthorsItem Autorsitem { get; set; }
    }
}
