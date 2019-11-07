using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;

namespace Visitors
{
    /// <summary>
    /// Summary description for VisitorsController
    /// </summary>
    public class VisitorsController : Controller
    {
        private readonly VisitorContext _context = null;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="visitorRepository">IVisitorRepository dependency</param>
        public VisitorsController([NotNull]VisitorContext visitorRepository)
        {
            _context = visitorRepository;
        }

        /// <summary>
        /// GetCount enforcing Auth
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        //  [Route("count")]
        //  [Route("~/noauth/api/visitors/count")]
        public JsonResult Count(string filter)
        {
            return Json(_context.Visitors.Count(q => String.IsNullOrEmpty(filter) ||
                                                q.FirstName.Contains(filter) ||
                                                q.LastName.Contains(filter) ||
                                                (q.FirstName + " " + q.LastName).Contains(filter) ||
                                                q.Company.Contains(filter)));
        }

        /// <summary>
        /// Get Visitors
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="pictureType">PictureType</param>
        /// <param name="pageSize">Size of page</param>
        /// <param name="pageCount">Size Count</param>
        /// <returns>List of visitors</returns>
       // [Route("")]
      //  [Route("~/noauth/api/visitors")]
        public JsonResult Get(string filter, PictureType pictureType, int pageSize, int pageCount)
        {
	   
 		//PORTNOTE: pulling all images into memory. EF will do fixup on query but include doesn't work
            var visitorPictures = _context.VisitorPictures.ToList();
	    var visitors = _context.Visitors.ToList();

            var results = visitors
                .Where(q =>
                    String.IsNullOrEmpty(filter) ||
                    q.FirstName.ToLower().Contains(filter.ToLower()))
                .Select(v => new
                {
                      Visitor = v,
                      VisitorPictures = v.VisitorPictures.Where(vp => vp.PictureType == (int)pictureType),
                })
                .OrderBy(v => v.Visitor.FirstName)
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .ToList();

            var result = results.Select(v => BuildVisitor(v.Visitor));

            return Json(result.ToList());
           
        }

        private static Visitor BuildVisitor(Visitor visitor)
        {
            //the idea is remove reference for improve 
            //client work without $ref
            var created = new Visitor
            {
                VisitorId = visitor.VisitorId,
                FirstName = visitor.FirstName,
                LastName = visitor.LastName,
                Company = visitor.Company,
                Email = visitor.Email,
                CreatedDateTime = visitor.CreatedDateTime,
                LastModifiedDateTime = visitor.LastModifiedDateTime,
                PersonalId = visitor.PersonalId,
                Position = visitor.Position,
                VisitorPictures = (visitor.VisitorPictures != null) ? visitor.VisitorPictures.Select(vp => new VisitorPicture()
                {
                    VisitorPictureId = vp.VisitorPictureId,
                    Content = vp.Content,
                    PictureType = vp.PictureType,
                    VisitorId = vp.VisitorId
                }).ToList()
                : null,
            };

            return created;
        }
    }
}