using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
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
      var expectedData = Amostra1Expected.Mpu.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }

    Assert.AreEqual(Amostra1Expected.Mpu.Metadata["n_samples"], dataReader.MpuSectionReader.ValuesCount);
    Assert.AreEqual(dataReader.MpuSectionReader.GetMetadata("n_samples"), dataReader.MpuSectionReader.ValuesCount);

    for (int i = 0; i < Amostra1Expected.Mpu.Data.Count; i++)
    {
      var data = dataReader.MpuSectionReader.GetValue(i);
      var expectedData = Amostra1Expected.Mpu.Data[i];

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

  [Test]
  public void ReadsGpsCorrectly()
  {
    var streamReader = new StreamReader("Assets/Tests/Amostra1Expected/amostra1.txt");
    var dataReader = new Reader(streamReader);

    Assert.IsTrue(dataReader.FinishedReading, "dataReader.FinishedReading");

    // Test if metadata was captured correctly

    foreach (var item in dataReader.GpsSectionReader.MetadataTypes.Keys)
    {
      var data = dataReader.GpsSectionReader.GetMetadata(item);
      var expectedData = Amostra1Expected.Gps.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }

    Assert.AreEqual(Amostra1Expected.Gps.Metadata["n_samples"], dataReader.GpsSectionReader.ValuesCount);
    Assert.AreEqual(dataReader.GpsSectionReader.GetMetadata("n_samples"), dataReader.GpsSectionReader.ValuesCount);

    for (int i = 0; i < Amostra1Expected.Gps.Data.Count; i++)
    {
      var data = dataReader.GpsSectionReader.GetValue(i);
      var expectedData = Amostra1Expected.Gps.Data[i];

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

  [Test]
  public void ReadsMpuCorrectlyFromAmostra2()
  {
    var streamReader = new StreamReader("Assets/Tests/Amostra2Expected/amostra2.txt");
    var dataReader = new Reader(streamReader);

    Assert.IsTrue(dataReader.FinishedReading, "dataReader.FinishedReading");

    // Test if metadata was captured correctly

    foreach (var item in dataReader.MpuSectionReader.MetadataTypes.Keys)
    {
      var data = dataReader.MpuSectionReader.GetMetadata(item);
      var expectedData = Amostra2Expected.Mpu.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }

    Assert.AreEqual(Amostra2Expected.Mpu.Metadata["n_samples"], dataReader.MpuSectionReader.ValuesCount);
    Assert.AreEqual(dataReader.MpuSectionReader.GetMetadata("n_samples"), dataReader.MpuSectionReader.ValuesCount);

    for (int i = 0; i < Amostra2Expected.Mpu.Data.Count; i++)
    {
      var data = dataReader.MpuSectionReader.GetValue(i);
      var expectedData = Amostra2Expected.Mpu.Data[i];

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

  [Test]
  public void ReadsGpsCorrectlyFromAmostra2()
  {
    var streamReader = new StreamReader("Assets/Tests/Amostra2Expected/amostra2.txt");
    var dataReader = new Reader(streamReader);

    Assert.IsTrue(dataReader.FinishedReading, "dataReader.FinishedReading");

    // Test if metadata was captured correctly

    foreach (var item in dataReader.GpsSectionReader.MetadataTypes.Keys)
    {
      var data = dataReader.GpsSectionReader.GetMetadata(item);
      var expectedData = Amostra2Expected.Gps.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }

    Assert.AreEqual(Amostra2Expected.Gps.Metadata["n_samples"], dataReader.GpsSectionReader.ValuesCount);
    Assert.AreEqual(dataReader.GpsSectionReader.GetMetadata("n_samples"), dataReader.GpsSectionReader.ValuesCount);

    for (int i = 0; i < Amostra2Expected.Gps.Data.Count; i++)
    {
      var data = dataReader.GpsSectionReader.GetValue(i);
      var expectedData = Amostra2Expected.Gps.Data[i];

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
