using System.Text;
using System.Collections.Generic;

namespace dBASE.NET
{
    /// <summary>
    /// DbfRecordDiff is a class to represent a diff state between records
    /// </summary>
    public class DbfRecordDiff
    {
        public DbfRecordDiff()
        {
            
        }
        public DbfRecordDiff(int recordIndex,DiffState state,IEnumerable<DbfColumnChange> columnsChanged,DbfRecord record)
        {
            Record = record;
            ColumnsChanged = columnsChanged;
            RecordIndex = recordIndex;
            State = state;            
        }
        public DbfRecordDiff(DbfRecord record,int recordIndex)
        {            
            Record = record;
            RecordIndex = recordIndex;
            State = DiffState.Unmodified;            
        }        
        /// <summary>
        /// The index
        /// </summary>
        /// <value></value>
        public int RecordIndex { get; private set; }
        public DbfRecord Record { get; private set; }
        public IEnumerable<DbfColumnChange> ColumnsChanged { get; private set; } = new List<DbfColumnChange>();       
        public DiffState State { get; private set; }
        public override string ToString()
        {
            var strBuilder = new StringBuilder();
            strBuilder.AppendFormat("Index: {0}, State: {1}", this.RecordIndex, this.State);

            if ((this.State == DiffState.Modified) || (this.State == DiffState.Added))
            {
                strBuilder.AppendLine();
                strBuilder.AppendLine("  {");
                foreach (DbfColumnChange columnDiff in this.ColumnsChanged)
                {
                    strBuilder.Append("    { ");
                    strBuilder.Append(columnDiff.Field.Name);

                    if (this.State == DiffState.Modified)
                    {
                        strBuilder.Append(", '");
                        strBuilder.Append(columnDiff.OldValue);
                        strBuilder.Append("'");
                    }

                    strBuilder.Append(", '");
                    strBuilder.Append(columnDiff.NewValue);
                    strBuilder.Append("'");
                    strBuilder.AppendLine("    }, ");
                }

                strBuilder.AppendLine("  }");
            }

            return strBuilder.ToString();
        }
    }
    public class DbfColumnChange
    {        
        public DbfField Field { get; set; }
        public object OldValue { get; set; }
        public object NewValue { get; set; }

    }
    public enum DiffState
    {
        Modified,
        Unmodified,
        Added         
    }
}