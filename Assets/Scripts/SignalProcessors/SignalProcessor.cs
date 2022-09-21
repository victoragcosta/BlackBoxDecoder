using System;
using System.Collections.Generic;
using DataTypes;
using UnityEngine;

namespace SignalProcessors
{
  using EulerTiltData = List<Vector3>;
  using QuaternionTiltData = List<Quaternion>;
  using PositionData = List<Vector3>;

  abstract class SignalProcessor<Param>
  {
    private Param parameters { get; set; }
    public SignalProcessor(Param parameters) {
      this.parameters = parameters;
    }

    public abstract Tuple<PositionData, EulerTiltData> ProcessEuler(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    );

    public abstract Tuple<PositionData, QuaternionTiltData> ProcessQuaternion(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    );
  }
}
