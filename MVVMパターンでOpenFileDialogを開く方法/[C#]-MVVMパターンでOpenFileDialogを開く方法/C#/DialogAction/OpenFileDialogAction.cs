namespace DialogAction
{
    using System.Windows;
    using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
    using Microsoft.Win32;
    using Okazuki.MVVM.PrismSupport.Interactivity;

    public class OpenFileDialogAction : DispatcherTriggerAction
    {
        private static readonly OpenFileDialogConfirmation NullObject = new OpenFileDialogConfirmation();

        #region OpenFileDialogのプロパティ(自動生成)
        public static readonly DependencyProperty MultiselectProperty =
            DependencyProperty.Register(
                "Multiselect",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? Multiselect
        {
            get
            {
                return (System.Boolean?)this.GetValue(MultiselectProperty);
            }

            set
            {
                this.SetValue(MultiselectProperty, value);
            }
        }

        public static readonly DependencyProperty ReadOnlyCheckedProperty =
            DependencyProperty.Register(
                "ReadOnlyChecked",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? ReadOnlyChecked
        {
            get
            {
                return (System.Boolean?)this.GetValue(ReadOnlyCheckedProperty);
            }

            set
            {
                this.SetValue(ReadOnlyCheckedProperty, value);
            }
        }

        public static readonly DependencyProperty ShowReadOnlyProperty =
            DependencyProperty.Register(
                "ShowReadOnly",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? ShowReadOnly
        {
            get
            {
                return (System.Boolean?)this.GetValue(ShowReadOnlyProperty);
            }

            set
            {
                this.SetValue(ShowReadOnlyProperty, value);
            }
        }

        public static readonly DependencyProperty AddExtensionProperty =
            DependencyProperty.Register(
                "AddExtension",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? AddExtension
        {
            get
            {
                return (System.Boolean?)this.GetValue(AddExtensionProperty);
            }

            set
            {
                this.SetValue(AddExtensionProperty, value);
            }
        }

        public static readonly DependencyProperty CheckFileExistsProperty =
            DependencyProperty.Register(
                "CheckFileExists",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? CheckFileExists
        {
            get
            {
                return (System.Boolean?)this.GetValue(CheckFileExistsProperty);
            }

            set
            {
                this.SetValue(CheckFileExistsProperty, value);
            }
        }

        public static readonly DependencyProperty CheckPathExistsProperty =
            DependencyProperty.Register(
                "CheckPathExists",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? CheckPathExists
        {
            get
            {
                return (System.Boolean?)this.GetValue(CheckPathExistsProperty);
            }

            set
            {
                this.SetValue(CheckPathExistsProperty, value);
            }
        }

        public static readonly DependencyProperty DefaultExtProperty =
            DependencyProperty.Register(
                "DefaultExt",
                typeof(System.String),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String DefaultExt
        {
            get
            {
                return (System.String)this.GetValue(DefaultExtProperty);
            }

            set
            {
                this.SetValue(DefaultExtProperty, value);
            }
        }

        public static readonly DependencyProperty DereferenceLinksProperty =
            DependencyProperty.Register(
                "DereferenceLinks",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? DereferenceLinks
        {
            get
            {
                return (System.Boolean?)this.GetValue(DereferenceLinksProperty);
            }

            set
            {
                this.SetValue(DereferenceLinksProperty, value);
            }
        }

        public static readonly DependencyProperty SafeFileNameProperty =
            DependencyProperty.Register(
                "SafeFileName",
                typeof(System.String),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String SafeFileName
        {
            get
            {
                return (System.String)this.GetValue(SafeFileNameProperty);
            }

            set
            {
                this.SetValue(SafeFileNameProperty, value);
            }
        }

        public static readonly DependencyProperty SafeFileNamesProperty =
            DependencyProperty.Register(
                "SafeFileNames",
                typeof(System.String[]),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String[] SafeFileNames
        {
            get
            {
                return (System.String[])this.GetValue(SafeFileNamesProperty);
            }

            set
            {
                this.SetValue(SafeFileNamesProperty, value);
            }
        }

        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register(
                "FileName",
                typeof(System.String),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String FileName
        {
            get
            {
                return (System.String)this.GetValue(FileNameProperty);
            }

            set
            {
                this.SetValue(FileNameProperty, value);
            }
        }

        public static readonly DependencyProperty FileNamesProperty =
            DependencyProperty.Register(
                "FileNames",
                typeof(System.String[]),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String[] FileNames
        {
            get
            {
                return (System.String[])this.GetValue(FileNamesProperty);
            }

            set
            {
                this.SetValue(FileNamesProperty, value);
            }
        }

        public static readonly DependencyProperty FilterProperty =
            DependencyProperty.Register(
                "Filter",
                typeof(System.String),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String Filter
        {
            get
            {
                return (System.String)this.GetValue(FilterProperty);
            }

            set
            {
                this.SetValue(FilterProperty, value);
            }
        }

        public static readonly DependencyProperty FilterIndexProperty =
            DependencyProperty.Register(
                "FilterIndex",
                typeof(System.Int32?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Int32? FilterIndex
        {
            get
            {
                return (System.Int32?)this.GetValue(FilterIndexProperty);
            }

            set
            {
                this.SetValue(FilterIndexProperty, value);
            }
        }

        public static readonly DependencyProperty InitialDirectoryProperty =
            DependencyProperty.Register(
                "InitialDirectory",
                typeof(System.String),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String InitialDirectory
        {
            get
            {
                return (System.String)this.GetValue(InitialDirectoryProperty);
            }

            set
            {
                this.SetValue(InitialDirectoryProperty, value);
            }
        }

        public static readonly DependencyProperty RestoreDirectoryProperty =
            DependencyProperty.Register(
                "RestoreDirectory",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? RestoreDirectory
        {
            get
            {
                return (System.Boolean?)this.GetValue(RestoreDirectoryProperty);
            }

            set
            {
                this.SetValue(RestoreDirectoryProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register(
                "Title",
                typeof(System.String),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.String Title
        {
            get
            {
                return (System.String)this.GetValue(TitleProperty);
            }

            set
            {
                this.SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty ValidateNamesProperty =
            DependencyProperty.Register(
                "ValidateNames",
                typeof(System.Boolean?),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Boolean? ValidateNames
        {
            get
            {
                return (System.Boolean?)this.GetValue(ValidateNamesProperty);
            }

            set
            {
                this.SetValue(ValidateNamesProperty, value);
            }
        }

        public static readonly DependencyProperty CustomPlacesProperty =
            DependencyProperty.Register(
                "CustomPlaces",
                typeof(System.Collections.Generic.IList<Microsoft.Win32.FileDialogCustomPlace>),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Collections.Generic.IList<Microsoft.Win32.FileDialogCustomPlace> CustomPlaces
        {
            get
            {
                return (System.Collections.Generic.IList<Microsoft.Win32.FileDialogCustomPlace>)this.GetValue(CustomPlacesProperty);
            }

            set
            {
                this.SetValue(CustomPlacesProperty, value);
            }
        }

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.Register(
                "Tag",
                typeof(System.Object),
                typeof(OpenFileDialogAction),
                new PropertyMetadata(null));

        public System.Object Tag
        {
            get
            {
                return (System.Object)this.GetValue(TagProperty);
            }

            set
            {
                this.SetValue(TagProperty, value);
            }
        }

        #endregion

        protected override void InvokeAction(InteractionRequestedEventArgs e)
        {
            var n = e.Context as OpenFileDialogConfirmation ?? NullObject;
            var dlg = new OpenFileDialog();

            this.ApplyMessagePropertyeValues(n, dlg);

            var conf = e.Context as Confirmation;

            // キャンセル時
            if (dlg.ShowDialog() != true)
            {
                if (conf != null)
                {
                    conf.Confirmed = false;
                }
                e.Callback();
                return;
            }

            this.ApplyDialogPropertyValues(n, dlg);

            if (conf != null)
            {
                conf.Confirmed = true;
            }
            e.Callback();
        }

        private void ApplyMessagePropertyeValues(OpenFileDialogConfirmation n, OpenFileDialog dlg)
        {
            #region OpenFileDialogのプロパティのセット(自動生成)
            dlg.Multiselect = n.Multiselect ?? this.Multiselect ?? dlg.Multiselect;
            dlg.ReadOnlyChecked = n.ReadOnlyChecked ?? this.ReadOnlyChecked ?? dlg.ReadOnlyChecked;
            dlg.ShowReadOnly = n.ShowReadOnly ?? this.ShowReadOnly ?? dlg.ShowReadOnly;
            dlg.AddExtension = n.AddExtension ?? this.AddExtension ?? dlg.AddExtension;
            dlg.CheckFileExists = n.CheckFileExists ?? this.CheckFileExists ?? dlg.CheckFileExists;
            dlg.CheckPathExists = n.CheckPathExists ?? this.CheckPathExists ?? dlg.CheckPathExists;
            dlg.DefaultExt = n.DefaultExt ?? this.DefaultExt ?? dlg.DefaultExt;
            dlg.DereferenceLinks = n.DereferenceLinks ?? this.DereferenceLinks ?? dlg.DereferenceLinks;
            dlg.FileName = n.FileName ?? this.FileName ?? dlg.FileName;
            dlg.Filter = n.Filter ?? this.Filter ?? dlg.Filter;
            dlg.FilterIndex = n.FilterIndex ?? this.FilterIndex ?? dlg.FilterIndex;
            dlg.InitialDirectory = n.InitialDirectory ?? this.InitialDirectory ?? dlg.InitialDirectory;
            dlg.RestoreDirectory = n.RestoreDirectory ?? this.RestoreDirectory ?? dlg.RestoreDirectory;
            dlg.Title = n.Title ?? this.Title ?? dlg.Title;
            dlg.ValidateNames = n.ValidateNames ?? this.ValidateNames ?? dlg.ValidateNames;
            dlg.CustomPlaces = n.CustomPlaces ?? this.CustomPlaces ?? dlg.CustomPlaces;
            dlg.Tag = n.Tag ?? this.Tag ?? dlg.Tag;
            #endregion
        }

        private void ApplyDialogPropertyValues(OpenFileDialogConfirmation n, OpenFileDialog dlg)
        {
            #region OpenFileDialogからNotificationへのプロパティのコピー(自動生成)
            if (n != NullObject)
            {
                n.Multiselect = dlg.Multiselect;
            }
            this.Multiselect = dlg.Multiselect;
            if (n != NullObject)
            {
                n.ReadOnlyChecked = dlg.ReadOnlyChecked;
            }
            this.ReadOnlyChecked = dlg.ReadOnlyChecked;
            if (n != NullObject)
            {
                n.ShowReadOnly = dlg.ShowReadOnly;
            }
            this.ShowReadOnly = dlg.ShowReadOnly;
            if (n != NullObject)
            {
                n.AddExtension = dlg.AddExtension;
            }
            this.AddExtension = dlg.AddExtension;
            if (n != NullObject)
            {
                n.CheckFileExists = dlg.CheckFileExists;
            }
            this.CheckFileExists = dlg.CheckFileExists;
            if (n != NullObject)
            {
                n.CheckPathExists = dlg.CheckPathExists;
            }
            this.CheckPathExists = dlg.CheckPathExists;
            if (n != NullObject)
            {
                n.DefaultExt = dlg.DefaultExt;
            }
            this.DefaultExt = dlg.DefaultExt;
            if (n != NullObject)
            {
                n.DereferenceLinks = dlg.DereferenceLinks;
            }
            this.DereferenceLinks = dlg.DereferenceLinks;
            if (n != NullObject)
            {
                n.SafeFileName = dlg.SafeFileName;
            }
            this.SafeFileName = dlg.SafeFileName;
            if (n != NullObject)
            {
                n.SafeFileNames = dlg.SafeFileNames;
            }
            this.SafeFileNames = dlg.SafeFileNames;
            if (n != NullObject)
            {
                n.FileName = dlg.FileName;
            }
            this.FileName = dlg.FileName;
            if (n != NullObject)
            {
                n.FileNames = dlg.FileNames;
            }
            this.FileNames = dlg.FileNames;
            if (n != NullObject)
            {
                n.Filter = dlg.Filter;
            }
            this.Filter = dlg.Filter;
            if (n != NullObject)
            {
                n.FilterIndex = dlg.FilterIndex;
            }
            this.FilterIndex = dlg.FilterIndex;
            if (n != NullObject)
            {
                n.InitialDirectory = dlg.InitialDirectory;
            }
            this.InitialDirectory = dlg.InitialDirectory;
            if (n != NullObject)
            {
                n.RestoreDirectory = dlg.RestoreDirectory;
            }
            this.RestoreDirectory = dlg.RestoreDirectory;
            if (n != NullObject)
            {
                n.Title = dlg.Title;
            }
            this.Title = dlg.Title;
            if (n != NullObject)
            {
                n.ValidateNames = dlg.ValidateNames;
            }
            this.ValidateNames = dlg.ValidateNames;
            if (n != NullObject)
            {
                n.CustomPlaces = dlg.CustomPlaces;
            }
            this.CustomPlaces = dlg.CustomPlaces;
            if (n != NullObject)
            {
                n.Tag = dlg.Tag;
            }
            this.Tag = dlg.Tag;

            #endregion
        }
    }
}
