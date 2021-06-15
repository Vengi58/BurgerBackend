using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace BurgerBackend.Services
{
    public class AWSs3ImageService : IImageSerce
    {
        private readonly string AwsBucketName;
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Client;

        public AWSs3ImageService(string awsAccessKeyId, string awsSecretAccessKey, string awsBucketName)
        {
            s3Client = new AmazonS3Client(awsAccessKeyId, awsSecretAccessKey, bucketRegion);
            AwsBucketName = awsBucketName;
        }

        public IEnumerable<string> GetImageIDs(string restaurantName)
        {
            var list = s3Client.ListObjectsAsync(AwsBucketName, restaurantName).Result;
            var files = list.S3Objects.Select(x => x.Key);
            return files.Select(x => Path.GetFileName(x)).ToList();
        }

        public MemoryStream GetImageOfRestaurant(string restaurantName, string imageID)
        {
            GetObjectRequest request = new GetObjectRequest
            {
                BucketName = AwsBucketName,
                Key = restaurantName + "/" + imageID
            };
            MemoryStream memoryStream = new();
            using (GetObjectResponse response = s3Client.GetObjectAsync(request).Result)
            using (Stream responseStream = response.ResponseStream)
            {
                CopyStream(responseStream, memoryStream);
            }
            return memoryStream;
        }

        public IEnumerable<MemoryStream> GetImages(string restaurantName)
        {
            foreach (var img in GetImageIDs(restaurantName))
            {
                yield return GetImageOfRestaurant(restaurantName, img);
            }
        }

        public void UploadImage(string restaurantName, IFormFile file)
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = AwsBucketName,
                Key = restaurantName + "/" + file.FileName,
                InputStream = file.OpenReadStream()
            };

            putRequest.Metadata.Add("x-amz-meta-title", "someTitle");
            _ = s3Client.PutObjectAsync(putRequest).Result;
        }

        private void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[16 * 1024 * 1024];
            int read;
            while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, read);
            }
            output.Position = 0;
        }
    }
}
