using MyShuttle.API.Model;
using MyShuttle.API.Model.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyShuttle.API.Data.DocumentDb.Queries
{
    public class TrackedRidesByVehicleQuery : BaseDocumentQuery
    {
        private List<string> _fields;

        public TrackedRidesByVehicleQuery(DocumentDbContext ctx) : base(ctx)
        {
            _fields = new List<string>();
            StartDateTime = null;
            ExclusiveEndDateTime = null;
        }

        public string DeviceId { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? ExclusiveEndDateTime { get; set; }


        public TrackedRidesByVehicleQuery WithField<T>(Expression<Func<TrackedRideDocument, T>> prop)
        {
            var memberExpression = prop.Body as MemberExpression;
            if (memberExpression != null)
            {
                var member = memberExpression.Member as PropertyInfo;
                if (member != null)
                {
                    var name = member.Name;
                    if (!_fields.Contains(name))
                    {
                        _fields.Add("c." + member.Name);
                    }
                }
            }

            return this;
        }


        public async Task<IEnumerable<TrackedRideDocument>> ExecuteAsync()
        {
            var collection = await DocumentContext.GetCollectionAsync(DocumentDbContext.CollectionTrackedRides);

            var sql = string.Format("SELECT {0} from c where c.Device='{1}' {2}",
                _fields.Any() ? string.Join(",", _fields) : "*",
                DeviceId,
                AdditionalWhereClausule());

            var rides = DocumentContext.CreateDocumentQuery<TrackedRideDocument>(collection, sql).ToList();
            return rides;
        }

        private string AdditionalWhereClausule()
        {
            if (StartDateTime == null || ExclusiveEndDateTime == null)
            {
                return string.Empty;
            }

            return string.Format("and c.StartTime >= '{0}' and c.EndTime < '{1}'",
                StartDateTime.Value.ToDocumentDbString(),
                ExclusiveEndDateTime.Value.ToDocumentDbString());
        }


    }
}
