using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using NewsPortal.Models;

namespace NewsPortal.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            var connectionString = "";

            using var connection = new SqlConnection(connectionString);

            var sql = "SELECT * FROM News ORDER BY UpdatedDate DESC";

            var news = connection.Query<News>(sql).ToList();

            return View(news);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(News model)
        {
            if(!ModelState.IsValid)
            {
                ViewBag.MessageCssClass = "alert-danger";
                ViewBag.Message = "Haber ekleme işlemi başarısız oldu.";
                return View("Message");
            }

            model.CreatedDate = DateTime.Now;
            model.UpdatedDate = DateTime.Now;

            var ImageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Img.FileName);

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", ImageName);

            using var stream = new FileStream(path, FileMode.Create);

            model.Img.CopyTo(stream);

            model.ImgPath = ImageName;

            var connectionString = "";

            using var connection = new SqlConnection(connectionString);

            var sql = "INSERT INTO News (Title, Summary, Detail, CreatedDate, UpdatedDate, ImgPath) VALUES (@Title, @Summary, @Detail, @CreatedDate, @UpdatedDate, @ImgPath)";

            var data = new
            {
                model.Title,
                model.Summary,
                model.Detail,
                model.CreatedDate,
                model.UpdatedDate,
                model.ImgPath,
            };

            var rowsAffected = connection.Execute(sql, data);

            ViewBag.MessageCssClass = "alert-success";
            ViewBag.Message = "Haber ekleme işlemi başarıyla gerçekleşti.";
            return View("Message");
        }

        public IActionResult Update(int id)
        {
            var connectionString = "Server=104.247.162.242\\MSSQLSERVER2019;Initial Catalog=miraykar_newsportal;User Id=miraykar_newsportaldbuser;Password=Se87l4?a0;TrustServerCertificate=True";

            using var connection = new SqlConnection(connectionString);

            var news = connection.QuerySingleOrDefault<News>("SELECT * FROM News WHERE Id = @Id", new { Id = id });

            return View(news);
        }

        [HttpPost]
        public IActionResult Update(News model)
        {
            var connectionString = "Server=104.247.162.242\\MSSQLSERVER2019;Initial Catalog=miraykar_newsportal;User Id=miraykar_newsportaldbuser;Password=Se87l4?a0;TrustServerCertificate=True";

            using var connection = new SqlConnection(connectionString);

            var sql = "UPDATE News SET Title = @Title, Summary = @Summary, Detail = @Detail, UpdatedDate = @UpdatedDate WHERE Id = @Id";

            var newData = new
            {
                model.Title,
                model.Summary,
                model.Detail,
                UpdatedDate = DateTime.Now,
                model.Id,
            };

            var rowsAffected = connection.Execute(sql, newData);

            ViewBag.Message = "Haber güncelleme işlemi başarıyla gerçekleşti.";
            ViewBag.MessageCssClass = "alert-success";
            return View("message");
        }

        public IActionResult Delete(int id)
        {
            var connectionString = "Server=104.247.162.242\\MSSQLSERVER2019;Initial Catalog=miraykar_newsportal;User Id=miraykar_newsportaldbuser;Password=Se87l4?a0;TrustServerCertificate=True";

            using var connection = new SqlConnection(connectionString);

            var sql = "DELETE FROM News WHERE Id = @Id";

            var rowsAffected = connection.Execute(sql, new { Id = id });

            return RedirectToAction("Index");
        }
    }
}