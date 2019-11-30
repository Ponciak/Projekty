using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database.Entities;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;


namespace WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class RepositoryController : ControllerBase
    {
        private DatabaseContext _dbContext;
      
        public RepositoryController(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }
        //
        [HttpGet ]
        public ActionResult<IEnumerable<LibraryRepositorys>> GetsAllItem()
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable<LibraryRepositorys> ItemsLibrary = _dbContext.Librarys.Select(item => new LibraryRepositorys()
            {
                ID = item.ID,
                Year = item.Year,
                TypeofMedia = item.TypeofMedia,
                Title = item.Title,
                AutorId = item.Autor.ID,
                AutorName = item.Autor.Name,
                DateRent = item.DateRent,
                Renter = item.Renter,
                Lender = item.Lender,
                BlobID = item.BlobID,
                State = item.State
            });
            return  ItemsLibrary.ToList();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Autors>> GetsAllAutorItem()
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable<Autors> Itemsautor = _dbContext.Autors.OrderBy(a=>a.Name).Select(item => new Autors()
            {
                ID = item.ID,
                Name=item.Name
            });
            return Itemsautor.ToList();
        }



        [HttpGet("{id_media},{searchString}")]
        public ActionResult<IEnumerable<LibraryRepositorys>> GetsAllItemByMedia(string id_media, string searchString)
        {
                                              

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (id_media == "ALL")

                {
                    if (searchString == "*")
                        searchString = null;
                    if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(id_media))
                    {
                        IQueryable<LibraryRepositorys> ItemsLibrary = _dbContext.Librarys.Where(m => (m.TypeofMedia != id_media) &&
                                                                                           ((searchString == null || m.Title.ToLower().Contains(searchString.ToLower())) ||
                                                                                           (searchString == null || m.Autor.Name.ToLower().Contains(searchString.ToLower())))).Select(item => new LibraryRepositorys()
                                                                                           {
                                                                                               ID = item.ID,
                                                                                               Year = item.Year,
                                                                                               TypeofMedia = item.TypeofMedia,
                                                                                               Title = item.Title,
                                                                                               AutorId = item.Autor.ID,
                                                                                               AutorName = item.Autor.Name,
                                                                                               DateRent = item.DateRent,
                                                                                               Renter = item.Renter,
                                                                                               Lender = item.Lender,
                                                                                               BlobID = item.BlobID,
                                                                                               State = item.State

                                                                                           });



                        return ItemsLibrary.ToList();
                    }
                }
                else
                {
                    if (searchString == "*")
                        searchString = null;
                    if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(id_media))
                    {
                        IQueryable<LibraryRepositorys> ItemsLibrary = _dbContext.Librarys.Where(m => (m.TypeofMedia == id_media) &&
                                                                                           ((searchString == null || m.Title.ToLower().Contains(searchString.ToLower())) ||
                                                                                           (searchString == null || m.Autor.Name.ToLower().Contains(searchString.ToLower())))).Select(item => new LibraryRepositorys()
                                                                                           {
                                                                                               ID = item.ID,
                                                                                               Year = item.Year,
                                                                                               TypeofMedia = item.TypeofMedia,
                                                                                               Title = item.Title,
                                                                                               AutorId = item.Autor.ID,
                                                                                               AutorName = item.Autor.Name,
                                                                                               DateRent = item.DateRent,
                                                                                               Renter = item.Renter,
                                                                                               Lender = item.Lender,
                                                                                               BlobID = item.BlobID,
                                                                                               State = item.State

                                                                                           });



                        return ItemsLibrary.ToList();
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("No Record Found against this id ");
            }
            return Ok("Update ok");
        }

        [HttpGet("{id_media},{id_rent},{searchString}")]
        public ActionResult<IEnumerable<LibraryRepositorys>> GetsAllItemByMediastatus(string id_media, string id_rent, string searchString)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                if (id_media == "ALL")

                {
                    bool stat = bool.Parse(id_rent);

                    if (searchString == "*")
                        searchString = null;
                    if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(id_media))
                    {
                        IQueryable<LibraryRepositorys> ItemsLibrary = _dbContext.Librarys.Where(m => (m.TypeofMedia != id_media) && (m.State == stat) &&
                                                                                           ((searchString == null || m.Title.ToLower().Contains(searchString.ToLower())) ||
                                                                                           (searchString == null || m.Autor.Name.ToLower().Contains(searchString.ToLower())))).Select(item => new LibraryRepositorys()
                                                                                           {
                                                                                               ID = item.ID,
                                                                                               Year = item.Year,
                                                                                               TypeofMedia = item.TypeofMedia,
                                                                                               Title = item.Title,
                                                                                               AutorId = item.Autor.ID,
                                                                                               AutorName = item.Autor.Name,
                                                                                               DateRent = item.DateRent,
                                                                                               Renter = item.Renter,
                                                                                               Lender = item.Lender,
                                                                                               BlobID = item.BlobID,
                                                                                               State = item.State

                                                                                           });

                        return ItemsLibrary.ToList();
                    }
                }
                else
                {

                    bool stat = bool.Parse(id_rent);

                    if (searchString == "*")
                        searchString = null;
                    if (!String.IsNullOrEmpty(searchString) || !String.IsNullOrEmpty(id_media))
                    {
                        IQueryable<LibraryRepositorys> ItemsLibrary = _dbContext.Librarys.Where(m => (m.TypeofMedia == id_media) && (m.State == stat) &&
                                                                                           ((searchString == null || m.Title.ToLower().Contains(searchString.ToLower())) ||
                                                                                           (searchString == null || m.Autor.Name.ToLower().Contains(searchString.ToLower())))).Select(item => new LibraryRepositorys()
                                                                                           {
                                                                                               ID = item.ID,
                                                                                               Year = item.Year,
                                                                                               TypeofMedia = item.TypeofMedia,
                                                                                               Title = item.Title,
                                                                                               AutorId = item.Autor.ID,
                                                                                               AutorName = item.Autor.Name,
                                                                                               DateRent = item.DateRent,
                                                                                               Renter = item.Renter,
                                                                                               Lender = item.Lender,
                                                                                               BlobID = item.BlobID,
                                                                                               State = item.State

                                                                                           });



                        return ItemsLibrary.ToList();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("No Record Found against this id ");
            }
            return Ok("Update ok");
        }

        [HttpGet("{id_item}")]
        public IEnumerable<LibraryRepositorys> Gets(int id_item)
        {
            IQueryable<LibraryRepositorys> ItemsLibrary = _dbContext.Librarys.Where(s => s.ID == id_item).Select(item => new LibraryRepositorys()
            {
                ID = item.ID,
                Year = item.Year,
                TypeofMedia = item.TypeofMedia,
                Title = item.Title,
                AutorId = item.Autor.ID,
                AutorName = item.Autor.Name,
                DateRent = item.DateRent,
                Renter = item.Renter,
                Lender = item.Lender,
                BlobID = item.BlobID,
                State = item.State
            });
            return ItemsLibrary.ToList();
        }


        [HttpPost("{AutorID},{autor_name}")]
        public IActionResult AddNewItem(int AutorID,string autor_name, [FromBody] LibraryRepositorys item)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (AutorID == 0)
            {
                if (autor_name.Length > 1)
                {
                    _dbContext.Autors.Add(new Autor()
                    {
                        Name = autor_name
                    });
                    _dbContext.SaveChanges(true);
                }

            }

            else
            {
                var autor_id = _dbContext.Autors.SingleOrDefault(a => a.ID == AutorID);

                if (autor_id == null)
                {
                    return NotFound();
                }
                else
                {
                    _dbContext.Librarys.Add(new Library()
                    {

                        Year = item.Year,
                        TypeofMedia = item.TypeofMedia,
                        Title = item.Title,
                        Autor = autor_id,
                        DateRent = item.DateRent,                       
                        BlobID = item.BlobID,
                        State = false

                    });
                }
                _dbContext.SaveChanges(true);
            }
            return StatusCode(StatusCodes.Status201Created);

        }

        [HttpPut("{id_item},{user_id}")]
        public IActionResult UpdateStatusItem(int id_item,string user_id)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Database.Entities.Library dbitem = _dbContext.Librarys.SingleOrDefault(i => i.ID == id_item);

                if (dbitem.State == false)
                {
                    dbitem.DateRent = DateTime.Now;
                    dbitem.Renter = user_id;
                    dbitem.Lender = "";
                    dbitem.State = true;
                }
                else
                {
                    dbitem.DateRent = DateTime.Now;
                    dbitem.Renter = "";
                    dbitem.Lender = "";
                    dbitem.State = false;
                };
                
                _dbContext.Librarys.Update(dbitem);
                _dbContext.SaveChanges(true);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("No Record Found against this id ");
            }
            return Ok("Update ok");
                        
        }


        [HttpPut("{id_item}")]
        public IActionResult EditItem(int id_item,  [FromBody] LibraryRepositorys item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                Database.Entities.Library Library = _dbContext.Librarys.Where(a => a.ID == id_item).FirstOrDefault();
                if (item.AutorId > 0)
                { var autor_id = _dbContext.Autors.SingleOrDefault(a => a.ID == item.AutorId);
                    Library.Autor = autor_id;                   
                }
                if(item.Title !=null)
                    Library.Title = item.Title;

                if (item.TypeofMedia != null)
                    Library.TypeofMedia = item.TypeofMedia;

                if (item.Year != null)
                    Library.Year = item.Year;

                if (item.BlobID != null)
                    Library.BlobID = item.BlobID;

                _dbContext.UpdateRange(Library);
                _dbContext.SaveChanges(true);
              
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("No Record Found against this id ");
            }
            return Ok("Update ok");

        }

        [HttpDelete("{id_item}")]
        public IActionResult DeleteItem(int id_item, [FromBody] LibraryRepositorys item)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id_item != item.ID)
            {
                return BadRequest();
            }

            try
            {
                Database.Entities.Library dbitem = _dbContext.Librarys.SingleOrDefault(i => i.ID == id_item);

                _dbContext.Librarys.Remove(dbitem);
                _dbContext.SaveChanges(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return NotFound("No Record Found against this id ");
            }
            return Ok("Delete ok");
        }

        [HttpGet("{id_user}")]
        public ActionResult<IEnumerable<LibraryRepositorys>> GetMyItemRent(string id_user)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            IQueryable<LibraryRepositorys> ItemsLibrary = _dbContext.Librarys.Where(s => s.Renter == id_user && s.State==true)
                                                                             .Select(item => new LibraryRepositorys()
            {
                ID = item.ID,
                Year = item.Year,
                TypeofMedia = item.TypeofMedia,
                Title = item.Title,
                AutorId = item.Autor.ID,
                AutorName = item.Autor.Name,
                DateRent = item.DateRent,
                Renter = item.Renter,
                Lender = item.Lender,
                BlobID = item.BlobID,
                State = item.State
            });
           
            return ItemsLibrary.ToList();
        }
    }
}