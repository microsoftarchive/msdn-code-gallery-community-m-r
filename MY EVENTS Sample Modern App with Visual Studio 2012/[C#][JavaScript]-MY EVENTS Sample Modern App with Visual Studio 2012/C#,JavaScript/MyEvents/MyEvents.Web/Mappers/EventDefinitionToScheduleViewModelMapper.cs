using System;
using System.Collections.Generic;
using System.Linq;
using MyEvents.Model;
using MyEvents.Web.Areas.Organizer.Models;

namespace MyEvents.Web.Mappers
{
    /// <summary>
    /// Event definition to schedule view model mapper.
    /// </summary>
    public class EventDefinitionToScheduleViewModelMapper
    {
        /// <summary>
        /// Maps the event definition into the the schedule view model.
        /// </summary>
        /// <param name="eventDefinition"></param>
        /// <returns></returns>
        public ScheduleViewModel Map(EventDefinition eventDefinition)
        {
            var times = GetTimes(eventDefinition);

            List<ScheduleSession> scheduleSessions =
                eventDefinition
                    .Sessions.Select(s =>
                                     new ScheduleSession
                                     {
                                         SessionId = s.SessionId,
                                         Title = s.Title,
                                         StartTime = DateTime.SpecifyKind(s.StartTime, DateTimeKind.Utc),
                                         Duration = s.Duration,
                                         RoomNumber = s.RoomNumber,
                                         Speaker = s.Speaker
                                     }).ToList();

            var scheduleEvent = new ScheduleEvent
                                    {
                                        EventDefinitionId = eventDefinition.EventDefinitionId,
                                        Rooms = eventDefinition.RoomNumber,
                                        Sessions = scheduleSessions
                                    };

            var viewModel = new ScheduleViewModel
            {
                EventDefinition = scheduleEvent,
                Times = times
            };
            return viewModel;
        }

        private List<DateTime> GetTimes(EventDefinition eventDefinition)
        {
            var times = new List<DateTime>();

            DateTime currentTime = eventDefinition.StartTime;

            do
            {
                times.Add(DateTime.SpecifyKind(currentTime, DateTimeKind.Utc));
                currentTime = currentTime.AddHours(1);
            } while (currentTime < eventDefinition.EndTime);

            return times;
        }
    }
}