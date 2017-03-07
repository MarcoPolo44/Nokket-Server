using Braintree;
using Microsoft.Owin;
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
        public class FormData
        {
            public string nonce { get; set; }
            public string amount { get; set; }
        }

        public class JsonResult
        {
            public string message;
        }

        public IBraintreeConfiguration config = new BraintreeConfiguration();

        // POST api/nonce/transaction
        [HttpPost]
        public IHttpActionResult transaction(FormData formData)
        {
            JsonResult jsonResult = new JsonResult();
            var gateway = config.GetGateway();

            string nonceFromClient = formData.nonce;
            decimal amountFromClient = Convert.ToDecimal(formData.amount);

            var request = new TransactionRequest
            {
                Amount = amountFromClient,
                PaymentMethodNonce = nonceFromClient,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true
                }
            };

            Result<Transaction> result = gateway.Transaction.Sale(request);
            if (result.IsSuccess())
            {
                Transaction transaction = result.Target;
                jsonResult.message = "created " + transaction.Id + " authorized";
            }
            else if (result.Transaction != null)
            {
                jsonResult.message = "";
            }
            else
            {
                string errorMessages = "";
                foreach (ValidationError error in result.Errors.DeepAll())
                {
                    errorMessages += "Error: " + (int)error.Code + " - " + error.Message + "\n";
                }
                jsonResult.message = errorMessages;
            }

            return Ok(jsonResult);
        }
    }
}
