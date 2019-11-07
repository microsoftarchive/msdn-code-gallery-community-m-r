using System;
using System.ComponentModel;

namespace MyShuttle.Client.Desktop.ViewModels
{
    public class StatisticsViewModel
    {
        public DateTime Date { get; set; }

        public int TotalCapacity { get; set; }

        public int ActualCapacity { get; set; }

        public int Outside { get; set; }

        public int Inside { get; set; }

        public double CapacityPercentage { get; set; }

        public double OutsidePercentage { get; set; }

        public double InsidePercentage { get; set; }

        public StatisticsViewModel()
        {
            if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(System.Windows.DependencyObject)).DefaultValue))
            {
                Date = new DateTime(2014, 9, 4);
            }
            else
            {
                Date = DateTime.Now.Date;
            }
            TotalCapacity = 400;
            ActualCapacity = 256;
            Outside = 56;
            Inside = 200;

            CapacityPercentage = ActualCapacity / (double)TotalCapacity * 100d;
            OutsidePercentage = Outside / (double)ActualCapacity * 100d;
            InsidePercentage = Inside / (double)ActualCapacity * 100d;
        }
    }
}
