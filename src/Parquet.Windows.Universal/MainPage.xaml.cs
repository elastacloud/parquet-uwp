using System;
using System.Collections.ObjectModel;
using Parquet.Data;
using Parquet.Windows.Universal.Controls;
using Parquet.Windows.Universal.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Parquet.Windows.Universal
{
   /// <summary>
   /// An empty page that can be used on its own or navigated to within a Frame.
   /// </summary>
   public sealed partial class MainPage : Page
   {
      public MainPage()
      {
         this.InitializeComponent();
      }

      private async void SettingsButton_Click(object sender, RoutedEventArgs e)
      {
         // put modal dialog here 
         var samples = new ParquetUwpFunctions();

         var dialog = new ContentDialog()
         {
            PrimaryButtonText = "Accept",
            IsPrimaryButtonEnabled = true,
            CloseButtonText = "Cancel",
            Title = "Add Settings"
         };

         var text = new TextBlock {Text = "Select sample size:"};
         var combo = new ComboBox
         {
            ItemsSource = _items,
            DisplayMemberPath = "Level",
            SelectedValuePath = "Level",
            SelectedValue = samples.SampleSize
         };

         var panel = new StackPanel();
         panel.Children.Add(text);
         panel.Children.Add(combo);
         dialog.Content = panel;
         var outcome = await dialog.ShowAsync();
         if (outcome != ContentDialogResult.Primary) return;
         samples.AddSampleSize((int) combo.SelectedValue);
      }

      private readonly ObservableCollection<SampleLevel> _items = new ObservableCollection<SampleLevel>
      {
         new SampleLevel(1000),
         new SampleLevel(2000),
         new SampleLevel(3000),
         new SampleLevel(4000),
         new SampleLevel(5000),
         new SampleLevel(6000),
         new SampleLevel(7000),
         new SampleLevel(8000),
         new SampleLevel(9000),
         new SampleLevel(10000)
      };
   }

   public class SampleLevel
   {
      public SampleLevel(int level)
      {
         Level = level;
      }

      public int Level { get; set; }
   }
}
