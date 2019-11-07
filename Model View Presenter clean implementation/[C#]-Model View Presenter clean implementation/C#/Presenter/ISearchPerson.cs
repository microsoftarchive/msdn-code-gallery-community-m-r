using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EModel;

public delegate void VoidHandler();

namespace Presenter
{
    /// <summary>
    /// Contract between View and Presenter is this interface.
    /// </summary>
    public interface ISearchPerson
    {
        string Name { get; }
        string Age { get;  }
        string Dept { get; }
        event VoidHandler Search;
        List<Person> Persons { set; }
    }
}
