using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parquet.Data;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;

namespace Parquet.Windows.Universal.Core
{
   class ParquetUniversal
   {
      public static async Task<DataSet> OpenFromFilePickerAsync()
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
            using (Stream stream = uwpStream.AsStreamForRead())
            {
               return ParquetReader.Read(stream,
                  new ParquetOptions { TreatByteArrayAsString = true });
            }
         }

      }
   }
}
