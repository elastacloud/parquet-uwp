using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFrame.Formats;
using DataFrame.Math.Data;
using DataScienceStudio.Model;
using Parquet.Data;
using Windows.ApplicationModel.DataTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;

namespace Parquet.Windows.Universal.Core
{
   class ParquetUniversal
   {
      public static async Task<DataFrameView> OpenFromFilePickerAsync()
      {
         var picker = new FileOpenPicker
         {
            ViewMode = PickerViewMode.List,
            SuggestedStartLocation = PickerLocationId.DocumentsLibrary
         };
         picker.FileTypeFilter.Add(".parquet");

         StorageFile file = await picker.PickSingleFileAsync();

         using (IRandomAccessStreamWithContentType uwpStream = await file.OpenReadAsync())
         {
            Frame df = await OpenAsync(uwpStream);

            return new DataFrameView(df, file.Name);
         }
      }

      public static async Task<DataFrameView> OpenFromDragDropAsync(DragEventArgs e)
      {
         if (e.DataView.Contains(StandardDataFormats.StorageItems))
         {
            IReadOnlyList<IStorageItem> items = await e.DataView.GetStorageItemsAsync();

            if (items.Count > 0)
            {
               var storageFile = items[0] as StorageFile;

               using (IRandomAccessStreamWithContentType uwpStream = await storageFile.OpenReadAsync())
               {
                  Frame df = await OpenAsync(uwpStream);

                  return new DataFrameView(df, storageFile.Name);
               }
            }
         }

         return null;
      }

      private async static Task<Frame> OpenAsync(IRandomAccessStreamWithContentType uwpStream)
      {
         using (Stream stream = uwpStream.AsStreamForRead())
         {
            var readerOptions = new ReaderOptions()
            {
               Count = new ParquetUwpFunctions().SampleSize,
               Offset = 0
            };

            return Frame.Read.Parquet(stream, new ParquetOptions { TreatByteArrayAsString = true }, new ReaderOptions { Offset = 0, Count = 100 });
         }
      }
   }
}
