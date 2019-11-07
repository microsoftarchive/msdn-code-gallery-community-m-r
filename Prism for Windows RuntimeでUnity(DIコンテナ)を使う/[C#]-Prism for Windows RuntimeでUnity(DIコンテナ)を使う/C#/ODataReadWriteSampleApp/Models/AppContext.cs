using Microsoft.Practices.Prism.Mvvm;
using ODataReadWriteSampleApp.Models.OData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODataReadWriteSampleApp.Models
{
    public class AppContext : BindableBase
    {
        /// <summary>
        /// ここのURLをhttp://www.odata.org/odata-services/のOData(read/write)のURLで初期化
        /// </summary>
        private DemoService service = new DemoService(new Uri("http://services.odata.org/V4/OData/(S(wasyyq3eccboj1b3i1iagwld))/OData.svc/"));

        /// <summary>
        /// Person関連の処理をするModel
        /// </summary>
        public PeopleModel PeopleModel { get; private set; }

        public AppContext()
        {
            this.PeopleModel = new PeopleModel(service);
        }
    }
}
