using CrudADO.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CrudADO.DAL
{
    public class PersonDAL : BaseDAL
    {
        public PersonDAL(IConfiguration conf):base(conf){}

        private const string SelectCommand = "SELECT Id, Name, Gender, BirthDate, Ethnicity from Persons";

        public List<Person> GetList() {
            var result = new List<Person>();
            var persons = ExecuteReader(SelectCommand);
            if (persons != null) {
                foreach (var currentPerson in persons)
                {
                    Person person = new Person();
                    person.Id = Convert.ToInt32(currentPerson.Id);
                    person.Name = currentPerson.Name;
                    person.Gender = (Gender) Convert.ToInt32(currentPerson.Gender);
                    person.Ethnicity = (Ethnicity)Convert.ToInt32(currentPerson.Ethnicity);
                    person.BirthDate = Convert.ToDateTime(currentPerson.BirthDate);
                    result.Add(person);
                }
            }
            return result;
        }

        public Person GetDetails(int Id) {
            var person = ExecuteReader($"{SelectCommand} WHERE Id = @Id", new SqlParameter("Id",Id));
            if (person != null) {
                Person result = new Person();
                result.Id = Id;
                result.Name = person[0].Name;
                result.Gender = (Gender) Convert.ToInt32(person[0].Gender);
                result.Ethnicity = (Ethnicity)Convert.ToInt32(person[0].Ethnicity);
                result.BirthDate = Convert.ToDateTime(person[0].BirthDate);
                return result;
            }
            return null;
        }

        public void Insert(Person person) {
            person.Valid();

            string InsertCommand = "INSERT INTO Persons (Name,Gender,BirthDate,Ethnicity)VALUES(@Name,@Gender,@BirthDate,@Eithnicity)";
            ExecuteNonQuery(InsertCommand,new List<SqlParameter>() { 
                new SqlParameter("Name",person.Name),
                new SqlParameter("Gender",person.Gender),
                new SqlParameter("BirthDate",person.BirthDate),
                new SqlParameter("Ethnicity",person.Ethnicity),
            });
        }

        public void Update(Person person) {
            if (person == null || person.Id == 0)
                throw new CustomMessageException("Não foi possível identificar a pessoa");
            person.Valid();

            string InsertCommand = "UPDATE Persons Name=@Name, Gender=@Gender,BirthDate=@BirthDate,Ethnicity=@Ethnicity WHERE Id = @Id";
            ExecuteNonQuery(InsertCommand,new List<SqlParameter>() { 
                new SqlParameter("Id",person.Id),
                new SqlParameter("Name",person.Name),
                new SqlParameter("Gender",person.Gender),
                new SqlParameter("BirthDate",person.BirthDate),
                new SqlParameter("Ethnicity",person.Ethnicity),
            });
        }
        public void Delete(int IdPerson) =>
            ExecuteNonQuery("DELETE FROM Persons WHERE Id = @Id", new SqlParameter("Id", IdPerson));
    }
}
