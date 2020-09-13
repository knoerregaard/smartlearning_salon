using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using SmartLearning_salon.Models;

using Azure.Storage;
using Azure.Storage.Blobs;
using System.Collections;

namespace SmartLearning_salon.Services.BlobStorage
{
    public class BlobStorage : IBlobStorage
    {
        private BlobContainerClient bcc;
        public BlobStorage(
           BlobContainerClient bcc, 
           string ContainerId)
        {
            this.bcc = bcc;

        }
        public async Task CreateBlobAsync(Stream stream, string filename)
        {
            try
            {
                //Opret hvis ikkke eksisterer
                await bcc.CreateIfNotExistsAsync();

                await bcc.UploadBlobAsync(filename, stream);
            }
            catch (Exception)
            {

                throw;
            }

        }
        public Uri GetBlobUri(string id)
        {
            return bcc.Uri;
            //var blob = bcc.GetBlobClient("smartfiles").Uri;
            //return blob;
        }
    }
}
