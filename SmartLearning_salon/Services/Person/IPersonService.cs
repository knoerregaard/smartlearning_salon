using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLearning_salon.Services.Person
{
    public interface IPersonService
    {
        Task AddItemAsync(Models.Person person);
        Task DeleteItemAsync(string id);
        Task<Models.Person> GetItemAsync(string id);
    }
}
