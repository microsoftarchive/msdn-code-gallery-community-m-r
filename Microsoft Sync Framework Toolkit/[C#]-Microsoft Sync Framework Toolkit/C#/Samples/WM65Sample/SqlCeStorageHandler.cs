// -----------------------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// -----------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlServerCe;
using DefaultScope;
using Microsoft.Samples.Synchronization.ClientServices;

namespace SmartDeviceProject1
{
    internal class SqlCeStorageHandler : IDisposable
    {
        #region SQL CE Commands

        private const string GET_ALL_PRIORITY = "SELECT [ID], [Name], [_MetadataID] FROM [Priority] WHERE [IsTombstone] = 0";

        private const string GET_ALL_STATUS = "SELECT [ID], [Name], [_MetadataID] FROM [Status] WHERE [IsTombstone] = 0";

        private const string GET_ALL_TAGS = "SELECT [ID], [Name], [_MetadataID] FROM [Tag] WHERE [IsTombstone] = 0";

        private const string GET_ALL_LISTS =
            "SELECT [ID], [Name], [Description], [UserID], [CreatedDate], [IsTombstone], [_MetadataID] FROM [List] WHERE [IsTombstone] = 0";

        private const string GET_ALL_ITEMS =
            "SELECT ID, ListID, UserID, Name, Description, Priority, Status, StartDate, EndDate, IsTombstone, [_MetadataID] FROM [Item] WHERE [IsTombstone]=0 AND [ListID]=@ListID";

        private const string SELECT_ITEM_CHANGES =
            "SELECT ID, ListID, UserID, Name, Description, Priority, Status, StartDate, EndDate, IsTombstone, [_MetadataID] FROM [Item] WHERE IsDirty = 1";

        private const string SELECT_LIST_CHANGES =
            "SELECT ID, Name, Description, UserID, CreatedDate, IsTombstone, [_MetadataID] FROM [List] WHERE IsDirty = 1";

        private const string SELECT_TAGITEMMAPPING_CHANGES =
            "SELECT TagID, ItemID, UserID, IsTombstone, [_MetadataID] FROM [TagItemMapping] WHERE IsDirty = 1";

        private const string UPDATE_ITEM =
            "UPDATE [Item] SET ListID=@ListID,UserID=@UserID,Name=@Name,Description=@Description,Priority=@Priority,Status=@Status,StartDate=@StartDate,EndDate=@EndDate,IsDirty=@IsDirty,[_MetadataID]=@MetadataId WHERE ID=@ItemID";

        private const string UPDATE_LIST =
            "UPDATE [List] SET Name=@Name,Description=@Description,UserID=@UserID,CreatedDate=@CreatedDate,IsDirty=@IsDirty,[_MetadataID]=@MetadataId WHERE ID=@ID";

        private const string INSERT_ITEM =
            "INSERT INTO [Item] (ID, ListID, UserID, Name, Description, Priority, Status, StartDate, EndDate, IsDirty, [_MetadataID]) VALUES (@ID, @ListID, @UserID, @Name, @Description, @Priority, @Status, @StartDate, @EndDate, @IsDirty, @MetadataId)";

        private const string INSERT_LIST =
            "INSERT INTO [List] (ID, Name, Description, UserID, CreatedDate, IsDirty, [_MetadataID]) VALUES (@ID, @Name, @Description, @UserID, @CreatedDate, @IsDirty, @MetadataId)";

        private const string INSERT_TAGITEMMAPPING = "INSERT INTO [TagItemMapping] (TagID, ItemID, UserID, IsDirty, [_MetadataID]) VALUES (@TagID, @ItemID, @UserID, @IsDirty, @MetadataId)";

        private const string TOMBSTONE_ITEM = "UPDATE [Item] SET IsDirty=1,IsTombstone=1 WHERE ID=@ID";

        private const string TOMBSTONE_LIST = "UPDATE [List] SET IsDirty=1,IsTombstone=1 WHERE ID=@ID";

        private const string TOMBSTONE_TAGITEMMAPPING = "UPDATE TagItemMapping SET IsDirty=1,IsTombstone=1 WHERE TagID=@TagID AND UserID=@UserID AND ItemID=@ItemID";

        private const string DELETE_ITEM = "DELETE FROM [Item] WHERE ID=@ID";

        private const string DELETE_LIST = "DELETE FROM [List] WHERE ID=@ID";

        private const string DELETE_LIST_USING_METADATAID = "DELETE FROM [List] WHERE [_MetadataID]=@ID";
        private const string DELETE_ITEM_USING_METADATAID = "DELETE FROM [Item] WHERE [_MetadataID]=@ID";

        private const string DELETE_TAGITEMMAPPING = "DELETE FROM [TagItemMapping] WHERE [_MetadataID]=@ID";

        private const string GET_ITEM = "SELECT 1 FROM [Item] WHERE ID=@ID";
        private const string GET_LIST = "SELECT 1 FROM [List] WHERE ID=@ID";
        private const string GET_TAGITEMMAPPING = "SELECT 1 FROM [TagItemMapping] WHERE TagID=@TagID AND UserID=@UserID AND ItemID=@ItemID";
        private const string GET_TAG = "SELECT 1 FROM [Tag] WHERE ID=@ID";
        private const string GET_STATUS = "SELECT 1 FROM [Status] WHERE ID=@ID";
        private const string GET_PRIORITY = "SELECT 1 FROM [Priority] WHERE ID=@ID";
        private const string GET_USER = "SELECT 1 FROM [User] WHERE ID=@ID";

        private const string INSERT_TAG = "INSERT INTO [Tag] (ID, [Name], [_MetadataID]) VALUES (@ID, @Name, @MetadataId)";
        private const string INSERT_PRIORITY = "INSERT INTO [Priority] (ID, [Name], [_MetadataID]) VALUES (@ID, @Name, @MetadataId)";
        private const string INSERT_STATUS = "INSERT INTO [Status] (ID, [Name], [_MetadataID]) VALUES (@ID, @Name, @MetadataId)";
        private const string INSERT_USER = "INSERT INTO [User] (ID, [Name], [_MetadataID]) VALUES (@ID, @Name, @MetadataId)";

        private const string UPDATE_TAG = "UPDATE [Tag] SET [Name]=@Name,[_MetadataID]=@MetadataId WHERE ID=@ID";
        private const string UPDATE_PRIORITY = "UPDATE [Priority] SET [Name]=@Name,[_MetadataID]=@MetadataId WHERE ID=@ID";
        private const string UPDATE_STATUS = "UPDATE [Status] SET [Name]=@Name,[_MetadataID]=@MetadataId WHERE ID=@ID";

        private const string DELETE_STATUS = "DELETE FROM [Status] WHERE [_MetadataID]=@ID";
        private const string DELETE_TAG = "DELETE FROM [Tag] WHERE [_MetadataID]=@ID";
        private const string DELETE_PRIORITY = "DELETE FROM [Priority] WHERE [_MetadataID]=@ID";

        private const string GET_ANCHOR = "SELECT [Anchor] FROM [__sync]";
        private const string UPDATE_ANCHOR = "UPDATE [__sync] SET [Anchor]=@Anchor";

        private const string TOMBSTONE_ITEMS_FOR_LIST = "UPDATE [Item] SET [IsDirty]=1,[IsTombstone]=1 WHERE [ListID]=@ListID";

        private const string TOMBSTONE_TAGITEMMAPPING_FOR_LIST =
            "UPDATE [TagItemMapping] SET [IsDirty]=1, [IsTombstone]=1 WHERE [ItemID] IN (SELECT [ID] FROM [Item] WHERE [ListID] = @ListID)";
        private const string TOMBSTONE_TAGITEMMAPPING_FOR_ITEM = "UPDATE [TagItemMapping] SET [IsDirty]=1, [IsTombstone]=1 WHERE [ItemID] = @ItemID";

        #endregion

        #region Members

        private readonly Dictionary<Type, Action<SqlCeOfflineEntity>> _insertCommands = new Dictionary<Type, Action<SqlCeOfflineEntity>>();
        private readonly Dictionary<Type, Action<SqlCeOfflineEntity>> _updateCommands = new Dictionary<Type, Action<SqlCeOfflineEntity>>();
        private readonly Dictionary<Type, Action<SqlCeOfflineEntity>> _deleteCommands = new Dictionary<Type, Action<SqlCeOfflineEntity>>();
        private readonly Dictionary<Type, Action<SqlCeOfflineEntity>> _tombstoneCommands = new Dictionary<Type, Action<SqlCeOfflineEntity>>();
        private readonly Dictionary<Type, Func<SqlCeOfflineEntity, bool>> _getCommands = new Dictionary<Type, Func<SqlCeOfflineEntity, bool>>();
        private static SqlCeConnection _connection = new SqlCeConnection(DataStoreHelper.DbConnectionString);

        #endregion

        #region Constructor

        public SqlCeStorageHandler()
        {
            _insertCommands.Add(typeof (Item), InsertItem);
            _insertCommands.Add(typeof(List), InsertList);
            _insertCommands.Add(typeof (TagItemMapping), InsertTagItemMapping);
            _insertCommands.Add(typeof(Tag), InsertTag);
            _insertCommands.Add(typeof(Status), InsertStatus);
            _insertCommands.Add(typeof(Priority), InsertPriority);
            _insertCommands.Add(typeof(User), InsertUser);

            _updateCommands.Add(typeof(Item), UpdateItem);
            _updateCommands.Add(typeof(List), UpdateList);
            _updateCommands.Add(typeof(Tag), UpdateTag);
            _updateCommands.Add(typeof(Status), UpdateStatus);
            _updateCommands.Add(typeof(Priority), UpdatePriority);

            // These delete commands are used for saving items from a download response.
            _deleteCommands.Add(typeof(Item), DeleteItemUsingMetadataId);
            _deleteCommands.Add(typeof(List), DeleteListUsingMetadataId);
            _deleteCommands.Add(typeof(TagItemMapping), DeleteTagItemMapping);
            _deleteCommands.Add(typeof(Tag), DeleteTag);
            _deleteCommands.Add(typeof(Status), DeleteStatus);
            _deleteCommands.Add(typeof(Priority), DeletePriority);

            _tombstoneCommands.Add(typeof(Item), TombstoneItem);
            _tombstoneCommands.Add(typeof(List), TombstoneList);
            _tombstoneCommands.Add(typeof(TagItemMapping), TombstoneTagItemMapping);

            _getCommands.Add(typeof(Item), ItemExists);
            _getCommands.Add(typeof(List), ListExists);
            _getCommands.Add(typeof(TagItemMapping), TagItemMappingExists);
            _getCommands.Add(typeof(Tag), TagExists);
            _getCommands.Add(typeof(Status), StatusExists);
            _getCommands.Add(typeof(Priority), PriorityExists);
            _getCommands.Add(typeof(User), UserExists);
        }

        #endregion

        /// <summary>
        /// Delete all tombstones and reset all the IsDirty bits from tables. This method 
        /// is called after a successful upload to clear the local tracking information.
        /// </summary>
        public void ResetDirtyAndDeleteTombstones()
        {
            using (GetSqlCeConnection())
            {
                SqlCeTransaction transaction = _connection.BeginTransaction();

                try
                {
                    using (var command = new SqlCeCommand())
                    {
                        command.CommandText =
                            "DELETE FROM [TagItemMapping] WHERE [IsTombstone]=1;";
                        command.Connection = GetSqlCeConnection();

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.CommandText =
                            "UPDATE [TagItemMapping] SET [IsDirty]=0 WHERE [IsDirty]=1";
                        command.Connection = GetSqlCeConnection();

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.CommandText =
                            "DELETE FROM [Item] WHERE [IsTombstone]=1;";
                        command.Connection = GetSqlCeConnection();

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.CommandText =
                            "UPDATE [Item] SET [IsDirty]=0 WHERE [IsDirty]=1";
                        command.Connection = GetSqlCeConnection();

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.CommandText =
                            "DELETE FROM [List] WHERE [IsTombstone]=1;";
                        command.Connection = GetSqlCeConnection();

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.CommandText =
                            "UPDATE [List] SET [IsDirty]=0 WHERE [IsDirty]=1";
                        command.Connection = GetSqlCeConnection();

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        /// <summary>
        /// Gets all entities that were created/modified/deleted locally after the last sync.
        /// </summary>
        /// <param name="state">A unique identifier for the changes that are uploaded</param>
        /// <returns>The set of incremental changes to send to the service.</returns>
        public IEnumerable<SqlCeOfflineEntity> GetChanges(Guid state)
        {
            var changeList = new List<SqlCeOfflineEntity>();

            using (GetSqlCeConnection())
            {
                SqlCeTransaction transaction = _connection.BeginTransaction();

                try
                {
                    using (var command = new SqlCeCommand(SELECT_ITEM_CHANGES, GetSqlCeConnection()))
                    {
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var item = new Item
                            {
                                ID = (Guid)reader["ID"],
                                ListID = (Guid)reader["ListID"],
                                UserID = (Guid)reader["UserID"],
                                Name = reader["Name"] as string,
                                Description = reader["Description"] as string,
                                Priority = reader["Priority"] as int?,
                                Status = reader["Status"] as int?,
                                StartDate = reader["StartDate"] as DateTime?,
                                EndDate = reader["EndDate"] as DateTime?,
                                ServiceMetadata = new OfflineEntityMetadata()
                                                      {
                                                          IsTombstone = (bool)reader["IsTombstone"],
                                                          Id = reader["_MetadataID"] as string
                                                      }
                            };

                            changeList.Add(item);
                        }
                    }

                    using (var command = new SqlCeCommand(SELECT_LIST_CHANGES, GetSqlCeConnection()))
                    {
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var listItem = new List
                            {
                                ID = (Guid)reader["ID"],
                                Name = reader["Name"] as string,
                                Description = reader["Description"] as string,
                                UserID = (Guid)reader["UserID"],
                                CreatedDate = (DateTime)reader["CreatedDate"],
                                ServiceMetadata = new OfflineEntityMetadata()
                                {
                                    IsTombstone = (bool)reader["IsTombstone"],
                                    Id = reader["_MetadataID"] as string
                                }
                            };

                            changeList.Add(listItem);
                        }
                    }

                    using (var command = new SqlCeCommand(SELECT_TAGITEMMAPPING_CHANGES, GetSqlCeConnection()))
                    {
                        var reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            var tagItemMapping = new TagItemMapping
                            {
                                TagID = (int)reader["TagID"],
                                ItemID = (Guid)reader["ItemID"],
                                UserID = (Guid)reader["UserID"],
                                ServiceMetadata = new OfflineEntityMetadata()
                                {
                                    IsTombstone = (bool)reader["IsTombstone"],
                                    Id = reader["_MetadataID"] as string
                                }
                            };

                            changeList.Add(tagItemMapping);
                        }
                    }

                    transaction.Commit();
                }
                catch 
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return changeList;
        }

        /// <summary>
        /// Save changes retrieved from the sync service. This method is called to save changes from a download response.
        /// </summary>
        /// <param name="serverBlob">New blob received from the service.</param>
        /// <param name="entities">List of entities received from the service.</param>
        public void SaveDownloadedChanges(byte[] serverBlob, IEnumerable<SqlCeOfflineEntity> entities)
        {
            using (GetSqlCeConnection())
            {
                SqlCeTransaction transaction = _connection.BeginTransaction();
                
                try
                {
                    foreach (var entity in entities)
                    {
                        if (entity.ServiceMetadata.IsTombstone)
                        {
                            // Call delete command
                            var deleteMethod = _deleteCommands[entity.GetType()];
                            deleteMethod(entity);
                        }
                        else
                        {
                            // Call insert/update command
                            var getMethod = _getCommands[entity.GetType()];

                            if (getMethod(entity))
                            {
                                var updateMethod = _updateCommands[entity.GetType()];
                                updateMethod(entity);
                            }
                            else
                            {
                                var insertMethod = _insertCommands[entity.GetType()];
                                insertMethod(entity);
                            }
                        }
                    }

                    SaveAnchor(serverBlob);

                    transaction.Commit();
                }
                catch 
                {
                    transaction.Rollback();
                    throw;
                }
                
            }
        }

        /// <summary>
        /// Update/Delete an existing item in the local database. This method is called to 
        /// apply changes received in the upload response.
        /// </summary>
        /// <param name="entity"></param>
        public void ApplyItem(SqlCeOfflineEntity entity)
        {
            if (entity.GetType() == typeof(List))
            {
                if (entity.ServiceMetadata.IsTombstone)
                {
                    if (!String.IsNullOrEmpty(entity.ServiceMetadata.Id))
                    {
                        // Delete using the metadata id since tombstones do not have the primary keys filled in.
                        // For deletes that are not the result of an upload/download response, we can use the primary keys.
                        DeleteListUsingMetadataId(entity);
                    }
                    else
                    {
                        DeleteList(entity);
                    }
                }
                else
                {
                    UpdateList(entity);
                }
            }
            else if (entity.GetType() == typeof(Item))
            {
                if (entity.ServiceMetadata.IsTombstone)
                {
                    if (!String.IsNullOrEmpty(entity.ServiceMetadata.Id))
                    {
                        // Delete using the metadata id since tombstones do not have the primary keys filled in.
                        DeleteItemUsingMetadataId(entity);
                    }
                    else
                    {
                        DeleteItem(entity);
                    }
                }
                else
                {
                    UpdateItem(entity);
                }
            }
        }

        #region Anchor

        /// <summary>
        /// Get the anchor/blob that was last saved received from the service.
        /// </summary>
        /// <returns>Server blob</returns>
        public byte[] GetAnchor()
        {
            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_ANCHOR;

                object result = command.ExecuteScalar();

                return result as byte[];
            }
        }

        /// <summary>
        /// Save the blob that was retrieved from the service.
        /// </summary>
        /// <param name="anchor">Server blob</param>
        public void SaveAnchor(byte[] anchor)
        {
            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = UPDATE_ANCHOR;
                command.Parameters.AddWithValue("@Anchor", anchor);

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Item

        public bool ItemExists(SqlCeOfflineEntity entity)
        {
            var item = (Item)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_ITEM;
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = item.ID;

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        public void InsertItem(SqlCeOfflineEntity entity)
        {
            InsertItem(entity, false);
        }

        public void InsertItem(SqlCeOfflineEntity entity, bool isDirty)
        {
            var item = (Item) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = INSERT_ITEM;

                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = item.ID;
                command.Parameters.Add("@ListID", SqlDbType.UniqueIdentifier).Value = item.ListID;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = item.UserID;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Description", item.Description ?? "");

                if (null == item.Priority)
                {
                    command.Parameters.AddWithValue("@Priority", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Priority", item.Priority);
                }

                if (null == item.Status)
                {
                    command.Parameters.AddWithValue("@Status", DBNull.Value);
                }
                else
                {
                    command.Parameters.AddWithValue("@Status", item.Status);    
                }

                if (null != item.StartDate)
                {
                    command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = item.StartDate;
                }
                else
                {
                    command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;
                }

                if (null != item.EndDate)
                {
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = item.EndDate;
                }
                else
                {
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                }

                command.Parameters.AddWithValue("@IsDirty", isDirty ? 1 : 0);

                if (null == item.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = item.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void UpdateItem(SqlCeOfflineEntity entity)
        {
            UpdateItem(entity, false);
        }

        public void UpdateItem(SqlCeOfflineEntity entity, bool isDirty)
        {
            var item = (Item)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();

                command.CommandText = UPDATE_ITEM;
                command.Parameters.Add("@ListID", SqlDbType.UniqueIdentifier).Value = item.ListID;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = item.UserID;
                command.Parameters.AddWithValue("@Name", item.Name);
                command.Parameters.AddWithValue("@Description", item.Description);
                command.Parameters.AddWithValue("@Priority", item.Priority);
                command.Parameters.AddWithValue("@Status", item.Status);

                if (null != item.StartDate)
                {
                    command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = item.StartDate;    
                }
                else
                {
                    command.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DBNull.Value;    
                }

                if (null != item.EndDate)
                {
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = item.EndDate;
                }
                else
                {
                    command.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DBNull.Value;
                }

                command.Parameters.AddWithValue("@IsDirty", isDirty ? 1 : 0);
                command.Parameters.Add("@ItemID", SqlDbType.UniqueIdentifier).Value = item.ID;

                if (null == item.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = item.ServiceMetadata.Id;
                }
                

                command.ExecuteNonQuery();
            }
        }

        public void DeleteItem(SqlCeOfflineEntity entity)
        {
            var item = (Item) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_ITEM;
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = item.ID;

                command.ExecuteNonQuery();
            }
        }

        public void DeleteItemUsingMetadataId(SqlCeOfflineEntity entity)
        {
            var item = (Item)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_ITEM_USING_METADATAID;
                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = item.ServiceMetadata.Id;

                command.ExecuteNonQuery();
            }
        }

        public void TombstoneItem(SqlCeOfflineEntity entity)
        {
            var item = (Item) entity;

            using (var connection = GetSqlCeConnection())
            {
                var transaction = connection.BeginTransaction();

                try
                {
                    using (var command = new SqlCeCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = TOMBSTONE_TAGITEMMAPPING_FOR_ITEM;
                        command.Parameters.Add("@ItemID", SqlDbType.UniqueIdentifier).Value = item.ID;

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = TOMBSTONE_ITEM;
                        command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = item.ID;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        public List<Item> GetAllItems(Guid listId)
        {
            var items = new List<Item>();

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_ALL_ITEMS;
                command.Parameters.Add("@ListID", SqlDbType.UniqueIdentifier).Value = listId;

                SqlCeDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var i = new Item
                    {
                        ID = (Guid)reader["ID"],
                        Name = reader["Name"] as string,
                        Description = reader["Description"] as string,
                        ListID = (Guid)reader["ListID"],
                        Priority = reader["Priority"] as int?,
                        Status = reader["Status"] as int?,
                        UserID = (Guid)reader["UserID"],
                        ServiceMetadata = new OfflineEntityMetadata()
                        {
                            IsTombstone = (bool)reader["IsTombstone"],
                            Id = reader["_MetadataID"] as string
                        }
                    };

                    items.Add(i);
                }
            }

            return items;
        }

        #endregion

        #region List

        public object GetAllLists()
        {
            var lists = new List<List>();

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_ALL_LISTS;

                SqlCeDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var l = new List
                                {
                                    ID = (Guid) reader["ID"],
                                    Name = reader["Name"] as string,
                                    Description = reader["Description"] as string,
                                    CreatedDate = (DateTime) reader["CreatedDate"],
                                    UserID = (Guid)reader["UserID"],
                                    ServiceMetadata = new OfflineEntityMetadata()
                                    {
                                        IsTombstone = (bool)reader["IsTombstone"],
                                        Id = reader["_MetadataID"] as string
                                    }
                                };

                    lists.Add(l);
                }
            }

            return lists;
        }

        public bool ListExists(SqlCeOfflineEntity entity)
        {
            var list = (List)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_LIST;
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = list.ID;

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        public void InsertList(SqlCeOfflineEntity entity)
        {
            InsertList(entity, false);
        }

        public void InsertList(SqlCeOfflineEntity entity, bool isDirty)
        {
            var list = (List)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = INSERT_LIST;

                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = list.ID;
                command.Parameters.AddWithValue("@Name", list.Name);
                command.Parameters.AddWithValue("@Description", list.Description ?? "");
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = list.UserID;
                command.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = list.CreatedDate;
                command.Parameters.AddWithValue("@IsDirty", isDirty ? 1 : 0);

                if (null == list.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = list.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void UpdateList(SqlCeOfflineEntity entity)
        {
            UpdateList(entity, false);
        }

        public void UpdateList(SqlCeOfflineEntity entity, bool isDirty)
        {
            var list = (List)entity;

                using (var command = new SqlCeCommand())
                {
                    command.Connection = GetSqlCeConnection();
                    command.CommandText = UPDATE_LIST;
                    command.Parameters.AddWithValue("@Name", list.Name);
                    command.Parameters.AddWithValue("@Description", list.Description);
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = list.UserID;
                    command.Parameters.Add("@CreatedDate", SqlDbType.DateTime).Value = list.CreatedDate;
                    command.Parameters.AddWithValue("@IsDirty", isDirty ? 1 : 0);
                    command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = list.ID;

                    if (null == list.ServiceMetadata.Id)
                    {
                        command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = list.ServiceMetadata.Id;
                    }

                    command.ExecuteNonQuery();
                }
        }

        public void DeleteList(SqlCeOfflineEntity entity)
        {
            var list = (List)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_LIST;
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = list.ID;

                command.ExecuteNonQuery();
            }
        }

        public void DeleteListUsingMetadataId(SqlCeOfflineEntity entity)
        {
            var list = (List)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_LIST_USING_METADATAID;
                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = list.ServiceMetadata.Id;

                command.ExecuteNonQuery();
            }
        }

        public void TombstoneList(SqlCeOfflineEntity entity)
        {
            var list = (List)entity;

            using (var connection = GetSqlCeConnection())
            {
                var transaction = connection.BeginTransaction();

                try
                {

                    using (var command = new SqlCeCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = TOMBSTONE_TAGITEMMAPPING_FOR_LIST;
                        command.Parameters.Add("@ListID", SqlDbType.UniqueIdentifier).Value = list.ID;

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = TOMBSTONE_ITEMS_FOR_LIST;
                        command.Parameters.Add("@ListID", SqlDbType.UniqueIdentifier).Value = list.ID;

                        command.ExecuteNonQuery();
                    }

                    using (var command = new SqlCeCommand())
                    {
                        command.Connection = connection;
                        command.CommandText = TOMBSTONE_LIST;
                        command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = list.ID;

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }

        #endregion

        #region TagItemMapping

        public bool TagItemMappingExists(SqlCeOfflineEntity entity)
        {
            var tagItemMapping = (TagItemMapping)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_TAGITEMMAPPING;
                command.Parameters.Add("@TagID", SqlDbType.Int).Value = tagItemMapping.TagID;
                command.Parameters.Add("@ItemID", SqlDbType.UniqueIdentifier).Value = tagItemMapping.ItemID;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = tagItemMapping.UserID;

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        public void InsertTagItemMapping(SqlCeOfflineEntity entity)
        {
            InsertTagItemMapping(entity, false);
        }

        public void InsertTagItemMapping(SqlCeOfflineEntity entity, bool isDirty)
        {
            var tagItemMapping = (TagItemMapping)entity;

                using (var command = new SqlCeCommand())
                {
                    command.Connection = GetSqlCeConnection();
                    command.CommandText = INSERT_TAGITEMMAPPING;

                    command.Parameters.Add("@TagID", SqlDbType.Int).Value = tagItemMapping.TagID;
                    command.Parameters.Add("@ItemID", SqlDbType.UniqueIdentifier).Value = tagItemMapping.ItemID;
                    command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = tagItemMapping.UserID;
                    command.Parameters.AddWithValue("@IsDirty", isDirty ? 1 : 0);

                    if (null == tagItemMapping.ServiceMetadata.Id)
                    {
                        command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                    }
                    else
                    {
                        command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = tagItemMapping.ServiceMetadata.Id;
                    }

                    command.ExecuteNonQuery();
                }
        }

        public void DeleteTagItemMapping(SqlCeOfflineEntity entity)
        {
            var tagItemMapping = (TagItemMapping)entity;
            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_TAGITEMMAPPING;
                
                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = tagItemMapping.ServiceMetadata.Id;

                command.ExecuteNonQuery();
            }
        }

        public void TombstoneTagItemMapping(SqlCeOfflineEntity entity)
        {
            var tagItemMapping = (TagItemMapping) entity;
            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = TOMBSTONE_TAGITEMMAPPING;
                command.Parameters.Add("@TagID", SqlDbType.Int).Value = tagItemMapping.TagID;
                command.Parameters.Add("@ItemID", SqlDbType.UniqueIdentifier).Value = tagItemMapping.ItemID;
                command.Parameters.Add("@UserID", SqlDbType.UniqueIdentifier).Value = tagItemMapping.UserID;

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Tag

        public List<Tag> GetAllTags()
        {
            var tags = new List<Tag>();
            
            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_ALL_TAGS;

                SqlCeDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var t = new Tag
                                {
                                    ID = (int) reader["ID"], 
                                    Name = reader["Name"] as string,
                                    ServiceMetadata = new OfflineEntityMetadata()
                                    {
                                        IsTombstone = false,
                                        Id = reader["_MetadataID"] as string
                                    }
                                };
                    tags.Add(t);
                }
            }

            return tags;
        }

        public bool TagExists(SqlCeOfflineEntity entity)
        {
            var tag = (Tag) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_TAG;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = tag.ID;

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        public void InsertTag(SqlCeOfflineEntity entity)
        {
            var tag = (Tag)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = INSERT_TAG;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = tag.ID;
                command.Parameters.AddWithValue("@Name", tag.Name);

                if (null == tag.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = tag.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void UpdateTag(SqlCeOfflineEntity entity)
        {
            var tag = (Tag)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = UPDATE_TAG;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = tag.ID;
                command.Parameters.AddWithValue("@Name", tag.Name);

                if (null == tag.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = tag.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void DeleteTag(SqlCeOfflineEntity entity)
        {
            var tag = (Tag)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_TAG;
                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = tag.ServiceMetadata.Id;

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Priority

        public List<Priority> GetAllPriority()
        {
            var priorities = new List<Priority>();

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_ALL_PRIORITY;

                SqlCeDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Priority()
                    {
                        ID = (int)reader["ID"],
                        Name = reader["Name"] as string,
                        ServiceMetadata = new OfflineEntityMetadata()
                        {
                            IsTombstone = false,
                            Id = reader["_MetadataID"] as string
                        }
                    };

                    priorities.Add(p);
                }
            }

            return priorities;
        }

        public bool PriorityExists(SqlCeOfflineEntity entity)
        {
            var priority = (Priority) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_PRIORITY;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = priority.ID;

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        public void InsertPriority(SqlCeOfflineEntity entity)
        {
            var priority = (Priority)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = INSERT_PRIORITY;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = priority.ID;
                command.Parameters.AddWithValue("@Name", priority.Name);

                if (null == priority.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = priority.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void UpdatePriority(SqlCeOfflineEntity entity)
        {
            var priority = (Priority) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = UPDATE_PRIORITY;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = priority.ID;
                command.Parameters.AddWithValue("@Name", priority.Name);

                if (null == priority.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = priority.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void DeletePriority(SqlCeOfflineEntity entity)
        {
            var priority = (Priority) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_PRIORITY;
                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = priority.ServiceMetadata.Id;

                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region Status

        public List<Status> GetAllStatus()
        {
            var statuses = new List<Status>();

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_ALL_STATUS;

                SqlCeDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var p = new Status
                                {
                                    ID = (int) reader["ID"],
                                    Name = reader["Name"] as string,
                                    ServiceMetadata = new OfflineEntityMetadata()
                                    {
                                        IsTombstone = false,
                                        Id = reader["_MetadataID"] as string
                                    }
                                };

                    statuses.Add(p);
                }
            }

            return statuses;
        }

        public bool StatusExists(SqlCeOfflineEntity entity)
        {
            var status = (Status) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_STATUS;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = status.ID;

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        public void InsertStatus(SqlCeOfflineEntity entity)
        {
            var status = (Status) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = INSERT_STATUS;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = status.ID;
                command.Parameters.AddWithValue("@Name", status.Name);

                if (null == status.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = status.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void UpdateStatus(SqlCeOfflineEntity entity)
        {
            var status = (Status) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = UPDATE_STATUS;
                command.Parameters.Add("@ID", SqlDbType.Int).Value = status.ID;
                command.Parameters.AddWithValue("@Name", status.Name);

                if (null == status.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = status.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public void DeleteStatus(SqlCeOfflineEntity entity)
        {
            var status = (Status) entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = DELETE_STATUS;
                command.Parameters.Add("@ID", SqlDbType.NVarChar).Value = status.ServiceMetadata.Id;
                command.ExecuteNonQuery();
            }
        }

        #endregion

        #region User

        public void InsertUser(SqlCeOfflineEntity entity)
        {
            var user = (User)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = INSERT_USER;
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = user.ID;
                command.Parameters.AddWithValue("@Name", user.Name);

                if (null == user.ServiceMetadata.Id)
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = DBNull.Value;
                }
                else
                {
                    command.Parameters.Add("@MetadataId", SqlDbType.NVarChar, 250).Value = user.ServiceMetadata.Id;
                }

                command.ExecuteNonQuery();
            }
        }

        public bool UserExists(SqlCeOfflineEntity entity)
        {
            var user = (User)entity;

            using (var command = new SqlCeCommand())
            {
                command.Connection = GetSqlCeConnection();
                command.CommandText = GET_USER;
                command.Parameters.Add("@ID", SqlDbType.UniqueIdentifier).Value = user.ID;

                object result = command.ExecuteScalar();

                return result != null;
            }
        }

        #endregion

        #region Private Methods

        private SqlCeConnection GetSqlCeConnection()
        {
            if (_connection == null)
            {
                _connection = new SqlCeConnection(DataStoreHelper.DbConnectionString);
            }

            try
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
            }
            catch (ObjectDisposedException)
            {
                _connection = new SqlCeConnection(DataStoreHelper.DbConnectionString);
                _connection.Open();
            }
            
            return _connection;
        }

        public static void CloseConnection()
        {
            if (null != _connection && _connection.State != ConnectionState.Closed)
            {
                _connection.Close();
                _connection.Dispose();
                _connection = null;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    CloseConnection();
                }

                _isDisposed = true;
            }
        }

        private bool _isDisposed;

        #endregion
    }
}