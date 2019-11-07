namespace MyCompany.Visitors.Client.UniversalApp.WindowsStore.Behaviors
{
    using Windows.UI.Xaml.Controls;
    using WinRtBehaviors;

    /// <summary>
    /// This behavior check if the text binded in a textblock is null.
    /// </summary>
    public class NullTextBehavior : Behavior<TextBlock>
    {
        /// <summary>
        ///  Text to show if text binded is null.
        /// </summary>
        public string DefaultText { get; set; }

        /// <summary>
        /// OnAttached method.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.LayoutUpdated += textBlockControl_LayoutUpdated;
        }

        private void textBlockControl_LayoutUpdated(object sender, object e)
        {
            var actualtextValue = AssociatedObject.GetValue(TextBlock.TextProperty).ToString();
            if (string.IsNullOrWhiteSpace(actualtextValue))
            {
                AssociatedObject.SetValue(TextBlock.TextProperty, DefaultText);
            }
        }

        /// <summary>
        /// OnDetaching method.
        /// </summary>
        protected override void OnDetaching()
        {
            AssociatedObject.LayoutUpdated -= textBlockControl_LayoutUpdated;
            base.OnDetaching();
        }
    }
}
