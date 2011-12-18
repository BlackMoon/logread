using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Collections;

namespace System.Data.DBF
{
    public struct DBFHeader
    {
        public int headerSize;
        public Encoding encoding;
        public int rowSize;
        public int rowCount;
        public DateTime generationDate;
        public byte dbfType;

        public DBFHeader(byte[] headerBuffer)
        {
            if (!headerBuffer.Length.Equals(32)) throw new Exception("buffer length must have 32 byte");
            dbfType = headerBuffer[0];
            generationDate = new DateTime(headerBuffer[1], headerBuffer[2], headerBuffer[3]);
            rowCount = headerBuffer[4] + headerBuffer[5] * 256 + headerBuffer[6] * 256 * 256 + headerBuffer[7] * 256 * 256 * 256;
            headerSize = headerBuffer[8] + headerBuffer[9] * 256;
            rowSize = headerBuffer[10] + headerBuffer[11] * 256;
            switch (headerBuffer[29])
            {
                case 1:
                    encoding = Encoding.GetEncoding(437);
                    break;
                case 2:
                    encoding = Encoding.GetEncoding(850);
                    break;
                case 200:
                    encoding = Encoding.GetEncoding(1250);
                    break;
                case 201:
                    encoding = Encoding.GetEncoding(1251);
                    break;
                default:
                    encoding = Encoding.GetEncoding(866);
                    break;
            }
        }
    }

    public class DBFColumn
    {
        private DBFReader _reader;

        public DBFReader Reader
        {
            get { return _reader; }
        }

        private int _columnNo;

        public int ColumnNo
        {
            get { return _columnNo; }
        }

        private string _columnName;

        public string ColumnName
        {
            get { return _columnName; }
        }

        private char _columnDataType;

        public char ColumnDataType
        {
            get { return _columnDataType; }
        }

        private Type _columnType;

        public Type ColumnType
        {
            get { return _columnType; }
        }

        private byte _size;

        public byte Size
        {
            get { return _size; }
        }

        private byte _pointSet;

        public byte PointSet
        {
            get { return _pointSet; }
        }

        protected internal DBFColumn(DBFReader reader, int index)
            : this(reader)
        {
            Init(index);
        }

        protected internal DBFColumn(DBFReader reader)
        {
            _reader = reader;
        }

        protected internal void Init(int index)
        {
            _columnNo = index;
            byte[] byteBuffer = Reader.ReadBytes(32 * (index + 1), 32);
            Decoder dec = Reader.Encoding.GetDecoder();
            char[] charBuffer = new char[11];
            dec.GetChars(byteBuffer, 0, 11, charBuffer, 0);
            _columnName = (new String(charBuffer)).Trim().Trim("\0".ToCharArray()).ToUpper();
            charBuffer = new char[1];
            dec.GetChars(byteBuffer, 11, 1, charBuffer, 0);
            _columnDataType = charBuffer[0];
            switch (ColumnDataType)
            {
                case 'C':
                    _columnType = typeof(String);
                    break;
                case 'Y':
                    _columnType = typeof(Decimal);
                    break;
                case 'D':
                    _columnType = typeof(DateTime);
                    break;
                case 'T':
                    _columnType = typeof(DateTime);
                    break;
                case 'B':
                    _columnType = typeof(Double);
                    break;
                case 'F':
                    _columnType = typeof(Double);
                    break;
                case 'I':
                    _columnType = typeof(Int32);
                    break;
                case 'L':
                    _columnType = typeof(Boolean);
                    break;
                case 'N':
                    _columnType = typeof(Double);
                    break;
                default:
                    _columnType = typeof(byte[]);
                    break;
            }
            _size = byteBuffer[16];
            _pointSet = byteBuffer[17];
        }
    }

    public class DBFRow : IDataRecord
    {
        #region Fields and Properties

        private int _rowNo;
        public int rowNo
        {
            get { return _rowNo; }
        }

        public int startIndex
        {
            get { return Reader.HeaderSize + Reader.RowSize * rowNo; }
        }

        private bool _isDeleted;

        public bool IsDeleted
        {
            get { return _isDeleted; }
        }

        private DBFReader _reader;
        public DBFReader Reader
        {
            get { return _reader; }
        }

        private byte[] _data;
        public byte[] data
        {
            get { return _data; }
        }        

        #endregion

        #region Own Members

        protected internal DBFRow(DBFReader reader, int index)
            : this(reader)
        {
            this.Init(index);
        }

        protected internal DBFRow(DBFReader reader)
        {
            _reader = reader;
        }

        protected internal void Init(int index)
        {
            _rowNo = index;
            _data = Reader.ReadBytes(startIndex, Reader.RowSize);
            if (_data[0] == 32) _isDeleted = false;
            else _isDeleted = true;
        }

        private byte[] GetBytes(int columnNo)
        {
            int index = 1;
            for (int i = 0; i < columnNo; i++)
            {
                index = index + Reader.GetColumn(i).Size;
            }
            byte[] retval = new byte[Reader.GetColumn(columnNo).Size];
            Array.Copy(_data, index, retval, 0, retval.Length);
            return retval;
        }

        private char[] GetChars(int columnNo)
        {
            byte[] buffer = GetBytes(columnNo);
            Decoder dec = Reader.Encoding.GetDecoder();
            char[] chars = new char[buffer.Length];
            dec.GetChars(buffer, 0, buffer.Length, chars, 0);
            return chars;
        }

        public string GetKey(string[] cols)
        {
            string key = "";
            object obj;

            for (int i = 0; i < cols.Length; i++)
            {
                obj = ((IDataRecord)this)[((IDataRecord)this).GetOrdinal(cols[i])];
                key += obj.ToString().Trim();
            }
            return key;
        }

        public string GetKey(int[] cols)
        {
            string key = "";
            object obj;

            for (int i = 0; i < cols.Length; i++)
            {
                obj = ((IDataRecord)this)[cols[i]];
                key += obj.ToString().Trim();
            }
            return key;
        }

        #endregion

        #region IDataRecord Members

        int IDataRecord.FieldCount
        {
            get { return Reader.ColumnCount; }
        }

        bool IDataRecord.GetBoolean(int i)
        {
            char[] chars = GetChars(i);
            switch (chars[0])
            {
                case 'Y':
                    return true;
                case 'y':
                    return true;
                case 'T':
                    return true;
                case 't':
                    return true;
                default:
                    return false;
            }

        }

        byte IDataRecord.GetByte(int i)
        {
            byte[] buffer = GetBytes(i);
            return buffer[0];
        }

        long IDataRecord.GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        char IDataRecord.GetChar(int i)
        {
            char[] buffer = GetChars(i);
            return buffer[i];
        }

        long IDataRecord.GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        IDataReader IDataRecord.GetData(int i)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        string IDataRecord.GetDataTypeName(int i)
        {
            return Reader.GetColumn(i).ColumnType.ToString();
        }

        DateTime IDataRecord.GetDateTime(int i)
        {
            switch (Reader.GetColumn(i).ColumnDataType)
            {
                case 'D':
                    char[] buffer = GetChars(i);
                    string yyyy = buffer[0].ToString() + buffer[1].ToString() + buffer[2].ToString() + buffer[3].ToString();
                    string mm = buffer[4].ToString() + buffer[5].ToString();
                    string dd = buffer[6].ToString() + buffer[7].ToString();
                    return new DateTime(System.Convert.ToInt16(yyyy), System.Convert.ToInt16(mm), System.Convert.ToInt16(dd));
                case 'T':
                    byte[] bytes = GetBytes(i);
                    long ticks = bytes[0] + bytes[1] * 256 + bytes[2] * 256 * 256 + bytes[3] * 256 * 256 * 256;
                    return new DateTime(ticks);
                default:
                    return new DateTime();
            }
        }

        decimal IDataRecord.GetDecimal(int i)
        {
            decimal retval = 0;
            if (Reader.GetColumn(i).ColumnDataType.Equals('N') || Reader.GetColumn(i).ColumnDataType.Equals('F'))
            {
                char[] chars = this.GetChars(i);
                string retString = "";
                for (int j = 0; j < chars.Length; j++)
                {
                    retString = retString + chars[j];
                }
                retString = retString.Replace(" ", "");
                System.Globalization.NumberFormatInfo nfi = new System.Globalization.NumberFormatInfo();
                nfi.CurrencyDecimalSeparator = ".";
                retval = Decimal.Parse(retString, nfi);
            }
            else
            {
                byte[] buffer = this.GetBytes(i);
                bool sign = System.Convert.ToBoolean(buffer[buffer.Length - 1] >> 7);
                for (int j = 0; j < buffer.Length; j++)
                {
                    if (sign) retval = retval + System.Convert.ToDecimal((255 - buffer[j]) * Math.Pow(256, j));
                    else retval = retval + System.Convert.ToDecimal(buffer[j] * Math.Pow(256, j));
                }
                if (sign) return (-1 - retval) / System.Convert.ToDecimal(Math.Pow(10, Reader.GetColumn(i).PointSet));
                else return retval / System.Convert.ToDecimal(Math.Pow(10, Reader.GetColumn(i).PointSet));
            }
            return retval;
        }

        double IDataRecord.GetDouble(int i)
        {
            return System.Convert.ToDouble(((IDataRecord)this).GetDecimal(i));
        }

        Type IDataRecord.GetFieldType(int i)
        {
            return Reader.GetColumn(i).ColumnType;
        }

        float IDataRecord.GetFloat(int i)
        {
            return (float)(((IDataRecord)this).GetDecimal(i));
        }

        Guid IDataRecord.GetGuid(int i)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        short IDataRecord.GetInt16(int i)
        {
            return System.Convert.ToInt16(((IDataRecord)this).GetInt64(i));
        }

        int IDataRecord.GetInt32(int i)
        {
            return System.Convert.ToInt32(((IDataRecord)this).GetInt64(i));
        }

        long IDataRecord.GetInt64(int i)
        {
            byte[] buffer = GetBytes(i);
            bool sign = System.Convert.ToBoolean(buffer[buffer.Length - 1] >> 7);
            Int64 retval = 0;
            for (int j = 0; j < Math.Min(9, buffer.Length); j++)
            {
                if (sign) retval = System.Convert.ToInt64(retval + (255 - buffer[j]) * Math.Pow(256, j));
                else retval = System.Convert.ToInt64(retval + buffer[j] * Math.Pow(256, j));
            }
            if (sign) return (-1 - retval);
            else return retval;
        }

        string IDataRecord.GetName(int i)
        {
            return Reader.GetColumnName(i);
        }

        int IDataRecord.GetOrdinal(string name)
        {
            return Reader.GetColumnIndex(name);
        }

        string IDataRecord.GetString(int i)
        {
            return new String(GetChars(i));
        }

        object IDataRecord.GetValue(int i)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        int IDataRecord.GetValues(object[] values)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        bool IDataRecord.IsDBNull(int i)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        object IDataRecord.this[string name]
        {
            get { return ((IDataRecord)this)[((IDataRecord)this).GetOrdinal(name)]; }
        }

        object IDataRecord.this[int i]
        {
            get
            {
                switch (Reader.GetColumn(i).ColumnDataType)
                {
                    case 'C':
                        return ((IDataRecord)this).GetString(i);
                    case 'Y':
                        return ((IDataRecord)this).GetDecimal(i);
                    case 'D':
                        return ((IDataRecord)this).GetDateTime(i);
                    case 'T':
                        return ((IDataRecord)this).GetDateTime(i);
                    case 'B':
                        return ((IDataRecord)this).GetDouble(i);
                    case 'F':
                        return ((IDataRecord)this).GetDouble(i);
                    case 'I':
                        return ((IDataRecord)this).GetInt32(i);
                    case 'L':
                        return ((IDataRecord)this).GetBoolean(i);
                    case 'N':
                        return ((IDataRecord)this).GetDouble(i);
                    default:
                        return null;
                }
            }
        }       

        #endregion
    }

    public class DBFReader : IList 
    {
        #region Fields
        
        private bool _isClosed;
        private string _fileName;
        private FileStream _fs;
        private BinaryReader _dbfReader;
        private DBFHeader _header;
        private List<DBFColumn> _columns;       

        #endregion

        #region Properties

        public Encoding Encoding
        {
            get { return _header.encoding; }
        }

        public int ColumnCount
        {
            get {return _columns.Count;}
        }

        public int HeaderSize
        {
            get { return _header.headerSize; }
        }

        public int RowSize
        {
            get { return _header.rowSize; }
        }

        #endregion

        #region Constructors

        public DBFReader(string fileName) : this()
        {
            _fileName = fileName;
        }


        public DBFReader()
        {
            _isClosed = true;
            _columns = new List<DBFColumn>();
        }

        #endregion

        #region Own Members

        public bool Open()
        {
            bool bres = true;
            Close();

            try
            {
                _fs = new FileStream(_fileName, FileMode.Open, FileAccess.ReadWrite);
                _dbfReader = new BinaryReader(_fs);
                _header = new DBFHeader(_dbfReader.ReadBytes(32));
                int columnCount = (_header.headerSize - 33) / 32;
                _dbfReader.BaseStream.Position = 32;
                byte endSymbol = _dbfReader.ReadByte();

                int i = 0;
                while ((endSymbol != 13) && (i < columnCount))
                {
                    i++;
                    _dbfReader.BaseStream.Position = 32 * (i + 1);
                    endSymbol = _dbfReader.ReadByte();
                }

                if (columnCount > i) columnCount = i;
                for (i = 0; i < columnCount; i++)
                {
                    DBFColumn column = new DBFColumn(this, i);
                    _columns.Add(column);
                }

                _isClosed = false;
            }
            catch 
            {
                bres = false;
            }
            return bres;
        }
        
        public void Close()
        {
            if (!_isClosed)
            {
                _dbfReader.Close();
                _fs.Close();
                _columns.Clear();
                _isClosed = true;                              
            }
        }

        public void Clear()
        {
            Close();
            Open();
        }

        public byte[] ReadBytes(int startIndex, int length)
        {
            _dbfReader.BaseStream.Position = startIndex;
            return _dbfReader.ReadBytes(length);
        }

        public char[] ReadChars(int startIndex, int length)
        {
            _dbfReader.BaseStream.Position = startIndex;
            return _dbfReader.ReadChars(length);
        }

        public int GetColumnIndex(string columnName)
        {
            for (int i = 0; i < _columns.Count; i++)
            {
                if (_columns[i].ColumnName.Equals(columnName.ToUpper())) return _columns[i].ColumnNo;
            }
            return -1;
        }

        public string GetColumnName(int columnIndex)
        {
            if (columnIndex < 0) return null;
            if (columnIndex >= _columns.Count) return null;
            return _columns[columnIndex].ColumnName;
        }

        public DBFColumn GetColumn(int columnIndex)
        {
            if (columnIndex < 0) return null;
            if (columnIndex >= _columns.Count) return null;
            return _columns[columnIndex];
        }

        public DBFColumn GetColumn(string columnName)
        {
            return GetColumn(GetColumnIndex(columnName));
        }

        public Dictionary<string, byte[]> ReadData(List<string> keys)
        {
            string key;
            byte[] buf = null ;
            DBFRow row;
            Dictionary<string, byte[]> data = new Dictionary<string, byte[]>();

            for (int i = 0; i < Count; i++)
            {
                row = (DBFRow)this[i];
                key = row.GetKey(keys.ToArray());

                buf = new byte[RowSize];
                Array.Copy(row.data, buf, RowSize);
                data.Add(key, buf);
            }
            return data;
        }

        #endregion

        #region IList Members

        public int Add(object value)
        {
            return ((DBFRow)value).rowNo;
        }        

        public bool Contains(object value)
        {
            return value.GetType().Equals(typeof(DBFRow));
        }

        public int IndexOf(object value)
        {
            return ((DBFRow)value).rowNo;
        }

        public void Insert(int index, object value)
        {
            
        }

        public bool IsFixedSize
        {
            get { return true; }
        }

        public bool IsReadOnly
        {
            get { return true; }
        }

        public void Remove(object value)
        {
            
        }

        public void RemoveAt(int index)
        {
            
        }

        public object this[int index]
        {
            get
            {
                return new DBFRow(this, index);
            }
            set
            {
                
            }
        }

        #endregion

        #region ICollection Members

        public void CopyTo(Array array, int index)
        {
            
        }

        public int Count
        {
            get { return this._header.rowCount; }
        }

        public bool IsSynchronized
        {
            get { return true; }
        }

        public object SyncRoot
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        #endregion

        #region IEnumerable Members

        public IEnumerator GetEnumerator()
        {
            return null;
        }

        #endregion   
    }    
}