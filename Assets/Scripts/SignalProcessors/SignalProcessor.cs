using System.Collections.Generic;
using UnityEngine;
using DataTypes;

namespace SignalProcessors
{
  using EulerTiltData = List<DataTypes.Vector3>;
  using QuaternionTiltData = List<Quaternion>;
  using PositionData = List<DataTypes.Vector3>;

  readonly struct SignalProcessorEulerReturn {
    public PositionData positionData { get; }
    public EulerTiltData tiltData { get; }
  }

  readonly struct SignalProcessorQuaternionReturn {
    public PositionData positionData { get; }
    public QuaternionTiltData tiltData { get; }
  }

  abstract class SignalProcessor<Param>
  {
    private Param parameters { get; set; }
    public SignalProcessor(Param parameters) {
      this.parameters = parameters;
    }

    public abstract SignalProcessorEulerReturn ProcessEuler(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    );

    public abstract SignalProcessorQuaternionReturn ProcessQuaternion(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    );
  }
}
