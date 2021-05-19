using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudADO.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Ethnicity Ethnicity { get; set; }

        public void Valid() {
            if (string.IsNullOrWhiteSpace(this.Name))
                throw new CustomMessageException("O nome informado não é válido!");
            if (this.BirthDate == DateTime.MinValue)
                throw new CustomMessageException("Data de nascimento inválida!");
            if (this.Gender == 0 && string.IsNullOrWhiteSpace(((Gender) Gender).ToString()))
                throw new CustomMessageException("Não foi possível identificar o gênero!");
            if (this.Ethnicity == 0 && string.IsNullOrWhiteSpace(((Ethnicity)Ethnicity).ToString()))
                throw new CustomMessageException("Não foi possível identificar a etnia!");
        }
    }
}
