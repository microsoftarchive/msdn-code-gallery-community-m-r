using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls.DataVisualization.Charting;

namespace MyEvents.Client.Organizer.Desktop.Views.UserControls
{
    /// <summary>
    /// Custom pie series class to override ActualLegendItemStyle error
    /// documented in: http://forums.silverlight.net/t/153144.aspx/1
    /// </summary>
    public class CustomPieSeries : PieSeries
    {
        /// <summary>
        /// Dependency property
        /// </summary>
        protected static readonly DependencyProperty ActualLegendItemStyleProperty = DependencyProperty.Register("ActualLegendItemStyle", typeof(Style), typeof(DataPointSeries), null);

        /// <summary>
        /// Standard property
        /// </summary>
        protected Style ActualLegendItemStyle
        {
            get
            {
                return (base.GetValue(ActualLegendItemStyleProperty) as Style);
            }
            set
            {
                base.SetValue(ActualLegendItemStyleProperty, value);
            }
        }
    }
}
