using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nokket.Controllers
{
    public class NonceController : ApiController
    {
        // GET api/nonce/transaction
        [HttpGet]
        public string transaction()
        {
            return "nonce";
        }
    }
}
