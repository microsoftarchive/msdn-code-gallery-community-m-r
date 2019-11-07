using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using MyEvents.Api.Client;
using MyEvents.Client.Organizer.Desktop.ViewModel;

namespace MyEvents.Client.Organizer.Desktop.Views
{
    /// <summary>
    /// Interaction logic for VManageMap.xaml
    /// </summary>
    public partial class VManageMap : UserControl
    {
        private bool _firstClick = false;

        /// <summary>
        /// Constructor
        /// </summary>
        public VManageMap(EventDefinition eventDefinition)
        {
            InitializeComponent();

            var _dataContext = (this.DataContext as ManageMapViewModel);

            _dataContext.DeletedRoomPointsEvent += VManageMap_DeletedRoomPointsEvent;

            _dataContext.Event = eventDefinition;

            for (int i = 1; i < eventDefinition.RoomNumber+1; i++)
            {
                Polygon polygon = new Polygon()
                {
                    Name = string.Format("room{0}polygon", i),
                    Fill = (SolidColorBrush)FindResource(string.Format("Room{0}Color", i)),
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 0.3,
                };

                Binding binding = new Binding(string.Format("EventRooms[{0}]", i));
                binding.Mode = BindingMode.TwoWay;

                polygon.SetBinding(Polygon.PointsProperty, binding);

                canvasDrawing.Children.Add(polygon);

                RadioButton RoomButton = new RadioButton()
                {
                    Name = string.Format("room{0}radio", i),
                    Content = string.Format("room {0}", i),
                    Template = (ControlTemplate)FindResource("RadioButtonTemplate"),
                    Foreground = (SolidColorBrush)FindResource(string.Format("Room{0}Color", i)),
                    Tag = i
                };

                RoomButton.Checked += RoomButton_Checked;

                stackRadioButtons.Children.Add(RoomButton);
            }
        }

        void RoomButton_Checked(object sender, RoutedEventArgs e)
        {
            _firstClick = true;
        }

        void VManageMap_DeletedRoomPointsEvent(int room)
        {
            if (room != 0)
            {
                UpdatePolygonBinding(room);
            }
        }

        private void canvasDrawing_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            var _pos = e.GetPosition(canvasDrawing);

            int _roomNumber = FindActiveRoom(stackRadioButtons);

            if (_roomNumber != 0)
            {
                var _dataContext = (this.DataContext as ManageMapViewModel);
                if (_firstClick)
                {
                    _firstClick = false;
                    _dataContext.DeleteRoomPointsCommand.Execute(_roomNumber);
                }
                _dataContext.AddPoint(_roomNumber, _pos);

                UpdatePolygonBinding(_roomNumber);
            }
        }

        private int FindActiveRoom(DependencyObject _parent)
        {
            int _childs = VisualTreeHelper.GetChildrenCount(_parent);

            for (int i = 0; i < _childs; i++)
            {
                RadioButton _radio = (RadioButton)VisualTreeHelper.GetChild(_parent, i);
                if (_radio.IsChecked.Value)
                    return (int)_radio.Tag;
            }

            return 0;
        }

        private Polygon FindPolygonByName(string name)
        {
            int _childs = VisualTreeHelper.GetChildrenCount(canvasDrawing);

            for (int i = 0; i < _childs; i++)
            {
                Polygon _polygon = (Polygon)VisualTreeHelper.GetChild(canvasDrawing, i);
                if (_polygon.Name == name)
                    return (Polygon)_polygon;
            }

            return null;
        }

        private void UpdatePolygonBinding(int _room)
        {
            string _polyName = string.Format("room{0}polygon", _room);
            Polygon polygon = FindPolygonByName(_polyName);

            if (polygon != null)
            {
                Binding binding = new Binding(string.Format("EventRooms[{0}]", _room));
                binding.Mode = BindingMode.TwoWay;
                polygon.ClearValue(Polygon.PointsProperty);
                polygon.SetBinding(Polygon.PointsProperty, binding);

                polygon.UpdateLayout();
            }
        }
    }
}
