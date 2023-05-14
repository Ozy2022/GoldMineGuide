using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.SQS;
using Amazon.SQS.Model;
using Microsoft.Extensions.Configuration;
using GoldMineGuide.Models;
using System.IO;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace GoldMineGuide.Controllers
{


    public class ConfirmationMessage : Controller
    {

        private const string QueueName = "DDACQueue";

        private List<string> getInfo()
        {
            List<string> values = new List<string>();

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");
            IConfigurationRoot configure = builder.Build();

            values.Add(configure["Values:Value1"]);
            values.Add(configure["Values:Value2"]);
            values.Add(configure["Values:Value3"]);

            return values; //

        }

        public async Task<IActionResult> Index(string msg = "")
        {
            List<string> Values = getInfo();
            AmazonSQSClient sqsClient = new AmazonSQSClient(Values[0], Values[1], Values[2], RegionEndpoint.USEast1);

            //get the queue URL
            var response = await sqsClient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });
            GetQueueAttributesRequest NumberRequest = new GetQueueAttributesRequest();
            NumberRequest.QueueUrl = response.QueueUrl;
            NumberRequest.AttributeNames.Add("ApproximateNumberOfMessages");

            GetQueueAttributesResponse response1 = await sqsClient.GetQueueAttributesAsync(NumberRequest);
            ViewBag.messageCount = response1.ApproximateNumberOfMessages;

            ViewBag.msg = msg;
            return View();
        }//
        
        [ValidateAntiForgeryToken]

        [Authorize(Roles = "Staff")]
        public async Task<IActionResult> sendEmailMessage( string methodname, string processtype, DateTime mpDate)
        {

            List<string> values = getInfo();
            AmazonSQSClient sqsClient = new AmazonSQSClient(values[0], values[1], values[2], RegionEndpoint.USEast1);

            ManagerData StuffInfo1 = new ManagerData
            {
                MethodInfoName = methodname,
                ProcessType = processtype,
                MiningProducedDate = mpDate

            };

            try
            {
                SendMessageRequest sendRequst = new SendMessageRequest
                {
                    MessageBody = JsonConvert.SerializeObject(StuffInfo1)
                };

                var response = await sqsClient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });
                sendRequst.QueueUrl = response.QueueUrl;

                await sqsClient.SendMessageAsync(sendRequst);

            }
            catch (AmazonSQSException ex)
            {
                return RedirectToAction("Index", "ConfirmationMessage", new { msg = "There is an Error. Message was not send! " + ex.Message });
            }

            return RedirectToAction("Index", "ConfirmationMessage", new { msg = "Mining gold type info to our Manger is sent!" });

        }


        //[Authorize(Roles = "Manager")]
        public async Task<IActionResult> ViewMessage()
        {
            List<string> values = getInfo();
            AmazonSQSClient sqsClient = new AmazonSQSClient(values[0], values[1], values[2], RegionEndpoint.USEast1);
            var response = await sqsClient.GetQueueUrlAsync(new GetQueueUrlRequest { QueueName = QueueName });

            List<KeyValuePair<ManagerData, string>> message = new List<KeyValuePair<ManagerData, string>>();

            try
            {
                ReceiveMessageRequest receiveRequest = new ReceiveMessageRequest()
                {
                    QueueUrl = response.QueueUrl,
                    WaitTimeSeconds = 10,
                    MaxNumberOfMessages = 10,
                    VisibilityTimeout = 20
                };
                ReceiveMessageResponse returnResponse = await sqsClient.ReceiveMessageAsync(receiveRequest);

                if (returnResponse.Messages.Count <= 0)
                {
                    return RedirectToAction("Index", "ConfirmationMessage", new { msg = "There is no data in Table" });
                }

                for (int i = 0; i < returnResponse.Messages.Count; i++)
                {
                    ManagerData MineGold1 = JsonConvert.DeserializeObject<ManagerData>(returnResponse.Messages[i].Body);
                    message.Add(new KeyValuePair<ManagerData, string>(MineGold1, returnResponse.Messages[i].ReceiptHandle));
                }

            }
            catch (AmazonSQSException ex)
            {
                return RedirectToAction("Index", "ConfirmationMessage", new {msg = ex.Message });
            }

           

            return View(message);
        }


    }
}
