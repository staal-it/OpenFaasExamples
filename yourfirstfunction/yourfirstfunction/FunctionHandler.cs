using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Function
{
    public class FunctionHandler
    {
        public string Handle(string input) {
            var person = new Person { Name = "dummy", Age = -1 };
            try {
                person = JsonConvert.DeserializeObject<Person>(input);
            } catch (Exception){
                Console.WriteLine("We're expecting valid json.");
                Console.WriteLine("Example: { 'name': 'Erwin', 'age': 42 }");
            }

            // Connect to sql and add person...
            var sqlUrl = Environment.GetEnvironmentVariable("database_url");

            string sqlPassword = ReadSecret("database-password");

            Console.WriteLine($"Connecting to {sqlUrl} using 'admin' and {sqlPassword}");

            PrintEnvironmentVariables();

            return $"You've added {person.Name} ({person.Age}) to the database\n";
        }

        private static string ReadSecret(string secretName)
        {
            var secret = string.Empty;

            try {
                secret = File.ReadAllText($"/var/openfaas/secrets/{secretName}").Trim();
            }
            catch (Exception)
            {
                Console.WriteLine($"Secret {secretName} could not be read");
            }

            return secret;
        }

        private static void PrintEnvironmentVariables()
        {
            foreach (DictionaryEntry de in Environment.GetEnvironmentVariables())
                Console.WriteLine("  {0} = {1}", de.Key, de.Value);
        }
    }

    public class Person
    {
        public string Name { get; set; }

        public int Age { get; set; }
    }
}
