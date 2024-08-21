using LabCourse.DbContexts;
using LabCourse.Entities;
using LabCourse.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LabCourse.Repositories
{
    public class StokuRepository : IStokuRepository
    {

        private readonly StokuDbContext _context;

        public StokuRepository(StokuDbContext context) {
            _context = context;
        }


        public async Task<IEnumerable<Stoku>> GetStocksAsync()
        {
            return await _context.Stoqet.OrderBy(s => s.Id).ToListAsync();
        }

        public async Task<Stoku?> GetStockByIdAsync(int id)
        {
            return await _context.Stoqet.FirstOrDefaultAsync(s => s.Id == id);
        }


        public async Task<bool> StockExistsAsync(int id)
        {
            return await _context.Stoqet.AnyAsync(s => s.Id == id);
        }

        public async Task<Stoku> AddStockAsync(Stoku stokuDto)
        {
            var exists = await StockExistsAsync(stokuDto.Id);

            if (exists)
            {
                throw new Exception("The stock with that ID already exists!");
            }
            _context.Stoqet.Add(stokuDto);
            await _context.SaveChangesAsync();

            return stokuDto;
        }


         public async Task<Stoku> UpdateStockAsync(Stoku stokuDtoForUpdate, int id)
        {
            var existingStock = await _context.Stoqet.FindAsync(stokuDtoForUpdate.Id);

            if (existingStock == null)
            {
                throw new Exception("Stock not found.");
            }

            _context.Entry(existingStock).CurrentValues.SetValues(stokuDtoForUpdate);
            await _context.SaveChangesAsync();

            return existingStock;
        }

         public void DeleteStocks(Stoku stokuDto)
        {
            _context.Stoqet.Remove(stokuDto);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

       

    }
}
