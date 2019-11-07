namespace CIK.News.Entities.UserAgg
{
    public class User : Entity
    {
        public virtual string UserName { get; set; }

        public virtual string DisplayName { get; set; }

        public virtual string Password { get; set; }

        public virtual string Email { get; set; }

        public int Role { get; set; }

        public Role RoleEnum
        {
            get { return (Role) this.Role; }
        }
    }
}