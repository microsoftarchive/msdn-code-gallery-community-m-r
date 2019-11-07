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
  using System.ComponentModel;


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

    IEnumerator<T> enumerator;
    T current;
    bool allowNullableColumns = true;
    bool closed = false;
    protected static Dictionary<Type, IList<Attribute>> propertyCache = new Dictionary<Type, IList<Attribute>>();
    protected IList<Attribute> attributes;

    #region Attribute inner type
    protected class Attribute
    {
      public delegate TProp 
           PropertyGetter<TProp>(T obj);

      PropertyInfo propertyInfo;
      public readonly Type Type;
      public readonly string Name;
      readonly Func<T, object> ValueAccessor;
      readonly Delegate DelegateAccessor;

      PropertyGetter<Int32> Int32Getter = null;
      PropertyGetter<Int16> Int16Getter = null;
      PropertyGetter<Int64> Int64Getter = null;
      PropertyGetter<string> StringGetter = null;
      PropertyGetter<Decimal> DecimalGetter = null;
      PropertyGetter<DateTime> DateTimeGetter = null;
      PropertyGetter<Guid> GuidGetter = null;

      readonly EvalType evalType;

      enum EvalType
      {
        Int16Getter,
        Int32Getter,
        Int64Getter,
        StringGetter,
        DecimalGetter,
        DateTimeGetter,
        GuidGetter,
        PropertyInfo,
        ValueAccessor
      }

      public Attribute(PropertyInfo pi)
      {
        this.propertyInfo = pi;
        this.Name = pi.Name;
        ValueAccessor = null;
        Type = pi.PropertyType;

        //get the property get method
        MethodInfo mi = pi.GetGetMethod();

        //create a delegate for the type
        DelegateAccessor = Delegate.CreateDelegate(typeof(PropertyGetter<int>)
                                   .GetGenericTypeDefinition()
                                   .MakeGenericType(typeof(T), pi.PropertyType), mi);

        //bind out typed delegates for common scalar types.
        //Then in GetValue switch to the appropriate typed getter instead
        //of using PropertyInfo.GetValue(object,object[]).
        //In testing, iterating 1,000,000 rows with 10 attributes per row 
        //took 72sec using PropertyInfo.GetValue, and took 2.7sec using
        //the switch(evalType) and jump to the typed delegate
        Int16Getter = DelegateAccessor as PropertyGetter<Int16>;
        Int32Getter = DelegateAccessor as PropertyGetter<Int32>;
        Int64Getter = DelegateAccessor as PropertyGetter<Int64>;
        StringGetter = DelegateAccessor as PropertyGetter<String>;
        DecimalGetter = DelegateAccessor as PropertyGetter<Decimal>;
        DateTimeGetter = DelegateAccessor as PropertyGetter<DateTime>;
        GuidGetter = DelegateAccessor as PropertyGetter<Guid>;

        if (Type == typeof(Int16))
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.Int16Getter;
        }
        else if (Type == typeof(Int32))
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.Int32Getter;
        }
        else if (Type == typeof(Int64))
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.Int64Getter;
        }
        else if (Type == typeof(String))
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.StringGetter;
        }
        else if (Type == typeof(Decimal))
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.DecimalGetter;
        }
        else if (Type == typeof(DateTime))
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.DateTimeGetter;
        }
        else if (Type == typeof(Guid))
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.GuidGetter;
        }
        else
        {
          evalType = EntityDataReader<T>.Attribute.EvalType.PropertyInfo;
          Console.WriteLine(Type.Name);
        }

      }
      public Attribute(string name, Type type, Func<T, object> getValue)
      {
        this.Name = name;
        this.Type = type;
        this.ValueAccessor = getValue;
        evalType = EntityDataReader<T>.Attribute.EvalType.ValueAccessor;
      }
      
      public object GetValue(T target)
      {
        switch (evalType)
        {
          case EntityDataReader<T>.Attribute.EvalType.Int16Getter:
            return Int16Getter(target);

          case EntityDataReader<T>.Attribute.EvalType.Int32Getter:
            return Int32Getter(target);

          case EntityDataReader<T>.Attribute.EvalType.Int64Getter:
            return Int64Getter(target);

          case EntityDataReader<T>.Attribute.EvalType.StringGetter:
            return StringGetter(target);

          case EntityDataReader<T>.Attribute.EvalType.DateTimeGetter:
            return DateTimeGetter(target);

          case EntityDataReader<T>.Attribute.EvalType.DecimalGetter:
            return DecimalGetter(target);

          case EntityDataReader<T>.Attribute.EvalType.GuidGetter:
            return GuidGetter(target);

          case EntityDataReader<T>.Attribute.EvalType.PropertyInfo:
            return propertyInfo.GetValue(target, null);

          case EntityDataReader<T>.Attribute.EvalType.ValueAccessor:
            return ValueAccessor(target);

          default:
            throw new InvalidOperationException("Invalid execution Path");
        }
      }
    }
    #endregion

 
    #region Constructors

    public EntityDataReader(IEnumerable<T> col) :  this(col, true) { }
    public EntityDataReader(IEnumerable<T> col, bool allowNullableColumns) :  this(col, null, allowNullableColumns) { }
    public EntityDataReader(IEnumerable<T> col, ObjectContext objectContext, bool allowNullableColumns) 
    {
      this.enumerator = col.GetEnumerator();
      this.allowNullableColumns = allowNullableColumns;
     
      Type thisType = typeof(T);

      //try to find the row metadata for this type in the cache.
      if (propertyCache.ContainsKey(thisType))
      {
        this.attributes = propertyCache[thisType];
        return;
      }
          
      //generate the metadata.  Done without a lock, so it's possible but harmless that this will be done more than once on startup
      var allProperties = (from p in thisType.GetProperties()
                          where p.PropertyType != typeof(EntityKey)   //EF infrastructure properties
                             && p.PropertyType != typeof(EntityState)  //EF infrastructure properties
                             && !typeof(EntityObject).IsAssignableFrom(p.PropertyType)  //EF Objects
                             && !typeof(INotifyPropertyChanged).IsAssignableFrom(p.PropertyType) //L2S objects
                          select new 
                          {
                            PropertyInfo=p,
                            IsRelationProperty = typeof(RelatedEnd).IsAssignableFrom(p.PropertyType)
                          }).ToList();


      var attributeList = new SortedList<string, Attribute>();

      //Not a collection of entities, just an IEnumerable<String> or other scalar type.
      if (allProperties.Count == 0)
      {
        attributeList.Add("Value", new Attribute("Value",typeof(T), v => v));
        this.attributes = attributeList.Values;
        return;
      }
      
      //add the normal scalar properties
      foreach (var pi in from p in allProperties
                         where !p.IsRelationProperty
                         select p.PropertyInfo)
      {
        attributeList.Add(pi.Name,new Attribute(pi));
      }

      //if there is no ObjectContext, then just return the scalar properties
      if (objectContext == null)
      {
        this.attributes = attributeList.Values;
        return;
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
                         where p.IsRelationProperty
                         select p.PropertyInfo)
      {
        //Skip EntityCollection relationships.  
        //We only want relationships with ZeroOrOne cardinality on the "to" side.
        if (!typeof(EntityReference).IsAssignableFrom(pi.PropertyType))
        {
          continue;
        }

        //Find the CLR Type at the other end of the relationship because we need to get its key attributes.
        //the property type is EntityReference<T>, we need T.
        Type relatedEntityCLRType = pi.PropertyType.GetGenericArguments().First();

        //Find the EntityType at the other end of the relationship because we need to get its key attributes.
        EntityType relatedEntityEFType = entityTypesByName[relatedEntityCLRType.FullName].FirstOrDefault();
        if (relatedEntityEFType == null)
        {
          throw new InvalidOperationException("Cannot find EntityType for Navigation Property " + pi.PropertyType.FullName);
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
              object val =  er.EntityKey.EntityKeyValues.First(k => k.Key == fkAttributeName).Value;
              return val;
            };
          attributeList.Add(fkAttributeName, new Attribute(fkAttributeName, kType, valueAccessor));
        }
        

      }
      this.attributes = attributeList.Values;

      //add to the property cache, if it's not already there
      lock (propertyCache)
      {
        if (!propertyCache.ContainsKey(thisType))
        {
          propertyCache[thisType] = this.attributes;
        }
      }

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
      if (!allowNullableColumns && this.IsNullable(t))
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
      if (!allowNullableColumns && o == null)
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
    
    public static IDataReader AsDataReader<T>(this IEnumerable<T> collection, ObjectContext cx) where T: EntityObject 
    {
      return new EntityDataReader<T>(collection,cx,true);
    }

    public static DataTable ToDataTable<T>(this IEnumerable<T> collection, ObjectContext cx) where T: EntityObject 
    {
      DataTable t = new DataTable();
      t.TableName = typeof(T).Name;
      EntityDataReader<T> dr = new EntityDataReader<T>(collection, cx, false);
      t.Load(dr);
      return t;
    }

    static IEnumerable<T> DetachAllFrom<T>(this IEnumerable<T> col, ObjectContext cx) 
    {
      foreach (var t in col)
      {
        cx.Detach(t);
        yield return t;
      }
    }


    public static IDataReader ExecuteReader<T>(this ObjectContext cx, IQueryable<T> query, bool detachObjects) 
    {
      //for non-entity objects, just use a simple property projection
      if (! typeof(EntityObject).IsAssignableFrom(typeof(T)) )
      {
        return query.AsDataReader();
      }

      if (detachObjects)
      {
        return new EntityDataReader<T>(query.DetachAllFrom(cx),cx,true);
      }
      return new EntityDataReader<T>(query,cx,true);
    }

    public static IDataReader AsDataReader<T>(this IEnumerable<T> collection)
    {
      return new EntityDataReader<T>(collection);
    }
    public static DataTable ToDataTable<T>(this IEnumerable<T> collection)
    {
      DataTable t = new DataTable();
      t.TableName = typeof(T).Name;
      EntityDataReader<T> dr = new EntityDataReader<T>(collection, false);
      t.Load(dr);
      return t;
    }
  }
}
