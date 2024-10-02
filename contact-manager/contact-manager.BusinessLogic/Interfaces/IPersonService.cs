using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using contact_manager.Infrastructure.Models;
using Microsoft.AspNetCore.Http;

namespace contact_manager.BusinessLogic.Interfaces
{
    public interface IPersonService
    {
        Task UploadCsvAsync(IFormFile file);
        Task<IEnumerable<Person>> GetPeopleAsync();
    }
}
