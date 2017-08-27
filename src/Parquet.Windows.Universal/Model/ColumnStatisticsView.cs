using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parquet.Windows.Universal.Model
{
   public class ColumnStatisticsView
   {
      public string ColumnName { get; set; }
      public double Sum { get; set; }
      public int NullCount { get; set; }
      public double Max { get; set; }
      public double Min { get; set; }
      public double Mean { get; set; }
      public double StandardDeviation { get; set; }
      public double Median { get; set; }
      public double Quartile25 { get; set; }
      public double Quartile75 { get; set; }
      public double Skewness { get; set; }
      public double Kurtosis { get; set; }
      public double Variance { get; set; }
      public int DistinctValuesCount { get; set; }
   }
}
