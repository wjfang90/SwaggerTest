using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerTest.Models;

namespace SwaggerTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        /// <summary>
        /// GET api/values 添加注释
        /// </summary>
        /// <returns></returns>
        [HttpGet("Get")]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 添加注释 GET api/values/5
        /// </summary>
        /// <param name="id">参数 id</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<object> Get(int id)
        {
            return new { id = id, value = "value" };
        }

        /// <summary>
        /// POST api/values
        /// </summary>
        /// <param name="value">string value</param>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/values/PostValue
        ///     {
        ///        "value": "value"
        ///     }
        ///
        /// </remarks>
        /// <returns>json value</returns>
        /// <response code="201">Returns the value</response>
        /// <response code="400">the value is null</response>
        [ProducesResponseType(201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpPost("PostValue")]
        public ActionResult<object> PostValue([FromForm]string value)
        {
            /*
                FromForm特性是用来绑定form-data格式中的表单数据，即Content-Type为multipart/form-data
             */
            return new { value = value };
        }

        [HttpPost("PostFromBodyValue")]
          public ActionResult<object> PostFromBodyValue([FromBody]People value)
        {
            /*  
                注意
                FromBody特性只接受Content-Type为application/json的请求,
                apicontroller 默认启用frombody，不需要显示设置
                [FromBody]注解无法绑定简单类型的参数，只支持object 复杂类型参数
             */
            return value;
        }

        /// POST api/values
        [HttpPost("PostPeople")]
        public ActionResult<People> PostPeople(People person)
        {
            return person;
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public ActionResult<object> Put(int id, [FromForm]string value)
        {
            return new { id = id, value = value };
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public ActionResult<object> Delete(int id)
        {
            if (id == default(int))
            {
                return NotFound();
            }
            return new { id = id };
        }
    }
}
