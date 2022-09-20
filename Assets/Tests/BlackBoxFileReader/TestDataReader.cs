using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using BlackBoxFileReader;
using Utils;

enum ExpectedDataType
{
  MPU,
  GPS,
  L,
}

public class TestDataReader
{
  static IEnumerable<IEnumerable<object>> TestReaders(ExpectedDataType type) {
    var streamReader1 = new StreamReader("Assets/Tests/BlackBoxFileReader/Amostra1Expected/amostra1.txt");
    var dataReader1 = new Reader(streamReader1);
    switch (type)
    {
      case ExpectedDataType.MPU:
        yield return new object[] { dataReader1, new Amostra1Expected.Mpu() };
        break;
      case ExpectedDataType.GPS:
        yield return new object[] { dataReader1, new Amostra1Expected.Gps() };
        break;
      case ExpectedDataType.L:
        yield return new object[] { dataReader1, new Amostra1Expected.L() };
        break;
    }
    streamReader1.Close();

    var streamReader2 = new StreamReader("Assets/Tests/BlackBoxFileReader/Amostra2Expected/amostra2.txt");
    var dataReader2 = new Reader(streamReader2);
    switch (type)
    {
      case ExpectedDataType.MPU:
        yield return new object[] { dataReader2, new Amostra2Expected.Mpu() };
        break;
      case ExpectedDataType.GPS:
        yield return new object[] { dataReader2, new Amostra2Expected.Gps() };
        break;
      case ExpectedDataType.L:
        yield return new object[] { dataReader2, new Amostra2Expected.L() };
        break;
    }
    streamReader2.Close();
  }

  [TestCaseSource(typeof(TestDataReader), nameof(TestReaders), new object[] { ExpectedDataType.MPU })]
  public void ReadsMpuCorrectly(Reader dataReader, ExpectedData expected)
  {
    Assert.IsTrue(dataReader.FinishedReading, "dataReader.FinishedReading");

    // Test if metadata was captured correctly

    foreach (var item in dataReader.MpuSectionReader.MetadataTypes.Keys)
    {
      var data = dataReader.MpuSectionReader.GetMetadata(item);
      var expectedData = expected.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }

    Assert.AreEqual(expected.Metadata["n_samples"], dataReader.MpuSectionReader.ValuesCount);
    Assert.AreEqual(dataReader.MpuSectionReader.GetMetadata("n_samples"), dataReader.MpuSectionReader.ValuesCount);

    for (int i = 0; i < expected.Data.Count; i++)
    {
      var data = dataReader.MpuSectionReader.GetValue(i);
      var expectedData = expected.Data[i];

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

  [TestCaseSource(typeof(TestDataReader), nameof(TestReaders), new object[] { ExpectedDataType.GPS })]
  public void ReadsGpsCorrectly(Reader dataReader, ExpectedData expected)
  {
    Assert.IsTrue(dataReader.FinishedReading, "dataReader.FinishedReading");

    // Test if metadata was captured correctly

    foreach (var item in dataReader.GpsSectionReader.MetadataTypes.Keys)
    {
      var data = dataReader.GpsSectionReader.GetMetadata(item);
      var expectedData = expected.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }

    Assert.AreEqual(expected.Metadata["n_samples"], dataReader.GpsSectionReader.ValuesCount);
    Assert.AreEqual(dataReader.GpsSectionReader.GetMetadata("n_samples"), dataReader.GpsSectionReader.ValuesCount);

    for (int i = 0; i < expected.Data.Count; i++)
    {
      var data = dataReader.GpsSectionReader.GetValue(i);
      var expectedData = expected.Data[i];

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

  [TestCaseSource(typeof(TestDataReader), nameof(TestReaders), new object[] { ExpectedDataType.L })]
  public void ReadsLCorrectly(Reader dataReader, ExpectedData expected)
  {
    Assert.IsTrue(dataReader.FinishedReading, "dataReader.FinishedReading");

    // Test if metadata was captured correctly

    foreach (var item in dataReader.LSectionReader.MetadataTypes.Keys)
    {
      var data = dataReader.LSectionReader.GetMetadata(item);
      var expectedData = expected.Metadata[item];
      Assert.IsInstanceOf(
        expectedData.GetType(),
        data,
        $"Type of metadata item {item} is wrong."
      );
      Assert.AreEqual(expectedData, data, $"Metadata item {item} was read wrong.");
    }
  }

}
