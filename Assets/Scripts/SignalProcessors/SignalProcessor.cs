using DataTypes;

namespace SignalProcessors
{

  abstract class SignalProcessor<Param>
  {
    protected Param parameters { get; set; }
    public SignalProcessor(Param parameters) {
      this.parameters = parameters;
    }

    public abstract ProcessedDataEuler ProcessEuler(RawData rawData);

    public abstract ProcessedDataQuaternion ProcessQuaternion(RawData rawData);
  }
}
