using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Parquet.Data;
using Parquet.Windows.Universal.Model;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using System.Collections;
using Frame = DataFrame.Math.Data.Frame;
using DataFrame.Math.Data;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Parquet.Windows.Universal.Controls
{
   public sealed partial class StatisticsViewControl : UserControl, IParquetDisplay
   {
      public StatisticsViewControl()
      {
         this.InitializeComponent();
      }

      public void Display(Frame f)
      {
         var columnValues = new List<ColumnStatisticsView>();
         for (int i = 0; i < f.Series.Count; i++)
         {
            List<double> doubles = GetColumnData(f, i, out int nullCount);

            if(doubles.Count == 0)
            {
               columnValues.Add(new ColumnStatisticsView());
               continue;
            }

            columnValues.Add(new ColumnStatisticsView()
            {
               ColumnName = f[i].Name,
               DistinctValuesCount = doubles.Distinct().Count(),
               Mean = doubles.Mean(),
               Sum = doubles.Sum(),
               Min = doubles.Min(),
               Max = doubles.Max(),
               Kurtosis = doubles.Kurtosis(),
               Skewness = doubles.Skewness(),
               Median = doubles.Median(),
               NullCount = nullCount,
               Quartile25 = doubles.Quartile25(),
               Quartile75 = doubles.Quartile75(),
               StandardDeviation = doubles.StandardDeviation(),
               Variance = doubles.Variance()
            });
         }

         SfGrid.ItemsSource = columnValues;
      }

      private static List<double> GetColumnData(Frame f, int index, out int nullCount)
      {
         var result = new List<double>();
         int invalids = 0;
         int nulls = 0;

         Series s = f[index];

         if (s.DataType == typeof(double))
         {
            result = (List<double>)s.Data;
            nullCount = 0;
            return result;
         }

         foreach (object uv in s.Data)
         {
            if (ReferenceEquals(uv, null))
            {
               nulls += 1;
               continue;
            }

            try
            {
               double v = Convert.ToDouble(uv);
               result.Add(v);
            }
            catch (FormatException)
            {
               invalids += 1;
            }
            catch (InvalidCastException)
            {
               invalids += 1;
            }
         }

         nullCount = nulls;

         return result;
      }
   }
}