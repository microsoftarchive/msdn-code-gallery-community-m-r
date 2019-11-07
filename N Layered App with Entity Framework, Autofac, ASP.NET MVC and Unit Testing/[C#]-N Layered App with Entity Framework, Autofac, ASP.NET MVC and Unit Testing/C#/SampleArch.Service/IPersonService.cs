using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SampleArch.Model;

namespace SampleArch.Service
{
    public interface IPersonService : IEntityService<Person>
    {
        Person GetById(long Id);
    }
}
