using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MvcApplication2.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Age { get; set; }
    }

    public static class ReaderExtensions
    {
        public static T GetValue<T>(this SqlDataReader reader, string columnName)
        {
            object val = reader[columnName];
            if (val != DBNull.Value)
            {
                return (T)val;
            }

            return default(T);
        }
    }

    public class PeopleDb
    {
        private readonly string _connectionString;

        public PeopleDb(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            var result = new List<Person>();
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People";
                connection.Open();
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    result.Add(GetFromReader(reader));
                }

                return result;
            }
        }

        public void AddPerson(string firstName, string lastName, int? age)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "INSERT INTO People VALUES (@firstName, @lastName, @age)";
                cmd.Parameters.AddWithValue("@firstName", firstName);
                cmd.Parameters.AddWithValue("@lastName", lastName);
                cmd.Parameters.AddWithValue("@age", (object)age ?? DBNull.Value);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Person GetById(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM People WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                var reader = cmd.ExecuteReader();
                reader.Read();
                return GetFromReader(reader);
            }
        }

        public void Update(Person p)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE Id = @id";
                cmd.Parameters.AddWithValue("@firstName", p.FirstName);
                cmd.Parameters.AddWithValue("@lastName", p.LastName);
                cmd.Parameters.AddWithValue("@age", (object)p.Age ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@id", p.Id);

                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "DELETE FROM People WHERE ID = @id";
                cmd.Parameters.AddWithValue("@id", id);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        private Person GetFromReader(SqlDataReader reader)
        {
            Person p = new Person();
            p.Id = reader.GetValue<int>("Id");
            p.Age = reader.GetValue<int?>("Age");
            p.FirstName = reader.GetValue<string>("FirstName");
            p.LastName = reader.GetValue<string>("LastName");

            return p;
        }
    }
}