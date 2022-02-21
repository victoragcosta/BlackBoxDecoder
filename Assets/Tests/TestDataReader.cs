using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using Amostra1Expected;
using BlackBoxFileReader;

public class TestDataExtractor
{
  [Test]
  public void ReadsMpuCorrectly()
  {
    var streamReader = new StreamReader("Assets/Tests/Amostra1Expected/amostra1.txt");
    var dataReader = new Reader(streamReader);

    Assert.IsTrue(dataReader.FinishedReading, "dataReader.FinishedReading");

    // Test if metadata was captured correctly

    foreach (var item in dataReader.MpuSectionReader.MetadataTypes.Keys)
    {
      var data = dataReader.MpuSectionReader.GetMetadata(item);
      var expectedData = Mpu.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }

    Assert.AreEqual(Mpu.Metadata["n_samples"], dataReader.MpuSectionReader.ValuesCount);
    Assert.AreEqual(dataReader.MpuSectionReader.GetMetadata("n_samples"), dataReader.MpuSectionReader.ValuesCount);

    for (int i = 0; i < Mpu.Data.Count; i++)
    {
      var data = dataReader.MpuSectionReader.GetValue(i);
      var expectedData = Mpu.Data[i];

      int j = 0;
      foreach (var keyValue in data)
      {
        var key = keyValue.Key;
        var value = keyValue.Value;

        Assert.IsInstanceOf(
          expectedData[j].GetType(),
          value,
          $"Type of value item {key} is wrong."
        );
        Assert.AreEqual(expectedData[j], value, $"Value item {key} was read wrong.");
        j++;
      }

    }
  }
}
