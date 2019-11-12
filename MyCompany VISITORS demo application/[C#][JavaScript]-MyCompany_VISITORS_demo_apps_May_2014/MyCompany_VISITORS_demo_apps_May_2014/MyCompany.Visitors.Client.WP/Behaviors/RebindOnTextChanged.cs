namespace MyCompany.Visitors.Client.WP.Behaviors
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Controls;
    using System.Windows.Interactivity;

    /// <summary>
    /// This Behavior makes a rebind when the content of a TextBox has changed.
    /// </summary>
    public class RebindOnTextChanged : Behavior<TextBox>
    {
        /// <summary>
        /// On Attached method.
        /// </summary>
        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.TextChanged += this.TextChanged;
        }

        /// <summary>
        /// On Detaching method.
        /// </summary>
        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.TextChanged -= this.TextChanged;
        }

        private void TextChanged(object sender, TextChangedEventArgs e)
        {
            var bindingExpression = this.AssociatedObject.GetBindingExpression(TextBox.TextProperty);
            if (bindingExpression != null)
            {
                bindingExpression.UpdateSource();
            }
        } 
    }
}
