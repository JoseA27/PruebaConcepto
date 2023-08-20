using AWSDB.Data;
using AWSDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace AWSDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDBContext db)
        {
            _db = db;
            _logger = logger;
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //---------------------------------------------------------------------------------------------------------

        public IActionResult Index()
        {
            var getArticulo = _db.Articulo.FromSqlRaw("getArticulo").ToList();
            return View(getArticulo);
        }
        public async Task<ActionResult> actualizar()
        {
            string Nombre = "Llave Inglesa";
            int Precio = 3500;
  
            var param = new SqlParameter[]
            {
                new SqlParameter()
                {
                    ParameterName = "@Nombre",
                    SqlDbType = System.Data.SqlDbType.VarChar,
                    Value = Nombre
                },
                new SqlParameter()
                {
                    ParameterName = "@Precio",
                    SqlDbType = System.Data.SqlDbType.Money,
                    Value = Precio
                }

            };

            var addArticulo = await _db.Database.ExecuteSqlRawAsync($"Exec AddArticulo @Nombre, @Precio", param);

			return View();
        }
	}
}