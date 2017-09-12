using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace Parquet.Windows.Universal.Controls
{
   public sealed partial class TabularDataGrid : UserControl, IParquetDisplay
   {
      public TabularDataGrid()
      {
         this.InitializeComponent();
      }

      public void Display(DataSet ds)
      {
         SfGrid.Columns.Clear();

         for(int i = 0; i < ds.Schema.Length; i++)
         {
            SfGrid.Columns.Add(CreateSfColumn(ds.Schema[i], i));
         }

         SfGrid.ItemsSource = ds.Select(r => new TableRowView(r));
         SfGrid.Columns.RemoveAt(SfGrid.Columns.Count - 1);
      }

      private GridColumn CreateSfColumn(SchemaElement se, int i)
      {
         GridColumn result;

         if(se.ElementType == typeof(int) ||
            se.ElementType == typeof(float) ||
            se.ElementType == typeof(double) ||
            se.ElementType == typeof(decimal))
         {
            result = new GridNumericColumn();
         }
         else if(se.ElementType == typeof(DateTime) ||
            se.ElementType == typeof(DateTimeOffset))
         {
            result = new GridDateTimeColumn();
         }
         else if(se.ElementType == typeof(bool))
         {
            result = new GridCheckBoxColumn();
         }
         else
         {
            result = new GridTextColumn();
         }

         result.MappingName = $"[{i}]";
         result.HeaderText = se.Name;
         result.AllowFiltering = false;
         result.AllowFocus = true;
         result.AllowResizing = true;
         result.AllowSorting = true;
         result.FilterBehavior = FilterBehavior.StronglyTyped;
         result.AllowEditing = true;

         result.ShowToolTip = true;
         result.ShowHeaderToolTip = true;

         return result;
      }
   }
}