using Clinic.Models.ClinicModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinic.Models.DbModels
{
    public class SQLRepository<T> : IClinicRepository<T> where T : BaseEntity
    {
        private readonly ClinicContext _db;

        public SQLRepository(ClinicContext clinicContext)
        {
           _db = clinicContext;
        }

        public IEnumerable<T> GetAll()
        {
            return _db.Set<T>();
        }

        public T GetOne(int id)
        {
            return _db.Set<T>().FirstOrDefault(e => e.Id == id);
        }

        public void Create(T item)
        {
            _db.Set<T>().Add(item);
        }

        public void Update(T item)
        {
            _db.Entry<T>(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            T item = GetOne(id);
            if (item != null)
                _db.Set<T>().Remove(item);
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
