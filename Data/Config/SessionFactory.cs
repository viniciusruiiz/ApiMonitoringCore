using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using JscanMonitoringCore.Data.Mapping;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using System.Reflection;

namespace JscanMonitoringCore.Data.Config
{
    /// <summary>
    /// Classe de configuração de sessão com o banco
    /// </summary>
    public static class SessionFactory
    {
        private static ISessionFactory session;

        private static ISessionFactory Session
        {
            get
            {
                if (session is null)
                    InitializeSessionFactory();

                return session;
            }
        }

        private static void InitializeSessionFactory()
        {
            session = Fluently.Configure()
                .Database(MySQLConfiguration.Standard.ConnectionString(
                    //ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString
                    //"Server=localhost;Database=master;Trusted_Connection=True;"
                    "Server=localhost;Port=3306;Database=bandtec;Uid=root;Pwd=bandtec"
                    )
                    .ShowSql()
                    .FormatSql())
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<ReadMapping>())
                .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                .BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return Session.OpenSession();
        }
    }
}
