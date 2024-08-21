using LabCourse.Entities;

namespace LabCourse.Interfaces
{
    public interface IStokuRepository
    {
        Task<IEnumerable<Stoku>> GetStocksAsync();
        Task<Stoku?> GetStockByIdAsync(int id);

        Task<bool> StockExistsAsync(int id);

        Task<Stoku> AddStockAsync(Stoku stokuDto);

        void DeleteStocks(Stoku stokuDto);

        Task<Stoku> UpdateStockAsync(Stoku UpdateStokuDto, int id);

        Task<bool> SaveChangesAsync();


    }
}
