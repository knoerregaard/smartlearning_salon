using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLearning_salon.Services.BlobStorage
{
    public interface IBlobStorage
    {

        Task CreateBlobAsync(Stream stream, string filename);


    }
}
