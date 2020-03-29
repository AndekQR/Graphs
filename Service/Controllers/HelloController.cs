using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Service.Controllers {

    public class HelloController : ApiController {

        [HttpGet]
        public string helloWorld() {
            return "Hello World";
        }
    }
}