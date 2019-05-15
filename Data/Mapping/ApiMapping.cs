using FluentNHibernate.Mapping;
using JscanMonitoringCore.Data.Model;

namespace JscanMonitoringCore.Data.Mapping
{
    public class ApiMapping : ClassMap<Api>
    {
        public ApiMapping()
        {
            Table("TB_API");
            Id(c => c.Id).GeneratedBy.Identity().Unique();
            Map(c => c.Name);
            Map(c => c.Type);
            Map(c => c.EndPoint);
        }
    }
}