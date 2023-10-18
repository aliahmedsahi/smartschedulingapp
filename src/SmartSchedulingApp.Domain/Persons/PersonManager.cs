using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace SmartSchedulingApp.Doctors
{
    public class PersonManager : DomainService
    {
        public async Task<Person> CreateAsync(
            [NotNull] Guid userId,
            [NotNull] string personType,
            [NotNull] int age,
            [NotNull] string gender,
            [NotNull] string otherProperty
            )
        {
            var id = GuidGenerator.Create();
            Person person = personType switch
            {
                "Doctor" => new Doctor(id, userId, gender, age, otherProperty),
                "Staff" => new Staff(id, userId, gender, age, otherProperty),
                "Manager" => new Manager(id, userId, gender, age, otherProperty),
                _ => throw new InvalidOperationException($"Invalid person type: {personType}")
            };

            return person;
        }
    }
}
