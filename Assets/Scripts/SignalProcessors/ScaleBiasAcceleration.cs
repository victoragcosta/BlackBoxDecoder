using System.Collections.Generic;
using System;
using System.Linq;

using DataTypes;
// function ret = calculate_aceleration(A, a_bias, esc_ac)
//     %% Converter aceleração em "g"
//     ax = esc_ac * (A(1) / 32767) - a_bias(1);
//     ay = esc_ac * (A(2) / 32767) - a_bias(2);
//     az = esc_ac * (A(3) / 32767) - a_bias(3);

//     ret = [ax, ay, az];
// end

// function ret = calculate_gyro(G, g_bias, esc_giro)
//     %% Converter giros em "graus/seg"
//     gx = esc_giro * (G(1) / 32767) - g_bias(1);
//     gy = esc_giro * (G(2) / 32767) - g_bias(2);
//     gz = esc_giro * (G(3) / 32767) - g_bias(3);

//     ret = [gx, gy, gz];
// end


// function ret = calculate_mag(H, h_offsets, h_scales)
//     %% Remove hard and soft iron dos dados do magnetometro
//     hx = (H(1) - h_offsets(1)) * h_scales(1);
//     hy = (H(2) - h_offsets(2)) * h_scales(2);
//     hz = (H(3) - h_offsets(3)) * h_scales(3);

//     %% Converter leitura do magnetometro em micro Testla p/ mili Gaus
//     % Trocando a ordem, porque os eixos do mag são X p/ Y do giro, Y p/ X
//     % do giro e -Z p/ Z do giro
//     temp_x = hy;
//     temp_y = hx;
//     temp_z = -hz;
//     hx = (4912 * temp_x/32767) * 10;
//     hy = (4912 * temp_y/32767) * 10;
//     hz = (4912 * temp_z/32767) * 10;

//     ret = [hx, hy, hz];
// end

namespace SignalProcessors
{
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

  class ScaleBiasAcceleration : SignalPreProcessor<ScaleBiasImuParams>
  {
    public ScaleBiasAcceleration(ScaleBiasImuParams parameters) : base(parameters) {}

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

    public override SignalPreProcessorReturn PreProcess(
      AccelerometerData accelerometerData,
      GyroscopeData gyroscopeData,
      MagnetometerData magnetometerData,
      GpsData gpsData
    )
    {
      throw new NotImplementedException();
    }
  }
}
