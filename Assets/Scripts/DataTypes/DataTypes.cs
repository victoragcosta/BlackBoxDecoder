using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes {
  public readonly struct Vector3 {
    public Vector3(Double x, Double y, Double z) {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public Double x { get; }
    public Double y { get; }
    public Double z { get; }
  }

  public class AccelerometerData : List<Vector3> {}
  public class GyroscopeData : List<Vector3> {}
  public class MagnetometerData : List<Vector3> {}

  public class GpsData {
    public Boolean valid { get; }

    // {"date_reading", "String"},
    // {"hour_reading", "String"},
    public DateTime readingTime { get; }

    // {"latitude", "String"},
    // {"north_south", "String"},
    public Double latitude { get; }

    // {"longitude", "String"},
    // {"east_west", "String"},
    public Double longitude { get; }

    // {"velocity", "Double"},
    // {"velocity_unit", "String"},
    public Double velocity { get; }

    // {"velocity_knots", "Double"},
    public Double velocityKnots { get; }

    // {"course", "Double"},
    public Double course { get; }

    // {"altitude", "Double"},
    // {"altitude_unit", "String"},
    public Double altitude { get; }

    // {"satellite_count", "Int"},
    public Int32 satelliteCount { get; }

    // Devo implementar?
    // {"PDOP", "Double"},
    // {"HDOP", "Double"},
    // {"VDOP", "Double"},
  };

  public class RawData {
    public AccelerometerData accelerometerData { get; }
    public GyroscopeData gyroscopeData { get; }
    public MagnetometerData magnetometerData { get; }
    public GpsData gpsData { get; }

    public RawData(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    ) {
      this.accelerometerData = accelerometerData;
      this.gyroscopeData = gyroscopeData;
      this.magnetometerData = magnetometerData;
      this.gpsData = gpsData;
    }
  }

  public class EulerTiltData : List<Vector3> {}
  public class QuaternionTiltData : List<Quaternion> {}
  public class PositionData : List<Vector3> {}

  public class ProcessedData<Tilt> {
    public PositionData positionData { get; }
    public Tilt tiltData { get; }
    public ProcessedData(
      PositionData positionData,
      Tilt tiltData
    ) {
      this.positionData = positionData;
      this.tiltData = tiltData;
    }
  }

  public class ProcessedDataEuler : ProcessedData<EulerTiltData> {
    public ProcessedDataEuler(
      PositionData positionData,
      EulerTiltData tiltData
    ) : base(positionData, tiltData) {}
  }
  public class ProcessedDataQuaternion : ProcessedData<QuaternionTiltData> {
    public ProcessedDataQuaternion(
      PositionData positionData,
      QuaternionTiltData tiltData
    ) : base(positionData, tiltData) {}
  }
}
