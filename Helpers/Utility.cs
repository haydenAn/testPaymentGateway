using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;

namespace DaystarPaymentGateway
{
    public static partial class Utility
    {
        private static readonly Logger Logger = LogManager.GetLogger("NLogWriter");

        public static bool ConvertAccountNumber(string accountNumber, out long partnerAccountNumber)
        {
            var isNumber = long.TryParse(accountNumber, out partnerAccountNumber);
            return isNumber;
        }

        public static string ConvertToJSON(object serializableObject)
        {
            return PrettifyJSON(JsonConvert.SerializeObject(serializableObject));
        }

        public static string PrettifyJSON(string json)
        {
            using (var stringReader = new StringReader(json))
            using (var stringWriter = new StringWriter())
            {
                var jsonReader = new JsonTextReader(stringReader);
                var jsonWriter = new JsonTextWriter(stringWriter) { Formatting = Formatting.Indented };
                jsonWriter.WriteToken(jsonReader);
                return stringWriter.ToString();
            }
        }

        public static string MaskAccountNumber(string json)
        {
            if (json.IndexOf("<AccountNum>", StringComparison.Ordinal) < json.IndexOf(@"</AccountNum>", StringComparison.Ordinal) - 16)
                json = json.Replace(json.Substring(json.IndexOf("<AccountNum>", StringComparison.Ordinal) + 12, 12), "***");

            return json;
        }

        public static void PrintTimeline(List<DateTime> timeline, string methodName)
        {
            DateTime? previousTimestamp = null;
            var timelapse = $"{methodName} Timeline:";
            var counter = 0;

            foreach (var timestamp in timeline)
            {
                if (counter == 0)
                {
                    previousTimestamp = timestamp;
                    counter++;
                    continue;
                }

                var ticks = timestamp - previousTimestamp;

                timelapse += $"{Environment.NewLine}\t\t\t\t\t\t\t\t\t Checkpoint {counter}: {ticks?.TotalMilliseconds} ms";

                previousTimestamp = timestamp;

                counter++;
            }

            Logger.Info(timelapse);
        }
    }
}
