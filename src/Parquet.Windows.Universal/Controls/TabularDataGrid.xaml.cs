using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DataFrame.Math.Data;
using Parquet.Data;
using Parquet.Windows.Universal.Model;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
using Windows.ApplicationModel.DataTransfer;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Frame = DataFrame.Math.Data.Frame;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Parquet.Windows.Universal.Controls
{
   public sealed partial class TabularDataGrid : UserControl, IParquetDisplay
   {
      private readonly DataTemplate _headerTooltipTemplate;

      public TabularDataGrid()
      {
         this.InitializeComponent();

         _headerTooltipTemplate = Application.Current.Resources["HeaderTooltipTemplate"] as DataTemplate;
      }

      public void Display(Frame f)
      {
         SfGrid.Columns.Clear();

         int i = 0;
         foreach(Series s in f.Series)
         {
            SfGrid.Columns.Add(CreateSfColumn(s, i++));
         }



         SfGrid.ItemsSource = Enumerable
            .Range(0, f.RowCount)
            .Select(rn => new TableRowView(f.GetRow(rn)));

         SfGrid.Columns.RemoveAt(SfGrid.Columns.Count - 1);
      }

      private GridColumn CreateSfColumn(Series s, int i)
      {
         GridColumn result;

         if(s.DataType == typeof(int) ||
            s.DataType == typeof(float) ||
            s.DataType == typeof(double) ||
            s.DataType == typeof(decimal))
         {
            result = new GridNumericColumn();
         }
         else if(s.DataType == typeof(DateTime) ||
            s.DataType == typeof(DateTimeOffset))
         {
            result = new GridDateTimeColumn();
         }
         else if(s.DataType == typeof(bool))
         {
            result = new GridCheckBoxColumn();
         }
         else
         {
            result = new GridTextColumn();
         }

         result.MappingName = $"[{i}]";
         result.HeaderText = s.Name;
         result.AllowFiltering = false;
         result.AllowFocus = true;
         result.AllowResizing = true;
         result.AllowSorting = true;
         result.FilterBehavior = FilterBehavior.StronglyTyped;
         result.AllowEditing = true;

         result.ShowToolTip = false;

         result.ShowHeaderToolTip = true;
         result.HeaderToolTipTemplate = _headerTooltipTemplate;

         return result;
      }
   }
}