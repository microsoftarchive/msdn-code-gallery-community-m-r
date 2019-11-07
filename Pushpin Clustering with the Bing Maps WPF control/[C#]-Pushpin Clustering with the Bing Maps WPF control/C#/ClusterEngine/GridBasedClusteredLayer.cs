using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace ClusterEngine
{
    /// <summary>
    /// This class uses the Grid Based clustering algorithm to sort data into clusters based on the current map view 
    /// and is derived from this code base: http://bingmapsv7modules.codeplex.com/wikipage?title=Client%20Side%20Clustering
    /// 
    /// This algorithm is very fast and clusters the data that is only in the current map view. This algorithm 
    /// recalculates the clusters every time the map moves (both when zooming and panning). As a result this causes 
    /// the grid cells that are used for calculating clusters to move and thus results in some clusters moving on 
    /// the screen when the map is moved the slighest bit. This makes for a less ideal user experience however this 
    /// is a trade off for performance. This algorithm is recommended for data sets of 5,000,000 data points or less.
    /// </summary>
    public class GridBasedClusteredLayer : IClusteredLayer
    {       
        #region Private Properties

        private Map _map;
        private MapLayer _baseLayer = new MapLayer();

        private BackgroundWorker _worker;

        private const int _maxZoomLevel = 21;

        private ClusterOptions _options;

        private List<Entity> _entities;

        /* Constants used to speed up calculations*/
        private double PiBy180 = (Math.PI / 180),
            OneBy4PI = 1 / (4 * Math.PI);

        #endregion

        #region Constructor

        public GridBasedClusteredLayer(Map map, ClusterOptions options)
            : base()
        {
            _options = options;

            _entities = new List<Entity>();

            _map = map;
            _map.ViewChangeEnd += (s, a) =>
            {
                Cluster();            
            };
            _map.SizeChanged += (s, a) =>
            {
                Cluster(); 
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

            Cluster();
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

            Cluster();
        }

        /// <summary>
        /// Clear the layer
        /// </summary>
        public void Clear()
        {
            _entities.Clear();

            Cluster();
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

            Cluster();
        }

        #endregion

        #region Private Methods

        private void Cluster()
        {
            int zoom = (int)Math.Round(_map.ZoomLevel);

            if (_worker == null)
            {
                _worker = new BackgroundWorker();
                _worker.WorkerSupportsCancellation = true;
                _worker.DoWork += (s, a) =>
                {
                    if (_entities != null && _entities.Count > 0)
                    {                        
                        int gridSize = _options.ClusterRadius * 2;
                        int numXCells = (int)Math.Ceiling(_map.ViewportSize.Width / gridSize);
                        int numYCells = (int)Math.Ceiling(_map.ViewportSize.Height / gridSize);

                        int numCells = numXCells * numYCells;

                        ClusteredPoint[] clusteredData = new ClusteredPoint[numCells];

                        Point pixel;
                        int k, j, key;

                        int maxX = (int)Math.Ceiling(_map.ViewportSize.Width + _options.ClusterRadius);
                        int maxY = (int)Math.Ceiling(_map.ViewportSize.Height + _options.ClusterRadius);

                        //Itirate through the data
                        foreach (var entity in _entities)
                        {
                            if (_worker.CancellationPending)
                            {
                                a.Cancel = true;
                                break;
                            }

                            pixel = _map.LocationToViewportPoint(entity.Location);

                            //Check to see if the pin is within the bounds of the viewable map
                            if (pixel != null && pixel.X <= maxX && pixel.Y <= maxY && pixel.X >= -_options.ClusterRadius && pixel.Y >= -_options.ClusterRadius)
                            {
                                //calculate the grid position on the map of where the location is located
                                k = (int)Math.Floor(pixel.X / gridSize);
                                j = (int)Math.Floor(pixel.Y / gridSize);

                                //calculates the grid location in the array
                                key = k + j * numXCells;

                                if (key >= 0 && key < numCells)
                                {
                                    if (clusteredData[key] == null)
                                    {
                                        clusteredData[key] = new ClusteredPoint()
                                        {
                                            Location = entity.Location,
                                            EntityIds = new List<int>(){
                                                entity.ID
                                            },
                                            Zoom = zoom
                                        };
                                    }
                                    else
                                    {
                                        clusteredData[key].EntityIds.Add(entity.ID);
                                    }
                                }
                            }
                        }

                        if (!a.Cancel)
                        {
                            a.Result = clusteredData.ToList();
                        }
                    }                    
                };
                _worker.RunWorkerCompleted += (s, a) =>
                {
                    if (!a.Cancelled)
                    {
                        if (a.Result != null)
                        {
                            Render(a.Result as List<ClusteredPoint>);
                        }
                        else
                        {
                            Render(null);
                        }
                    }
                    else
                    {
                        _worker.RunWorkerAsync();
                    }
                };
            }

            if (_worker != null && _worker.IsBusy && _worker.WorkerSupportsCancellation)
            {
                _worker.CancelAsync();
            }
            else
            {
                _worker.RunWorkerAsync();
            }
        }

        private void Render(List<ClusteredPoint> clusters)
        {
            _baseLayer.Children.Clear();

            foreach (var c in clusters)
            {
                if (c != null)
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