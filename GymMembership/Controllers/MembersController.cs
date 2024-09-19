using GymMembership.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GymMembership.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {

        public readonly IConfiguration _configuration;

        public MembersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // GET: api/GetAllMembers
        [HttpGet]
        [Route("GetAllMembers")]
        public JsonResult GetAllMembers()
        {
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("GymMembership").ToString());
            SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM members", connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<Members> members = new List<Members>();

            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Members member = new Members();
                    member.Id = Convert.ToInt32(dataTable.Rows[i]["id"]);
                    member.FirstName = Convert.ToString(dataTable.Rows[i]["first_name"]);
                    member.MiddleName = Convert.ToString(dataTable.Rows[i]["middle_name"]);
                    member.LastName = Convert.ToString(dataTable.Rows[i]["last_name"]);
                    member.Address = Convert.ToString(dataTable.Rows[i]["address"]);
                    member.PhoneNumber = Convert.ToString(dataTable.Rows[i]["phone_number"]);
                    member.IsMember = Convert.ToByte(dataTable.Rows[i]["is_gym_member"]) == 0 ? false : true;
                    member.IsMonthly = Convert.ToByte(dataTable.Rows[i]["is_monthly"]) == 0 ? false : true;
                    var dateTime = Convert.ToString(dataTable.Rows[i]["created_at"]);
                    member.DateCreated = DateTime.Parse(dateTime);

                    members.Add(member);
                }
            }

            if (members.Count > 0)
            {
                return new JsonResult(members);
            }
            else
            {
                Respones error = new Respones();
                error.StatusCode = 404;
                error.RequestStatus = "Success";
                error.ErrorResponse = "No Members Found";
                return new JsonResult(error);
            }

        }

        //// GET: api/<MembersController>
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<MembersController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<MembersController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<MembersController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<MembersController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
