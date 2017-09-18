using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;


namespace S3.Example
{
    class Program
    {

        static void Main(string[] args)
        {
            S3DemoService demoService = new S3DemoService();


            // demoService.RunBucketCreationDemo();

            // demoService.RunBucketListingDemo();

            //demoService.ListBucketContent();

            demoService.UploadAllTxtFileFromDirectoryToBucket();

            Console.WriteLine("Bucket Demo Done...");
            Console.ReadKey();
        }
    }
}
