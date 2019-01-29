
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI_DAPPER.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly DB.Entity.IRepository.IUsersRepository userRepository;
        public ValuesController(DB.Entity.IRepository.IUsersRepository _userRepository)
        {
            userRepository = _userRepository;
        }
        // GET api/values
        [HttpGet]
        public JsonResult Get()
        {
            //常规直接使用new的方式
            //Task<DB.Entity.Model.Users> user =  new DB.Entity.Repository.UsersRepository().GetUserDetailAsync(1);

            Task<List<DB.Entity.Model.Users>> list = userRepository.GetUsersAsync();
            return Json(list.Result);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            Task<DB.Entity.Model.Users> user = userRepository.GetUserDetailAsync(id);
            return Json(user.Result);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public int Put(int id, [FromBody]DB.Entity.Model.Users entity)
        { 
            int a = userRepository.AddEntity(entity);
            return a;
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
