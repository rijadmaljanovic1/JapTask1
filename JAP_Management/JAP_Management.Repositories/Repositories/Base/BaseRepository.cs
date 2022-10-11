using JAP_Management.Infrastructure.Database;
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.Base
{
    public class BaseRepository<T> : IBaseRepository<T>where T:class
    {
        protected readonly DatabaseContext _context;
        public BaseRepository(DatabaseContext context)
        {
            _context = context;
        }

        //Add
        public async Task<T> Add(T entity)
        {
            _context.Set<T>().Add(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        //Update
        public async Task<T> Update(T entity) 
        {
            _context.Set<T>().Update(entity);

            await _context.SaveChangesAsync();

            return entity;
        }
       
        //Delete
        public async Task<T> Delete(string id) 
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return entity;
            }


            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

        //Delete
        public async Task<T> DeleteSelection(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);

            if (entity == null)
            {
                return entity;
            }


            _context.Set<T>().Remove(entity);

            await _context.SaveChangesAsync();

            return entity;
        }

    }
}
