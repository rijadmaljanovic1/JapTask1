using JAP_Management.Core.Entities;
using JAP_Management.Core.Models;
using JAP_Management.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JAP_Management.Repositories.Repositories.Ranks
{
    public class RankRepository : IRankRepository
    {
        private readonly DatabaseContext _databaseContext;
        public RankRepository(DatabaseContext databaseContext) 
        {
            _databaseContext = databaseContext;
        }

        public  List<Rank> GetRanks()
        {
            try
            {
                return _databaseContext.Ranks
                   .FromSqlRaw("dbo.spSelectionsSuccess")
                   .AsNoTracking().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error -> ", ex);
                return null;
            }
        }
    }
}
