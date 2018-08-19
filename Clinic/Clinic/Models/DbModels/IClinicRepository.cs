using Clinic.Models.ClinicModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.DbModels
{
    interface IClinicRepository<T> : IDisposable where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T GetOne(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }
}
