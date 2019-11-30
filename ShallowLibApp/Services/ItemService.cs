using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AuthDatabase.Entities;

using AutoMapper;
using AzureBlobLearning.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.WindowsAzure.Storage.Blob;
using ShallowLibApp.Models;

namespace ShallowLibApp.Services
{
    public class ItemService : ILibService
    {
        string url = "http://localhost:58326";
        HttpClient httpClient = new HttpClient();

        private readonly IMapper _mapper;
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;

        public ItemService(IMapper mapper, IAzureBlobConnectionFactory azureBlobConnectionFactory)
        {
            _mapper = mapper;
            _azureBlobConnectionFactory = azureBlobConnectionFactory;
        }

        public async Task<LibraryItem[]> GetIncompleteItemsAsync(AppUser user)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);

            IEnumerable<LibraryRepositorys> dtoItems = await ServiceClient.GetsAllItemAsync();

            IEnumerable<LibraryItem> returnValue = _mapper.Map<IEnumerable<LibraryItem>>(dtoItems);
            

            return returnValue.ToArray();
        }

        public async Task<LibraryItem[]> GetsdetailsItem(int id_item)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);

            
            IEnumerable<LibraryRepositorys> dtoItems = await ServiceClient.GetsAsync( id_item);

            IEnumerable<LibraryItem> returnValue = _mapper.Map<IEnumerable<LibraryItem>>(dtoItems);


            return returnValue.ToArray();
        }

        public void AddItemAsync( LibraryItem newItem, AppUser user)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);
           var doitem =  ServiceClient.AddNewItemAsync(newItem.AutorId, newItem.AutorName, _mapper.Map<LibraryRepositorys>(newItem));
        }

        public async void UpdateStatItem(int id_item, string user_id)
        {
          ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);
          await ServiceClient.UpdateStatusItemAsync(id_item, user_id);         
        }

        public async Task<LibraryItem[]> GetItembyMedia(AppUser user, string id_media, string searchString)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);
            IEnumerable<LibraryRepositorys> typemediaItems = await ServiceClient.GetsAllItemByMediaAsync(id_media,searchString);
            IEnumerable<LibraryItem> returnValue = _mapper.Map<IEnumerable<LibraryItem>>(typemediaItems);
            return returnValue.ToArray();
        }

         public  async Task<AuthorsItem[]> GetAllAutor(AppUser user)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);


            IEnumerable<Autors> dtoItems = await ServiceClient.GetsAllAutorItemAsync();

            IEnumerable<AuthorsItem> returnValue = _mapper.Map<IEnumerable<AuthorsItem>>(dtoItems);


            return returnValue.ToArray();
        }

        public void EditItemAsync(int id_item ,LibraryItem newItem, AppUser user)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);
            ServiceClient.EditItemAsync(id_item,  _mapper.Map<LibraryRepositorys>(newItem));
        }

        public async void DeleteItem(int id_item , LibraryItem newItem)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);

           await  ServiceClient.DeleteItemAsync(newItem.ID, _mapper.Map<LibraryRepositorys>(newItem));
            
        }

        public async Task<LibraryItem[]> GetItembyMediastatus(AppUser user, string id_media, string id_rent, string searchString)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);

            IEnumerable<LibraryRepositorys> typemediaItems = await ServiceClient.GetsAllItemByMediastatusAsync(id_media,id_rent, searchString);

            IEnumerable<LibraryItem> returnValue = _mapper.Map<IEnumerable<LibraryItem>>(typemediaItems);


            return returnValue.ToArray();
        }

        public async Task<LibraryItem[]> GetMyRent( string id_user)
        {
            ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);

            IEnumerable<LibraryRepositorys> typemediaItems = await ServiceClient.GetMyItemRentAsync(id_user);

            IEnumerable<LibraryItem> returnValue = _mapper.Map<IEnumerable<LibraryItem>>(typemediaItems);

            return returnValue.ToArray();
        }

        public async Task DeleteAllAsync()
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                        await ((CloudBlockBlob)blob).DeleteIfExistsAsync();
                }
                blobContinuationToken = response.ContinuationToken;
            } while (blobContinuationToken != null);
        }

        public async Task DeleteAsync(string fileUri)
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

            Uri uri = new Uri(fileUri);
            string filename = Path.GetFileName(uri.LocalPath);

            var blob = blobContainer.GetBlockBlobReference(filename);
            await blob.DeleteIfExistsAsync();
        }

        public async Task<IEnumerable<Uri>> ListAsync()
        {
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();
            var allBlobs = new List<Uri>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                        allBlobs.Add(blob.Uri);
                }
                blobContinuationToken = response.ContinuationToken;
            } while (blobContinuationToken != null);
            return allBlobs;
        }

        public async Task UploadAsync( LibraryItem newItem, AppUser user)
        {

                ShallowLibServiceHttp ServiceClient = new ShallowLibServiceHttp(url, httpClient);
                await ServiceClient.AddNewItemAsync(newItem.AutorId, newItem.AutorName, _mapper.Map<LibraryRepositorys>(newItem));
          
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }


    }
}