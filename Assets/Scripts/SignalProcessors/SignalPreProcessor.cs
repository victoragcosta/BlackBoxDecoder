using DataTypes;

namespace SignalProcessors
{

  class SignalPreProcessorReturn {
    public AccelerometerData accelerometerData { get; }
    public GyroscopeData gyroscopeData { get; }
    public MagnetometerData magnetometerData { get; }
    public GpsData gpsData { get; }

    public SignalPreProcessorReturn(
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

  abstract class SignalPreProcessor<Param>
  {
    private Param parameters { get; set; }
    public SignalPreProcessor(Param parameters) {
      this.parameters = parameters;
    }

    public abstract SignalPreProcessorReturn PreProcess(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    );
  }
}
