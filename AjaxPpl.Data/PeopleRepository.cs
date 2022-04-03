using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace AjaxPpl.Data
{
   public  class PeopleRepository
    {
        private string _connectionstring;
        public PeopleRepository(string connect)
        {
            _connectionstring = connect;
        }


        public List<Person> GetPeople()
        {
            using var conn = new SqlConnection(_connectionstring);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "select * from people";
            conn.Open();
            var reader = cmd.ExecuteReader();
            List<Person> ppl = new();
            while (reader.Read())
            {
                ppl.Add(new Person
                {
                    Id = (int)reader["Id"],
                    FirstName = (string)reader["FirstName"],
                    LastName = (string)reader["LastName"],
                    Age = (int)reader["Age"],

                });
            }

            return ppl;

        }
            
        public void Add(Person person)
        {
            using var conn = new SqlConnection(_connectionstring);
            using var cmd = conn.CreateCommand();
            cmd.CommandText = "insert into people (FirstName, LastName, Age) values (@first, @last, @age)";
           
            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            conn.Open();
            cmd.ExecuteNonQuery();
        }

        public void UpdatePerson(Person person)
        {
            using SqlConnection conn = new SqlConnection(_connectionstring);
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"update people
                           set FirstName = @first, LastName = @last, Age= @age where Id = @Id";

            cmd.Parameters.AddWithValue("@first", person.FirstName);
            cmd.Parameters.AddWithValue("@last", person.LastName);
            cmd.Parameters.AddWithValue("@age", person.Age);
            cmd.Parameters.AddWithValue("@Id", person.Id);
            conn.Open();
            cmd.ExecuteNonQuery();
        }
        public void Delete(int personid)
        {
            using SqlConnection conn = new SqlConnection(_connectionstring);
            using SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = @"delete from people where Id= @Id";
            cmd.Parameters.AddWithValue("@Id", personid);
            conn.Open();
            cmd.ExecuteNonQuery();

        }
    }
}
