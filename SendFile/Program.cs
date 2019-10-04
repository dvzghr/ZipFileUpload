using System;
using System.IO;
using System.IO.Compression;
using RestSharp;

namespace SendFile
{
    class Program
    {
        static void Main(string[] args)
        {
            SendStream();
        }
        private static void SendStream()
        {
            while (true)
            {
                Console.WriteLine("Press to upload...");
                Console.ReadKey();
                var client = new RestClient("http://localhost:8080/api");
                var req = new RestRequest("upload", Method.POST);

                using (var stream = new MemoryStream())
                {
                    using (var zip = new ZipArchive(stream, ZipArchiveMode.Create, true))
                        zip.CreateEntryFromFile("log.txt", "log.txt");

                    //ReceiveStream(stream);

                    var fileParameter = FileParameter.Create("some_name", stream.ToArray(), "upload.zip", "application/zip");
                    req.Files.Add(fileParameter);
                    var resp = client.Post(req);
                    Console.WriteLine(resp.Content);
                    
                    Console.WriteLine("Upload finished!");
                }
            }
        }

        private static void ReceiveStream(Stream stream)
        {
            using (var zip = new ZipArchive(stream, ZipArchiveMode.Read))
            {
                foreach (var entry in zip.Entries)
                {
                    Console.WriteLine(entry.Name);
                }
            }
        }
    }
}