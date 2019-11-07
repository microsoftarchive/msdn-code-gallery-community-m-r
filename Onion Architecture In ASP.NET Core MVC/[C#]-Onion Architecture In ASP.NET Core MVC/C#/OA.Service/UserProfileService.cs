using OA.Data;
using OA.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OA.Service
{
    public class UserProfileService: IUserProfileService
    {
        private IRepository<UserProfile> userProfileRepository;

        public UserProfileService(IRepository<UserProfile> userProfileRepository)
        {           
            this.userProfileRepository = userProfileRepository;
        }

        public UserProfile GetUserProfile(long id)
        {
            return userProfileRepository.Get(id);
        }
    }
}
