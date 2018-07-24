using System;
using System.Collections.Generic;
using Parquet.Data;

namespace Parquet.Windows.Universal.Core
{
   public class Frame
   {
      public Frame(DataField[] dataFields)
      {
         Fields = dataFields;
         DataPoints = new Array[Fields.Length];
      }

      public DataField[] Fields { get; }
      public Array[] DataPoints { get; }
      public int RowCount { get; internal set; }

      internal void AppendToArray(int idx, Array dataPoints)
      {
         if (DataPoints[idx] == null)
         {
            DataPoints[idx] = dataPoints;
         }
         else
         {
            dataPoints.CopyTo(DataPoints[idx], 0);
         }
      }

      internal object[] GetRow(int rn)
      {
         object[] vals = new object[Fields.Length];
         for (int i = 0; i < Fields.Length; i++)
         {
            vals[i] = DataPoints[i].GetValue(rn);
         }
         return vals;
      }
   }
}