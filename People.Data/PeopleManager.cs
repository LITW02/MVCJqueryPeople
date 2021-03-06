﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People.Data
{
    public class PeopleManager
    {
        private string _connectionString;

        public PeopleManager(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IEnumerable<Person> GetAll()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM People";
                List<Person> people = new List<Person>();
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = new Person();
                    person.Id = (int)reader["Id"];
                    person.FirstName = (string)reader["FirstName"];
                    person.LastName = (string)reader["LastName"];
                    person.Age = (int)reader["Age"];
                    people.Add(person);
                }

                return people;
            }
        }

        public IEnumerable<Person> SearchByLastName(string lastName)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM People WHERE LastName LIKE @lastName";
                cmd.Parameters.AddWithValue("@lastName", "%" + lastName + "%");
                List<Person> people = new List<Person>();
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Person person = new Person();
                    person.Id = (int)reader["Id"];
                    person.FirstName = (string)reader["FirstName"];
                    person.LastName = (string)reader["LastName"];
                    person.Age = (int)reader["Age"];
                    people.Add(person);
                }

                return people;
            }
        } 

        public void Add(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO People (FirstName, LastName, Age) VALUES " +
                                  "(@firstName, @lastName, @age)";
                cmd.Parameters.AddWithValue("@firstName", person.FirstName);
                cmd.Parameters.AddWithValue("@lastName", person.LastName);
                cmd.Parameters.AddWithValue("@age", person.Age);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void Delete(int personId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "DELETE FROM People WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", personId);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public Person GetById(int personId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand cmd = connection.CreateCommand();
                cmd.CommandText = "SELECT * FROM People WHERE Id = @id";
                cmd.Parameters.AddWithValue("@id", personId);
                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                reader.Read();
                Person person = new Person();
                person.Id = (int)reader["Id"];
                person.FirstName = (string)reader["FirstName"];
                person.LastName = (string)reader["LastName"];
                person.Age = (int)reader["Age"];
                return person;
            }
        }

        public void Update(Person person)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                SqlCommand command = connection.CreateCommand();
                command.CommandText =
                    "UPDATE People SET FirstName = @firstName, LastName = @lastName, Age = @age WHERE Id = @id";
                command.Parameters.AddWithValue("@firstName", person.FirstName);
                command.Parameters.AddWithValue("@lastName", person.LastName);
                command.Parameters.AddWithValue("@age", person.Age);
                command.Parameters.AddWithValue("@id", person.Id);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
