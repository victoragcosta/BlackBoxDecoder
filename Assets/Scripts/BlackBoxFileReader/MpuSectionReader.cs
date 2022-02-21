using System.Collections.Generic;
using System;
using UnityEngine;

namespace BlackBoxFileReader
{
  public class MpuSectionReader : SectionReader
  {
    private List<String> metadataOrder = new List<String>()
    {
      "collision_date",
      "collision_time",
      "collision_temperature",
      "SRAM_MPU_addr",
      "SRAM_GPS_addr",

      "LINE_0",
      "LINE_1",
      "LINE_2",

      "n_samples"
    };
    public override List<String> MetadataOrder => metadataOrder;

    private List<List<String>> metadataLinesOrdered = new List<List<String>>()
    {

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

    private Dictionary<String, String> metadataTypes = new Dictionary<String, String>()
    {
      {"collision_date", "String"},
      {"collision_time", "String"},
      {"collision_temperature", "Int"},
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


    public override int ValuesCount => (int)GetMetadata("n_samples");
  }
}
