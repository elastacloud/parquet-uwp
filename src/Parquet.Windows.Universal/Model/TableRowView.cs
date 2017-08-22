using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parquet.Data;

namespace Parquet.Windows.Universal.Model
{
   public class TableRowView
   {
      private readonly Row _parquetRow;

      public TableRowView(Row parquetRow)
      {
         this._parquetRow = parquetRow;
      }

      public object this[int i]
      {
         get
         {
            return _parquetRow[i];
         }
      }

   }
}
