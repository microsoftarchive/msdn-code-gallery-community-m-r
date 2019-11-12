namespace MyCompany.Expenses.Client.WP.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Navigation;
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;

    /// <summary>
    /// Track control
    /// </summary>
    public partial class TrackRadialProgress : UserControl
    {
        private static readonly DependencyProperty StartTrackingAnimationProperty = DependencyProperty.Register("StartTrackingAnimation", typeof(bool), typeof(TrackRadialProgress), new PropertyMetadata((d, val) =>
            {
                var thisView = (d as TrackRadialProgress);
                bool value;
                bool.TryParse(val.NewValue.ToString(), out value);
                if (value)
                    thisView.FillAnimation.Begin();
                else
                    thisView.FillAnimation.Stop();
            }));

        /// <summary>
        /// Start or stop animation.
        /// </summary>
        public bool StartTrackingAnimation
        {
            get { return (bool)this.GetValue(StartTrackingAnimationProperty); }
            set { this.SetValue(StartTrackingAnimationProperty, value); }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TrackRadialProgress()
        {
            InitializeComponent();
        }
    }
}
