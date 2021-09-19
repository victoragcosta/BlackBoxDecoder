using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using static Utils.Utils;

public class DataReader
{

  private StreamReader reader;

  private MpuSectionReader mpuSectionReader = new MpuSectionReader();
  public MpuSectionReader MpuSectionReader => mpuSectionReader;

  private bool finishedReading = false;
  public bool FinishedReading => finishedReading;

  public DataReader(StreamReader reader) {
    this.reader = reader;

    String line = ReadNotEmptyLine(reader);

    while(line != null) {
      switch (line)
      {
        case "#[m":
          mpuSectionReader.ReadSection(reader);
          break;
        default:
          SectionDiscarder();
          break;
      }
      line = ReadNotEmptyLine(reader);
    }

    finishedReading = true;
  }

  // Must have this.reader set
  // Reads a section and ignores it
  private void SectionDiscarder() {
    String line = ReadNotEmptyLine(reader);
    while(line != null && !line.EndsWith("]#"))
      line = ReadNotEmptyLine(reader);
  }
}

public abstract class SectionReader
{
  // Useful
  private Regex linePattern = new Regex(@"^LINE_(?<index>\d+)$", RegexOptions.Compiled);

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

  private dynamic TypeCast(String value, String type) {
    switch (type)
    {
      case "Int":
        return Int32.Parse(value);
      case "Double":
        return Double.Parse(value);
      case "String":
      default:
        return value;
    }
  }

  public dynamic GetMetadata(String key) => metadata[key];
  public Dictionary<String, dynamic> GetValue(int index) => values[index];

  public void ReadSection(StreamReader reader) {

    // Read metadata
    foreach (var item in MetadataOrder)
    {
      var line = ReadNotEmptyLine(reader);

      if(linePattern.IsMatch(item)) {
        Match m = linePattern.Match(item);
        int index = int.Parse(m.Groups["index"].Value);
        List<String> parameters = MetadataLinesOrder[index];

        // Stores each parameter of the line
        String[] values = line.Split(" ".ToCharArray());

        for (int i = 0; i < parameters.Count; i++)
        {
          var key = parameters[i];
          metadata[key] = TypeCast(values[i], MetadataTypes[key]);
        }
      } else {
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

public class MpuSectionReader : SectionReader
{
  private List<String> metadataOrder = new List<String>(){
    "colision_date",
    "colision_time",
    "colision_temperature",
    "SRAM_MPU_addr",
    "SRAM_GPS_addr",

    "LINE_0",
    "LINE_1",
    "LINE_2",

    "n_samples"
  };
  public override List<String> MetadataOrder => metadataOrder;

  private List<List<String>> metadataLinesOrdered = new List<List<String>>(){

    // LINE_1
    new List<String>{
      "cf_ok",
      "st_cf",
      "aesc_cf",
      "gesc_cf",
      "tp_cf",
      "ax_offset",
      "ay_offset",
      "az_offset",
      "gx_offset",
      "gy_offset",
      "gz_offset",
    },

    // LINE_2
    new List<String>{
      "cf_ok_mag",
      "sth_cf_mag",
      "hx_ASA",
      "hy_ASA",
      "hz_ASA",
      "hx_offset",
      "hy_offset",
      "hz_offset",
      "hx_escala",
      "hy_escala",
      "hz_escala",
    },

    // LINE_3
    new List<String> {
      "st_op",
      "sth_op",
      "aesc_op",
      "gesc_op",
      "fammost",
      "bw_ag",
      "reserved01",
      "reserved02",
      "reserved03",
      "reserved04",
      "reserved05",
    },

  };
  public override List<List<String>> MetadataLinesOrder => metadataLinesOrdered;

  private Dictionary<String, String> metadataTypes = new Dictionary<String, String>(){
    {"colision_date", "String"},
    {"colision_time", "String"},
    {"colision_temperature", "Int"},
    {"SRAM_MPU_addr", "Hex"},
    {"SRAM_GPS_addr", "Hex"},

    // Line 1
    {"cf_ok", "String"},
    {"st_cf", "String"},
    {"aesc_cf", "String"},
    {"gesc_cf", "String"},
    {"tp_cf", "String"},
    {"ax_offset", "Double"},
    {"ay_offset", "Double"},
    {"az_offset", "Double"},
    {"gx_offset", "Double"},
    {"gy_offset", "Double"},
    {"gz_offset", "Double"},

    // Line 2
    {"cf_ok_mag", "String"},
    {"sth_cf_mag", "String"},
    {"hx_ASA", "Double"},
    {"hy_ASA", "Double"},
    {"hz_ASA", "Double"},
    {"hx_offset", "Double"},
    {"hy_offset", "Double"},
    {"hz_offset", "Double"},
    {"hx_escala", "Double"},
    {"hy_escala", "Double"},
    {"hz_escala", "Double"},

    // Line 3
    {"st_op", "String"},
    {"sth_op", "String"},
    {"aesc_op", "Int"},
    {"gesc_op", "Int"},
    {"fammost", "Int"},
    {"bw_ag", "String"},
    {"reserved01", "String"},
    {"reserved02", "String"},
    {"reserved03", "String"},
    {"reserved04", "String"},
    {"reserved05", "String"},

    {"n_samples", "Int"}
  };
  public override Dictionary<String, String> MetadataTypes => metadataTypes;

  private List<String> valueParameters = new List<String>
  {
    "cont",
    "adr",
    "ax",
    "ay",
    "az",
    "gx",
    "gy",
    "gz",
    "hx",
    "hy",
    "hz",
  };
  public override List<string> ValueParameters => valueParameters;

  private Dictionary<String, String> valueTypes = new Dictionary<String, String>(){
    {"cont", "Int"},
    {"adr", "Hex"},
    {"ax", "Int"},
    {"ay", "Int"},
    {"az", "Int"},
    {"gx", "Int"},
    {"gy", "Int"},
    {"gz", "Int"},
    {"hx", "Int"},
    {"hy", "Int"},
    {"hz", "Int"},
  };
  public override Dictionary<string, string> ValueTypes => valueTypes;


  public override int ValuesCount => (int) GetMetadata("n_samples");
}
