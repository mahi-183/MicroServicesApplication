// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValuesController.cs" company="Bridgelabz">
//   Copyright © 2019 Company="BridgeLabz"
// </copyright>
// <creator name="Mahesh Aurad"/>
// --------------------------------------------------------------------------------------------------------------------
namespace UserMicroservice.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using UserModel;
    using UserModel.Enumerable;

    /// <summary>
    /// Values controller.
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Return the value.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// Gets the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Get the details.</returns>
        /// <exception cref="System.Exception">Throw new exceptions.</exception>
        [HttpGet]
        [Route("get/{id}")]
        public async Task<IList<NotesModel>> Get(string id)
        {
            try
            {
                IList<NotesModel> product = null;
                using (var client = new HttpClient())
                {
                    //// gets the token which is passed
                    var accessToken = Request.Headers["Authorization"].ToString();
                    string[] strArr = null;
                    char[] splitchar = { ' ' };
                    
                    //// splits the token to get the token without bearer keyword
                    strArr = accessToken.Split(splitchar);
                    
                    //// adding token in httpclient instance
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", strArr[1]);
                    
                    //// calling api from other services
                    var response = await client.GetAsync("https://localhost:44337/api/Notes/GetNotesById?userId=" + id);
                    response.EnsureSuccessStatusCode();
                    product = await response.Content.ReadAsAsync<IList<NotesModel>>();
                }

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Posts the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        /// <summary>
        /// Puts the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="value">The value.</param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
