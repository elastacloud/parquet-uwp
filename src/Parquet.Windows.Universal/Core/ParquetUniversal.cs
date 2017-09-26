using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFrame.Formats;
using DataFrame.Math.Data;
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
      public static async Task<Frame> OpenFromFilePickerAsync()
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
            return await OpenAsync(uwpStream);
         }
      }

      public static async Task<Frame> OpenFromDragDropAsync(DragEventArgs e)
      {
         if (e.DataView.Contains(StandardDataFormats.StorageItems))
         {
            IReadOnlyList<IStorageItem> items = await e.DataView.GetStorageItemsAsync();

            if (items.Count > 0)
            {
               var storageFile = items[0] as StorageFile;

               using (IRandomAccessStreamWithContentType uwpStream = await storageFile.OpenReadAsync())
               {
                  return await OpenAsync(uwpStream);
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

            return Matrix.Read().Parquet().FromStream(stream);
         }
      }
   }
}
