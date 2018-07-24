using System.Collections.Generic;
using System.Linq;

namespace DataScienceStudio.Model
{
   public class TableRowView
   {
      private readonly object[] _parquetRow;

      public TableRowView(object[] row)
      {
         this._parquetRow = row;
      }

      public object this[int i] => _parquetRow[i];
   }
}
