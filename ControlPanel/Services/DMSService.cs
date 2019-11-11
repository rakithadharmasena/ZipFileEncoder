using ControlPanel.Configuration;
using ControlPanel.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ControlPanel.Services
{
    public interface IDMSService
    {
        Task<StatusDTO> PostFile(string PostParams,string username,string password);
    }

    public class DMSService : IDMSService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IEncryptionService _encryptionService;
        private Config _config;

        public DMSService(IHttpClientFactory clientFactory, IOptions<Config> configAccessor, IEncryptionService encryptionService)
        {
            _clientFactory = clientFactory;
            _config = configAccessor.Value;
            _encryptionService = encryptionService;
        }
        

        public async Task<StatusDTO> PostFile(string file, string username, string password)
        {
            //encrypt file
            byte[] encryptedText = _encryptionService.Encrypt(file);
            //add the file to request body
            var dic = new Dictionary<string, byte[]>
            {
                { "Content" , encryptedText }
            };
            var jsonContent = JsonConvert.SerializeObject(dic);
            var body = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            //create client
            string URI = _config.DMS.BaseURL + "/" + _config.DMS.FileMethod;
            var client = _clientFactory.CreateClient();
            //add authentication headers
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.ASCII.GetBytes(string.Format("{0}:{1}", username, password))));

            try
            {
                var response = await client.PostAsync(URI, body);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    return new StatusDTO{
                        IsSuccess = false,
                        Message = "Invalid Username or Password"
                    };
                }

                var msg = await response.Content.ReadAsStringAsync();
                var jsonObj = JsonConvert.DeserializeAnonymousType(msg, new { Message = string.Empty , FileId = string.Empty});

                if (!string.IsNullOrEmpty(jsonObj.FileId))
                {
                    return new StatusDTO
                    {
                        IsSuccess = true,
                        Message = string.Concat("Message from Data Management Service : ", jsonObj.Message,
                        string.Concat("File id : ", jsonObj.FileId))
                    };
                }
                else
                {
                    return new StatusDTO
                    {
                        IsSuccess = false,
                        Message = jsonObj.Message
                    };
                }
            }
            catch (Exception)
            {
                return new StatusDTO
                {
                    IsSuccess = false,
                    Message = "Error connecting to Data Management Services"
                };
            }
            
        }
    }
}
