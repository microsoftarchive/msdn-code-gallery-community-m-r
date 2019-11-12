using ClusterEngine;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Controls;

namespace BingMapsWPF_Clustering
{
    /// <summary>
    /// A custom class that defines the cluster options for this 
    /// </summary>
    public class MyClusterOptions: ClusterOptions
    {
        public MyClusterOptions(int radius):
            base()
        {
            this.ClusterRadius = radius;
        }

        public override Pushpin RenderEntity(Entity entity)
        {
            var p = new Pushpin();
            MapLayer.SetPosition(p, entity.Location);
            p.Tag = entity;
            p.ToolTip = new ToolTip()
            {
                Content = (entity as CustomEntity).Title
            };

            return p;
        }

        public override Pushpin RenderCluster(ClusteredPoint cluster)
        {
            var p = new Pushpin();
            MapLayer.SetPosition(p, cluster.Location);
            p.Content = "+";
            p.Tag = cluster;
            p.ToolTip = new ToolTip()
            {
                Content = string.Format("{0} Clustered Entities", cluster.EntityIds.Count)
            };

            return p;
        }
    }
}
