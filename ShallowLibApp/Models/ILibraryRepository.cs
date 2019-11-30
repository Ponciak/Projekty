using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShallowLibApp.Models
{
    public interface ILibraryRepository
    {
        IEnumerable<Library> Pobierzwszystkiemedia();
        Library PobierzmediaID(int mediaID);

        void AddMedia(Library library);
        void EditMedia(Library library);

        void DeleteMedia(Library library);

    }
}
