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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Parquet.Windows.Universal.Controls
{
   public sealed partial class StatisticsViewControl : UserControl, IParquetDisplay
   {
      public StatisticsViewControl()
      {
         this.InitializeComponent();
      }

      public void Display(DataSet ds)
      {
         var columnValues = new List<ColumnStatisticsView>();
         for (int i = 0; i < ds.ColumnCount; i++)
         {
            List<double> doubles = GetColumnData(ds, i);

            if(doubles.Count == 0)
            {
               columnValues.Add(new ColumnStatisticsView());
               continue;
            }

            columnValues.Add(new ColumnStatisticsView()
            {
               ColumnName = ds.Schema.ColumnNames[i],
               DistinctValuesCount = doubles.Distinct().Count(),
               Mean = doubles.Mean(),
               Sum = doubles.Sum(),
               Min = doubles.Min(),
               Max = doubles.Max(),
               Kurtosis = doubles.Kurtosis(),
               Skewness = doubles.Skewness(),
               Median = doubles.Median(),
               NullCount = doubles.Count(v => v == null),
               Quartile25 = doubles.Quartile25(),
               Quartile75 = doubles.Quartile75(),
               StandardDeviation = doubles.StandardDeviation(),
               Variance = doubles.Variance()
            });
         }

         SfGrid.ItemsSource = columnValues;
      }

      private static List<double> GetColumnData(DataSet ds, int index)
      {
         var result = new List<double>();
         int invalids = 0;
         int nulls = 0;

         IList untypedValues = ds.GetColumn(index);

         foreach (object uv in untypedValues)
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

         return result;
      }
   }
}