// <copyright file="FloatableWindow.cs" company="Microsoft Corporation">
// ===============================================================================
//
//
// Project: Microsoft Silverlight Rough Cut Editor
// FILES: FloatableWindow.cs                     
//
// ===============================================================================
// This source is subject to the Microsoft Public License (Ms-PL).
// Please see http://go.microsoft.com/fwlink/?LinkID=131993 for details.
// All other rights reserved.
// ===============================================================================
// </copyright>

namespace System.Windows.Controls
{
    using System.Collections;
    using System.ComponentModel;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    /// <summary>
    /// Provides a window that can be displayed over a parent window and blocks
    /// interaction with the parent window.
    /// </summary>
    /// <QualityBand>Preview</QualityBand>
    [TemplatePart(Name = PARTTitleControl, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PARTChrome, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PartClose, Type = typeof(ButtonBase))]
    [TemplatePart(Name = PARTContentPresenter, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PARTContentRoot, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PARTOverlay, Type = typeof(Panel))]
    [TemplatePart(Name = PARTRoot, Type = typeof(FrameworkElement))]
    [TemplatePart(Name = PARTResizer, Type = typeof(FrameworkElement))]
    [TemplateVisualState(Name = VSMSTATEStateClosed, GroupName = VSMGROUPWindow)]
    [TemplateVisualState(Name = VSMSTATEStateOpen, GroupName = VSMGROUPWindow)]
    public class FloatableWindow : ContentControl
    {
        /// <summary>
        /// Identifies the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.HasCloseButton" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.HasCloseButton" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty HasCloseButtonProperty = DependencyProperty.Register(
            "HasCloseButton", 
            typeof(bool), 
            typeof(FloatableWindow), 
            new PropertyMetadata(true, OnHasCloseButtonPropertyChanged));

        /// <summary>
        /// Identifies the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.OverlayBrush" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.OverlayBrush" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty OverlayBrushProperty = DependencyProperty.Register(
            "OverlayBrush", typeof(Brush), typeof(FloatableWindow), new PropertyMetadata(OnOverlayBrushPropertyChanged));

        /// <summary>
        /// Identifies the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.OverlayOpacity" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.OverlayOpacity" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty OverlayOpacityProperty = DependencyProperty.Register(
            "OverlayOpacity", 
            typeof(double), 
            typeof(FloatableWindow), 
            new PropertyMetadata(OnOverlayOpacityPropertyChanged));

        /// <summary>
        /// Identifies the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.Title" />
        /// dependency property.
        /// </summary>
        /// <value>
        /// The identifier for the
        /// <see cref = "P:System.Windows.Controls.FloatableWindow.Title" />
        /// dependency property.
        /// </value>
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            "Title", typeof(object), typeof(FloatableWindow), null);

        /// <summary>
        /// The name of the Chrome template part.
        /// </summary>
        private const string PARTChrome = "Chrome";

        /// <summary>
        /// The name of the CloseButton template part.
        /// </summary>
        private const string PartClose = "CloseButton";

        /// <summary>
        /// The name of the ContentPresenter template part.
        /// </summary>
        private const string PARTContentPresenter = "ContentPresenter";

        /// <summary>
        /// The name of the ContentRoot template part.
        /// </summary>
        private const string PARTContentRoot = "ContentRoot";

        /// <summary>
        /// The name of the Overlay template part.
        /// </summary>
        private const string PARTOverlay = "Overlay";

        /// <summary>
        /// The name of the Resizer template part.
        /// </summary>
        private const string PARTResizer = "Resizer";

        /// <summary>
        /// The name of the Root template part.
        /// </summary>
        private const string PARTRoot = "Root";

        /// <summary>
        /// The par t_ title control.
        /// </summary>
        private const string PARTTitleControl = "TitleControl";

        /// <summary>
        /// The name of the WindowStates VSM group.
        /// </summary>
        private const string VSMGROUPWindow = "WindowStates";

        /// <summary>
        /// The name of the Closing VSM state.
        /// </summary>
        private const string VSMSTATEStateClosed = "Closed";

        /// <summary>
        /// The name of the Opening VSM state.
        /// </summary>
        private const string VSMSTATEStateOpen = "Open";

        /// <summary>
        /// The z.
        /// </summary>
        private static int z;

        /// <summary>
        /// Private accessor for the Chrome.
        /// </summary>
        private FrameworkElement chrome;

        /// <summary>
        /// Private accessor for the click point on the chrome.
        /// </summary>
        private Point clickPoint;

        /// <summary>
        /// Private accessor for the Closing storyboard.
        /// </summary>
        private Storyboard closed;

        /// <summary>
        /// Private accessor for the ContentPresenter.
        /// </summary>
        private FrameworkElement contentPresenter;

        /// <summary>
        /// Private accessor for the translate transform that needs to be applied on to the ContentRoot.
        /// </summary>
        private TranslateTransform contentRootTransform;

        /// <summary>
        /// Content area desired height.
        /// </summary>
        private double desiredContentHeight;

        /// <summary>
        /// Content area desired width.
        /// </summary>
        private double desiredContentWidth;

        /// <summary>
        /// Desired margin for the window.
        /// </summary>
        private Thickness desiredMargin;

        /// <summary>
        /// Private accessor for the Dialog Result property.
        /// </summary>
        private bool? dialogresult;

        /// <summary>
        /// Set in the overloaded Show method.  Offsets the Popup horizontally from the top left corner of the browser window by this amount.
        /// </summary>
        private double horizontalOffset;

        /// <summary>
        /// Private accessor for the FloatableWindow InteractionState.
        /// </summary>
        private WindowInteractionState interactionState;

        /// <summary>
        /// Boolean value that specifies whether the application is exit or not.
        /// </summary>
        private bool isAppExit;

        /// <summary>
        /// Boolean value that specifies whether the window is in closing state or not.
        /// </summary>
        private bool isClosing;

        /// <summary>
        /// Boolean value that specifies whether the mouse is captured or not.
        /// </summary>
        private bool isMouseCaptured;

        /// <summary>
        /// Boolean value that specifies whether the window is opened.
        /// </summary>
        private bool isOpen;

        /// <summary>
        /// Private accessor for the IsModal
        /// </summary>
        [DefaultValue(false)]
        private bool modal;

        /// <summary>
        /// Private accessor for the Opening storyboard.
        /// </summary>
        private Storyboard opened;

        /// <summary>
        /// Private accessor for the Resizer.
        /// </summary>
        private FrameworkElement resizer;

        /// <summary>
        /// Private accessor for the Root of the window.
        /// </summary>
        private FrameworkElement root;

        /// <summary>
        /// The _title control.
        /// </summary>
        private FrameworkElement titleControl;

        /// <summary>
        /// Set in the overloaded Show method.  Offsets the Popup vertically from the top left corner of the browser window by this amount.
        /// </summary>
        private double verticalOffset;

        /// <summary>
        /// Private accessor for the position of the window with respect to RootVisual.
        /// </summary>
        private Point windowPosition;

        /// <summary>
        /// The focus border brush.
        /// </summary>
        private Brush focusBorderBrush;

        /// <summary>
        /// The focus title brush.
        /// </summary>
        private Brush focusTitleBrush;

        /// <summary>
        /// The not focused border brush.
        /// </summary>
        private Brush notFocusedBorderBrush;

        /// <summary>
        /// The not focused chrome brush.
        /// </summary>
        private Brush notFocusedChromeBrush;

        /// <summary>
        /// The not focused title brush.
        /// </summary>
        private Brush notFocusedTitleBrush;

        // <summary>
        // Initializes a new instance of the <see cref="FloatableWindow"/> class. 
        // Initializes a new instance of the <see cref="T:System.Windows.Controls.FloatableWindow"/> class.
        // </summary>
        public FloatableWindow()
        {
            this.DefaultStyleKey = typeof(FloatableWindow);
            this.InteractionState = WindowInteractionState.NotResponding;
            this.ResizeMode = ResizeMode.CanResize;

            // this.focusBorderBrush = new SolidColorBrush(Color.FromArgb(255, 0, 20, 60));
            this.focusBorderBrush = new SolidColorBrush(Color.FromArgb(255, 89, 89, 89));
            this.focusTitleBrush = new SolidColorBrush(Colors.White);
            this.notFocusedTitleBrush = new SolidColorBrush(Color.FromArgb(255, 212, 212, 212));
            this.notFocusedChromeBrush = new SolidColorBrush(Color.FromArgb(255, 89, 89, 89));
        }

        /// <summary>
        /// Occurs when the <see cref = "T:System.Windows.Controls.FloatableWindow" />
        /// is closed.
        /// </summary>
        public event EventHandler Closed;

        /// <summary>
        /// Occurs when the <see cref = "T:System.Windows.Controls.FloatableWindow" />
        /// is closing.
        /// </summary>
        public event EventHandler<CancelEventArgs> Closing;

        public event Action<double, double> WindowPositionChanged;

        public event Action<double, double> WindowSizeChanged;

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// <see cref = "T:System.Windows.Controls.FloatableWindow" /> was accepted or
        /// canceled.
        /// </summary>
        /// <value>
        /// True if the child window was accepted; false if the child window was
        /// canceled. The default is null.
        /// </value>
        [TypeConverter(typeof(NullableBoolConverter))]
        public bool? DialogResult
        {
            get
            {
                return this.dialogresult;
            }

            set
            {
                if (this.dialogresult != value)
                {
                    this.dialogresult = value;
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the
        /// <see cref = "T:System.Windows.Controls.FloatableWindow" /> has a close
        /// button.
        /// </summary>
        /// <value>
        /// True if the child window has a close button; otherwise, false. The
        /// default is true.
        /// </value>
        public bool HasCloseButton
        {
            get
            {
                return (bool)this.GetValue(HasCloseButtonProperty);
            }

            set
            {
                this.SetValue(HasCloseButtonProperty, value);
            }
        }

        /// <summary>
        /// Gets a value indicating whether HasFocus.
        /// </summary>
        public bool HasFocus { get; private set; }

        /// <summary>
        /// Setting for the horizontal positioning offset for start position
        /// </summary>
        public double HorizontalOffset
        {
            get
            {
                return this.horizontalOffset;
            }

            set
            {
                this.horizontalOffset = value;
            }
        }

        /// <summary>
        /// Gets the internal accessor for the modal of the window.
        /// </summary>
        public bool IsModal
        {
            get
            {
                return this.modal;
            }
        }

        /// <summary>
        /// Gets or sets the visual brush that is used to cover the parent
        /// window when the child window is open.
        /// </summary>
        /// <value>
        /// The visual brush that is used to cover the parent window when the
        /// <see cref = "T:System.Windows.Controls.FloatableWindow" /> is open. The
        /// default is null.
        /// </value>
        public Brush OverlayBrush
        {
            get
            {
                return (Brush)this.GetValue(OverlayBrushProperty);
            }

            set
            {
                this.SetValue(OverlayBrushProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets the opacity of the overlay brush that is used to cover
        /// the parent window when the child window is open.
        /// </summary>
        /// <value>
        /// The opacity of the overlay brush that is used to cover the parent
        /// window when the <see cref = "T:System.Windows.Controls.FloatableWindow" />
        /// is open. The default is 1.0.
        /// </value>
        public double OverlayOpacity
        {
            get
            {
                return (double)this.GetValue(OverlayOpacityProperty);
            }

            set
            {
                this.SetValue(OverlayOpacityProperty, value);
            }
        }

        /// <summary>
        /// Gets or sets ParentLayoutRoot.
        /// </summary>
        public Panel ParentLayoutRoot { get; set; }

        /// <summary>
        /// Gets or sets ResizeMode.
        /// </summary>
        public ResizeMode ResizeMode { get; set; }

        /// <summary>
        /// Gets or sets the title that is displayed in the frame of the
        /// <see cref = "T:System.Windows.Controls.FloatableWindow" />.
        /// </summary>
        /// <value>
        /// The title displayed at the top of the window. The default is null.
        /// </value>
        public object Title
        {
            get
            {
                return this.GetValue(TitleProperty);
            }

            set
            {
                this.SetValue(TitleProperty, value);
            }
        }

        /// <summary>
        /// Setting for the vertical positioning offset for start position
        /// </summary>
        public double VerticalOffset
        {
            get
            {
                return this.verticalOffset;
            }

            set
            {
                this.verticalOffset = value;
            }
        }

        /// <summary>
        /// Gets the internal accessor for the PopUp of the window.
        /// </summary>
        internal Popup ChildWindowPopup { get; private set; }

        /// <summary>
        /// Gets the internal accessor for the close button of the window.
        /// </summary>
        internal ButtonBase CloseButton { get; private set; }

        /// <summary>
        /// Gets the internal accessor for the ContentRoot of the window.
        /// </summary>
        internal FrameworkElement ContentRoot { get; private set; }

        /// <summary>
        /// Gets the InteractionState for the FloatableWindow.
        /// </summary>
        internal WindowInteractionState InteractionState
        {
            get
            {
                return this.interactionState;
            }

            private set
            {
                if (this.interactionState != value)
                {
                    WindowInteractionState oldValue = this.interactionState;
                    this.interactionState = value;
                    FloatableWindowAutomationPeer peer =
                        FloatableWindowAutomationPeer.FromElement(this) as FloatableWindowAutomationPeer;

                    if (peer != null)
                    {
                        peer.RaiseInteractionStatePropertyChangedEvent(oldValue, this.interactionState);
                    }
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the PopUp is open or not.
        /// </summary>
        internal bool IsOpen
        {
            get
            {
                return (this.ChildWindowPopup != null && this.ChildWindowPopup.IsOpen)
                       || ((this.ParentLayoutRoot != null) && this.ParentLayoutRoot.Children.Contains(this));
            }
        }

        /// <summary>
        /// Gets the internal accessor for the overlay of the window.
        /// </summary>
        internal Panel Overlay { get; private set; }

        /// <summary>
        /// Gets the root visual element.
        /// </summary>
        private static Control RootVisual
        {
            get
            {
                return Application.Current == null ? null : (Application.Current.RootVisual as Control);
            }
        }

        /// <summary>
        /// Closes a <see cref="T:System.Windows.Controls.FloatableWindow"/>.
        /// </summary>
        public void Close()
        {
            // AutomationPeer returns "Closing" when Close() is called
            // but the window is not closed completely:
            this.InteractionState = WindowInteractionState.Closing;
            CancelEventArgs e = new CancelEventArgs();
            this.OnClosing(e);

            // On ApplicationExit, close() cannot be cancelled
            if (!e.Cancel || this.isAppExit)
            {
                if (RootVisual != null)
                {
                    RootVisual.IsEnabled = true;
                }

                // Close Popup
                if (this.IsOpen)
                {
                    if (this.closed != null)
                    {
                        // Popup will be closed when the storyboard ends
                        this.isClosing = true;
                        try
                        {
                            var sb = this.GetVisualStateStoryboard("WindowStates", "Closed");
                            sb.Completed += (s, args) =>
                                {
                                    this.ParentLayoutRoot.Children.Remove(this);
                                    this.OnClosed(EventArgs.Empty);
                                    this.UnSubscribeFromEvents();
                                    this.UnsubscribeFromTemplatePartEvents();

                                    if (Application.Current.RootVisual != null)
                                    {
                                        Application.Current.RootVisual.GotFocus -=
                                            new RoutedEventHandler(this.RootVisual_GotFocus);
                                    }
                                };
                            this.ChangeVisualState();
                        }
                        finally
                        {
                            this.isClosing = false;
                        }
                    }
                    else
                    {
                        // If no closing storyboard is defined, close the Popup
                        this.ChildWindowPopup.IsOpen = false;
                        this.OnClosed(EventArgs.Empty);
                    }

                    if (!this.dialogresult.HasValue)
                    {
                        // If close action is not happening because of DialogResult property change action,
                        // Dialogresult is always false:
                        this.dialogresult = false;
                    }

                    // this.OnClosed(EventArgs.Empty);
                    // this.UnSubscribeFromEvents();
                    // this.UnsubscribeFromTemplatePartEvents();

                    // if (Application.Current.RootVisual != null)
                    // {
                    // Application.Current.RootVisual.GotFocus -= new RoutedEventHandler(this.RootVisual_GotFocus);
                    // }
                }
            }
            else
            {
                // If the Close is cancelled, DialogResult should always be NULL:
                this.dialogresult = null;
                this.InteractionState = WindowInteractionState.Running;
            }
        }

        /// <summary>
        /// Builds the visual tree for the
        /// <see cref="T:System.Windows.Controls.FloatableWindow"/> control when a
        /// new template is applied.
        /// </summary>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", 
            Justification = "No need to split the code into two parts.")]
        public override void OnApplyTemplate()
        {
            this.UnsubscribeFromTemplatePartEvents();

            base.OnApplyTemplate();

            this.CloseButton = this.GetTemplateChild(PartClose) as ButtonBase;

            if (this.CloseButton != null)
            {
                if (this.HasCloseButton)
                {
                    this.CloseButton.Visibility = Visibility.Visible;
                }
                else
                {
                    this.CloseButton.Visibility = Visibility.Collapsed;
                }

                this.CloseButton.IsTabStop = false;
            }

            if (this.closed != null)
            {
                this.closed.Completed -= new EventHandler(this.Closing_Completed);
            }

            if (this.opened != null)
            {
                this.opened.Completed -= new EventHandler(this.Opening_Completed);
            }

            this.titleControl = this.GetTemplateChild(PARTTitleControl) as FrameworkElement;

            this.root = this.GetTemplateChild(PARTRoot) as FrameworkElement;
            this.resizer = this.GetTemplateChild(PARTResizer) as FrameworkElement;

            if (this.root != null)
            {
                IList groups = VisualStateManager.GetVisualStateGroups(this.root);

                if (groups != null)
                {
                    IList states = null;

                    foreach (VisualStateGroup vsg in groups)
                    {
                        if (vsg.Name == FloatableWindow.VSMGROUPWindow)
                        {
                            states = vsg.States;
                            break;
                        }
                    }

                    if (states != null)
                    {
                        foreach (VisualState state in states)
                        {
                            if (state.Name == FloatableWindow.VSMSTATEStateClosed)
                            {
                                this.closed = state.Storyboard;
                            }

                            if (state.Name == FloatableWindow.VSMSTATEStateOpen)
                            {
                                this.opened = state.Storyboard;
                            }
                        }
                    }
                }

                // TODO: Figure out why I can't wire up the event below in SubscribeToTemplatePartEvents
                this.root.MouseLeftButtonDown += new MouseButtonEventHandler(this.ContentRoot_MouseLeftButtonDown);

                if (this.ResizeMode != ResizeMode.NoResize)
                {
                    this.resizer.MouseLeftButtonDown +=
                        new MouseButtonEventHandler(this.Resizer_MouseLeftButtonDown);
                    this.resizer.MouseLeftButtonUp +=
                        new MouseButtonEventHandler(this.Resizer_MouseLeftButtonUp);
                    this.resizer.MouseMove += new MouseEventHandler(this.Resizer_MouseMove);
                    this.resizer.MouseEnter += new MouseEventHandler(this.Resizer_MouseEnter);
                    this.resizer.MouseLeave += new MouseEventHandler(this.Resizer_MouseLeave);
                }
                else
                {
                    this.resizer.Opacity = 0;
                }
            }

            this.ContentRoot = this.GetTemplateChild(PARTContentRoot) as FrameworkElement;

            this.chrome = this.GetTemplateChild(PARTChrome) as FrameworkElement;

            this.Overlay = this.GetTemplateChild(PARTOverlay) as Panel;

            this.contentPresenter = this.GetTemplateChild(PARTContentPresenter) as FrameworkElement;

            this.SubscribeToTemplatePartEvents();
            this.SubscribeToStoryBoardEvents();
            this.desiredMargin = this.Margin;
            this.Margin = new Thickness(0);

            // Update overlay size
            if (this.IsOpen && (this.ChildWindowPopup != null))
            {
                this.desiredContentHeight = this.Height;
                this.desiredContentWidth = this.Width;
                this.UpdateOverlaySize();
                this.UpdateRenderTransform();
                this.ChangeVisualState();
            }
        }

        /// <summary>
        /// The show.
        /// </summary>
        public void Show()
        {
            this.ShowWindow(false);
        }

        /// <summary>
        /// The show.
        /// </summary>
        /// <param name="horizontalOffset">
        /// The horizontal offset.
        /// </param>
        /// <param name="verticalOffset">
        /// The vertical offset.
        /// </param>
        public void Show(double horizontalOffset, double verticalOffset)
        {
            this.horizontalOffset = horizontalOffset;
            this.verticalOffset = verticalOffset;
            this.ShowWindow(false);
        }

        /// <summary>
        /// The show dialog.
        /// </summary>
        public void ShowDialog()
        {
            this.verticalOffset = 0;
            this.horizontalOffset = 0;
            this.ShowWindow(true);
        }

        /// <summary>
        /// Executed when the application is exited.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// Event args.
        /// </param>
        internal void Application_Exit(object sender, EventArgs e)
        {
            if (this.IsOpen)
            {
                this.isAppExit = true;
                try
                {
                    this.Close();
                }
                finally
                {
                    this.isAppExit = false;
                }
            }
        }

        /// <summary>
        /// Executed when focus is given to the window via a click.  Attempts to bring current 
        /// window to the front in the event there are more windows.
        /// </summary>
        internal void BringToFront()
        {
            z++;
            Canvas.SetZIndex(this, z);
        }

        /// <summary>
        /// Executed when the CloseButton is clicked.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Routed event args.
        /// </param>
        internal void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Brings the window to the front of others
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        internal void ContentRoot_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.FocusWindow();
        }

        /// <summary>
        /// Opens a <see cref="T:System.Windows.Controls.FloatableWindow"/> and
        /// returns without waiting for the
        /// <see cref="T:System.Windows.Controls.FloatableWindow"/> to close.
        /// </summary>
        /// <param name="isModal">
        /// The is Modal.
        /// </param>
        /// <exception cref="T:System.InvalidOperationException">
        /// The child window is already in the visual tree.
        /// </exception>
        internal void ShowWindow(bool isModal)
        {
            this.modal = isModal;

            // AutomationPeer returns "Running" when Show() is called
            // but the FloatableWindow is not ready for user interaction:
            this.InteractionState = WindowInteractionState.Running;

            this.SubscribeToEvents();
            this.SubscribeToTemplatePartEvents();
            this.SubscribeToStoryBoardEvents();

            // MaxHeight and MinHeight properties should not be overwritten:
            this.MaxHeight = double.PositiveInfinity;
            this.MaxWidth = double.PositiveInfinity;

            if (this.modal)
            {
                if (this.ChildWindowPopup == null)
                {
                    this.ChildWindowPopup = new Popup();

                    try
                    {
                        this.ChildWindowPopup.Child = this;
                    }
                    catch (ArgumentException)
                    {
                        // If the FloatableWindow is already in the visualtree, we cannot set it to be the child of the popup
                        // we are throwing a friendlier exception for this case:
                        this.InteractionState = WindowInteractionState.NotResponding;
                        throw new InvalidOperationException(
                            RCE.Infrastructure.Resources.Resources.ChildWindow_InvalidOperation);
                    }
                }

                if (this.ChildWindowPopup != null && Application.Current.RootVisual != null)
                {
                    this.ChildWindowPopup.IsOpen = true;

                    this.ChildWindowPopup.HorizontalOffset = this.horizontalOffset;
                    this.ChildWindowPopup.VerticalOffset = this.verticalOffset;

                    // while the FloatableWindow is open, the DialogResult is always NULL:
                    this.dialogresult = null;
                }
            }
            else
            {
                if (this.ParentLayoutRoot != null)
                {
                    this.SetValue(Canvas.TopProperty, this.verticalOffset);
                    this.SetValue(Canvas.LeftProperty, this.horizontalOffset);
                    this.ParentLayoutRoot.Children.Add(this);

                    ////this.BringToFront();
                }
                else
                {
                    throw new ArgumentNullException(
                        "ParentLayoutRoot", "You need to specify a root Panel element to add the window elements to.");
                }
            }

            // disable the underlying UI
            if (RootVisual != null && this.modal)
            {
                RootVisual.IsEnabled = false;
            }

            // if the template is already loaded, display loading visuals animation
            if (this.ContentRoot == null)
            {
                this.Loaded += (s, args) =>
                    {
                        if (this.ContentRoot != null)
                        {
                            this.ChangeVisualState();
                        }
                    };
            }
            else
            {
                this.ChangeVisualState();
            }
        }

        /// <summary>
        /// Raises the
        /// <see cref="E:System.Windows.Controls.FloatableWindow.Closed"/> event.
        /// </summary>
        /// <param name="e">
        /// The event data.
        /// </param>
        protected virtual void OnClosed(EventArgs e)
        {
            EventHandler handler = this.Closed;

            if (null != handler)
            {
                handler(this, e);
            }

            this.isOpen = false;
            if (!this.modal)
            {
                this.ParentLayoutRoot.Children.Remove(this);
            }
        }

        /// <summary>
        /// Raises the
        /// <see cref="E:System.Windows.Controls.FloatableWindow.Closing"/> event.
        /// </summary>
        /// <param name="e">
        /// The event data.
        /// </param>
        protected virtual void OnClosing(CancelEventArgs e)
        {
            EventHandler<CancelEventArgs> handler = this.Closing;

            if (null != handler)
            {
                handler(this, e);
            }
        }

        /// <summary>
        /// Returns a
        /// <see cref="T:System.Windows.Automation.Peers.FloatableWindowAutomationPeer"/>
        /// for use by the Silverlight automation infrastructure.
        /// </summary>
        /// <returns>
        /// <see cref="T:System.Windows.Automation.Peers.FloatableWindowAutomationPeer"/>
        /// for the <see cref="T:System.Windows.Controls.FloatableWindow"/> object.
        /// </returns>
        protected override AutomationPeer OnCreateAutomationPeer()
        {
            return new FloatableWindowAutomationPeer(this);
        }

        /// <summary>
        /// The on got focus.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnGotFocus(RoutedEventArgs e)
        {
            base.OnGotFocus(e);
            this.chrome.SetValue(Border.BackgroundProperty, this.focusBorderBrush);
            this.titleControl.SetValue(Control.ForegroundProperty, this.focusTitleBrush);
            this.notFocusedBorderBrush = this.BorderBrush;
            this.BorderBrush = this.focusBorderBrush;
            this.HasFocus = true;
        }

        /// <summary>
        /// The on lost focus.
        /// </summary>
        /// <param name="e">
        /// The e.
        /// </param>
        protected override void OnLostFocus(RoutedEventArgs e)
        {
            base.OnLostFocus(e);
            this.chrome.SetValue(Border.BackgroundProperty, this.notFocusedChromeBrush);
            this.titleControl.SetValue(Control.ForegroundProperty, this.notFocusedTitleBrush);
            this.BorderBrush = this.notFocusedBorderBrush;

            this.HasFocus = false;
        }

        /// <summary>
        /// This method is called every time a
        /// <see cref="T:System.Windows.Controls.FloatableWindow"/> is displayed.
        /// </summary>
        protected virtual void OnOpened()
        {
            this.UpdatePosition();
            this.isOpen = true;

            if (this.Overlay != null)
            {
                this.Overlay.Opacity = this.OverlayOpacity;
                this.Overlay.Background = this.OverlayBrush;
            }

            if (!this.Focus())
            {
                // If the Focus() fails it means there is no focusable element in the 
                // FloatableWindow. In this case we set IsTabStop to true to have the keyboard functionality
                this.IsTabStop = true;
                this.Focus();
            }
        }

        /// <summary>
        /// Finds the X coordinate of a point that is defined by a line.
        /// </summary>
        /// <param name="p1">
        /// Starting point of the line.
        /// </param>
        /// <param name="p2">
        /// Ending point of the line.
        /// </param>
        /// <param name="y">
        /// Y coordinate of the point.
        /// </param>
        /// <returns>
        /// X coordinate of the point.
        /// </returns>
        private static double FindPositionX(Point p1, Point p2, double y)
        {
            if (y == p1.Y || p1.X == p2.X)
            {
                return p2.X;
            }

            System.Diagnostics.Debug.Assert(p1.Y != p2.Y, "Unexpected equal Y coordinates");

            return (((y - p1.Y) * (p1.X - p2.X)) / (p1.Y - p2.Y)) + p1.X;
        }

        /// <summary>
        /// Inverts the input matrix.
        /// </summary>
        /// <param name="matrix">
        /// The matrix values that is to be inverted.
        /// </param>
        /// <returns>
        /// Returns a value indicating whether the inversion was successful or not.
        /// </returns>
        private static bool InvertMatrix(ref Matrix matrix)
        {
            double determinant = (matrix.M11 * matrix.M22) - (matrix.M12 * matrix.M21);

            if (determinant == 0.0)
            {
                return false;
            }

            Matrix matCopy = matrix;
            matrix.M11 = matCopy.M22 / determinant;
            matrix.M12 = -1 * matCopy.M12 / determinant;
            matrix.M21 = -1 * matCopy.M21 / determinant;
            matrix.M22 = matCopy.M11 / determinant;
            matrix.OffsetX = ((matCopy.OffsetY * matCopy.M21) - (matCopy.OffsetX * matCopy.M22)) / determinant;
            matrix.OffsetY = ((matCopy.OffsetX * matCopy.M12) - (matCopy.OffsetY * matCopy.M11)) / determinant;

            return true;
        }

        /// <summary>
        /// HasCloseButtonProperty PropertyChangedCallback call back static function.
        /// </summary>
        /// <param name="d">
        /// FloatableWindow object whose HasCloseButton property is changed.
        /// </param>
        /// <param name="e">
        /// DependencyPropertyChangedEventArgs which contains the old and new values.
        /// </param>
        private static void OnHasCloseButtonPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatableWindow cw = (FloatableWindow)d;

            if (cw.CloseButton != null)
            {
                if ((bool)e.NewValue)
                {
                    cw.CloseButton.Visibility = Visibility.Visible;
                }
                else
                {
                    cw.CloseButton.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// OverlayBrushProperty PropertyChangedCallback call back static function.
        /// </summary>
        /// <param name="d">
        /// FloatableWindow object whose OverlayBrush property is changed.
        /// </param>
        /// <param name="e">
        /// DependencyPropertyChangedEventArgs which contains the old and new values.
        /// </param>
        private static void OnOverlayBrushPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatableWindow cw = (FloatableWindow)d;

            if (cw.Overlay != null)
            {
                cw.Overlay.Background = (Brush)e.NewValue;
            }
        }

        /// <summary>
        /// OverlayOpacityProperty PropertyChangedCallback call back static function.
        /// </summary>
        /// <param name="d">
        /// FloatableWindow object whose OverlayOpacity property is changed.
        /// </param>
        /// <param name="e">
        /// DependencyPropertyChangedEventArgs which contains the old and new values.
        /// </param>
        private static void OnOverlayOpacityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FloatableWindow cw = (FloatableWindow)d;

            if (cw.Overlay != null)
            {
                cw.Overlay.Opacity = (double)e.NewValue;
            }
        }

        /// <summary>
        /// Changes the visual state of the FloatableWindow.
        /// </summary>
        private void ChangeVisualState()
        {
            if (this.isClosing)
            {
                VisualStateManager.GoToState(this, VSMSTATEStateClosed, true);
            }
            else
            {
                VisualStateManager.GoToState(this, VSMSTATEStateOpen, true);
                this.FocusWindow();
            }
        }

        /// <summary>
        /// Executed when the a key is presses when the window is open.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Key event args.
        /// </param>
        private void ChildWindow_KeyDown(object sender, KeyEventArgs e)
        {
            FloatableWindow ew = sender as FloatableWindow;
            System.Diagnostics.Debug.Assert(ew != null, "FloatableWindow instance is null.");

            // Ctrl+Shift+F4 closes the FloatableWindow
            if (e != null && !e.Handled && e.Key == Key.F4
                && ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control)
                && ((Keyboard.Modifiers & ModifierKeys.Shift) == ModifierKeys.Shift))
            {
                ew.Close();
                e.Handled = true;
            }
        }

        /// <summary>
        /// Executed when the window loses focus.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Routed event args.
        /// </param>
        private void ChildWindow_LostFocus(object sender, RoutedEventArgs e)
        {
            // If the FloatableWindow loses focus but the popup is still open,
            // it means another popup is opened. To get the focus back when the
            // popup is closed, we handle GotFocus on the RootVisual
            // TODO: Something else could get focus and handle the GotFocus event right.  
            // Try listening to routed events that were Handled (new SL 3 feature)
            // Blocked by Jolt bug #29419
            if (this.IsOpen && Application.Current != null && Application.Current.RootVisual != null)
            {
                this.InteractionState = WindowInteractionState.BlockedByModalWindow;
                Application.Current.RootVisual.GotFocus += new RoutedEventHandler(this.RootVisual_GotFocus);
            }
        }

        /// <summary>
        /// Executed when FloatableWindow size is changed.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Size changed event args.
        /// </param>
        private void ChildWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (this.modal)
            {
                if (this.Overlay != null)
                {
                    if (e.NewSize.Height != this.Overlay.Height)
                    {
                        this.desiredContentHeight = e.NewSize.Height;
                    }

                    if (e.NewSize.Width != this.Overlay.Width)
                    {
                        this.desiredContentWidth = e.NewSize.Width;
                    }
                }

                if (this.IsOpen)
                {
                    this.UpdateOverlaySize();
                }
            }
        }

        /// <summary>
        /// Executed when mouse left button is down on the chrome.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Mouse button event args.
        /// </param>
        private void Chrome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.chrome != null)
            {
                e.Handled = true;

                if (this.CloseButton != null && !this.CloseButton.IsTabStop)
                {
                    try
                    {
                        this.FocusWindow();
                    }
                    finally
                    {
                        this.CloseButton.IsTabStop = false;
                    }
                }
                else
                {
                    this.FocusWindow();
                }

                this.chrome.CaptureMouse();
                this.isMouseCaptured = true;
                this.clickPoint = e.GetPosition(sender as UIElement);
            }
        }

        /// <summary>
        /// Executed when mouse left button is up on the chrome.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Mouse button event args.
        /// </param>
        private void Chrome_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (this.chrome != null)
            {
                // e.Handled = true;
                this.chrome.ReleaseMouseCapture();
                this.isMouseCaptured = false;
            }
        }

        private void OnPositionChanged(double x, double y)
        {
            Action<double, double> handler = this.WindowPositionChanged;
            if (handler != null)
            {
                handler.Invoke(x, y);
            }
        }

        /// <summary>
        /// Executed when mouse moves on the chrome.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Mouse event args.
        /// </param>
        private void Chrome_MouseMove(object sender, MouseEventArgs e)
        {
            // if (this.isMouseCaptured && this.ContentRoot != null && Application.Current != null && Application.Current.RootVisual != null)
            // {
            // Point position = e.GetPosition(Application.Current.RootVisual);

            // GeneralTransform gt = this.ContentRoot.TransformToVisual(Application.Current.RootVisual);

            // if (gt != null)
            // {
            // Point p = gt.Transform(this.clickPoint);
            // this.windowPosition = gt.Transform(new Point(0, 0));

            // if (position.X < 0)
            // {
            // double Y = FindPositionY(p, position, 0);
            // position = new Point(0, Y);
            // }

            // if (position.X > this.Width)
            // {
            // double Y = FindPositionY(p, position, this.Width);
            // position = new Point(this.Width, Y);
            // }

            // if (position.Y < 0)
            // {
            // double X = FindPositionX(p, position, 0);
            // position = new Point(X, 0);
            // }

            // if (position.Y > this.Height)
            // {
            // double X = FindPositionX(p, position, this.Height);
            // position = new Point(X, this.Height);
            // }

            // double x = position.X - p.X;
            // double y = position.Y - p.Y;
            // UpdateContentRootTransform(x, y);
            // }
            //// } 

            if (this.isMouseCaptured && this.ContentRoot != null)
            {
                // If the child window is dragged out of the page, return
                if (Application.Current != null && Application.Current.RootVisual != null
                    &&
                    (e.GetPosition(Application.Current.RootVisual).X < 0
                     || e.GetPosition(Application.Current.RootVisual).Y < 0))
                {
                    return;
                }

                TransformGroup transformGroup = this.ContentRoot.RenderTransform as TransformGroup;

                if (transformGroup == null)
                {
                    transformGroup = new TransformGroup();
                    transformGroup.Children.Add(this.ContentRoot.RenderTransform);
                }

                TranslateTransform t = new TranslateTransform();
                t.X = e.GetPosition(this.ContentRoot).X - this.clickPoint.X;
                t.Y = e.GetPosition(this.ContentRoot).Y - this.clickPoint.Y;
                if (transformGroup != null)
                {
                    transformGroup.Children.Add(t);
                    this.ContentRoot.RenderTransform = transformGroup;
                }
            }
        }

        /// <summary>
        /// Executed when the Closing storyboard ends.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Event args.
        /// </param>
        private void Closing_Completed(object sender, EventArgs e)
        {
            if (this.ChildWindowPopup != null)
            {
                this.ChildWindowPopup.IsOpen = false;
            }

            // AutomationPeer returns "NotResponding" when the FloatableWindow is closed:
            this.InteractionState = WindowInteractionState.NotResponding;

            if (this.closed != null)
            {
                this.closed.Completed -= new EventHandler(this.Closing_Completed);
            }
        }

        /// <summary>
        /// Executed when the ContentPresenter size changes.
        /// </summary>
        /// <param name="sender">
        /// Content Presenter object.
        /// </param>
        /// <param name="e">
        /// SizeChanged event args.
        /// </param>
        private void ContentPresenter_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            // timheuer: not sure really why this is here?
            // if (this.ContentRoot != null && Application.Current != null && Application.Current.RootVisual != null && isOpen)
            // {
            // GeneralTransform gt = this.ContentRoot.TransformToVisual(Application.Current.RootVisual);

            // if (gt != null)
            // {
            // Point p = gt.Transform(new Point(0, 0));

            // double x = this.windowPosition.X - p.X;
            // double y = this.windowPosition.Y - p.Y;
            // UpdateContentRootTransform(x, y);
            // }
            // }

            // RectangleGeometry rg = new RectangleGeometry();
            // rg.Rect = new Rect(0, 0, this.contentPresenter.ActualWidth, this.contentPresenter.ActualHeight);
            // this.contentPresenter.Clip = rg;
            // this.UpdatePosition();
        }

        /// <summary>
        /// The focus window.
        /// </summary>
        private void FocusWindow()
        {
            this.Focus();
            this.BringToFront();
            this.Focus();
        }

        /// <summary>
        /// The get visual state storyboard.
        /// </summary>
        /// <param name="visualStateGroupName">
        /// The visual state group name.
        /// </param>
        /// <param name="visualStateName">
        /// The visual state name.
        /// </param>
        /// <returns>
        /// </returns>
        private Storyboard GetVisualStateStoryboard(string visualStateGroupName, string visualStateName)
        {
            foreach (VisualStateGroup g in VisualStateManager.GetVisualStateGroups((FrameworkElement)this.ContentRoot.Parent))
            {
                if (g.Name != visualStateGroupName)
                {
                    continue;
                }

                foreach (VisualState s in g.States)
                {
                    if (s.Name != visualStateName)
                    {
                        continue;
                    }

                    return s.Storyboard;
                }
            }

            return null;
        }

        /// <summary>
        /// Executed when the opening storyboard finishes.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Event args.
        /// </param>
        private void Opening_Completed(object sender, EventArgs e)
        {
            if (this.opened != null)
            {
                this.opened.Completed -= new EventHandler(this.Opening_Completed);
            }

            // AutomationPeer returns "ReadyForUserInteraction" when the FloatableWindow 
            // is open and all animations have been completed.
            this.InteractionState = WindowInteractionState.ReadyForUserInteraction;
            this.OnOpened();
        }

        /// <summary>
        /// Executed when the page resizes.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Event args.
        /// </param>
        private void Page_Resized(object sender, EventArgs e)
        {
            if (this.ChildWindowPopup != null)
            {
                this.UpdateOverlaySize();
            }
        }

        /// <summary>
        /// The resizer_ mouse enter.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Resizer_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!this.isMouseCaptured)
            {
                this.resizer.Opacity = 1;
            }
        }

        /// <summary>
        /// The resizer_ mouse leave.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Resizer_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!this.isMouseCaptured)
            {
                this.resizer.Opacity = .25;
            }
        }

        /// <summary>
        /// The resizer_ mouse left button down.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Resizer_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.resizer.CaptureMouse();
            this.isMouseCaptured = true;
            this.clickPoint = e.GetPosition(sender as UIElement);
        }

        /// <summary>
        /// The resizer_ mouse left button up.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Resizer_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.resizer.ReleaseMouseCapture();
            this.isMouseCaptured = false;
            this.resizer.Opacity = 0.25;
        }

        private void OnSizeChanged(double height, double width)
        {
            Action<double, double> handler = this.WindowSizeChanged;
            if (handler != null)
            {
                handler.Invoke(height, width);
            }
        }

        /// <summary>
        /// The resizer_ mouse move.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void Resizer_MouseMove(object sender, MouseEventArgs e)
        {
            if (this.isMouseCaptured && this.ContentRoot != null)
            {
                // If the child window is dragged out of the page, return
                if (Application.Current != null && Application.Current.RootVisual != null
                    &&
                    (e.GetPosition(Application.Current.RootVisual).X < 0
                     || e.GetPosition(Application.Current.RootVisual).Y < 0))
                {
                    return;
                }

                Point p = e.GetPosition(this.ContentRoot);

                if ((p.X > this.clickPoint.X) && (p.Y > this.clickPoint.Y))
                {
                    if (this.ResizeMode == ResizeMode.CanResizeHorizontally || this.ResizeMode == ResizeMode.CanResize)
                    {
                        this.Width = (double)(p.X - (12 - this.clickPoint.X));
                    }

                    if (this.ResizeMode == ResizeMode.CanResizeVertically || this.ResizeMode == ResizeMode.CanResize)
                    {
                        this.Height = (double)(p.Y - (12 - this.clickPoint.Y));
                    }
                }
            }
        }

        /// <summary>
        /// Executed when the root visual gets focus.
        /// </summary>
        /// <param name="sender">
        /// Sender object.
        /// </param>
        /// <param name="e">
        /// Routed event args.
        /// </param>
        private void RootVisual_GotFocus(object sender, RoutedEventArgs e)
        {
            this.Focus();
            this.InteractionState = WindowInteractionState.ReadyForUserInteraction;
        }

        /// <summary>
        /// Subscribes to events when the FloatableWindow is opened.
        /// </summary>
        private void SubscribeToEvents()
        {
            if (Application.Current != null && Application.Current.Host != null
                && Application.Current.Host.Content != null)
            {
                Application.Current.Exit += new EventHandler(this.Application_Exit);
                Application.Current.Host.Content.Resized += new EventHandler(this.Page_Resized);
            }

            this.KeyDown += new KeyEventHandler(this.ChildWindow_KeyDown);
            if (this.modal)
            {
                this.LostFocus += new RoutedEventHandler(this.ChildWindow_LostFocus);
            }

            this.SizeChanged += new SizeChangedEventHandler(this.ChildWindow_SizeChanged);
        }

        /// <summary>
        /// Subscribes to events that are on the storyboards. 
        /// Unsubscribing from these events happen in the event handlers individually.
        /// </summary>
        private void SubscribeToStoryBoardEvents()
        {
            if (this.closed != null)
            {
                this.closed.Completed += new EventHandler(this.Closing_Completed);
            }

            if (this.opened != null)
            {
                this.opened.Completed += new EventHandler(this.Opening_Completed);
            }
        }

        /// <summary>
        /// Subscribes to events on the template parts.
        /// </summary>
        private void SubscribeToTemplatePartEvents()
        {
            if (this.CloseButton != null)
            {
                this.CloseButton.Click += new RoutedEventHandler(this.CloseButton_Click);
            }

            if (this.chrome != null)
            {
                this.chrome.MouseLeftButtonDown += new MouseButtonEventHandler(this.Chrome_MouseLeftButtonDown);
                this.chrome.MouseLeftButtonUp += new MouseButtonEventHandler(this.Chrome_MouseLeftButtonUp);
                this.chrome.MouseMove += new MouseEventHandler(this.Chrome_MouseMove);
            }

            if (this.contentPresenter != null)
            {
                this.contentPresenter.SizeChanged += new SizeChangedEventHandler(this.ContentPresenter_SizeChanged);
            }
        }

        /// <summary>
        /// Unsubscribe from events when the FloatableWindow is closed.
        /// </summary>
        private void UnSubscribeFromEvents()
        {
            if (Application.Current != null && Application.Current.Host != null
                && Application.Current.Host.Content != null)
            {
                Application.Current.Exit -= new EventHandler(this.Application_Exit);
                Application.Current.Host.Content.Resized -= new EventHandler(this.Page_Resized);
            }

            this.KeyDown -= new KeyEventHandler(this.ChildWindow_KeyDown);
            if (this.modal)
            {
                this.LostFocus -= new RoutedEventHandler(this.ChildWindow_LostFocus);
            }

            this.SizeChanged -= new SizeChangedEventHandler(this.ChildWindow_SizeChanged);
        }

        /// <summary>
        /// Unsubscribe from the events that are subscribed on the template part elements.
        /// </summary>
        private void UnsubscribeFromTemplatePartEvents()
        {
            if (this.CloseButton != null)
            {
                this.CloseButton.Click -= new RoutedEventHandler(this.CloseButton_Click);
            }

            if (this.chrome != null)
            {
                this.chrome.MouseLeftButtonDown -= new MouseButtonEventHandler(this.Chrome_MouseLeftButtonDown);
                this.chrome.MouseLeftButtonUp -= new MouseButtonEventHandler(this.Chrome_MouseLeftButtonUp);
                this.chrome.MouseMove -= new MouseEventHandler(this.Chrome_MouseMove);
            }

            if (this.contentPresenter != null)
            {
                this.contentPresenter.SizeChanged -= new SizeChangedEventHandler(this.ContentPresenter_SizeChanged);
            }
        }

        /// <summary>
        /// Updates the ContentRootTranslateTransform.
        /// </summary>
        /// <param name="X">
        /// X coordinate of the transform.
        /// </param>
        /// <param name="Y">
        /// Y coordinate of the transform.
        /// </param>
        private void UpdateContentRootTransform(double x, double y)
        {
            if (this.contentRootTransform == null)
            {
                this.contentRootTransform = new TranslateTransform();
                this.contentRootTransform.X = x;
                this.contentRootTransform.Y = y;

                TransformGroup transformGroup = this.ContentRoot.RenderTransform as TransformGroup;

                if (transformGroup == null)
                {
                    transformGroup = new TransformGroup();
                    transformGroup.Children.Add(this.ContentRoot.RenderTransform);
                }

                transformGroup.Children.Add(this.contentRootTransform);
                this.ContentRoot.RenderTransform = transformGroup;
            }
            else
            {
                this.contentRootTransform.X += x;
                this.contentRootTransform.Y += y;
            }
        }

        /// <summary>
        /// Updates the size of the overlay of the window.
        /// </summary>
        private void UpdateOverlaySize()
        {
            if (this.modal)
            {
                if (this.Overlay != null && Application.Current != null && Application.Current.Host != null
                    && Application.Current.Host.Content != null)
                {
                    this.Height = Application.Current.Host.Content.ActualHeight;
                    this.Width = Application.Current.Host.Content.ActualWidth;
                    this.Overlay.Height = this.Height;
                    this.Overlay.Width = this.Width;

                    if (this.ContentRoot != null)
                    {
                        this.ContentRoot.Width = this.desiredContentWidth;
                        this.ContentRoot.Height = this.desiredContentHeight;
                        this.ContentRoot.Margin = this.desiredMargin;
                    }
                }
            }
            else
            {
                if (this.Overlay != null)
                {
                    this.Overlay.Visibility = Visibility.Collapsed;
                }
            }
        }

        /// <summary>
        /// Updates the position of the window in case the size of the content changes.
        /// This allows FloatableWindow only scale from right and bottom.
        /// </summary>
        private void UpdatePosition()
        {
            if (this.ContentRoot != null && Application.Current != null && Application.Current.RootVisual != null)
            {
                GeneralTransform gt = this.ContentRoot.TransformToVisual(Application.Current.RootVisual);

                if (gt != null)
                {
                    this.windowPosition = gt.Transform(new Point(0, 0));
                }
            }
        }

        /// <summary>
        /// Updates the render transform applied on the overlay.
        /// </summary>
        private void UpdateRenderTransform()
        {
            if (this.root != null && this.ContentRoot != null)
            {
                // The Overlay part should not be affected by the render transform applied on the
                // FloatableWindow. In order to achieve this, we adjust an identity matrix to represent
                // the root's transformation, invert it, apply the inverted matrix on the root, so that 
                // nothing is affected by the rendertransform, and apply the original transform only on the Content
                GeneralTransform gt = this.root.TransformToVisual(null);
                if (gt != null)
                {
                    Point p10 = new Point(1, 0);
                    Point p01 = new Point(0, 1);
                    Point transform10 = gt.Transform(p10);
                    Point transform01 = gt.Transform(p01);

                    Matrix transformToRootMatrix = Matrix.Identity;
                    transformToRootMatrix.M11 = transform10.X;
                    transformToRootMatrix.M12 = transform10.Y;
                    transformToRootMatrix.M21 = transform01.X;
                    transformToRootMatrix.M22 = transform01.Y;

                    MatrixTransform original = new MatrixTransform();
                    original.Matrix = transformToRootMatrix;

                    InvertMatrix(ref transformToRootMatrix);
                    MatrixTransform mt = new MatrixTransform();
                    mt.Matrix = transformToRootMatrix;

                    TransformGroup tg = this.root.RenderTransform as TransformGroup;

                    if (tg != null)
                    {
                        tg.Children.Add(mt);
                    }
                    else
                    {
                        this.root.RenderTransform = mt;
                    }

                    tg = this.ContentRoot.RenderTransform as TransformGroup;

                    if (tg != null)
                    {
                        tg.Children.Add(original);
                    }
                    else
                    {
                        this.ContentRoot.RenderTransform = original;
                    }
                }
            }
        }
    }
}