using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeItUpData.Library.DataAccess;
using TimeItUpData.Library.Models;

namespace TimeItUpData.Library.Repositories
{
    public class SplitRepository : ISplitRepository
    {
        private readonly EFDbContext _context;

        public SplitRepository(EFDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Split>> GetAllSplitsAsync()
        {
            return await _context.Splits.ToListAsync();
        }

        public async Task<ICollection<Split>> GetAllActiveSplitsAsync()
        {
            return await _context.Splits.Where(z => z.StartAt != DateTime.MinValue && z.EndAt == DateTime.MinValue).ToListAsync();
        }

        public async Task<ICollection<Split>> GetAllPastSplitsAsync()
        {
            return await _context.Splits.Where(z => z.StartAt != DateTime.MinValue && z.EndAt != DateTime.MinValue).ToListAsync();
        }

        public async Task<Split> GetSplitByIdAsync(int id)
        {
            return await _context.Splits.Where(z => z.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ICollection<Split>> GetSplitsByIdsAsync(ICollection<int> idSet)
        {
            return await _context.Splits.Where(z => idSet.Contains(z.Id)).ToListAsync();
        }

        public void RemoveSplit(Split split)
        {
            _context.Splits.Remove(split);
        }

        public async Task AddSplitAsync(Split split)
        {
            await _context.Splits.AddAsync(split);
        }

        public bool CheckIfSplitExist(int id)
        {
            return _context.Splits.Any(z => z.Id == id);
        }
    }
}
