using System;
using System.Collections.Generic;

namespace DataTypes {
  readonly struct Vector3 {
    public Vector3(Double x, Double y, Double z) {
      this.x = x;
      this.y = y;
      this.z = z;
    }

    public Double x { get; }
    public Double y { get; }
    public Double z { get; }
  }

  class AccelerometerData : List<Vector3> {}
  class GyroscopeData : List<Vector3> {}
  class MagnetometerData : List<Vector3> {}

  class GpsData {
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
}
