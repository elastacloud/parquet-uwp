using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parquet.Data;

namespace Parquet.Windows.Universal
{
    public interface IParquetDisplay
    {
       void Display(DataSet ds);
    }
}
