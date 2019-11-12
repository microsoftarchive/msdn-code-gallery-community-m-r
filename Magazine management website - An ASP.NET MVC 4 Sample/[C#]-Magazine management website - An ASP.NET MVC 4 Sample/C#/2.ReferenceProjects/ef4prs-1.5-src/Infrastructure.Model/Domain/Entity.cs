using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Infrastructure.Model.Domain
{
    public abstract class Entity
    {
        public virtual int Id
        {
            get;
            set;
        }

        public virtual bool IsTransient()
        {
            return Id == default(int);
        }
    }
}
