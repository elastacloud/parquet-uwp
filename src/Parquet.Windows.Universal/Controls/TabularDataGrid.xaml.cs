using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Parquet.Data;
using Parquet.Windows.Universal.Model;
using Syncfusion.Data;
using Syncfusion.UI.Xaml.Grid;
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
            SchemaElement se = ds.Schema.Elements[i];

            SfGrid.Columns.Add(new GridTextColumn()
            {
               MappingName = $"[{i}]",
               HeaderText = se.Name,
               AllowFiltering = true,
               AllowFocus = true,
               AllowResizing = true,
               AllowSorting = true,
               FilterBehavior = FilterBehavior.StringTyped
            });
         }

         SfGrid.ItemsSource = ds.Select(r => new TableRowView(r));
         SfGrid.Columns.RemoveAt(SfGrid.Columns.Count - 1);
      }
   }
}