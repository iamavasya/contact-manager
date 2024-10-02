using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using contact_manager.Infrastructure.Models;

namespace contact_manager.Infrastructure.Interfaces
{
    public interface IPersonRepository
    {
        Task AddPeopleAsync(IEnumerable<Person> person);
        Task<IEnumerable<Person>> GetPeopleAsync();
    }
}
