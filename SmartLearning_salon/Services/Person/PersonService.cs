using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using SmartLearning_salon.Models;

namespace SmartLearning_salon.Services.Person
{
    public class PersonService : Controller, IPersonService
    {
        private readonly Container container;

        public PersonService(
            CosmosClient client, 
            string DatabaseId, 
            string ContainerId)
        {
            this.container = client.GetContainer(DatabaseId, ContainerId);
        }
        public async Task AddItemAsync(Models.Person person)
        {
            await container.CreateItemAsync<Models.Person>(person, new PartitionKey(person.Id));
        }
        public async Task DeleteItemAsync(string id)
        {
            await container.DeleteItemAsync<Models.Person>(id, new PartitionKey(id));
        }

        public async Task<Models.Person> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<Models.Person> response = await container.ReadItemAsync<Models.Person>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw;
            }
        }
        public async Task UpdateItem(Models.Person person)
        {
            try
            {
                await container.UpsertItemAsync<Models.Person>(person, new PartitionKey(person.Id));
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                throw;
            }
        }
    }
}
