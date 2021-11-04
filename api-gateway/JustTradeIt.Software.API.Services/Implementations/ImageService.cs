using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.Internal;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Net.Http.Headers;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly IConfiguration _configuration;

        private readonly string imgurl = "https://tradebucketo.s3.eu-west-1.amazonaws.com/";


        public ImageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadImageToBucket(string email, IFormFile image)
        {
            var awsconfig = _configuration.GetSection("Aws");
            var bucketName = awsconfig.GetSection("BucketName").Value;
            var KeyId = awsconfig.GetSection("KeyId").Value;
            var keySecret = awsconfig.GetSection("KeySecret").Value;
            IAmazonS3 client = new AmazonS3Client(KeyId, keySecret, RegionEndpoint.EUWest1);
            var keyName = "";
            byte[] fileBytes = new Byte[image.Length];
            image.OpenReadStream().Read(fileBytes, 0, Int32.Parse(image.Length.ToString()));

            
            
            using (Stream fileToUpload = new MemoryStream(fileBytes))
            {
                var putObjectRequest = new PutObjectRequest
                {
                    BucketName = bucketName,
                    Key = image.FileName,
                    InputStream = fileToUpload,
                    ContentType = image.ContentType,
                    CannedACL = S3CannedACL.PublicRead
                    
                    
                };

                var response = await client.PutObjectAsync(putObjectRequest);
                Console.WriteLine(response.ToString());
                return imgurl + image.FileName;
            }
        }
    }
}