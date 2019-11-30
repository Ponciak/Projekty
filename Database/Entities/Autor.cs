using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Entities
{

    public class Autor
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public virtual ICollection<Library> Librarys { get; set; }
       // = new List<Library>();

       
    }

}





