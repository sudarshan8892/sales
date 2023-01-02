using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using SalesAssignment.Models;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SalesAssignment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : Controller
    {
        private readonly AssignmentContext _context;
        public SalesController(AssignmentContext context)
        {
            _context = context;
        }
        // GET: api/<SalesController>
        [HttpGet]
        public JsonResult Get()
        {
            var sales = _context.Sales.ToList();
            return Json(sales);
        }
        // GET api/<SalesController>/5
        [HttpGet("{OrderId}")]
        public JsonResult Get(long OrderId)
        {
            var value = _context.Sales.FirstOrDefault();
            return Json(value);
        }

        [HttpGet]
        [Route("GetSalesGreterThan")]
        public List<Sale> GetSalesGreterThan(int totalRevenue)
        {
            List<Sale> lstSales = null;
            try
            {
                SqlParameter sqlParameter = new SqlParameter("@TotalRevenue", totalRevenue);
                lstSales = _context.Sales.FromSqlRaw("SELECT * FROM SalesDetailsGreterThen(@TotalRevenue)",
                                                              sqlParameter).ToList();
            }
            catch (Exception ex)
            {
                lstSales = null;
            }
            return lstSales;
        }

        [HttpGet]
        [Route("SalesDetailsLesserThan")]
        public List<Sale> SalesDetailsLesserThan(int totalRevenue)
        {
            List<Sale> lstSales = null;
            try
            {
                SqlParameter sqlParameter = new SqlParameter("@TotalRevenue", totalRevenue);
                lstSales = _context.Sales.FromSqlRaw("SELECT * FROM SalesDetailsLesserThan(@TotalRevenue)",
                                                              sqlParameter).ToList();
            }
            catch (Exception ex)
            {
                lstSales = null;
            }
            return lstSales;
        }

        [HttpGet]
        [Route("salesDetailsBetween")]
        public List<Sale> salesDetailsBetween(int totalRevenue,int revenue)
        {
            List<Sale> lstSales = null;
            try
            {
                SqlParameter sqlParameter = new SqlParameter("@TotalRevenue",totalRevenue);
                SqlParameter sqlParameter1 = new SqlParameter("@Revenue", revenue);

                lstSales = _context.Sales.FromSqlRaw("SELECT * FROM salesDetailsBetween(@TotalRevenue,@Revenue)",
                                                              sqlParameter,sqlParameter1).ToList();
            }
            catch (Exception ex)
            {
                lstSales = null;
            }
            return lstSales;
        }

        [HttpGet]
        [Route("salesDetailsNotEqualTo")]
        public List<Sale> salesDetailsNotEqualTo(double totalRevenue)
        {
            List<Sale> lstSales = null;
            try
            {
                SqlParameter sqlParameter = new SqlParameter("@TotalRevenue", totalRevenue);

                lstSales = _context.Sales.FromSqlRaw("SELECT * FROM salesDetailsNotEqualTo(@TotalRevenue)",
                                                              sqlParameter).ToList();
            }
            catch (Exception ex)
            {
                lstSales = null;
            }
            return lstSales;
        }


        [HttpGet("page")]
        public async Task<ActionResult<List<Sale>>> salesDetailsInPage(int page)
        {
            if (_context.Sales == null)
                return NotFound();

            var pageResults = 5f;
            var pageCount = Math.Ceiling(_context.Sales.Count() / pageResults);

            var sales = await _context.Sales
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToListAsync();

            var response = new SalesResponse
            {
                Sales = sales,
                CurrentPage = page,
                Pages = (int)pageCount
            };

            return Ok(response);
        }
        // POST api/<SalesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }
        // PUT api/<SalesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SalesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
