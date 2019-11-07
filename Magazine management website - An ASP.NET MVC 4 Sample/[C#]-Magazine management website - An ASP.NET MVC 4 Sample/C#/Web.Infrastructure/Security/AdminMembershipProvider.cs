namespace CIK.News.Web.Infras.Security
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using CIK.News.Entities.UserAgg;
    using CIK.News.Framework.Encyption;
    using CIK.News.Framework.Encyption.Impl;
    using CIK.News.Mapping;

    using WebMatrix.WebData;

    public class CustomAdminMembershipProvider : SimpleMembershipProvider
    {
        // TODO: will do a better way
        private const string SELECT_ALL_USER_SCRIPT = "select * from [dbo].[User]private where UserName = '{0}'";

        private readonly IEncrypting _encryptor;

        private readonly SimpleSecurityContext _simpleSecurityContext;

        public CustomAdminMembershipProvider() : this(new SimpleSecurityContext())
        {
        }

        public CustomAdminMembershipProvider(SimpleSecurityContext simpleSecurityContext)
            : this(new Encryptor(), new SimpleSecurityContext("DefaultDb"))
        {
        }

        public CustomAdminMembershipProvider(IEncrypting encryptor, SimpleSecurityContext simpleSecurityContext)
        {
            _encryptor = encryptor;
            _simpleSecurityContext = simpleSecurityContext;
        }

        public override bool ValidateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException("Argument cannot be null or empty", "username");
            }

            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException("Argument cannot be null or empty", "password");
            }

            var hash = _encryptor.Encode(password);

            var users = _simpleSecurityContext.Users.SqlQuery(string.Format(SELECT_ALL_USER_SCRIPT, username));

            if (users == null && !users.Any())
            {
                return false;
            }

            var firstOrDefault  = users.FirstOrDefault();

            return firstOrDefault != null 
                && String.Compare(firstOrDefault.Password, hash, StringComparison.Ordinal) == 0;
        }
    }

    public class SimpleSecurityContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public SimpleSecurityContext()
            : this("DefaultDb")
        {
        }

        public SimpleSecurityContext(string connStringName) :
            base(connStringName)
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Configurations.Add(new UserMapping());
        }
    }
}