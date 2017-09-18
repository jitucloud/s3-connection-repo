using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;

namespace S3.Example
{
    class S3DemoService
    {
        private AmazonS3Client GetAmazonS3Client()
        {
            return new AmazonS3Client(RegionEndpoint.APSoutheast2);
        }


        public void RunBucketCreationDemo()
        {
            using (IAmazonS3 s3Client = GetAmazonS3Client())
            {
                try
                {
                    PutBucketRequest putBucketRequest = new PutBucketRequest();
                    String newBucketName = "jitu-test3";
                    putBucketRequest.BucketName = newBucketName;
                    PutBucketResponse putBucketResponse = s3Client.PutBucket(putBucketRequest);
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket creation has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }

        public void RunBucketListingDemo()
        {
            using (IAmazonS3 s3Client = GetAmazonS3Client())
            {
                try
                {
                    ListBucketsResponse response = s3Client.ListBuckets();
                    List<S3Bucket> buckets = response.Buckets;
                    foreach (S3Bucket bucket in buckets)
                    {
                        Console.WriteLine("Found bucket name {0} created at {1}", bucket.BucketName, bucket.CreationDate);
                    }
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket listing has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }

        public void ListBucketContent()
        {
            using (IAmazonS3 s3Client = GetAmazonS3Client())
            {
                try
                {
                    ListObjectsRequest request = new ListObjectsRequest();
                    request.BucketName = "jitu-test1";
                    ListObjectsResponse response = s3Client.ListObjects(request);
                    foreach (S3Object o in response.S3Objects)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", o.Key, o.Size, o.LastModified);
                    }
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket listing has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }

        }


        public void DownloadObjecttoFileFromBucket()
        {
            using (IAmazonS3 s3Client = GetAmazonS3Client())
            {
                try
                {
                    GetObjectRequest request = new GetObjectRequest();
                    request.BucketName = "jitu-test1";
                    request.Key = "logigng.log";
                    GetObjectResponse response = s3Client.GetObject(request);
                    response.WriteResponseStreamToFile("D:\\jitu.txt");
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket listing has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }

        }

        public void UploadSingleFileToBucket()
        {
            using (IAmazonS3 s3Client = GetAmazonS3Client())
            {
                try
                {
                    TransferUtility utility = new TransferUtility(s3Client);
                    utility.Upload("D:\\aa.txt", "jitu-test2");
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket listing has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }

        }

        public void UploadDirectoryToBucket()
        {
            using (IAmazonS3 s3Client = GetAmazonS3Client())
            {
                try
                {
                    
                    TransferUtility utility = new TransferUtility(s3Client);
                    utility.UploadDirectory("D:\\test", "jitu-test2");
                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket listing has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }
        }


        public void UploadAllTxtFileFromDirectoryToBucket()
        {
            using (IAmazonS3 s3Client = GetAmazonS3Client())
            {
                try
                {
                    TransferUtility utility = new TransferUtility(s3Client);

                    // 3. Same as 2 and some optional configuration 
                    //    Search recursively for .txt files to upload).
                    TransferUtilityUploadDirectoryRequest request =
                        new TransferUtilityUploadDirectoryRequest
                        { 
                            BucketName = "jitu-test3",
                            Directory = "D:\\test",
                            SearchOption = SearchOption.AllDirectories,
                            SearchPattern = "*.txt"
                        };

                    utility.UploadDirectory(request);

                }
                catch (AmazonS3Exception e)
                {
                    Console.WriteLine("Bucket listing has failed.");
                    Console.WriteLine("Amazon error code: {0}",
                        string.IsNullOrEmpty(e.ErrorCode) ? "None" : e.ErrorCode);
                    Console.WriteLine("Exception message: {0}", e.Message);
                }
            }

        }
    }
}
