using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Entities
{
    public class Library  
    {
        public int ID { get; set; }
        public string TypeofMedia { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public virtual Autor Autor { get; set; }
        [Required]
        public string Year { get; set; }

        public DateTime ?DateRent { get; set; }
        
        public string  Renter { get; set; }
        public string  Lender { get; set; }
        [Required]
        public string BlobID { get; set; }
        [Required]
        public bool  State { get; set; }
       
    }



}
