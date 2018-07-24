using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Parquet.Windows.Universal.Core;

namespace DataScienceStudio.Model
{
   public class DataFrameView
   {
      public DataFrameView(Frame df, string fileName)
      {
         Df = df;
         FileName = fileName;
      }

      public Frame Df { get; }

      public string FileName { get; }

      public override string ToString()
      {
         return FileName;
      }
   }
}
