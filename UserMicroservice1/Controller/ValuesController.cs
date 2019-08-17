using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserModel;

namespace UserMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IList<NotesModel>> Get(string id)
        {
            try
            {
                IList<NotesModel> product = null;
                using (var client = new HttpClient())
                {
                    /// gets the token which is passed
                    var accessToken = Request.Headers["Authorization"].ToString();
                    string[] strArr = null;
                    char[] splitchar = { ' ' };
                    /// splits the token to get the token without bearer keyword
                    strArr = accessToken.Split(splitchar);
                    /// adding token in httpclient instance
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strArr[1]);
                    /// calling api from other services
                    var response = await client.GetAsync("https://localhost:44337/api/Notes/GetNotes" + id);
                    response.EnsureSuccessStatusCode();
                    product = await response.Content.ReadAsAsync<IList<NotesModel>>();
                    // return product;
                }
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
