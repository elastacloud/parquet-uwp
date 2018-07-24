using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            using (var parquetReader = new ParquetReader(stream))
            {
               DataField[] dataFields = parquetReader.Schema.GetDataFields();
               var frame = new Frame(dataFields);

               for (int i = 0; i < parquetReader.RowGroupCount; i++)
               {
                  // create row group reader
                  using (ParquetRowGroupReader groupReader = parquetReader.OpenRowGroupReader(i))
                  {
                     // read all columns inside each row group (you have an option to read only
                     // required columns if you need to.
                     DataColumn[] columns = dataFields.Select(groupReader.ReadColumn).ToArray();
                     for (int j = 0; j < columns.Length; j++)
                     {
                        Array data = columns[j].Data;
                        if (j == 0)
                           frame.RowCount += data.Length;

                        frame.AppendToArray(j, data);
                     }
                  }
               }
               return frame;
            }
         }
      }
   }
}
