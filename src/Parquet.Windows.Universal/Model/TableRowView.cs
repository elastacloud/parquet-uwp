using System.Collections.Generic;
using System.Linq;

namespace DataScienceStudio.Model
{
   public class TableRowView
   {
      private readonly object[] _parquetRow;

      public TableRowView(IReadOnlyCollection<object> row)
      {
         this._parquetRow = row.ToArray();
      }

      public object this[int i] => _parquetRow[i];
   }
}
