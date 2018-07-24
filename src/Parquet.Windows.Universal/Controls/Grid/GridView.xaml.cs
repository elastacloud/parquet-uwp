using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using UC = Windows.UI.Xaml.Controls.UserControl;
//using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DataScienceStudio.Model;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.Data;
using Frame = Parquet.Windows.Universal.Core.Frame;
using Parquet.Data;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DataScienceStudio.Controls.Grid
{
   public sealed partial class GridView : UC
   {
      private Frame _df;
      
      public GridView()
      {
         this.InitializeComponent();
      }

      public Frame DataFrame
      {
         get => this.DataContext as Frame;

         set
         {
            SfGrid.DataContext = value;
            Render(value);
         }
      }

      private void Render(Frame df)
      {
         SfGrid.Columns.Clear();

         int i = 0;
         foreach (DataField s in df.Fields)
         {
            SfGrid.Columns.Add(CreateSfColumn(s, i++));
         }



         SfGrid.ItemsSource = Enumerable
            .Range(0, df.RowCount)
            .Select(rn => new TableRowView(df.GetRow(rn)));

         SfGrid.Columns.RemoveAt(SfGrid.Columns.Count - 1);
         SfGrid.ColumnSizer = GridLengthUnitType.Auto;
      }

      private GridColumn CreateSfColumn(DataField s, int i)
      {
         GridColumn result;

         if (isNumeric(s.DataType))
         {
            result = new GridNumericColumn();
            result.HeaderTemplate = this.Resources["headerTemplateNum"] as DataTemplate;
         }
         else if (s.DataType == DataType.DateTimeOffset)
         {
            result = new GridDateTimeColumn();
            result.HeaderTemplate = this.Resources["headerTemplateDate"] as DataTemplate;
         }
         else if (s.DataType == DataType.Boolean)
         {
            result = new GridCheckBoxColumn();
            result.HeaderTemplate = this.Resources["headerTemplateBool"] as DataTemplate;
         }
         else
         {
            result = new GridTextColumn();
            result.HeaderTemplate = this.Resources["headerTemplateStr"] as DataTemplate;
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

         return result;
      }

      private bool isNumeric(DataType dataType)
      {
         return dataType == DataType.Short || dataType == DataType.UnsignedShort || dataType == DataType.Int16 || dataType == DataType.UnsignedInt16 || dataType == DataType.Int32 || dataType == DataType.Int64 || dataType == DataType.Int96;
      }

   }
}
