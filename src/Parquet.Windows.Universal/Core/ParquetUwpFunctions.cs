using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Parquet.Windows.Universal.Core
{
    internal class ParquetUwpFunctions
    {
       internal ParquetUwpFunctions()
       {
          ApplicationDataContainer localSettings = ApplicationData.Current.LocalSettings;
          ParquetContainer = !localSettings.Containers.ContainsKey("ParquetContainer")
             ? localSettings.CreateContainer("ParquetContainer", ApplicationDataCreateDisposition.Always)
             : localSettings.Containers["ParquetContainer"];
       }

       internal ApplicationDataContainer ParquetContainer { get; private set; }
       
       internal int SampleSize => !String.IsNullOrEmpty(ParquetContainer.Values["Level"] as string) ? 
         int.Parse((string) ParquetContainer.Values["Level"]) : 
         5000;

       internal void AddSampleSize(int sampleSize)
       {
          ParquetContainer.Values.Remove("Level");
          ParquetContainer.Values.Add("Level", Convert.ToString(sampleSize));
       }
    }
}
