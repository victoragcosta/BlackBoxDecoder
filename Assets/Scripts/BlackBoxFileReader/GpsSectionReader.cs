using System.Collections.Generic;
using System;
using UnityEngine;

namespace BlackBoxFileReader
{
  public class GpsSectionReader : SectionReader
  {
    private List<String> metadataOrder = new List<String>()
    {
      "sel_date",
      "sel_time",
      "gps_start_addr",
      "n_samples",
    };
    public override List<String> MetadataOrder => metadataOrder;

    private List<List<String>> metadataLinesOrdered = new List<List<String>>();
    public override List<List<String>> MetadataLinesOrder => metadataLinesOrdered;

    private Dictionary<String, String> metadataTypes = new Dictionary<String, String>()
    {
      {"sel_date", "String"},
      {"sel_time", "String"},
      {"gps_start_addr", "Hex"},
      {"n_samples", "Int"},
    };
    public override Dictionary<String, String> MetadataTypes => metadataTypes;

    private List<String> valueParameters = new List<String>
    {
      "cont",
      "adr",
      "valid",
      "date_reading",
      "hour_reading",
      "latitude",
      "north_south",
      "longitude",
      "east_west",
      "velocity",
      "velocity_unit",
      "velocity_knots",
      "course",
      "altitude",
      "altitude_unit",
      "satellite_count",
      "PDOP",
      "HDOP",
      "VDOP",
      "adr_mpu",
    };
    public override List<string> ValueParameters => valueParameters;

    private Dictionary<String, String> valueTypes = new Dictionary<String, String>(){
      {"cont", "Int"},
      {"adr", "Hex"},
      {"valid", "String"},
      {"date_reading", "String"},
      {"hour_reading", "String"},
      {"latitude", "String"},
      {"north_south", "String"},
      {"longitude", "String"},
      {"east_west", "String"},
      {"velocity", "Double"},
      {"velocity_unit", "String"},
      {"velocity_knots", "Double"},
      {"course", "Double"},
      {"altitude", "Double"},
      {"altitude_unit", "String"},
      {"satellite_count", "Int"},
      {"PDOP", "Double"},
      {"HDOP", "Double"},
      {"VDOP", "Double"},
      {"adr_mpu", "Hex"},
    };
    public override Dictionary<string, string> ValueTypes => valueTypes;


    public override int ValuesCount => (int)GetMetadata("n_samples");
  }
}
