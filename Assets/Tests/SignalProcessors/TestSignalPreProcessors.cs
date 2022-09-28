using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using DataTypes;

namespace TestSignalPreProcessors
{
  public class TestSignalPreProcessors
  {
    [TestCaseSource(typeof(TestCases), nameof(TestCases.ScaleBiasImuCases))]
    public RawData ScaleBiasImuPreProcessCorrectly(
      RawData rawData
    )
    {
      throw new NotImplementedException();
    }

  }

  public class TestCases
  {
    public static IEnumerable ScaleBiasImuCases
    {
      get
      {
        yield return new TestCaseData(
          new RawData(
            (AccelerometerData) new List<Vector3>(),
            (GyroscopeData) new List<Vector3>(),
            (MagnetometerData) new List<Vector3>(),
            new GpsData()
          )
        ).Returns(
          new RawData(
            (AccelerometerData) new List<Vector3>(),
            (GyroscopeData) new List<Vector3>(),
            (MagnetometerData) new List<Vector3>(),
            new GpsData()
          )
        );
      }
    }
  }
}
