using System;
using System.Collections.Generic;

namespace Utils {
  public abstract class ExpectedData {
    public abstract Dictionary<String, dynamic> Metadata { get; }
    public abstract List<List<dynamic>> Data { get; }
  }
}
