namespace MyCompany.Travel.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MyCompany.Travel.Model;
    using System.Data.Entity;
    using System.Threading.Tasks;

    /// <summary>
    /// The travel Request repository implementation
    /// </summary>
    public class TravelRequestRepository : ITravelRequestRepository
    {
        private readonly MyCompanyContext _context;

        /// <summary>
        /// Creates a new instance of TravelRequestRepository class
        /// </summary>
        /// <param name="context">The EF context</param>
        public TravelRequestRepository(MyCompanyContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            _context = context;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="travelRequestId"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see travelRequestId="MyCompany.Travel.Data.ITravelRequestRepository"/></returns>
        public async Task<TravelRequest> GetAsync(int travelRequestId)
        {
            return await _context.TravelRequests
                    .FindAsync(travelRequestId);
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="travelRequestId"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see travelRequestId="MyCompany.Travel.Data.ITravelRequestRepository"/></returns>
        public async Task<TravelRequest> GetWithEmployeeInfoAsync(int travelRequestId)
        {
            var travelRequests = await _context.TravelRequests
                .Where(t => t.TravelRequestId == travelRequestId)
                .Select(t => new
                {
                    TravelRequest = t,
                    Employee = t.Employee,
                })
                .ToListAsync();

            var result = travelRequests.Select(t => BuildTravelRequest(t.TravelRequest))
                .FirstOrDefault();

            return result;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="travelRequestId"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<TravelRequest> GetCompleteInfoAsync(int travelRequestId, PictureType pictureType)
        {
            var travelRequests = await _context.TravelRequests
                .Where(t => t.TravelRequestId == travelRequestId)
                .Select(t => new
                {
                    TravelRequest = t,
                    Employee = t.Employee,
                    TravelAttachments = t.TravelAttachments,
                    EmployeePictures = t.Employee.EmployeePictures.Where(ep => ep.PictureType == pictureType)
                })
                .ToListAsync();

            var result = travelRequests.Select(t => BuildTravelRequest(t.TravelRequest))
                .FirstOrDefault();

            return result;
        }


        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<IEnumerable<TravelRequest>> GetAllAsync()
        {
            return await _context.TravelRequests.ToListAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="employeeIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<IEnumerable<TravelRequest>> GetUserTravelRequestsAsync(string employeeIdentity, string filter, int status, int pageSize, int pageCount)
        {
            var travelRequests = await _context.TravelRequests
                .Where(q => q.Employee.Email == employeeIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter)
                                || q.From.Contains(filter)
                                || q.To.Contains(filter)
                                || q.Name.Contains(filter)
                                || q.Description.Contains(filter)
                                || q.Comments.Contains(filter)
                                || q.AccommodationNeed.Contains(filter)
                                || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter)
                                || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            )
                .OrderByDescending(q => q.Depart)
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .Select(t => new
                {
                    TravelRequest = t,
                    Employee = t.Employee,

                })
                .ToListAsync();

            var result = travelRequests.Select(t => BuildTravelRequest(t.TravelRequest))
                .ToList();

            return result;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="employeeIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<IEnumerable<TravelRequest>> GetNotFinishedUserTravelRequestsAsync(string employeeIdentity, string filter, int status)
        {
            var travelRequests = await _context.TravelRequests
                .Where(q => q.Employee.Email == employeeIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            && q.Return >= DateTime.UtcNow
                            )
                .OrderByDescending(q => q.Depart)
                .Select(t => new
                {
                    TravelRequest = t,
                    Employee = t.Employee,

                })
                .ToListAsync();

            return travelRequests.Select(t => BuildTravelRequest(t.TravelRequest)).ToList();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="employeeIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<int> GetUserCountAsync(string employeeIdentity, string filter, int status)
        {
            return await _context.TravelRequests
                    .Where(q => q.Employee.Email == employeeIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            )
                .CountAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<IEnumerable<TravelRequest>> GetTeamTravelRequestsAsync(string managerIdentity, string filter, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            var results = await _context.TravelRequests
                 .Where(q => q.Employee.Team.Manager.Email == managerIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            )
                .OrderByDescending(q => q.Depart)
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .Select(t => new
                {
                    TravelRequest = t,
                    Employee = t.Employee,
                    EmployeePictures = t.Employee.EmployeePictures.Where(ep => ep.PictureType == pictureType)

                })
                .ToListAsync();

            return results.Select(t => BuildTravelRequest(t.TravelRequest))
            .ToList();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<IEnumerable<TravelRequest>> GetNotFinishedTeamTravelRequestsAsync(string managerIdentity, string filter, int status, PictureType pictureType)
        {
            var results = await _context.TravelRequests
                 .Where(q => q.Employee.Team.Manager.Email == managerIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            && q.Return >= DateTime.UtcNow
                            )
                .OrderByDescending(q => q.Depart)
                .Select(t => new
                {
                    TravelRequest = t,
                    Employee = t.Employee,
                    EmployeePictures = t.Employee.EmployeePictures.Where(ep => ep.PictureType == pictureType)

                })
                .ToListAsync();

            return results.Select(t => BuildTravelRequest(t.TravelRequest))
            .ToList();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pictureType"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pageSize"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="pageCount"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<IEnumerable<TravelRequest>> GetAllTravelRequestsAsync(string filter, int status, PictureType pictureType, int pageSize, int pageCount)
        {
            var results = await _context.TravelRequests
                 .Where(q => ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            )
                .OrderByDescending(q => q.Depart)
                .Skip(pageSize * pageCount)
                .Take(pageSize)
                .Select(t => new
                {
                    TravelRequest = t,
                    Employee = t.Employee,
                    EmployeePictures = t.Employee.EmployeePictures.Where(ep => ep.PictureType == pictureType)

                })
                .ToListAsync();

            return results.Select(t => BuildTravelRequest(t.TravelRequest))
            .ToList(); ;
        }



        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="maxPicturesPerCity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<IEnumerable<TravelDistribution>> GetTeamTravelDistributionAsync(string managerIdentity, int maxPicturesPerCity)
        {
            DateTime sinceDate = DateTime.UtcNow.AddYears(-1);
            var results = await _context.TravelRequests
                .Include(t => t.Employee)
                 .Where(q => q.Employee.Team.Manager.Email == managerIdentity
                            && q.Depart >= sinceDate
                            && (q.Status == TravelRequestStatus.Completed || q.Status == TravelRequestStatus.Approved)
                       )
                .OrderByDescending(q => q.Depart)
                .GroupBy(q => q.To.ToLower())
                .ToListAsync();

            int grandTotal = results.Sum(g => g.Count());

            List<TravelDistribution> travelDistribution =
                results.Select(r => new TravelDistribution()
                {
                    City = r.Key,
                    YearCount = r.Count(),
                    MonthCount = r.Count(t => t.Depart >= DateTime.UtcNow.AddMonths(-1)),
                    Percent = Math.Round((decimal)r.Count() * 100 / (decimal)grandTotal, 0),
                    EmployeesPictures = GetPictures(r.Select(t => t.EmployeeId).Distinct().Take(maxPicturesPerCity).ToList(), PictureType.Small)
                }).ToList();


            return travelDistribution;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public int GetTeamCount(string managerIdentity, string filter, int status)
        {
            return _context.TravelRequests
                 .Where(q => q.Employee.Team.Manager.Email == managerIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            )
                .Count();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="managerIdentity"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<int> GetTeamCountAsync(string managerIdentity, string filter, int status)
        {
            return await _context.TravelRequests
                 .Where(q => q.Employee.Team.Manager.Email == managerIdentity
                            &&
                            ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            )
                .CountAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="filter"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <param name="status"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<int> GetAllCountAsync(string filter, int status)
        {
            return await _context.TravelRequests
                 .Where(q => ((int)q.Status & status) == (int)q.Status
                            &&
                            (String.IsNullOrEmpty(filter) || q.From.Contains(filter) || q.To.Contains(filter) || q.Name.Contains(filter) || q.Description.Contains(filter)
                                || q.Comments.Contains(filter) || q.AccommodationNeed.Contains(filter) || q.RelatedProject.Contains(filter)
                                || q.Employee.FirstName.Contains(filter) || q.Employee.LastName.Contains(filter)
                                || (q.Employee.FirstName + " " + q.Employee.LastName).Contains(filter))
                            )
                .CountAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="travelRequest"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        /// <returns><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></returns>
        public async Task<int> AddAsync(TravelRequest travelRequest)
        {
            travelRequest.CreationDate = DateTime.UtcNow;
            travelRequest.LastModifiedDate = DateTime.UtcNow;
            travelRequest.Employee = null;

            _context.TravelRequests.Add(travelRequest);
            await _context.SaveChangesAsync();
                       
            return travelRequest.TravelRequestId;
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="travelRequest"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        public async Task UpdateAsync(TravelRequest travelRequest)
        {
            _context.Entry<TravelRequest>(travelRequest)
                .State = EntityState.Modified;

            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// <see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/>
        /// </summary>
        /// <param name="travelRequestId"><see cref="MyCompany.Travel.Data.Repositories.ITravelRequestRepository"/></param>
        public async Task DeleteAsync(int travelRequestId)
        {
            var travelRequest = await _context.TravelRequests.FindAsync(travelRequestId);
            if (travelRequest != null)
            {
                _context.TravelRequests.Remove(travelRequest);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Dispose all resources
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }

        /// <summary>
        /// Dispose all resource
        /// </summary>
        /// <param name="disposing">Dispose managed resources check</param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            GC.SuppressFinalize(this);
        }
        TravelRequest BuildTravelRequest(TravelRequest travelRequest)
        {
            return new TravelRequest()
            {
                TravelRequestId = travelRequest.TravelRequestId,
                Name = travelRequest.Name,
                Description = travelRequest.Description,
                From = travelRequest.From,
                To = travelRequest.To,
                Depart = travelRequest.Depart,
                Return = travelRequest.Return,
                CreationDate = travelRequest.CreationDate,
                LastModifiedDate = travelRequest.LastModifiedDate,
                AccommodationNeed = travelRequest.AccommodationNeed,
                TransportationNeed = travelRequest.TransportationNeed,
                Comments = travelRequest.Comments,
                RelatedProject = travelRequest.RelatedProject,
                Status = travelRequest.Status,
                TravelType = travelRequest.TravelType,
                EmployeeId = travelRequest.EmployeeId,
                TravelAttachments = travelRequest.TravelAttachments != null ?
                        travelRequest.TravelAttachments
                        .Select(ta => new TravelAttachment()
                        {
                            Name = ta.Name,
                            FileName = ta.FileName,
                            TravelAttachmentId = ta.TravelAttachmentId,
                            TravelRequestId = ta.TravelRequestId
                        }).ToList()
                        : null,
                Employee = new Employee()
                {
                    EmployeeId = travelRequest.Employee.EmployeeId,
                    FirstName = travelRequest.Employee.FirstName,
                    LastName = travelRequest.Employee.LastName,
                    Email = travelRequest.Employee.Email,
                    TeamId = travelRequest.Employee.TeamId,
                    JobTitle = travelRequest.Employee.JobTitle,
                    EmployeePictures = travelRequest.Employee.EmployeePictures != null ?
                        travelRequest.Employee.EmployeePictures
                        .Select(e => new EmployeePicture()
                        {
                            Employee = null,
                            PictureType = e.PictureType,
                            EmployeePictureId = e.EmployeePictureId,
                            EmployeeId = e.EmployeeId,
                            Content = e.Content
                        }).ToList()
                        : null,
                },
            };
        }

        private List<EmployeePicture> GetPictures(List<int> employeeIds, PictureType pictureType)
        {
            return _context.EmployeePictures
                            .Where(e => employeeIds.Contains(e.EmployeeId) && e.PictureType == pictureType)
                            .ToList()
                            .Select(e => new EmployeePicture()
                            {
                                Employee = null,
                                PictureType = e.PictureType,
                                EmployeePictureId = e.EmployeePictureId,
                                EmployeeId = e.EmployeeId,
                                Content = e.Content
                            }).ToList();
        }
    }
}