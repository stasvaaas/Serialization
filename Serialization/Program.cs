using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;

namespace Serialization
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person { FirstName = "Alex", LastName = "Alexov", Age = 18, Weight = 99.9 };
            Converter(person);
        }

        public static void Converter(Person person)
        {
            //1.to JSON
            string json = JsonConvert.SerializeObject(person);
            Console.WriteLine(json);

            //2.ToString Binary
            IFormatter formatter = new BinaryFormatter();
            byte[] serializedData;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                formatter.Serialize(memoryStream, person);
                serializedData = memoryStream.ToArray();
                Console.WriteLine(BitConverter.ToString(serializedData).Replace("-", ""));
            }
            Person pers;
            using (MemoryStream memoryStream = new MemoryStream(serializedData))
            {
                pers = (Person)formatter.Deserialize(memoryStream);
                Console.WriteLine($"{pers.Age} {pers.LastName} {pers.FirstName}");
            }

            //3. Binary to JSON
            string des = JsonConvert.SerializeObject(pers);
            Console.WriteLine(des);

            //4.JSON to Person
            Person deserializedPerson = JsonConvert.DeserializeObject<Person>(des);
        }
    }
}