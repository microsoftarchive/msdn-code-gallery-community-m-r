using System;

using Microsoft.WindowsAzure.Storage.Table;

namespace Eldert.IoT.Data.DataTypes
{
    /// <summary>
    /// Represents an engine information object for Azure Table Storage.
    /// </summary>
    public class EngineInformation : TableEntity
    {
        public Guid Identifier { get; set; }
        
        public string ShipName { get; set; }
        
        public string EngineName { get; set; }

        public double Temperature { get; set; }

        public double RPM { get; set; }

        public bool Warning { get; set; }

        public int EngineWarning { get; set; }

        public DateTime CreatedDateTime { get; set; }

        public void SetKeys()
        {
            PartitionKey = ShipName;
            RowKey = Identifier.ToString();
        }
    }
}
