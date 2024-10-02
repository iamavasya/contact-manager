using contact_manager.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using contact_manager.Infrastructure.Data;
using contact_manager.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace contact_manager.Infrastructure.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly ManagerDbContext _context;

        public PersonRepository(ManagerDbContext context)
        {
            _context = context;
        }

        public async Task AddPeopleAsync(IEnumerable<Person> people)
        {
            _context.People.AddRange(people);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Person>> GetPeopleAsync()
        {
            return await _context.People.ToListAsync();
        }
    }
}
