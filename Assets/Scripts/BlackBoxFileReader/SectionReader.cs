using System.Globalization;
using System.Runtime.Serialization.Formatters;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using static Utils.Utils;

namespace BlackBoxFileReader
{

  public abstract class SectionReader
  {
    // Useful
    private Regex linePattern = new Regex(@"^LINE_(?<index>\d+)$", RegexOptions.Compiled);
    private IFormatProvider provider = CultureInfo.CreateSpecificCulture("en-US");

    // Configuration
    public abstract List<String> MetadataOrder { get; }
    public abstract List<List<String>> MetadataLinesOrder { get; }
    public abstract Dictionary<String, String> MetadataTypes { get; }
    public abstract List<String> ValueParameters { get; }
    public abstract Dictionary<String, String> ValueTypes { get; }
    public abstract int ValuesCount { get; }

    // Storage
    private Dictionary<String, dynamic> metadata = new Dictionary<String, dynamic>();
    private List<Dictionary<String, dynamic>> values = new List<Dictionary<String, dynamic>>();

    private dynamic TypeCast(String value, String type)
    {
      switch (type)
      {
        case "Int":
          return Int32.Parse(value);
        case "Double":
          return Double.Parse(value, provider);
        case "String":
        default:
          return value;
      }
    }

    public dynamic GetMetadata(String key) => metadata[key];
    public Dictionary<String, dynamic> GetValue(int index) => values[index];

    public void ReadSection(StreamReader reader)
    {

      // Read metadata
      foreach (var item in MetadataOrder)
      {
        var line = ReadNotEmptyLine(reader);

        if (linePattern.IsMatch(item))
        {
          Match m = linePattern.Match(item);
          int index = int.Parse(m.Groups["index"].Value);
          List<String> parameters = MetadataLinesOrder[index];

          // Stores each parameter of the line
          List<String> values = new List<String>(line.Split(" ".ToCharArray())).FindAll(e => e != "");

          for (int i = 0; i < parameters.Count; i++)
          {
            var key = parameters[i];
            metadata[key] = TypeCast(values[i], MetadataTypes[key]);
          }
        }
        else
        {
          metadata[item] = TypeCast(line, MetadataTypes[item]);
        }

      }

      // Read sequential values section
      for (int i = 0; i < ValuesCount; i++)
      {
        var line = ReadNotEmptyLine(reader);
        var parameters = line.Split(" ".ToCharArray());
        var converted = new Dictionary<String, dynamic>();
        for (int j = 0; j < ValueParameters.Count; j++)
        {
          var key = ValueParameters[j];
          converted[key] = TypeCast(parameters[j], ValueTypes[key]);
        }
        values.Add(converted);
      }
    }
  }

}
