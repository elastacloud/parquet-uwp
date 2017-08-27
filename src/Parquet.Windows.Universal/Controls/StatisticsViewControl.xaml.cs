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
          var summary = new DataSetSummaryStats(ds);
          for (int i = 0; i < ds.ColumnCount; i++)
          {
             var column = summary.GetColumnStats(i);
             columnValues.Add(new ColumnStatisticsView()
             {
                ColumnName = column.ColumnName,
                DistinctValuesCount = (int)column.DistinctValuesCount,
                Mean = column.Mean,
                Sum = column.Sum,
                Min = column.Min,
                Max = column.Max,
                Kurtosis = column.Kutosis,
                Skewness = column.Skewness,
                Median = column.Median,
                NullCount = column.NullCount,
                Quartile25 = column.Quartile25,
                Quartile75 = column.Quartile75,
                StandardDeviation = column.StandardDeviation,
                Variance = column.Variance
             });
          }

          SfGrid.ItemsSource = columnValues;
      }
    }
}
