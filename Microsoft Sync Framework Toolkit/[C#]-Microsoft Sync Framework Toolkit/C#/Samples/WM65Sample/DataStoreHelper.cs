// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Data.SqlServerCe;
using System.IO;
using SmartDeviceProject1.Properties;

namespace SmartDeviceProject1
{
    /// <summary>
    /// This class contains helper methods to create a database and tables based on the schema in a sql file.
    /// </summary>
    internal class DataStoreHelper
    {
        #region Private Members

        private const string CONNECTION_STRING_TEMPLATE =
            @"Data Source = {0}; default lock timeout=30000; max buffer size=5120;Max Database Size=256";

        private static string _dbConnectionstring;

        #endregion

        #region Public Properties

        /// <summary>
        /// Connection string for the SQL CE database.
        /// </summary>
        public static string DbConnectionString { get { return _dbConnectionstring; } }

        /// <summary>
        /// SQL CE sdf file name.
        /// </summary>
        public static string ListDbFileName { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Create a database if one does not exist.
        /// </summary>
        public static void InitializeDataStore()
        {
            _dbConnectionstring = String.Format(CONNECTION_STRING_TEMPLATE, ListDbFileName);
            if (!File.Exists(ListDbFileName))
            {
                CreateDatabase();
            }

            // We maintain an static open connection to the local SQL CE database 
            // for the time the application runs. In case we switch users, without restarting the application, the connection
            // needs to be reset so that it does not point to the previous users database.
            SqlCeStorageHandler.CloseConnection();
        }

        #endregion

        #region Private Methods

        private static void CreateDatabase()
        {
            if (!File.Exists(ListDbFileName))
            {
                using (var sqlEngine = new SqlCeEngine(_dbConnectionstring))
                {
                    sqlEngine.CreateDatabase();
                    CreateInitialDatabaseObjects();
                }
            }
        }

        /// <summary>
        /// Parse the SQL file and create database objects.
        /// </summary>
        private static void CreateInitialDatabaseObjects()
        {
            using (var connection = new SqlCeConnection(_dbConnectionstring))
            {
                string[] commands = Resources.ListDb.Replace(Environment.NewLine, "\n").Split(new[] {';'});

                var sqlCommand = new SqlCeCommand() {Connection = connection};
                connection.Open();

                foreach (var command in commands)
                {
                    if (command.Replace("\n", String.Empty).Replace("GO", String.Empty).Length == 0)
                    {
                        continue;
                    }

                    sqlCommand.CommandText = command.Replace("GO", String.Empty);
                    sqlCommand.ExecuteNonQuery();
                }
            }
        }

        #endregion
    }
}
