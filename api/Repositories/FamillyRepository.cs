using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Interfaces;
using api.Models;

namespace api.Repositories
{
    public class FamillyRepository : IFamillyRepository
    {
        private readonly ApplicationDbContext _context;

        public FamillyRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<FamillyData> CreateAsync(FamillyData familly)
        {
            await _context.FamillyDatas.AddAsync(familly);
            await _context.SaveChangesAsync();

            return familly;
        }
    }
}