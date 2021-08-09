using DaystarPaymentGateway.FirstData;

namespace DaystarPaymentGateway
{
    class Constants
    {
        public const string TestGatewayURL = "https://api.demo.globalgatewaye4.firstdata.com/transaction/v27";
        public const string TestExactID = @"HA0349-26";
        public const string TestPassword = @"SJ401BDLsMYgTJ6ZQQxGzzDrNusGHOaP";
        public const string TestHMACKey = @"1kuQtj8NqUwBeEXDP7NwbMLPVnJZHlot";
        public const string TestHMACKeyID = @"602537";

        public const string GatewayURL = "https://api.globalgatewaye4.firstdata.com/transaction/v27";
        public const string ExactID = @"L30479-41";
        public const string Password = @"KdI4oDXQUlA5lnkxaiBptVqekX982REh";
        public const string HMACKey = @"aug0MNE98GsJEJN51Iih4d4J6f5vY9bj";
        public const string HMACKeyID = @"610636";

        public const string UnitedStatesCountryCode = "US";

        public class HMAC
        {
            public const string DateFormat = "yyyy-MM-ddTHH:mm:ssZ";
            public const string Post = "POST";
            public const string SOAP = "text/xml";
            public const string URI = "/transaction/v27";
            public const string AuthorizationPrefix = "GGE4_API";
        }

        public const string Authorization = @"Authorization";
        public const string ContentType = @"Content-Type";
        public const string ContentSHA1 = @"x-gge4-content-sha1";
        public const string Date = @"x-gge4-date";

        public const string FakeZip = "0000";
        public const string FakeState = "XX";
        public const string FakeCardSecurityCode = "000";

        public const string CurrencyFormat = "F2";

        // Daystar's main line
        public const string PhoneType = "H";
        public const string DefaultPhone = "8003290029"; 
                                                         
        // Daystar's address
        public static readonly Address_Type DefaultAddress = new Address_Type
        {
            Address1 = "3901 Hwy 121",
            Address2 = null,
            City = "Bedford",
            State = "TX",
            Zip = "76021",
            CountryCode = "USA", // This may have to be "United States" per DPG IDs 910354 & 910355
            PhoneNumber = DefaultPhone,
            PhoneType = PhoneType
        };


        //1 – MOTO (Mail Order / Telephone Order) Indicator – Single Transaction 
        //7 – ECI Indicator – Channel Encrypted Transaction
        public const string ECommerceFlag = "7";
    }
}
