using SmartSchedulingApp.Persons;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace SmartSchedulingApp.Doctors
{
    public class PersonAppService : ApplicationService, IPersonAppService
    {
        private readonly IPersonRepository _personRepository;
        public PersonAppService(
            IPersonRepository personRepository
            )
        {
            _personRepository = personRepository;
        }
        
        /// <summary>
        /// Preferring Business Layer over Domain Layer and directly querying the data through
        /// Data Access Layer, reduces complexity and avoids unnecessary code in this scenario.
        /// In this case we do not need to access the Person object from DAL, because we are only 
        /// reading the data
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<PersonDto> GetByUserIdAsync(Guid id)
        {
            var person = await _personRepository.GetByUserIdAsync(id);
            return ObjectMapper.Map<Person, PersonDto>(person);
        }
    }
}
