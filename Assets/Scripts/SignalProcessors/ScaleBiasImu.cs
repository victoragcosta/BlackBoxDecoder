using System.Collections.Generic;
using System;
using System.Linq;

using DataTypes;

namespace SignalProcessors
{
  /// <summary>
  ///   <c cref="ScaleBiasImuParams">ScaleBiasImuParams</c> is a data class with
  ///   all the needed parameters to adjust all IMU data.
  /// </summary>
  class ScaleBiasImuParams
  {
    public Vector3 accelerationBias { get; }
    public Double accelerationScale { get; }
    public Vector3 gyroscopeBias { get; }
    public Double gyroscopeScale { get; }
    public Vector3 magnetometerBias { get; }
    public Vector3 magnetometerScale { get; }

    public ScaleBiasImuParams(
      Vector3 accelerationBias,
      Double accelerationScale,
      Vector3 gyroscopeBias,
      Double gyroscopeScale,
      Vector3 magnetometerBias,
      Vector3 magnetometerScale
    ) {
      this.accelerationBias = accelerationBias;
      this.accelerationScale = accelerationScale;
      this.gyroscopeBias = gyroscopeBias;
      this.gyroscopeScale = gyroscopeScale;
      this.magnetometerBias = magnetometerBias;
      this.magnetometerScale = magnetometerScale;
    }
  };

  /// <summary>
  ///   <c cref="ScaleBiasImu">ScaleBiasImu</c> is a preprocessor
  ///   that transforms IMU data into the correct units for calculations and removes
  ///   bias from calibration.
  ///
  ///   <para>
  ///     Acceleration is outputted in g's, gyroscope data is outputted in
  ///     degrees per second and magnetometer data is outputted in milligauss.
  ///   </para>
  /// </summary>
  class ScaleBiasImu : SignalPreProcessor<ScaleBiasImuParams>
  {
    public ScaleBiasImu(ScaleBiasImuParams parameters) : base(parameters) {}

    private List<Vector3> scaleData(List<Vector3> data, Vector3 scale)
    {
      return data.Select(vec => new Vector3(
        scale.x * (vec.x / 32767),
        scale.y * (vec.y / 32767),
        scale.z * (vec.z / 32767)
      )).ToList();
    }

    private List<Vector3> scaleData(List<Vector3> data, Double scale)
    {
      return this.scaleData(data, new Vector3(scale, scale, scale));
    }

    private List<Vector3> scaleMagnetometerData(List<Vector3> unbiasedData, Vector3 scale)
    {
      return unbiasedData.Select(vec => new Vector3(
        vec.x * scale.x,
        vec.y * scale.y,
        vec.z * scale.z
      )).ToList();
    }



    /// <summary>
    ///   Subtracts a number from each direction. These numbers are captured from
    ///   calibration data, available in the BlackBoxFile metadata.
    /// </summary>
    /// <param name="data">
    ///   The scaled data for accelerometer and gyroscope and the raw data for
    ///   magnetometer.
    /// </param>
    /// <param name="bias">The vector with the bias for each direction.</param>
    /// <returns></returns>
    private List<Vector3> removeBiasFromData(List<Vector3> data, Vector3 bias)
    {
      return data.Select(vec => new Vector3(
        vec.x - bias.x,
        vec.y - bias.y,
        vec.z - bias.z
      )).ToList();
    }

    private List<Vector3> convertFromMicroTeslaToMilliGauss(List<Vector3> data)
    {
      List<Vector3> switchedAxis = data.Select(vec => new Vector3(
        vec.y,
        vec.x,
        -vec.z
      )).ToList();

      return data.Select(vec => new Vector3(
        (4912 * vec.x/32767) * 10,
        (4912 * vec.y/32767) * 10,
        (4912 * vec.z/32767) * 10
      )).ToList();
    }

    public override SignalPreProcessorReturn PreProcess(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    )
    {
      // NOTE: `this.parameters` is inherited from `SignalPreProcessor`

      var scaledAcc = this.scaleData(accelerometerData, this.parameters.accelerationScale);
      var unbiasedAcc = (AccelerometerData)this.removeBiasFromData(
        scaledAcc,
        this.parameters.accelerationBias
      );

      var scaledGyro = this.scaleData(gyroscopeData, this.parameters.gyroscopeScale);
      var unbiasedGyro = (GyroscopeData)this.removeBiasFromData(
        scaledGyro,
        this.parameters.gyroscopeBias
      );

      var unbiasedMag = this.removeBiasFromData(magnetometerData, this.parameters.magnetometerBias);
      var scaledMag = this.scaleMagnetometerData(unbiasedMag, this.parameters.magnetometerScale);
      var convertedMag = (MagnetometerData)this.convertFromMicroTeslaToMilliGauss(scaledMag);

      return new SignalPreProcessorReturn(
        unbiasedAcc,
        unbiasedGyro,
        convertedMag,
        gpsData
      );
    }
  }
}
