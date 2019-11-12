using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace ClusterEngine
{
    /// <summary>
    /// This class uses the Point Based clustering algorithm to sort data into clusters based on zoom level 
    /// and is derived from this code base: http://bingmapsv7modules.codeplex.com/wikipage?title=Point%20Based%20Clustering
    /// 
    /// This algorithm sorts all data into clusters once per zoom level. This makes for fast refresh rates 
    /// of data one clustered zoom levels and makes for a smooth user experience as the location of a 
    /// cluster is only calculated once per zoom level. However the trade off of this enhanced user 
    /// experience is that zooming into new zoom levels can result in a slow refresh the first time as that
    /// new zoom level is being clustered. As such this algorithm is recommended for data sets of 10,000 or less.
    /// </summary>
    public class PointBasedClusteredLayer : IClusteredLayer
    {
        #region Private Properties

        private Map _map;
        private MapLayer _baseLayer = new MapLayer();
        private int _currentZoomLevel;

        private const int _maxZoomLevel = 21;

        private ClusterOptions _options;

        private List<Entity> _entities;
        private IList<ClusteredPoint>[] _clusteredData;
        private bool[] _zoomLocked;

        #endregion

        #region Constructor

        public PointBasedClusteredLayer(Map map, ClusterOptions options)
            : base()
        {
            _options = options;

            _entities = new List<Entity>();

            //Initialize clustered data dictionary for 21 zoom levels
            _clusteredData = new IList<ClusteredPoint>[_maxZoomLevel + 1];
            _zoomLocked = new bool[_maxZoomLevel + 1];

            for (int z = 1; z <= _maxZoomLevel; z++)
            {
                _clusteredData[z] = new List<ClusteredPoint>();
            }

            _map = map;
            _currentZoomLevel = (int)Math.Round(_map.ZoomLevel);
            _map.ViewChangeEnd += (s, a) =>
            {
                int zoom = (int)Math.Round(_map.ZoomLevel);

                if (zoom == _currentZoomLevel)
                {
                    //Handle Pan Event
                    Render();
                }
                else
                {
                    _currentZoomLevel = zoom;

                    //Handle Zoom event
                    if (zoom <= _maxZoomLevel)
                    {
                        if (!_zoomLocked[zoom] && (_clusteredData[zoom] == null || _clusteredData[zoom].Count == 0))
                        {
                            Cluster(zoom);
                        }
                    }
                }               
            };
            _map.SizeChanged += (s, a) =>
            {
                Render();
            };
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Add an entity to the layer.
        /// </summary>
        /// <param name="item">An object that inherits from the Entity Class</param>
        public void AddEntity(Entity item)
        {
            _entities.Add(item);

            RefreshClusters();
        }

        /// <summary>
        /// Add multiple entities to the layer.
        /// </summary>
        /// <param name="items">A list of objects that inherit from the Entity Class</param>
        public void AddEntities(IList<Entity> items)
        {
            foreach (var i in items)
            {
                _entities.Add(i);
            }

            RefreshClusters();
        }

        /// <summary>
        /// Clear the layer
        /// </summary>
        public void Clear()
        {
            _entities.Clear();

            RefreshClusters();
        }

        /// <summary>
        /// Get an Entity by id.
        /// </summary>
        /// <param name="id">ID of the entity to get</param>
        /// <returns>An object that inherits from the Entity Class</returns>
        public Entity GetEntity(int id)
        {
            return (from e in _entities
                    where e.ID == id
                    select e).FirstOrDefault();
        }

        /// <summary>
        /// Get a list of Entities by id.
        /// </summary>
        /// <param name="id">A list of entity ID's to get</param>
        /// <returns>A list of objects that inherits from the Entity Class</returns>
        public IList<Entity> GetEntities(IList<int> ids)
        {
            return (from e in _entities
                    where ids.Contains(e.ID)
                    select e).ToList();
        }

        /// <summary>
        /// Gets a reference to the base MapLayer used for visualizing clusters. 
        /// </summary>
        /// <returns>MapLayer of clustered data</returns>
        public MapLayer GetMapLayer()
        {
            return _baseLayer;
        }

        /// <summary>
        /// Method to set the cluster options.
        /// </summary>
        /// <param name="options">Cluster options to use when clustering.</param>
        public void SetOptions(ClusterOptions options)
        {
            _options = options;

            RefreshClusters();
        }

        #endregion

        #region Private Methods

        private void RefreshClusters()
        {
            for (var i = 1; i <= _maxZoomLevel; i++)
            {
                _clusteredData[i].Clear();
            }

            int zoom = (int)Math.Round(_map.ZoomLevel);

            Cluster(zoom);
        }

        private void Cluster(int zoom)
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.DoWork += (s, a) =>
            {
                _zoomLocked[zoom] = true;

                if (_clusteredData[zoom] == null)
                {
                    _clusteredData[zoom] = new List<ClusteredPoint>();
                }

                if (_entities != null && _entities.Count > 0)
                {
                    double tileZoomRatio = 256 * Math.Pow(2, zoom);
                    Point pixel;
                    bool IsInCluster;

                    //Itirate through the data
                    foreach (var entity in _entities)
                    {
                        IsInCluster = false;
                        pixel = Helpers.CalculateGlobalPixel(entity.Location, tileZoomRatio);

                        foreach (var cluster in _clusteredData[zoom])
                        {
                            //See if pixel fits into any existing clusters
                            if (pixel.Y >= cluster.Top && pixel.Y <= cluster.Bottom &&
                                ((cluster.Left <= cluster.Right && pixel.X >= cluster.Left && pixel.X <= cluster.Right) ||
                                (cluster.Left >= cluster.Right && (pixel.X >= cluster.Left || pixel.X <= cluster.Right))))
                            {
                                cluster.EntityIds.Add(entity.ID);
                                IsInCluster = true;
                                break;
                            }
                        }

                        //If entity is not in a cluster then it does not fit an existing cluster
                        if (!IsInCluster)
                        {
                            ClusteredPoint cluster = new ClusteredPoint();

                            cluster.Location = entity.Location;
                            cluster.Left = pixel.X - _options.ClusterRadius;
                            cluster.Right = pixel.X + _options.ClusterRadius;
                            cluster.Top = pixel.Y - _options.ClusterRadius;
                            cluster.Bottom = pixel.Y + _options.ClusterRadius;
                            cluster.Zoom = zoom;
                            cluster.EntityIds = new List<int>(){
                            entity.ID
                        };

                            if (cluster.Left < 0)
                            {
                                cluster.Left += tileZoomRatio;
                            }

                            if (cluster.Right > tileZoomRatio)
                            {
                                cluster.Right -= tileZoomRatio;
                            }

                            _clusteredData[zoom].Add(cluster);
                        }
                    }
                }         
            };
            worker.RunWorkerCompleted += (s, a) =>
            {
                _zoomLocked[zoom] = false;

                Render();  
            };
            worker.RunWorkerAsync();
        }

        private void Render()
        {
            if (!_zoomLocked[_currentZoomLevel])
            {
                var bounds = _map.BoundingRectangle;

                var clusters = (from c in _clusteredData[_currentZoomLevel]
                                where c.Location.Latitude <= bounds.North &&
                                    c.Location.Latitude >= bounds.South &&
                                    ((bounds.West > bounds.East && c.Location.Longitude >= bounds.East || c.Location.Longitude <= bounds.West) ||
                                    (bounds.West < bounds.East && c.Location.Longitude <= bounds.East && c.Location.Longitude >= bounds.West))
                                select c).ToList<ClusteredPoint>();

                _baseLayer.Children.Clear();

                foreach (var c in clusters)
                {
                    if (c.EntityIds.Count == 1)
                    {
                        var entity = (from e in _entities
                                      where e.ID == c.EntityIds[0]
                                      select e).FirstOrDefault();

                        if (entity != null)
                        {
                            var p = _options.RenderEntity(entity);
                            _baseLayer.Children.Add(p);
                        }
                    }
                    else
                    {
                        var p = _options.RenderCluster(c);
                        _baseLayer.Children.Add(p);
                    }
                }
            }
        }

        #endregion
    }
}