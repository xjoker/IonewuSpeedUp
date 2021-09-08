using System;
using CommandLine;
using RestSharp;

namespace ionewuSpeedUp
{
    internal class Program
    {
        /// <summary>
        ///     提速地址
        /// </summary>
        private const string SpeedUpUrl = "https://kdts.ionewu.com/c/open";

        /// <summary>
        ///     状态查询地址
        /// </summary>
        private const string SpeedUpStatusUrl = "https://kdts.ionewu.com/c/status";

        private static void Main(string[] args)
        {
            string uid = null;
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(o => { uid = o.Uid; });

            if (string.IsNullOrEmpty(uid))
            {
                Console.WriteLine("UID未提供");
                return;
            }


            var openClient = new RestClient(SpeedUpUrl);
            var openRequest = new RestRequest(Method.POST);
            openRequest.AddJsonBody($"{{\"uid\":\"{uid}\",\"appid\":\"\",\"sid\":104,\"pid\":\"\",\"type\":2}}");
            var openResponse = openClient.Execute(openRequest);
            Console.WriteLine($"{DateTime.Now}\t提速结果:\t{openResponse.Content}");

            var statusClient = new RestClient(SpeedUpStatusUrl);
            var statusRequest = new RestRequest(Method.POST);
            statusRequest.AddJsonBody($"{{\"uid\":\"{uid}\",\"appid\":\"\",\"sid\":104,\"pid\":\"\",\"type\":2}}");
            var statusResponse = statusClient.Execute(statusRequest);
            Console.WriteLine($"{DateTime.Now}\t账号信息:\t{statusResponse.Content}");
        }

        public class Options
        {
            [Option('u', "uid", Required = true, HelpText = "微信小程序右上角ID")]
            public string Uid { get; set; }
        }
    }
}