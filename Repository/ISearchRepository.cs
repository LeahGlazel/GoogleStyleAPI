using Entities;

namespace Repository
{
    public interface ISearchRepository
    {
        Task<List<DataFormat>> GetData(string searchText, int start);
        Task<long?> GetCountData(string searchText);
    }
}