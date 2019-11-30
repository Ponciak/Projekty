using AuthDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShallowLibApp.Models;

namespace ShallowLibApp.Services
{
    public interface IService
    {

        Task<LibraryViewModel[]> GetIncompleteItemsAsync(AppUser user);

        Task<Guid> AddItemAsync(LibraryViewModel newItem, AppUser user);

        Task<bool> MarkDoneAsync(LibraryViewModel item, AppUser user);
    }
}
