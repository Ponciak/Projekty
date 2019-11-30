using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AuthDatabase.Entities;
using AzureBlobLearning.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ShallowLibApp.Models;
using ShallowLibApp.Services;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShallowLibApp.Controllers
{
    public class HomeController : Controller
    {

        private readonly ILibService _libService;
        private readonly UserManager<AppUser> _userManager;
        private IHostingEnvironment _env;
        private readonly IAzureBlobConnectionFactory _azureBlobConnectionFactory;


        public HomeController(ILibService libService, UserManager<AppUser> userManager, IHostingEnvironment env, IAzureBlobConnectionFactory azureBlobConnectionFactory)
        {        
            _libService = libService;
            _userManager = userManager;
            _env = env; ;
            _azureBlobConnectionFactory = azureBlobConnectionFactory;


        }


        [HttpGet]
        public async Task<IActionResult> Index( string id, string searchString)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            if (id == null)
                id = "ALL";
            if (searchString == null)
                searchString = "*";

            IEnumerable<LibraryItem> currentTodoItems = await _libService.GetItembyMedia(currentUser, id, searchString);

            var libraryVM = new LibraryViewModel() { Items = currentTodoItems };          

            if (libraryVM == null)
            {
                libraryVM.Items.Select(m => m);

                return RedirectToAction(nameof(Index));
            }

            return View(libraryVM);
        }

        
        public async Task<IActionResult> DodajAutora()
        {
         
            if (ModelState.IsValid)

            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Challenge();
                }

                return View();
            }

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> DodajAutora(LibraryViewModel libraryItem)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            if (ModelState.IsValid)
            {   
                var Item = new LibraryItem
                {                   
                    AutorId = 0,                   
                    AutorName = libraryItem.Items2.AutorName,                  
                };

                _libService.AddItemAsync(Item, currentUser);

                return RedirectToAction(nameof(CreateMedia));
            }

            return View(libraryItem);
        }

        public async Task<IActionResult> CreateMedia(string BlobID)
        {
            if (string.IsNullOrEmpty(BlobID))
                ViewBag.ImgPath = "/images/" + BlobID;


            if (ModelState.IsValid)

            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Challenge();
                }

                IEnumerable<AuthorsItem> authors = await _libService.GetAllAutor(currentUser);
                IEnumerable<LibraryItem> media = await _libService.GetIncompleteItemsAsync(currentUser);
                LibraryViewModel viewModel = new LibraryViewModel { Iauthors = authors, Items = media };

                ViewBag.ImgPath = "/images/" + BlobID;

                return View(viewModel);
            }

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateMedia(LibraryViewModel libraryItem)
        {
           
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            if (ModelState.IsValid)            
            {
                if (libraryItem.Items2.AutorId > 0)
                { libraryItem.Items2.AutorName = "1"; }
                else
                { libraryItem.Items2.AutorId = 0; }

                    var Item = new LibraryItem
                    {
                        Year = libraryItem.Items2.Year,
                        TypeofMedia = libraryItem.Items2.TypeofMedia,
                        Title = libraryItem.Items2.Title,
                        AutorId = libraryItem.Items2.AutorId,
                        DateRent = DateTime.Now,
                        BlobID = libraryItem.Items2.BlobID,
                        AutorName = libraryItem.Items2.AutorName,
                        State = false
                    };

                    _libService.AddItemAsync(Item, currentUser);


                IEnumerable<AuthorsItem> authors = await _libService.GetAllAutor(currentUser);
                IEnumerable<LibraryItem> media = await _libService.GetIncompleteItemsAsync(currentUser);
                LibraryViewModel viewModel = new LibraryViewModel { Iauthors = authors, Items=media };


                return View(viewModel);
            }

          return View(libraryItem);

        }



        public async Task<IActionResult> UploadAsync(string BlobID)
        {
            

            if (ModelState.IsValid)

            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Challenge();
                }

                IEnumerable<AuthorsItem> authors = await _libService.GetAllAutor(currentUser);
                IEnumerable<LibraryItem> media = await _libService.GetIncompleteItemsAsync(currentUser);
                LibraryViewModel viewModel = new LibraryViewModel { Iauthors = authors, Items = media };

                if (string.IsNullOrEmpty(BlobID))
                    ViewBag.ImgPath = "https://ppoassetsstore.blob.core.windows.net/shallowlib/1637107205128650000_4379bf12-bb7b-4b31-98cc-00d4bb131717.jpg";
                else
                ViewBag.ImgPath = BlobID;

                return View(viewModel);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> UploadAsync(LibraryViewModel libraryItem, string BlobID)
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            try
            {
                
                if (libraryItem.Items2.AutorId > 0)
                { libraryItem.Items2.AutorName = "1"; }
                else
                { libraryItem.Items2.AutorId = 0; }
                if (ModelState.IsValid)
                {
                    if (libraryItem.Items2.AutorId > 0)
                    { libraryItem.Items2.AutorName = "1"; }
                    else
                    { libraryItem.Items2.AutorId = 0; }

                    var Item = new LibraryItem
                    {
                        Year = libraryItem.Items2.Year,
                        TypeofMedia = libraryItem.Items2.TypeofMedia,
                        Title = libraryItem.Items2.Title,
                        AutorId = libraryItem.Items2.AutorId,
                        DateRent = DateTime.Now,
                        BlobID = libraryItem.Items2.BlobID,
                        AutorName = libraryItem.Items2.AutorName,
                        State = false
                    };

                    _libService.AddItemAsync(Item, currentUser);


                    IEnumerable<AuthorsItem> authors = await _libService.GetAllAutor(currentUser);
                    IEnumerable<LibraryItem> media = await _libService.GetIncompleteItemsAsync(currentUser);
                    LibraryViewModel viewModeladd = new LibraryViewModel { Iauthors = authors, Items = media ,Items2= Item };


                    return RedirectToAction(nameof(UploadAsync));



                }
            }
            catch (Exception ex)
            {
                ViewData["message"] = ex.Message;
                ViewData["trace"] = ex.StackTrace;
                return View("Error");
            }
            return View(libraryItem);
        }

        [HttpPost("UploadAsync2")]
        public async Task<IActionResult> UploadAsync2(IFormCollection form)
        {
            
            var blobContainer = await _azureBlobConnectionFactory.GetBlobContainer();

           for (int i = 0; i < form.Files.Count; i++)
            {
                var blob = blobContainer.GetBlockBlobReference(GetRandomBlobName(form.Files[i].FileName));

                blob.Properties.ContentType = "image/jpeg";

                using (var stream = form.Files[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }
                return RedirectToAction(nameof(UploadAsync), new { BlobID = blob.Uri.ToString() });
            }
            return RedirectToAction(nameof(UploadAsync));
        }
           

        //[HttpPost("UploadFile")]
        //public async  Task<IActionResult> UploadFile(IFormCollection form )
        //{
        //    var webroot = _env.WebRootPath;
        //    var filePath = Path.Combine(webroot.ToString() + "\\images\\" + form.Files[0].FileName);

        //    if(form.Files[0].FileName.Length>0)
        //    {
        //        using(var stream = new FileStream(filePath,FileMode.Create))
        //        {
        //            await form.Files[0].CopyToAsync(stream);
        //        }
        //    }

        //   //if (Convert.ToString(form["ID"]) == string.Empty || Convert.ToString(form["ID"]) == "0")
        //   return RedirectToAction(nameof(CreateMedia), new { BlobID = Convert.ToString(form.Files[0].FileName) });

        //    //return RedirectToAction(nameof(EditMedia), new { BlobID = Convert.ToString(form.Files[0].FileName), ID= Convert.ToString(form["ID"]) });

        //}

        public async Task<IActionResult> EditMedia(LibraryItem item_lib, string BlobID)
        {
            if (string.IsNullOrEmpty(BlobID))
                ViewBag.ImgPath = "/images/" + BlobID;


            if (ModelState.IsValid)

            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    return Challenge();
                }

                //IEnumerable<AuthorsItem> authors = await _libService.GetAllAutor(currentUser);
                IEnumerable<LibraryItem> media = await _libService.GetsdetailsItem(item_lib.ID);
                LibraryViewModel viewModel = new LibraryViewModel { Items = media };

                ViewBag.ImgPath = "/images/" + BlobID;

                return View(viewModel);
            }

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditMedia(LibraryViewModel libraryItem)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            if (ModelState.IsValid)
            {                
                var Item = new LibraryItem
                {
                    Year = libraryItem.Items2.Year,
                    TypeofMedia = libraryItem.Items2.TypeofMedia,
                    Title = libraryItem.Items2.Title,
                    AutorId = libraryItem.Items2.AutorId,
                    DateRent = DateTime.Now,
                    BlobID = libraryItem.Items2.BlobID,
                    AutorName = libraryItem.Items2.AutorName,
                    State = false
                };

                _libService.EditItemAsync(Item.ID,Item, currentUser);                
                IEnumerable<LibraryItem> media = await _libService.GetsdetailsItem(Item.ID);
                LibraryViewModel viewModel = new LibraryViewModel {  Items = media };
                return View(viewModel);
            }
            return View(libraryItem);
        }


        //public async Task<IActionResult> ItemByMediastring(string id)
        //{
        //    var currentUser = await _userManager.GetUserAsync(User);
        //    if (currentUser == null)
        //    {
        //        return Challenge();
        //    }
        //    IEnumerable<LibraryItem> currentTodoItems = await _libService.GetIncompleteItemsAsync(currentUser);
        //    var libraryVM = new LibraryViewModel()
        //    {
        //        Items = currentTodoItems.Where(m => m.Title.Contains(id) || m.AutorName.Contains(id) || id==null ).Select(m => m)
        //    };
        //    return View(libraryVM);
        //}

        [HttpGet]
        public async Task<IActionResult> Szczegoly(LibraryItem item, int id_item)
        {
          
            IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(item.ID);            
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            var libraryVM = new LibraryViewModel() { Items = currentTodoItems };
            {             
                libraryVM.Items.Select(m => m);
            };
            return View(libraryVM);
        }

        public async Task<IActionResult> Wypozycz(LibraryItem library)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                _libService.UpdateStatItem(library.ID, currentUser.Id);
                IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(library.ID);
                var libraryVM = new LibraryViewModel() { Items = currentTodoItems };
                return View(libraryVM);                
            }
            return View(library);
        }
        [HttpPost]
        public async Task<IActionResult> Wypozycz(LibraryItem item, int id)
        {
             await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {   
                IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(item.ID);
                var libraryVM = new LibraryViewModel() { Items = currentTodoItems };
                return View(libraryVM);                
            }
            return View(item);
        }
        public async Task<IActionResult> Zwroc(LibraryItem library)
        {
            var currentUser = await _userManager.GetUserAsync(User);

            if (ModelState.IsValid)
            {
                _libService.UpdateStatItem(library.ID, currentUser.Id);
                IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(library.ID);
                var libraryVM = new LibraryViewModel() { Items = currentTodoItems };
                return View(libraryVM);                
            }
            return View(library);
        }
        [HttpPost]
        public async Task<IActionResult> Zwroc(LibraryItem item,int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {                
                IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(item.ID);
                var libraryVM = new LibraryViewModel() { Items = currentTodoItems };
                return View(libraryVM);             
            }
            return View(item);
        }
        public async Task<IActionResult> Delete(LibraryItem item)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            if (ModelState.IsValid)
            {
                IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(item.ID);               
                var libraryVM = new LibraryViewModel() { Items = currentTodoItems };
                {
                    libraryVM.Items.Select(m => m);
                };
                var idblob =  libraryVM.Items.First(c=>c.ID==item.ID).BlobID;
                string uri = idblob;                                
                await _libService.DeleteAsync(uri);
                _libService.DeleteItem(item.ID, item);                
                return RedirectToAction(nameof(DeleteInfo));
            }
            return View(item);
        }
        
        public async Task<IActionResult> DeleteInfo(LibraryItem library, int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (ModelState.IsValid)
            {
                IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(library.ID);
                var libraryVM = new LibraryViewModel() { Items = currentTodoItems };
                return View(libraryVM);
            }
            return View(library);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(LibraryItem library,int id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }

            if (ModelState.IsValid)
            {                
                IEnumerable<LibraryItem> currentTodoItems = await _libService.GetsdetailsItem(library.ID);
                var libraryVM = new LibraryViewModel() { Items = currentTodoItems };                
                return RedirectToAction(nameof(Index));
            }
            return View(library);
        }

        [HttpGet]
        public async Task<IActionResult> MediaTypeStat(string id, string searchString)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }
            
            if (id == null)
                id = "ALL";
            if (searchString == null)
                searchString = "*";

            string id_rent = id.Split(',').Last().ToString();            
            id = id.Split(',').First();

            IEnumerable<LibraryItem> currentTodoItems = await _libService.GetItembyMediastatus(currentUser, id,id_rent, searchString);

            var libraryVM = new LibraryViewModel() { Items = currentTodoItems };

            if (libraryVM == null)
            {
                libraryVM.Items.Select(m => m);

                return RedirectToAction(nameof(Index));
            }

            return View(libraryVM);
        }

        [HttpGet]
        public async Task<IActionResult>Myrent()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                return Challenge();
            }


            IEnumerable<LibraryItem> currentTodoItems = await _libService.GetMyRent(currentUser.Id.ToString());

            var libraryVM = new LibraryViewModel() { Items = currentTodoItems };

            if (currentTodoItems == null)
            {
                libraryVM.Items.Select(m => m);

                return RedirectToAction(nameof(Index));
            }

            return View(libraryVM);
        }

        private string GetRandomBlobName(string filename)
        {
            string ext = Path.GetExtension(filename);
            return string.Format("{0:10}_{1}{2}", DateTime.Now.Ticks, Guid.NewGuid(), ext);
        }

    }
}
