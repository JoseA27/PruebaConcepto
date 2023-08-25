using AWSDB.Data;
using AWSDB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Diagnostics;

namespace AWSDB.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;
		private readonly ApplicationDBContext _db;
		string connetionString;
		public HomeController(ILogger<HomeController> logger, ApplicationDBContext db)
		{
			_db = db;
			_logger = logger;
			connetionString = "server=tecdb.ctfxom3mv69f.us-east-2.rds.amazonaws.com,1433;Database=WebLeads;TrustServerCertificate=True;User ID=admin;Password=tecaws123;";
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

		public IActionResult Volver()
		{
			return RedirectToAction("Index", "Home");
		}
		public IActionResult Create()
		{
			return View();
		}
		public IActionResult CreateArticle()
		{
			return RedirectToAction("Create", "Home");
		}


		public IActionResult actualizar()
		{
			
			using (SqlConnection connection = new SqlConnection(connetionString))
			{
				connection.Open();
				using (SqlCommand command = new SqlCommand("AddArticulo", connection))
				{
					command.CommandType = CommandType.StoredProcedure;

					command.Parameters.AddWithValue("@inNombre", "Lata Zinc");
					command.Parameters.AddWithValue("@inPrecio", 4000);
					command.Parameters.Add("@outResultCode", SqlDbType.Int).Direction = ParameterDirection.Output;

					command.ExecuteNonQuery();

					int resultCode = Convert.ToInt32(command.Parameters["@outResultCode"].Value);
					connection.Close();
					if (resultCode==50002)
					{
						return RedirectToAction("Create", "Home");
					}
					else
					{
						return RedirectToAction("Index", "Home");
					}
				}
			}
		}

		
	}
}