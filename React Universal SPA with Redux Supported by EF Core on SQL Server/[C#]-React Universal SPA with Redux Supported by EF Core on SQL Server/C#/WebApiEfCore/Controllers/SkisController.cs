using System;
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
    public class SkisController: ControllerBase
    {
        private readonly ISkisRepo _skisRepo;
        private readonly IReviewRepo _reviewRepo;

        public SkisController(ISkisRepo skisRepo, IReviewRepo reviewRepo)
        {
            _skisRepo = skisRepo;
            _reviewRepo = reviewRepo;
        }

        // GET api/Skis/getSkis/{styleId}
        [HttpGet("getSkis/{styleId}")]
        public ActionResult<SkisDTO> GetSkis(int styleId)
        {
            var result = _skisRepo.GetSkis(styleId);

            return Ok(result);
        }

        // GET api/Skis/getStyleBasic/{styleId}
        [HttpGet("getStyleBasic/{styleId}")]
        public ActionResult<StyleBasicDTO> GetStyleBasic(int styleId)
        {
            var result = _skisRepo.GetStyleBasic(styleId);

            return Ok(result);
        }

        // GET api/Skis/getSpecs/{styleId}
        [HttpGet("getSpecs/{styleId}")]
        public ActionResult<IQueryable<spSpec>>  GetSpecs(int styleId)
        {
            var results = _skisRepo.GetSpecs(styleId);
            
            return Ok(results);
        }

        // GET api/Skis/getReviews/{styleId}
        [HttpGet("getReviews/{styleId}")]
        public ActionResult<ReviewsDTO> GetReviews(int styleId)
        {
            var result = _skisRepo.GetReviews(styleId);

            return Ok(result);
        }

        // POST api/Skis/addReview
        [HttpPost("addReview")]
        [Authorize]
        public ActionResult<ReviewAddReturnDTO> AddReview(Review review)
        {
            try
            {
                var result = _reviewRepo.AddReview(review);

                return Ok(result);
            }
            catch (Exception e)
            {
                // TODO log e
                Console.WriteLine(e);

                return new BadRequestResult();
            }

        }

    }
}
