//===============================================================================
// Copyright © 2008 Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
//===============================================================================
//Version 1.0.0.1 Added GetSchemaTable support for Loading DataTables from EntityDataReader

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using System.Data.SqlClient;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Data.Common;
using System.Data.Objects.DataClasses;
using System.Data.Metadata.Edm;

namespace Microsoft.Samples.EntityDataReader
{
  class PeekableEnumerator<T> : IEnumerator<T>
  {
    IEnumerator<T> inner;
    bool noItems = false;
    bool onFirstItem = false;
    T firstItem;
    public bool HasItems
    {
      get
      {
        return !noItems;
      }
    }
    public T FirstItem
    {
      get
      {
        return firstItem;
      }
    }
    public PeekableEnumerator(IEnumerator<T> innerEnumerator)
    {
      this.inner = innerEnumerator;
      if (!inner.MoveNext())
      {
        noItems = true;
        return;
      }
      onFirstItem = true;
      firstItem = inner.Current;

    }


    #region IEnumerator<T> Members

    public T Current
    {
      get 
      {
        return inner.Current;        
      }
    }


    public void Dispose()
    {
      inner.Dispose();
    }


    object System.Collections.IEnumerator.Current
    {
      get { return Current; }
    }

    public bool MoveNext()
    {
      if (noItems)
      {
        return false;
      }
      if (onFirstItem)
      {
        onFirstItem = false;
        return true;
      }
      return inner.MoveNext();
    }

    public void Reset()
    {
      if (onFirstItem)
      {
        throw new NotSupportedException();
      }
      inner.Reset();
    }

    #endregion
  }

  class C
  {
    public object Current { get { return null; } }
    public void MoveNext() { }
    public void Reset() { }
    public Enumerator GetEnumerator() { return null; }

    void f()
    {
      C c;
      foreach (var e in c)
      {
      }
    }
  }

  class EntityDataReader<T> : DbDataReader, IDataReader
  {

    PeekableEnumerator<T> enumerator;
    bool closed = false;
    bool eof = false;
    bool allowNullableColumns = true;

    class Attribute
    {
      public Attribute(PropertyInfo pi)
      {
        this.propertyInfo = pi;
        this.Name = pi.Name;
        GetValue = o => pi.GetValue(o, null);
        Type = pi.PropertyType;
      }
      public Attribute(string name, Type type, Func<object, object> getValue)
      {
        this.Name = name;
        this.Type = type;
        this.GetValue = getValue;
      }
      PropertyInfo propertyInfo;
      public readonly Type Type;
      public readonly string Name;
      public readonly Func<object, object> GetValue;
    }
    static Dictionary<Type, IList<Attribute>> propertyCache = new Dictionary<Type, IList<Attribute>>();
    
    IList<Attribute> props;



    public EntityDataReader(IEnumerable<T> col)
    {
      this.enumerator = new PeekableEnumerator<T>(col.GetEnumerator());

      
      Type t = typeof(T);
      //considering some support for untyped collections
      //nb a is b does not imply List<a> is IList<b>, so this is only true
      //for object[] and types explicitly implementing IList<object>
      //if (col is IList<object> && ((IList<object>)col).Count > 0)
      //{
      //  IList<object> list = (IList<object>)col;
      //  t = list[0].GetType();        
      //}

      if (!propertyCache.ContainsKey(t))
      {
        lock (propertyCache)
        {
          if (!propertyCache.ContainsKey(t))
          {
            var cc = from p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                     //where typeof(IConvertible).IsAssignableFrom(p.PropertyType)
                     select p;
            List<Attribute> props = new List<Attribute>();
            foreach (var p in t.GetProperties(BindingFlags.Instance | BindingFlags.Public))
            {
              Type pt = p.PropertyType;
              if (typeof(IConvertible).IsAssignableFrom(pt))
              {
                props.Add(new Attribute(p));
              }
              else if (pt == typeof(string) || pt == typeof(Guid) || pt == typeof(byte[]))
              {
                props.Add(new Attribute(p));
              }
              else if (pt.IsGenericType && pt.GetGenericTypeDefinition() == typeof(int?).GetGenericTypeDefinition())
              {
                props.Add(new Attribute(p));
              }
              else if (pt.Assembly == typeof(int).Assembly)
              {
                throw new InvalidOperationException(pt.Name + " Type not supported");
              }

              //else if (pt.Name == "EntityReference`1")
              //{
                

              //  StructuralObject  entity;

              //  if (!enumerator.HasItems)
              //  {
              //    throw new InvalidOperationException("Can't support empty collection of EntityFramework entities");
              //  }
              //  entity = (StructuralObject)(object)enumerator.FirstItem;
              //  //Console.WriteLine(entity.EntityKey.EntityContainerName);
              //  var mq = entity.;
                
              //  //EntityReference er = (EntityReference)p.GetValue(enumerator.FirstItem,null);
              //  //int ix = 0;
              //  //foreach (var fkp in er.EntityKey.EntityKeyValues)
              //  //{
                  
              //  //  int i = ix;
              //  //  EntityReference e = er;
              //  //  Attribute fka = new EntityDataReader<T>.Attribute(fkp.Key,fkp.Value.GetType(), 
              //  //    e => ((StructuralType)e)
                  
              //  //}
                
                
              //}
            }
            
            
            propertyCache.Add(t,cc.Select(p => new Attribute(p)).ToList());
          }
        }
      }
      this.props = propertyCache[t];
      
    }

    public EntityDataReader(IEnumerable<T> col, bool allowNullableColumns)
      : this(col)
    {
      this.allowNullableColumns = allowNullableColumns;
    }

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

    public override bool Read()
    {
      if (eof)
        return false;

      eof = !enumerator.MoveNext();

      return !eof;
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
        return props.Count ;
      }
    }


    TField GetValue<TField>(int i)
    {
      return (TField)props[i].GetValue(enumerator.Current);
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
      return props[i].Type.Name;
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
      Type t = props[i].Type;
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
      return props[i].Name;
    }

    public override int GetOrdinal(string name)
    {
      for (int i = 0; i < props.Count; i++)
      {
        if (props[i].Name == name)
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

    public override object GetValue(int i)
    {
      object o = GetValue<object>(i);
      if (!allowNullableColumns && o == null)
      {
        return DBNull.Value;
      }
      return o;
    }

    public override int GetValues(object[] values)
    {
      for (int i = 0; i < props.Count; i++)
      { 
        values[i] = GetValue(i);
      }
      return props.Count;
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



    public override System.Collections.IEnumerator GetEnumerator()
    {
      return this.enumerator;
    }

    public override bool HasRows
    {
      get { return this.enumerator.HasItems; }
    }
  }


  public static class AsDataReaderExtension
  {
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
