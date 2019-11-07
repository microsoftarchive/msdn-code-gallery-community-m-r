using MyShuttle.Client.Core.DocumentResponse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace MyShuttle.Client.UniversalApp.Model
{

    public class GroupRide
    {
        public ObservableCollection<Ride> Names { get; set; }
        public GroupRide(ObservableCollection<Ride> rides)
        {
            Names = rides;
        }
    }
}
