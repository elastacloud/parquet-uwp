using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DataFrame.Math.Data;
using DataScienceStudio.Model;
using Parquet.Windows.Universal.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
//using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace DataScienceStudio.Controls.Project
{
   public sealed partial class DataSources : Windows.UI.Xaml.Controls.UserControl
   {
      public event Action<DataFrameView> DataFrameViewChanged;

      public DataSources()
      {
         this.InitializeComponent();
      }

      private async void AddDataSource_Click(object sender, RoutedEventArgs e)
      {
         DataFrameView dfv = await ParquetUniversal.OpenFromFilePickerAsync();
         NotYetLoaded.Visibility = Visibility.Collapsed;

         DataSourcesList.Items.Add(dfv);
         if (DataSourcesList.Items.Count == 1)
         {
            DataSourcesList.SelectedIndex = 0;
            DataFrameViewChanged?.Invoke(DataSourcesList.SelectedItem as DataFrameView);
         }
      }

      private void DataSourcesList_SelectionChanged(object sender, Windows.UI.Xaml.Controls.SelectionChangedEventArgs e)
      {
         if (DataSourcesList.SelectedItem == null) return;

         DataFrameViewChanged?.Invoke(DataSourcesList.SelectedItem as DataFrameView);
      }
   }
}
