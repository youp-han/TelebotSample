using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using TeleBotService.Model;
using TeleBotService.Repository;

namespace TeleBotService.ControlCenter
{
    public class LottoRepository : iLottoRepository
    {
        private static RestCall _restCall = new RestCall();
        private static string _uri = ConfigurationSettings.AppSettings["Url_address"];

        string ReceiveData(string endPoint)
        {
            return _restCall.RestClientComm(_uri, endPoint, "GET");
        }



        public string GetLastestLottNumber()
        {
            var result = ReceiveData("GetLatestLottoNumber");

            JObject JData = JObject.Parse(result);
            string success = JData["result"].ToString();

            if (JData != null && JData["result"].ToString().Equals("success"))
            {
                JObject JsonData = JObject.Parse(JData["data"].ToString());

                success =
                    JsonData["drawDate"] + ", "
                                         + JsonData["seqNo"] + " 회차 당첨번호는 " + Environment.NewLine
                                         + JsonData["num1"] + ", "
                                         + JsonData["num2"] + ", "
                                         + JsonData["num3"] + ", "
                                         + JsonData["num4"] + ", "
                                         + JsonData["num5"] + ", "
                                         + JsonData["num6"] + ", 보너스 번호 "
                                         + JsonData["bonus"] + " 입니다." + Environment.NewLine;
            }

            return success;
        }


        public string GetLastestFirstPrice()
        {
            var result = ReceiveData("GetLatestPriceMoney");

            JObject JData = JObject.Parse(result);
            string success = JData["result"].ToString();

            if (JData != null && JData["result"].ToString().Equals("success"))
            {
                JObject JsonData = JObject.Parse(JData["data"].ToString());

                success =
                    JsonData["drawDate"] + "에는, 총"
                                         + JsonData["firstPriceSelected"] + " 명이 당첨되었습니다. " + Environment.NewLine
                                         + JsonData["firstPriceTotal"] + " 원의 총 상금 중, 각자, "
                                         + JsonData["eachReceivedFirstPrice"] + " 원씩 받았습니다. 정말 억수로 받았네요. :) " + Environment.NewLine;
            }

            return success;
        }


        public string GetRecommendedNumbers()
        {
            var result = ReceiveData("GetRecommendedNumbers");

            JObject JData = JObject.Parse(result);
            string success = JData["result"].ToString();

            if (JData != null && JData["result"].ToString().Equals("success"))
            {
                JObject JsonData = JObject.Parse(JData["data"].ToString());

                success = " 추천 당첨번호는 " + Environment.NewLine
                                       + JsonData["num1"] + ", "
                                       + JsonData["num2"] + ", "
                                       + JsonData["num3"] + ", "
                                       + JsonData["num4"] + ", "
                                       + JsonData["num5"] + ", "
                                       + JsonData["num6"] + ", 보너스 번호 "
                                       + JsonData["bonus"] + " 입니다." + Environment.NewLine;
            }

            return success;
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
