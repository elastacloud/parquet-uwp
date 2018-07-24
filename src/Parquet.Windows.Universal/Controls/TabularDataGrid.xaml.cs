using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DataScienceStudio.Model;
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
using Frame = Parquet.Windows.Universal.Core.Frame;

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

      public void Display(Frame df)
      {
         SfGrid.Columns.Clear();

         int i = 0;
         foreach(DataField s in df.Fields)
         {
            SfGrid.Columns.Add(CreateSfColumn(s, i++));
         }


         SfGrid.ItemsSource = Enumerable
            .Range(0, df.RowCount)
            .Select(rn => new TableRowView(df.GetRow(rn)));

         SfGrid.Columns.RemoveAt(SfGrid.Columns.Count - 1);
      }

      private GridColumn CreateSfColumn(DataField s, int i)
      {
         GridColumn result;

         if(isNumeric(s.DataType))
         {
            result = new GridNumericColumn();
         }
         else if(s.DataType == DataType.DateTimeOffset)
         {
            result = new GridDateTimeColumn();
         }
         else if(s.DataType == DataType.Boolean)
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

      private bool isNumeric(DataType dataType)
      {
         return dataType == DataType.Short || dataType == DataType.UnsignedShort || dataType == DataType.Int16 || dataType == DataType.UnsignedInt16 || dataType == DataType.Int32 || dataType == DataType.Int64 || dataType == DataType.Int96;
      }
   }
}