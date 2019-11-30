using AuthDatabase.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ShallowLibApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;

namespace ShallowLibApp.Services
{
    public interface ILibService
    {
        Task<LibraryItem[]> GetIncompleteItemsAsync(AppUser user);
        Task<AuthorsItem[]> GetAllAutor(AppUser user);
        Task<LibraryItem[]> GetMyRent(string id_user);
        Task<LibraryItem[]> GetItembyMedia(AppUser user, string id_media, string searchString);
        Task<LibraryItem[]> GetItembyMediastatus(AppUser user, string id_media,string id_rent, string searchString);
        Task<LibraryItem[]> GetsdetailsItem(int id_item);
        void UpdateStatItem(int id_item,string user_id);
        void DeleteItem(int id_item, LibraryItem newItem);
        void AddItemAsync(LibraryItem newItem, AppUser user);
        void EditItemAsync(int id_item ,LibraryItem newItem, AppUser user);
        Task<IEnumerable<Uri>> ListAsync();
        Task UploadAsync( LibraryItem newItem, AppUser user);
        Task DeleteAsync(string fileUri);
        Task DeleteAllAsync();


    }
}
