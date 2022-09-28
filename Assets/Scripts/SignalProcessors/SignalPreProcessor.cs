using DataTypes;

namespace SignalProcessors
{

  abstract class SignalPreProcessor<Param>
  {
    protected Param parameters { get; set; }
    public SignalPreProcessor(Param parameters) {
      this.parameters = parameters;
    }

    public abstract RawData PreProcess(RawData rawData);
  }
}
