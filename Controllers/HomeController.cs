using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Data_Grid.Models;
using System.Data.SqlClient;
namespace Data_Grid.Controllers
{
    public class HomeController : Controller
    {
        SqlCommand com = new SqlCommand();
        SqlDataReader dr;
        SqlConnection con = new SqlConnection();
        List<Address> addresses = new List<Address>();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            con.ConnectionString =Data_Grid.Properties.Resources.ConnectionString;
        }

        public IActionResult Index()
        {
            FetchData();
            return View(addresses);
        }
        private void FetchData()
        {
            if(addresses.Count > 0)
            {
                addresses.Clear();
            }
            try
            {
                con.Open();
                com.Connection = con;
                com.CommandText = "SELECT TOP (1000) [AddressID],[AddressLine],[City],[StateProvinceID],[PostalCod],[SpatialLocation],[RowID],[ModifiedDate] FROM [AdventureWorks2019].[dbo].[Address]";
                dr = com.ExecuteReader();
                while (dr.Read())
                {
                    addresses.Add(new Address() {AddressID = dr["AddressID"].ToString()
                    ,AddressLine = dr["AddressLine"].ToString()
                    ,City = dr["City"].ToString()
                    ,StateProvinceID = dr["StateProvinceID"].ToString()
                    ,PostalCode = dr["PostalCod"].ToString()
                    ,SpatialLocation = dr["SpatialLocation"].ToString()
                    ,RowID = dr["RowID"].ToString()
                    ,ModifiedDate = dr["ModifiedDate"].ToString()
                    });
                }
                con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
    }
}
