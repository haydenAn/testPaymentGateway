using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using DaystarPaymentGateway2.Models;
using DaystarPaymentGateway;
using DaystarPaymentGateway.FirstData;

namespace DaystarPaymentGateway2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public static bool TestMode;

        public HomeController(ILogger<HomeController> logger) // , bool testMode)
        {
            _logger = logger;
            TestMode = false; // testMode;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public string Status()
        {
            return "working";
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public string Tokenize(string cc)
        {
            return "got 101";
        }

        [HttpPost]
        public Response TokenizeCreditCard(string cc)
        {
            return new Response()
            {
                Token = Guid.NewGuid().ToString().Replace("-", "")
            };
        }

		[HttpPost]
		public Response ChargeToken([FromBody] CardRequest request)
		{
			var transaction = new Transaction
			{
				ExactID = TestMode ? Constants.TestExactID : Constants.ExactID,
				Password = TestMode ? Constants.TestPassword : Constants.Password,
				Transaction_Type = Definitions.FirstData.TransactionType.Purchase,
				Card_Number = request.CreditCardNumber,
				CAVV = string.IsNullOrEmpty(request.CardSecurityCode) ? Constants.FakeCardSecurityCode : request.CardSecurityCode,
				CardHoldersName = Helper.Sanitize(request.NameOnAccount),
				DollarAmount = request.Amount.ToString(Constants.CurrencyFormat),
				Currency = request.Currency,
				Reference_No = request.PartnerAccountNumber.ToString(),
				Customer_Ref = request.ID.ToString(),
				Reference_3 = request.Source,
				Client_Email = Definitions.Emails.DPG,
				//Address = string.IsNullOrEmpty(request.AddressLine1) ? Constants.DefaultAddress
				//    : new Address_Type
				//    {
				//        Address1 = Helper.Sanitize(request.AddressLine1),
				//        Address2 = string.IsNullOrEmpty(request.AddressLine2) ? null : Helper.Sanitize(request.AddressLine2),
				//        City = Helper.Sanitize(request.City),
				//        State = string.IsNullOrEmpty(request.State) ? Constants.FakeState : Helper.Sanitize(request.State),
				//        Zip = string.IsNullOrEmpty(request.Zip)
				//            ? Constants.FakeZip
				//            : request.Zip.Length > 5 && request.Country.StartsWith("US")
				//                ? request.Zip.Substring(0, 5)
				//                : Helper.Sanitize(request.Zip),
				//        CountryCode = request.Country,
				//        PhoneNumber = string.IsNullOrEmpty(request.PhoneNumber) ? Constants.DefaultPhone : request.PhoneNumber,
				//        PhoneType = Constants.PhoneType
				//    },
				Expiry_Date = $"{request.ExpirationMonth}{request.ExpirationYear.Substring(request.ExpirationYear.Length - 2)}"
			};

			TransactionResult res = new TransactionResult();
			res.Bank_Resp_Code = "Bank101";
			request.ID = Convert.ToInt64(DateTime.Now.ToString("yyyyMMddhhmmssff"));
			// Even is approved
			if (request.Amount % 2 == 0)
			{
				res.Bank_Message = "Use your credit wisely";
				res.Transaction_Approved = true;
				res.Authorization_Num = Guid.NewGuid().ToString().Replace("-", "");
			}
			else
			{
				res.Bank_Message = "Declined bank msg";
				res.Transaction_Approved = false;
				res.Authorization_Num = "Declined";
			}

			var response = new Response
			{
				ID = request.ID,
				ResponseCode = DaystarPaymentGateway.FirstDataAccess.ComputeResponseCode(request.ID, res.Bank_Resp_Code, res.Bank_Message, res.Transaction_Approved),
				ApprovedAmount = res.Transaction_Approved ? request.Amount.ToString(Constants.CurrencyFormat) : 0.ToString(Constants.CurrencyFormat),
				GatewayID = res.Authorization_Num,
				Message = res.Bank_Message
			};

			return response;
		}
	}
}
