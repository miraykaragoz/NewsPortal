using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsPortal.Models;

namespace NewsPortal.Controllers
{
    public class HomeController : Controller
    {        
        public IActionResult Index()
        {
            var connectionString = "";

            using var connection = new SqlConnection(connectionString);

            var sql = "SELECT * FROM News ORDER BY UpdatedDate DESC";

            var news = connection.Query<News>(sql).ToList();

            return View(news);
        }

        public IActionResult Detail(int id)
        {
            if(id == null)
            {
                return RedirectToAction("Index");
            }

            var newsModel = new News();

            var connectionString = "";

            using var connection = new SqlConnection(connectionString);

            var sql = "SELECT * FROM News WHERE Id = @id";

            var news = connection.QueryFirstOrDefault<News>(sql, new { id });

            if(news == null)
            {
                ViewBag.MessageCssClass = "alert-danger";
                ViewBag.Message = "Bu id'ye sahip bir haber bulunamadý.";
                return View("Message");
            }

            return View(news);
        }
    }
}