using Braintree;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Nokket.Controllers
{
    public class Client_TokenController : ApiController
    {
        public class JsonResult
        {
            public string client_token;
        }

        public IBraintreeConfiguration config = new BraintreeConfiguration();

        // GET api/cient_token
        public IHttpActionResult Get()
        {
            var gateway = config.GetGateway();

            JsonResult jsonResult = new JsonResult();
            jsonResult.client_token = gateway.ClientToken.generate();

            return Ok(jsonResult);
        }
    }
}
