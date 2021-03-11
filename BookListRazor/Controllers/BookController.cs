using BookListRazor.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookListRazor.Controllers
{
    [Route("api/Book")]
    [ApiController]
    public class BookController : Controller
    {
        private readonly ApplicationDbContext _db;

        public BookController(ApplicationDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            return Json(new { data = _db.Book.ToList() });
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _db.Book.FindAsync(id);

            if (book == null)
            {
                return Json(new { success = false,message = "Erro while Deleting!" });
            }

            _db.Book.Remove(book);
            await _db.SaveChangesAsync();

            return Json(new { success = true, message = "Delete successful!" });
        }
    }
}
