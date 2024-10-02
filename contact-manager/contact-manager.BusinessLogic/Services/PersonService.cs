using contact_manager.BusinessLogic.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using contact_manager.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using contact_manager.Infrastructure.Models;
using contact_manager.BusinessLogic.Dtos;
using System.Globalization;
using CsvHelper;

namespace contact_manager.BusinessLogic.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task UploadCsvAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File is empty");
            }

            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
                var personDtos = csvReader.GetRecords<PersonDto>().ToList();
                var people = personDtos.Select(dto => new Person
                {
                    Name = dto.Name,
                    DateOfBirth = dto.DateOfBirth,
                    Married = dto.Married,
                    Phone = dto.Phone,
                    Salary = dto.Salary
                }).ToList();

                await _personRepository.AddPeopleAsync(people);
            }
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            return await _personRepository.GetPeopleAsync();
        }

    }
}
