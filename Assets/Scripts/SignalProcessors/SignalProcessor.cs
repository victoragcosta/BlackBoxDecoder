using System.Collections.Generic;
using UnityEngine;
using DataTypes;

namespace SignalProcessors
{
  using EulerTiltData = List<DataTypes.Vector3>;
  using QuaternionTiltData = List<Quaternion>;
  using PositionData = List<DataTypes.Vector3>;

  class SignalProcessorReturn<Tilt> {
    public PositionData positionData { get; }
    public Tilt tiltData { get; }
    public SignalProcessorReturn(
      PositionData positionData,
      Tilt tiltData
    ) {
      this.positionData = positionData;
      this.tiltData = tiltData;
    }
  }

  class SignalProcessorEulerReturn : SignalProcessorReturn<EulerTiltData> {
    public SignalProcessorEulerReturn(
      PositionData positionData,
      EulerTiltData tiltData
    ) : base(positionData, tiltData) {}
  }

  class SignalProcessorQuaternionReturn : SignalProcessorReturn<QuaternionTiltData> {
    public SignalProcessorQuaternionReturn(
      PositionData positionData,
      QuaternionTiltData tiltData
    ) : base(positionData, tiltData) {}
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
