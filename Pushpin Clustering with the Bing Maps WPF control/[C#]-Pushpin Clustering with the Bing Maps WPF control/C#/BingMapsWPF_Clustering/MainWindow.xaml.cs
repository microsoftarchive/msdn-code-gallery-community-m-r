using ClusterEngine;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;

namespace BingMapsWPF_Clustering
{
    public partial class MainWindow : Window
    {
        private List<Entity> _mockData;
        private ClusterOptions _options;
        private bool _generatingData;
        private BackgroundWorker _worker;

        public MainWindow()
        {
            InitializeComponent();

            _options = new MyClusterOptions(45);

            //Background worker for generating mock data
            _worker = new BackgroundWorker();
            _worker.DoWork += (s, a) =>
            {
                GenerateMockData((int)a.Argument);
            };
            _worker.RunWorkerCompleted += (s, a) =>
            {
                _generatingData = false;
                StatusTbx.Text += "Mock data generated.\r\n";
            };
        }

        #region Button Handlers

        private void GenerateData_Clicked(object sender, RoutedEventArgs e)
        {
            MyMap.Children.Clear();

            int size;

            if (string.IsNullOrWhiteSpace(EntitySize.Text) ||
                !int.TryParse(EntitySize.Text, out size))
            {
                StatusTbx.Text += "Invalid mock data size.\r\n";
                return;
            }

            StatusTbx.Text += "Generating Mock data.\r\n";

            _worker.RunWorkerAsync(size);            
        }

        private void ViewAllData_Clicked(object sender, RoutedEventArgs e)
        {
            MyMap.Children.Clear();

            if (_generatingData)
            {
                StatusTbx.Text += "Mock data is still generating.\r\n";
                return;
            }

            if (_mockData == null)
            {
                StatusTbx.Text += "No mock data generated.\r\n";
                return;
            }

            foreach (var entity in _mockData)
            {
                MyMap.Children.Add(_options.RenderEntity(entity));
            }

            StatusTbx.Text += "All data displayed without clustering.\r\n";
        }

        private void PointClusterData_Clicked(object sender, RoutedEventArgs e)
        {
            MyMap.Children.Clear();

            if (_generatingData)
            {
                StatusTbx.Text += "Mock data is still generating.\r\n";
                return;
            }

            if (_mockData == null)
            {
                StatusTbx.Text += "No mock data generated.\r\n";
                return;
            }

            //Create an instance of the Point Based clustering layer
            var layer = new PointBasedClusteredLayer(MyMap, _options);
            
            //Get the map layer from the clustered layer and add to map
            MyMap.Children.Add(layer.GetMapLayer());

            //Add mock data to cluster layer
            layer.AddEntities(_mockData);

            StatusTbx.Text += "Point based clustering is enabled.\r\n";
        }

        private void GridClusterData_Clicked(object sender, RoutedEventArgs e)
        {
            MyMap.Children.Clear();

            if (_generatingData)
            {
                StatusTbx.Text += "Mock data is still generating.\r\n";
                return;
            }

            if (_mockData == null)
            {
                StatusTbx.Text += "No mock data generated.\r\n";
                return;
            }

            //Create an instance of the Grid Based clustering layer
            var layer = new GridBasedClusteredLayer(MyMap, _options);

            //Get the map layer from the clustered layer and add to map
            MyMap.Children.Add(layer.GetMapLayer());

            //Add mock data to cluster layer
            layer.AddEntities(_mockData);

            StatusTbx.Text += "Grid based clustering is enabled.\r\n";
        }

        #endregion

        #region Private Helper Methods

        /// <summary>
        /// Method that generates mock Entity data
        /// </summary>
        private void GenerateMockData(int numEntities)
        {
            if (_mockData != null)
            {
                _mockData.Clear();
            }
            else
            {
                _mockData = new List<Entity>();
            }

            Random rand = new Random();

            for (int i = 0; i < numEntities; i++)
            {
                _mockData.Add(new CustomEntity()
                {
                    ID = i,
                    Location = new Location()
                    {
                        Latitude = rand.NextDouble() * 180 - 90,
                        Longitude = rand.NextDouble() * 360 - 180
                    },
                    Title = string.Format("Entity: {0}", i)
                });
            }
        }

        #endregion
    }
}
