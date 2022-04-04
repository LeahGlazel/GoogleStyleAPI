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
        public async Task<IActionResult> GetData(string searchText, int start)
        {
            var res = await repository.GetData(searchText, start);
            if (res != null)
                return Ok(res);
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetCountData(string searchText)
        {
            var res = await repository.GetCountData(searchText);
            if (res != null)
                return Ok(res);
            return NotFound();
        }
    }
}
