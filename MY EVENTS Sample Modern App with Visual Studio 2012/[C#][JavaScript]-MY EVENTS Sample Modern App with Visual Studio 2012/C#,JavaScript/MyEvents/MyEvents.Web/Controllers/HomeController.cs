using System;
using System.Collections.Generic;
using System.Web.Mvc;
using MyEvents.Data;
using MyEvents.Model;
using MyEvents.Web.Models;

namespace MyEvents.Web.Controllers
{
    /// <summary>
    /// Home Controller.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IEventDefinitionRepository eventsRepository;
        private const int PageSize = 10;

        /// <summary>
        /// Home controller constructor.
        /// </summary>
        public HomeController(IEventDefinitionRepository eventsRepository)
        {
            if (eventsRepository == null)
                throw new ArgumentNullException("eventsRepository");

            this.eventsRepository = eventsRepository;
        }

        /// <summary>
        /// Index action.
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int? pageIndex)
        {
            pageIndex = pageIndex ?? 0;
            IEnumerable<EventDefinition> events = this.eventsRepository.GetAll(PageSize, pageIndex.Value);
            int eventsCount = this.eventsRepository.GetCount();
            var viewModel = new HomeViewModel(events, eventsCount, pageIndex.Value, PageSize);
            
            ActionResult result = Request.IsAjaxRequest() ? (ActionResult)PartialView("EventList", viewModel) : View(viewModel);

            return result;
        }

     
    }
}
