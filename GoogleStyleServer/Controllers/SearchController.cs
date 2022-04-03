using Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;

namespace GoogleStyleServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private ISearchRepository repository;
        public SearchController(ISearchRepository _repository)
        {
            repository = _repository;  
        }
        [HttpGet]
        public async Task<List<DataFormat>> getData(string searchText, int start)
        {
            return await repository.GetData(searchText, start);
        }
    }
}
