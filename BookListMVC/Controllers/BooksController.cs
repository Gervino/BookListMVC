using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BookListMVC.AppDbContext;
using BookListMVC.DTO;
using BookListMVC.Models;
using BookListMVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static BookListMVC.Enum.EnumDeclaration;

namespace BookListMVC.Controllers
{
    //[Route("api/Books")]
    //[ApiController]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public BooksController(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        [BindProperty]
        public Book Book { get; set; }


        public IActionResult Index()
        {
            var bookObj = new Book();
            //bookObj.Author 

            //var bookDTO = _mapper.Map<BookDTO>(bookObj);
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Book = new Book();
            if (id == null)
            {
                //Create
                return View(Book);
            }
            //Update
            Book = _db.Books.FirstOrDefault(u => u.Id == id);
            if (Book == null)
            {
                return NotFound();
            }
            return View(Book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert()
        {
            if (ModelState.IsValid)
            {
                if (Book.Id == 0)
                {
                    //Create
                    _db.Books.Add(Book);
                }
                else
                {
                    //Update
                    _db.Books.Update(Book);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            
            return View(Book);
        }

        #region API Calls
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Json(new { data = await _db.Books.ToListAsync() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var bookFromDb = await _db.Books.FirstOrDefaultAsync(u => u.Id == id);

            if (bookFromDb == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            _db.Books.Remove(bookFromDb);
            await _db.SaveChangesAsync();
            return Json(new { success = true, message = "Delete successfull" });
        }
        #endregion

        public void SetTable()
        {
            LayOutVar aa = new LayOutVar();

            aa.Idd = 3;
        }        
    }
}