using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using IO.Swagger.Model;

namespace TestJsonPolymorphism
{
    class Program
    {
        static HttpClient client = new HttpClient();
        static void Main(string[] args)
        {
            RunAsAsync().GetAwaiter().GetResult();
        }

        static async Task RunAsAsync()
        {
            client.BaseAddress = new Uri("https://localhost:44335");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                Person person = new Person()
                {
                    Id = 1,
                    FullName = "John Smith",
                    Pet = new Dog()
                    {
                        Name = "Gentle",
                        PetType = "Dog",
                        Bark = "Bark"
                    }
                };
                Person response = await UpdatePersonAsync(person);
                if (person.Pet is Dog)
                {
                    Console.WriteLine("Received a person with pet Dog");
                }
                else if (person.Pet is Cat)
                {
                    Console.WriteLine("Received a person with pet Cat");
                }
                else if (person.Pet is BasePet)
                {
                    Console.WriteLine("Received a person with pet BasePet");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);
                }
            }
            Console.ReadLine();
        }

        static async Task<Person> UpdatePersonAsync(Person person)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync("person/update", person);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsAsync<Person>();
        }
    }
}
