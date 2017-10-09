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
using Parquet.Windows.Universal.Core;
using Windows.ApplicationModel.DataTransfer;
using Frame = DataFrame.Math.Data.Frame;
using DataScienceStudio.Model;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Parquet.Windows.Universal.Controls
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class TabControl : UserControl
   {
      public TabControl()
      {
         this.InitializeComponent();

         TabControlItems.Visibility = Visibility.Collapsed;
         EmptyPanel.Visibility = Visibility.Visible;
      }

      public void SetDataset(Frame f)
      {
         if (f == null) return;

         TabControlItems.Visibility = Visibility.Visible;
         EmptyPanel.Visibility = Visibility.Collapsed;

         TabularDataGrid.DataFrame = f;
         StatisticsViewControl.Display(f);
      }

      private async void OpenFileButton_Click(object sender, RoutedEventArgs e)
      {
         DataFrameView f = await ParquetUniversal.OpenFromFilePickerAsync();

         SetDataset(f.Df);
      }

      private void EmptyPanel_DragOver(object sender, DragEventArgs e)
      {
         e.AcceptedOperation = DataPackageOperation.Copy;
      }

      private async void UserControl_Drop(object sender, DragEventArgs e)
      {
         DataFrameView dfv = await ParquetUniversal.OpenFromDragDropAsync(e);

         SetDataset(dfv.Df);
      }
   }
}