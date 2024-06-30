using FidelityStatement.Web.API.DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FidelityStatement.Web.API.Controllers
{
    public class TransactionImportController : Controller
    {
        // GET: TransactionImportController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TransactionImportController/Details/5
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("File is empty.");
            }

            var activities = new List<Transaction>();

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                string headerLine = await stream.ReadLineAsync(); // Read header line

                while (!stream.EndOfStream)
                {
                    var line = await stream.ReadLineAsync();
                    var values = line.Split(',');

                    var activity = new Transaction
                    {
                        RunDate = DateTime.ParseExact(values[0], "MM/dd/yyyy", CultureInfo.InvariantCulture),
                        Action = values[1],
                        Symbol = values[2],
                        Description = values[3],
                        Type = values[4],
                        Quantity = decimal.Parse(values[5]),
                        Price = string.IsNullOrWhiteSpace(values[6]) ? (decimal?)null : decimal.Parse(values[6]),
                        Commission = string.IsNullOrWhiteSpace(values[7]) ? (decimal?)null : decimal.Parse(values[7]),
                        Fees = string.IsNullOrWhiteSpace(values[8]) ? (decimal?)null : decimal.Parse(values[8]),
                        AccruedInterest = string.IsNullOrWhiteSpace(values[9]) ? (decimal?)null : decimal.Parse(values[9]),
                        Amount = decimal.Parse(values[10]),
                        CashBalance = decimal.Parse(values[11]),
                        SettlementDate = string.IsNullOrWhiteSpace(values[12]) ? (DateTime?)null : DateTime.ParseExact(values[12], "MM/dd/yyyy", CultureInfo.InvariantCulture)
                    };
                    activities.Add(activity);
                }
            }

            //_context.Transaction.AddRange(activities);
            //await _context.SaveChangesAsync();

            return Ok(new { count = activities.Count });
        }
    }
}
