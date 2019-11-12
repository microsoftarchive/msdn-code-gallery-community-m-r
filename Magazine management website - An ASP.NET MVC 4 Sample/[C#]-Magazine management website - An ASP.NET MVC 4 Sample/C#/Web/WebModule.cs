namespace CIK.News.Web
{
    using Autofac;

    using CIK.News.Framework.Configurations;
    using CIK.News.Framework.Encyption.Impl;
    using CIK.News.Web.App_Start;
    using CIK.News.Web.Infras;
    using CIK.News.Web.Infras.MediaItem;
    using CIK.News.Web.Infras.Repository;
    using CIK.News.Web.Infras.ViewModels.Admin.Persistences;

    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterInstance(new MyDbContext("DefaultDb")).As<MyDbContext>().SingleInstance();

            builder.RegisterType<CategoryRepository>().AsImplementedInterfaces();
            builder.RegisterType<ItemRepository>().AsImplementedInterfaces();
            builder.RegisterType<UserRepository>().AsImplementedInterfaces();

            builder.RegisterType<ItemCreatingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ItemDeletingPersistence>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<ItemEditingPersistence>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<ExConfigurationManager>().AsImplementedInterfaces().SingleInstance();
            builder.RegisterType<MediaItemStorage>().AsImplementedInterfaces().SingleInstance();

            builder.RegisterType<Encryptor>().AsImplementedInterfaces().SingleInstance();

            MappingConfig.Configure();
        }
    }
}