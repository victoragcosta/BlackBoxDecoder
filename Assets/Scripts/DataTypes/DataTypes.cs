using System;
using System.Collections.Generic;
using UnityEngine;

namespace DataTypes {
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
}
