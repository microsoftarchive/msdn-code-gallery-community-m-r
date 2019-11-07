using System;
using System.Collections.Generic;
using System.Linq;
using MyEvents.Api.Models;
using MyEvents.Model;

namespace MyEvents.Api.Mappers
{
    /// <summary>
    /// Maps the event definition into the the schedule view model.
    /// </summary>
    public class EventDefinitionToScheduleViewModelMapper
    {
        /// <summary>
        /// Maps the event definition into the the schedule view model.
        /// </summary>
        /// <param name="registeredUserId">Id user of the caller</param>
        /// <param name="eventDefinition"></param>
        /// <returns></returns>
        public Schedule Map(int registeredUserId, EventDefinition eventDefinition)
        {
            var times = GetTimes(eventDefinition);

            List<ScheduleSession> scheduleSessions =
                eventDefinition
                    .Sessions.Select(s => GetScheduleSession(registeredUserId, s)).ToList();

            return GetViewModel(eventDefinition, times, scheduleSessions); ;
        }

        private ScheduleSession GetScheduleSession(int registeredUserId, Session session)
        {
            return new ScheduleSession
                {
                    SessionId = session.SessionId,
                    EventDefinitionId = session.EventDefinitionId,
                    Title = session.Title,
                    StartTime = session.StartTime,
                    Duration = session.Duration,
                    TimeZoneOffset = session.TimeZoneOffset,
                    RoomNumber = session.RoomNumber,
                    Speaker = session.Speaker,
                    Description = session.Description,
                    IsFavorite = session.SessionRegisteredUsers.Any(sr => sr.RegisteredUserId == registeredUserId),
                    UserScore = session.SessionRegisteredUsers.Any(sr => sr.RegisteredUserId == registeredUserId) ?
                    session.SessionRegisteredUsers.First(sr => sr.RegisteredUserId == registeredUserId).Score : 0,
                    SessionRegisteredUsers = session.SessionRegisteredUsers != null ? session.SessionRegisteredUsers.Select(ru => new MyEvents.Api.Models.SessionRegisteredUser()
                    {
                        SessionRegisteredUserId = ru.SessionRegisteredUserId,
                        SessionId = ru.SessionId,
                        RegisteredUserId = ru.RegisteredUserId,
                        Score = ru.Score,
                        Rated = ru.Rated,
                        FacebookId = ru.FacebookId
                    }) : null
                };
        }

        private static Schedule GetViewModel(EventDefinition eventDefinition, List<DateTime> times, List<ScheduleSession> scheduleSessions)
        {
            var scheduleEvent = new ScheduleEvent
            {
                EventDefinitionId = eventDefinition.EventDefinitionId,
                Rooms = eventDefinition.RoomNumber,
                Sessions = scheduleSessions
            };

            var viewModel = new Schedule
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
                times.Add(currentTime);
                currentTime = currentTime.AddHours(1);
            } while (currentTime < eventDefinition.EndTime);

            return times;
        }
    }
}