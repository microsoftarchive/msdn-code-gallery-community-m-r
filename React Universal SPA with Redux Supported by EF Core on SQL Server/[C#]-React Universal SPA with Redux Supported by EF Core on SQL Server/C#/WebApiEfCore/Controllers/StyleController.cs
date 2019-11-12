using DataAccessEfCore.DTOs;
using DataAccessEfCore.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace WebApiEfCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StyleController: ControllerBase
    {
        private readonly IStyleRepo _styleRepo;

        public StyleController(IStyleRepo styleRepo)
        {
            _styleRepo = styleRepo;
        }

        // GET api/Style/getStylesFiltered
        [HttpGet("getStylesFiltered")]
        public ActionResult<StylesFilteredDTO> GetStylesFiltered([FromQuery] StylesFilterDTO filter)
        {
            var result = _styleRepo.FilterStyles(filter);

            return Ok(result);
        }
    }
}
