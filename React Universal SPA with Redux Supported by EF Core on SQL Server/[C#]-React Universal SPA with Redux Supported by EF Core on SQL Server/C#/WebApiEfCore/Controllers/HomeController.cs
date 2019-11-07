using System.Linq;
using DataAccessEfCore.DbModels;
using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class HomeController : ControllerBase
    {
        private readonly IStyleRepo _styleRepo;
        private readonly IGeneralRepo<Category> _categoryRepo;

        public HomeController(IStyleRepo styleRepo, IGeneralRepo<Category> categoryRepo)
        {
            _styleRepo = styleRepo;
            _categoryRepo = categoryRepo;
        }

        // GET api/Home/getHomePage
        [HttpGet("getHomePage")]
        public ActionResult<InitialHomePageDTO> GetHomePage()
        {
            var result = new InitialHomePageDTO
            {
                Categories = _categoryRepo.GetAll(),
                Populars = _styleRepo.GetPopulars(),
                Clearances = _styleRepo.GetClearances()
            };

            return Ok(result);
        }

        // GET api/Home/getCategories
        [HttpGet("getCategories")]
        public ActionResult<IQueryable<Category>>  GetCategories()
        {
            var result = _categoryRepo.GetAll();

            return Ok(result);
        }

        // GET api/Home/getStylesPolularClearance
        [HttpGet("getStylesPolularClearance")]
        public ActionResult<StylesPopularClearanceDTO> GetStylesPolularClearance()
        {
            var result = new StylesPopularClearanceDTO()
            {
                Polulars = _styleRepo.GetPopulars(),
                Clearances = _styleRepo.GetClearances()
            };

            return Ok(result);
        }

    }
}