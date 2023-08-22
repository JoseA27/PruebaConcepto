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

			string connetionString = null;
			SqlConnection connection;
			SqlDataAdapter adapter;
			SqlCommand command = new SqlCommand();
			SqlParameter param, param2, param3;
			DataSet ds = new DataSet();
			connetionString = "server = tecdb.ctfxom3mv69f.us-east-2.rds.amazonaws.com,1433;Database=WebLeads;TrustServerCertificate=True;User ID=admin;Password=tecaws123;";
			connection = new SqlConnection(connetionString);
			connection.Open();
			command.Connection = connection;
			command.CommandType = CommandType.StoredProcedure;
			command.CommandText = "AddArticulo";
			param = new SqlParameter("@inNombre", "Teja");
			param2 = new SqlParameter("@inPrecio", "1000");
			param3 = new SqlParameter("@outResultCode", 0);
			param.Direction = ParameterDirection.Input;
			param2.Direction = ParameterDirection.Input;
			param3.Direction = ParameterDirection.Output;
			param.DbType = DbType.String;
			param2.DbType = DbType.Currency;
			param3.DbType = DbType.Int32;
			command.Parameters.Add(param);
			command.Parameters.Add(param2);
			command.Parameters.Add(param3);
			adapter = new SqlDataAdapter(command);
			adapter.Fill(ds);
			connection.Close();
			return RedirectToAction("Index", "Home");
		}
	}
}