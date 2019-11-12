//===============================================================================
// Copyright © 2008 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
//
//This code lives here: http://code.msdn.microsoft.com/LinqEntityDataReader
//Please visit for comments, issues and updates
//
//Version 1.0.0.1 Added GetSchemaTable support for Loading DataTables from EntityDataReader
//Version 1.0.0.2 Added support for Entity Framework types, including Foreign Key columns
//Version 1.0.0.3 In DataReader.GetValue, now using dynamic methods for common scalar types instead of reflection with PropertyInfo.GetValue()
//Version 1.0.0.4 Added support for simple IEnumerable<T> where T is a scalar to support, eg, passing List<int> to a TVP
//Version 1.0.0.5 Simplified the Attribute code, added dynamic method support for all scalar types


namespace Microsoft.Samples.EntityDataReader
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Data;
  using System.Reflection;
  using System.Data.Common;
  using System.Data.Objects.DataClasses;
  using System.Data.Metadata.Edm;
  using System.Data.Objects;


  /// <summary>
  /// The EntityDataReader wraps a collection of CLR objects in a DbDataReader.  
  /// Only "scalar" properties are projected, with the exception that Entity Framework
  /// EntityObjects that have references to other EntityObjects will have key values for
  /// the related entity projected.
  /// 
  /// This is useful for doing high-speed data loads with SqlBulkCopy, and copying collections
  /// of entities ot a DataTable for use with SQL Server Table-Valued parameters, or for interop
  /// with older ADO.NET applciations.
  /// 
  /// For explicit control over the fields projected by the DataReader, just wrap your collection
  /// of entities in a anonymous type projection before wrapping it in an EntityDataReader.
  /// 
  /// Instead of 
  /// IEnumerable<Order> orders;
  /// ...
  /// IDataReader dr = orders.AsDataReader();
  /// 
  /// do
  /// IEnumerable<Order> orders;
  /// ...
  /// var q = from o in orders
  ///         select new 
  ///         {
  ///            ID=o.ID,
  ///            ShipDate=o.ShipDate,
  ///            ProductName=o.Product.Name,
  ///            ...
  ///         }
  /// IDataReader dr = q.AsDataReader();
  /// 
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class EntityDataReader<T> : DbDataReader, IDataReader
  {

    readonly IEnumerator<T> enumerator;
    readonly bool exposeNullableColumns = true;

    T current;
    bool closed = false;

    protected static Dictionary<Type, IList<Attribute>> propertyCache = new Dictionary<Type, IList<Attribute>>();
    protected readonly IList<Attribute> attributes;

    #region Attribute inner type
    protected class Attribute
    {
      //PropertyInfo propertyInfo;
      public readonly Type Type;
      public readonly string Name;
      readonly Func<T, object> ValueAccessor;

      Func<T, TProp> MakeFunc<TProp>(PropertyInfo pi)
      {
        Type FuncOfT = typeof(Func<T, int>).GetGenericTypeDefinition();
        MethodInfo mi = pi.GetGetMethod();
        //create a delegate for the type
        var GetterDelegate = Delegate.CreateDelegate(FuncOfT.MakeGenericType(typeof(T), pi.PropertyType), mi);
        return (Func<T, TProp>)GetterDelegate;
      }
      public Attribute(PropertyInfo pi)
      {
        this.Name = pi.Name;
        Type = pi.PropertyType;

        
        if (Type == typeof(Byte))
        {
          var Getter = MakeFunc<Byte>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Int16))
        {
          var Getter = MakeFunc<Int16>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Int32))
        {
          var Getter = MakeFunc<Int32>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Int64))
        {
          var Getter = MakeFunc<Int64>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(String))
        {
          var Getter = MakeFunc<String>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Decimal))
        {
          var Getter = MakeFunc<Decimal>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(DateTime))
        {
          var Getter = MakeFunc<DateTime>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Guid))
        {
          var Getter = MakeFunc<Guid>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Single))
        {
          var Getter = MakeFunc<Single>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Double))
        {
          var Getter = MakeFunc<Double>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(TimeSpan))
        {
          var Getter = MakeFunc<TimeSpan>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Boolean))
        {
          var Getter = MakeFunc<Boolean>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
          //nullable types
        else if (Type == typeof(Byte?))
        {
          var Getter = MakeFunc<Byte?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Int16?))
        {
          var Getter = MakeFunc<Int16?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Int32?))
        {
          var Getter = MakeFunc<Int32?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Int64?))
        {
          var Getter = MakeFunc<Int64?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Decimal?))
        {
          var Getter = MakeFunc<Decimal?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(DateTime?))
        {
          var Getter = MakeFunc<DateTime?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Guid?))
        {
          var Getter = MakeFunc<Guid?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Single?))
        {
          var Getter = MakeFunc<Single?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Double?))
        {
          var Getter = MakeFunc<Double?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(TimeSpan?))
        {
          var Getter = MakeFunc<TimeSpan?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else if (Type == typeof(Boolean?))
        {
          var Getter = MakeFunc<Boolean?>(pi);
          ValueAccessor = (T t) => Getter(t);
        }
        else
        {
          ValueAccessor = (T t) => pi.GetValue(t,null);
          System.Diagnostics.Debug.WriteLine("Unregisterd property type.  Using reflection to access value." + Type.Name);
        }

       

      }


      public Attribute(string name, Type type, Func<T, object> getValue)
      {
        this.Name = name;
        this.Type = type;
        this.ValueAccessor = getValue;
      }
      
      public object GetValue(T target)
      {
        return ValueAccessor(target);
      }
    }
    #endregion

    #region "Scalar Types"

    static bool IsScalarType(Type t)
    {
      return scalarTypes.Contains(t);
    }
    static HashSet<Type> scalarTypes = LoadScalarTypes();
    static HashSet<Type> LoadScalarTypes()
    {
      HashSet<Type> set = new HashSet<Type>() 
                              { 
                                //reference types
                                typeof(String),
                                typeof(Byte[]),
                                //value types
                                typeof(Byte),
                                typeof(Int16),
                                typeof(Int32),
                                typeof(Int64),
                                typeof(Single),
                                typeof(Double),
                                typeof(Decimal),
                                typeof(DateTime),
                                typeof(Guid),
                                typeof(Boolean),
                                typeof(TimeSpan),
                                //nullable value types
                                typeof(Byte?),
                                typeof(Int16?),
                                typeof(Int32?),
                                typeof(Int64?),
                                typeof(Single?),
                                typeof(Double?),
                                typeof(Decimal?),
                                typeof(DateTime?),
                                typeof(Guid?),
                                typeof(Boolean?),
                                typeof(TimeSpan?)
                              };
      

      return set;

    }
    #endregion


    #region Constructors

    public EntityDataReader(IEnumerable<T> col) :  this(col, true) { }
    public EntityDataReader(IEnumerable<T> col, bool exposeNullableColumns) :  this(col, null, exposeNullableColumns) { }
    public EntityDataReader(IEnumerable<T> col, ObjectContext objectContext, bool exposeNullableColumns) 
    {
      this.enumerator = col.GetEnumerator();
      this.exposeNullableColumns = exposeNullableColumns;
     
      Type thisType = typeof(T);

      //try to find the row metadata for this type in the cache.
      if (propertyCache.ContainsKey(thisType))
      {
        this.attributes = propertyCache[thisType];
        return;
      }

      //generate the metadata.  Done outside the lock, 
      //so it's possible but harmless that this will be done more than once on startup
      this.attributes = DiscoverMetadata(thisType, objectContext);

      //add to the property cache, if it's not already there
      lock (propertyCache)
      {
        if (!propertyCache.ContainsKey(thisType))
        {
          propertyCache[thisType] = this.attributes;
        }
      }

    }

    static IList<Attribute> DiscoverMetadata(Type thisType, ObjectContext objectContext)
    {

      //Not a collection of entities, just an IEnumerable<String> or other scalar type.
      //So add just a single Attribute that returns the object itself
      if (IsScalarType(thisType))
      {
        return new Attribute[] { new Attribute("Value", thisType, t => t) };
      }


      var attributeList = new SortedList<string, Attribute>();

      //find all the scalar properties and any EF EntityReference properties
      var allProperties = (from p in thisType.GetProperties()
                           where IsScalarType(p.PropertyType)
                              || typeof(EntityReference).IsAssignableFrom(p.PropertyType)
                           select new
                           {
                             PropertyInfo = p,
                             IsEntityReference = typeof(EntityReference).IsAssignableFrom(p.PropertyType)
                           }).ToList();


      //add the normal scalar properties
      foreach (var pi in from p in allProperties
                         where !p.IsEntityReference
                         select p.PropertyInfo)
      {
        attributeList.Add(pi.Name, new Attribute(pi));
      }

      //if there is no ObjectContext, then just return the scalar properties
      if (objectContext == null)
      {
        return attributeList.Values;
      }

      //if there is an ObjectContext, recreate foreign key column values
      //by adding Attributes for any key values of referenced entities 
      //that aren't already exposed as scalar properties
      var mw = objectContext.MetadataWorkspace;
      var entityTypesByName = mw.GetItems<EntityType>(DataSpace.OSpace).ToLookup(e => e.FullName);

      //find the EntityType metadata for T 
      EntityType thisEntity = entityTypesByName[thisType.FullName].First();
      var thisEntityKeys = thisEntity.KeyMembers.ToDictionary(k => k.Name);

      //For each EntityRelation property add add the keys of the related Entity
      foreach (var pi in from p in allProperties
                         where p.IsEntityReference
                         select p.PropertyInfo)
      {
        //Find the name of the CLR Type at the other end of the reference because we need to get its key attributes.
        //the property type is EntityReference<T>, we need T.
        string relatedEntityCLRTypeName = pi.PropertyType.GetGenericArguments().First().FullName;

        //Find the EntityType at the other end of the relationship because we need to get its key attributes.
        EntityType relatedEntityEFType = entityTypesByName[relatedEntityCLRTypeName].FirstOrDefault();
        if (relatedEntityEFType == null)
        {
          throw new InvalidOperationException("Cannot find EntityType for EntityReference Property " + pi.Name);
        }

        //Add attributes for each key value of the related entity.  These are the properties that
        //would probably appear in the storage object.  The names will be the same as they are on the 
        //related entity, except with a check to make sure that we're not introducing a duplicate.
        foreach (var key in relatedEntityEFType.KeyMembers)
        {
          string fkAttributeName = key.Name;
          if (thisEntityKeys.ContainsKey(fkAttributeName))
          {
            continue;  //skip these.  They are already exposed as regular scalar properties.
          }

          //Now ensure that the key name doesn't collide with the name of an attribute.
          //Uniqify by prefixing the related Entity name, eg ID => ProductID
          while (attributeList.ContainsKey(fkAttributeName))
          {
            string re = relatedEntityEFType.Name;

            if (re.ToUpper() == re)
            {
              //ID => PRODUCT_ID
              fkAttributeName = re + "_" + fkAttributeName;
            }
            else
            {
              //ID => ProductID
              fkAttributeName = re + fkAttributeName;
            }
          }

          //bind out local variables for the valueAccessor closure.
          Type kType = Type.GetType(key.TypeUsage.EdmType.FullName);
          PropertyInfo entityReferenceProperty = pi;
          Console.WriteLine(key.Name);
          Func<T, object> valueAccessor = o =>
          {
            EntityReference er = (EntityReference)entityReferenceProperty.GetValue(o, null);
            object val = er.EntityKey.EntityKeyValues.First(k => k.Key == fkAttributeName).Value;
            return val;
          };
          attributeList.Add(fkAttributeName, new Attribute(fkAttributeName, kType, valueAccessor));
        }


      }

      return attributeList.Values;
    }

    #endregion

    #region Utility Methods
    static Type nullable_T = typeof(System.Nullable<int>).GetGenericTypeDefinition();
    bool IsNullable(Type t)
    {
      return (t.IsGenericType
          && t.GetGenericTypeDefinition() == nullable_T);
    }
    Type StripNullableType(Type t)
    {
      return t.GetGenericArguments()[0];
    }
    #endregion
    
    #region GetSchemaTable


    const string shemaTableSchema = @"<?xml version=""1.0"" standalone=""yes""?>
<xs:schema id=""NewDataSet"" xmlns="""" xmlns:xs=""http://www.w3.org/2001/XMLSchema"" xmlns:msdata=""urn:schemas-microsoft-com:xml-msdata"">
  <xs:element name=""NewDataSet"" msdata:IsDataSet=""true"" msdata:MainDataTable=""SchemaTable"" msdata:Locale="""">
    <xs:complexType>
      <xs:choice minOccurs=""0"" maxOccurs=""unbounded"">
        <xs:element name=""SchemaTable"" msdata:Locale="""" msdata:MinimumCapacity=""1"">
          <xs:complexType>
            <xs:sequence>
              <xs:element name=""ColumnName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""ColumnOrdinal"" msdata:ReadOnly=""true"" type=""xs:int"" default=""0"" minOccurs=""0"" />
              <xs:element name=""ColumnSize"" msdata:ReadOnly=""true"" type=""xs:int"" minOccurs=""0"" />
              <xs:element name=""NumericPrecision"" msdata:ReadOnly=""true"" type=""xs:short"" minOccurs=""0"" />
              <xs:element name=""NumericScale"" msdata:ReadOnly=""true"" type=""xs:short"" minOccurs=""0"" />
              <xs:element name=""IsUnique"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""IsKey"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""BaseServerName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""BaseCatalogName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""BaseColumnName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""BaseSchemaName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""BaseTableName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""DataType"" msdata:DataType=""System.Type, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""AllowDBNull"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""ProviderType"" msdata:ReadOnly=""true"" type=""xs:int"" minOccurs=""0"" />
              <xs:element name=""IsAliased"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""IsExpression"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""IsIdentity"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""IsAutoIncrement"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""IsRowVersion"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""IsHidden"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""IsLong"" msdata:ReadOnly=""true"" type=""xs:boolean"" default=""false"" minOccurs=""0"" />
              <xs:element name=""IsReadOnly"" msdata:ReadOnly=""true"" type=""xs:boolean"" minOccurs=""0"" />
              <xs:element name=""ProviderSpecificDataType"" msdata:DataType=""System.Type, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""DataTypeName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""XmlSchemaCollectionDatabase"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""XmlSchemaCollectionOwningSchema"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""XmlSchemaCollectionName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""UdtAssemblyQualifiedName"" msdata:ReadOnly=""true"" type=""xs:string"" minOccurs=""0"" />
              <xs:element name=""NonVersionedProviderType"" msdata:ReadOnly=""true"" type=""xs:int"" minOccurs=""0"" />
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:choice>
    </xs:complexType>
  </xs:element>
</xs:schema>";
    public override DataTable GetSchemaTable()
    {
      DataSet s = new DataSet();
      s.ReadXmlSchema(new System.IO.StringReader(shemaTableSchema));
      DataTable t = s.Tables[0];
      for (int i = 0; i < this.FieldCount; i++)
      {
        DataRow row = t.NewRow();
        row["ColumnName"] = this.GetName(i);
        row["ColumnOrdinal"] = i;

        Type type = this.GetFieldType(i);
        if (type.IsGenericType
          && type.GetGenericTypeDefinition() == typeof(System.Nullable<int>).GetGenericTypeDefinition())
        {
          type = type.GetGenericArguments()[0];
        }
        row["DataType"] = this.GetFieldType(i);
        row["DataTypeName"] = this.GetDataTypeName(i);
        row["ColumnSize"] = -1;
        t.Rows.Add(row);
      }
      return t;

    }
    #endregion

    #region IDataReader Members

    public override void Close()
    {
      closed = true;
    }

    public override int Depth
    {
      get { return 1; }
    }


    public override bool IsClosed
    {
      get { return closed; }
    }

    public override bool NextResult()
    {
      return false;
    }

    int entitiesRead = 0;
    public override bool Read()
    {
       bool rv =  enumerator.MoveNext();
       if (rv)
       {
         current = enumerator.Current;
         entitiesRead += 1;
       }
       return rv;
    }

    public override int RecordsAffected
    {
      get { throw new NotImplementedException(); }
    }

    #endregion

    #region IDisposable Members

    protected override void Dispose(bool disposing)
    {
      Close();
      base.Dispose(disposing);
    }

    #endregion

    #region IDataRecord Members

    public override int FieldCount
    {
      get
      {
        return attributes.Count;
      }
    }

    TField GetValue<TField>(int i)
    {
      return (TField)attributes[i].GetValue(current);
    }
    public override bool GetBoolean(int i)
    {
      return GetValue<bool>(i);
    }

    public override byte GetByte(int i)
    {
      return GetValue<byte>(i);
    }

    public override long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    public override char GetChar(int i)
    {
      return GetValue<char>(i);
    }

    public override long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
    {
      throw new NotImplementedException();
    }

    //public override DbDataReader GetData(int i)
    //{
    //  throw new NotImplementedException();
    //}

    public override string GetDataTypeName(int i)
    {
      return attributes[i].Type.Name;
    }

    public override DateTime GetDateTime(int i)
    {
      return GetValue<DateTime>(i);
    }

    public override decimal GetDecimal(int i)
    {
      return GetValue<decimal>(i);
    }

    public override double GetDouble(int i)
    {
      return GetValue<double>(i);
    }

    public override Type GetFieldType(int i)
    {
      Type t = attributes[i].Type;
      if (!exposeNullableColumns && this.IsNullable(t))
      {
        return this.StripNullableType(t);
      }
      return t;
    }

    public override float GetFloat(int i)
    {
      return GetValue<float>(i);
    }

    public override Guid GetGuid(int i)
    {
      return GetValue<Guid>(i);
    }

    public override short GetInt16(int i)
    {
      return GetValue<short>(i);
    }

    public override int GetInt32(int i)
    {
      return GetValue<int>(i);
    }

    public override long GetInt64(int i)
    {
      return GetValue<long>(i);
    }

    public override string GetName(int i)
    {
      return attributes[i].Name;
    }

    public override int GetOrdinal(string name)
    {
      for (int i = 0; i < attributes.Count; i++)
      {
        if (attributes[i].Name == name)
        {
          return i;
        }
      }
      return -1;
    }

    public override string GetString(int i)
    {
      return GetValue<string>(i);
    }



    public override int GetValues(object[] values)
    {
      for (int i = 0; i < attributes.Count; i++)
      {
        values[i] = GetValue(i);
      }
      return attributes.Count;
    }


    
    public override object GetValue(int i)
    {
      object o = GetValue<object>(i);
      if (!exposeNullableColumns && o == null)
      {
        return DBNull.Value;
      }
      return o;
    }

    public override bool IsDBNull(int i)
    {
      object o = GetValue<object>(i);
      return (o == null);
    }

    public override object this[string name]
    {
      get { return GetValue(GetOrdinal(name)); }
    }

    public override object this[int i]
    {
      get { return GetValue(i); }
    }

    #endregion

    #region DbDataReader Members



    public override System.Collections.IEnumerator GetEnumerator()
    {
      return this.enumerator;
    }

    public override bool HasRows
    {
      get { throw new NotSupportedException(); }
    }
    #endregion

  }

  public static class EntityDataReaderExtensions
  {
    /// <summary>
    /// Wraps the IEnumerable in a DbDataReader, having one column for each "scalar" property of the type T.  
    /// The collection will be enumerated as the client calls IDataReader.Read().
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="collection"></param>
    /// <returns></returns>
    public static IDataReader AsDataReader<T>(this IEnumerable<T> collection)
    {
      return new EntityDataReader<T>(collection);
    }

    /// <summary>
    /// Enumerates the collection and copies the data into a DataTable.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="collection">The collection to copy to a DataTable</param>
    /// <returns>A DataTable containing the scalar projection of the collection.</returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
    {
      DataTable t = new DataTable();
      t.TableName = typeof(T).Name;
      EntityDataReader<T> dr = new EntityDataReader<T>(collection, false);
      t.Load(dr);
      return t;
    }

    /// <summary>
    /// Wraps the collection in a DataReader, but also includes columns for the key attributes of related Entities.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="collection">A collection to wrap in a DataReader</param>
    /// <param name="cx">The Entity Framework ObjectContext, used for metadata access</param>
    /// <returns>A DbDataReader wrapping the collection.</returns>
    public static IDataReader AsDataReader<T>(this IEnumerable<T> collection, ObjectContext cx) where T: EntityObject 
    {
      return new EntityDataReader<T>(collection,cx,true);
    }

    /// <summary>
    /// Wraps the collection in a DataReader, but also includes columns for the key attributes of related Entities.
    /// </summary>
    /// <typeparam name="T">The element type of the collectin.</typeparam>
    /// <param name="collection">A collection to wrap in a DataReader</param>
    /// <param name="detachObjects">Option to detach each object in the collection from the ObjectContext.  This can reduce memory usage for queries returning large numbers of objects.</param>
    /// <returns>A DbDataReader wrapping the collection.</returns>
    /// <summary>
    public static IDataReader AsDataReader<T>(this IEnumerable<T> collection, ObjectContext cx, bool detachObjects) where T : EntityObject
    {
      if (detachObjects)
      {
        return new EntityDataReader<T>(collection.DetachAllFrom(cx),cx,true);
      }
      return new EntityDataReader<T>(collection,cx,true);
    }
    static IEnumerable<T> DetachAllFrom<T>(this IEnumerable<T> col, ObjectContext cx) 
    {
      foreach (var t in col)
      {
        cx.Detach(t);
        yield return t;
      }
    }

    /// <summary>
    /// Enumerates the collection and copies the data into a DataTable, but also includes columns for the key attributes of related Entities.
    /// </summary>
    /// <typeparam name="T">The element type of the collection.</typeparam>
    /// <param name="collection">The collection to copy to a DataTable</param>
    /// <param name="cx">The Entity Framework ObjectContext, used for metadata access</param>
    /// <returns>A DataTable containing the scalar projection of the collection.</returns>
    public static DataTable ToDataTable<T>(this IEnumerable<T> collection, ObjectContext cx) where T: EntityObject 
    {
      DataTable t = new DataTable();
      t.TableName = typeof(T).Name;
      EntityDataReader<T> dr = new EntityDataReader<T>(collection, cx, false);
      t.Load(dr);
      return t;
    }



 
  }
}
