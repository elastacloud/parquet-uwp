using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using DataFrame.Math.Data;
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
   public sealed partial class ProjectWorkspace : Windows.UI.Xaml.Controls.UserControl
   {
      public ProjectWorkspace()
      {
         this.InitializeComponent();

         DataSources.DataFrameViewChanged += DataSources_DataFrameViewChanged;
      }

      private void DataSources_DataFrameViewChanged(Model.DataFrameView obj)
      {
         this.DataFrame = obj.Df;
         this.RowCount.Text = obj.Df.RowCount.ToString();
         if (obj.Df.RowCount == 1)
         {
            this.RowCountLabel.Text = "row";
         }
      }

      #region [ temp ]

      public Frame DataFrame
      {
         get => TabularDataGrid.DataFrame;
         set => TabularDataGrid.DataFrame = value;
      }

      #endregion

   }
}
