using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;

namespace OwinSource
{
    public class Middleware<TModelType> : OwinMiddleware where TModelType : class,new()
    {
        public Middleware(OwinMiddleware next)
            : base(next)
        {

        }

        public override Task Invoke(IOwinContext context)
        {

            if (context.Request.Method == "GET")
            {

            }
            using (var sr = new System.IO.StreamReader(context.Request.Body))
            {
                var form = new TModelType();
                var body = sr.ReadToEnd();

                // Receiving the values from querystring

                if (string.IsNullOrEmpty(body) == true)
                {
                    #region
                    body = context.Request.QueryString.ToString();
                    var formData = body.Split(new[] { '&' }).Select(x => x.Split(new[] { '=' })).ToDictionary(x => x[0], x => x[1]);
                    var properties = typeof(TModelType).GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (var property in properties)
                    {
                        if (formData.Count > 0)
                        {
                            if (formData.ContainsKey(property.Name))
                            {
                                property.SetValue(form, Convert.ChangeType(formData[property.Name], property.PropertyType));
                            }
                        }
                    }
                    #endregion
                }

                // Receiving values from Form Body

                if (string.IsNullOrEmpty(body) != true)
                {
                   



                }

                context.Set("Body", form);
            }

            return this.Next.Invoke(context);

        }
    }
}
