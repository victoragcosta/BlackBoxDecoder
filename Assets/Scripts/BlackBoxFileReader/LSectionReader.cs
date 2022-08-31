using System.Collections.Generic;
using System;
using UnityEngine;

namespace BlackBoxFileReader
{
  public class LSectionReader : SectionReader
  {
    private List<String> metadataOrder = new List<String>()
    {
      // Dados gerais da calibração ao ligar
      "OP_OK",
      "OP_BATEU",
      "OP_ST_OK",
      "OP_STH_OK",
      "OP_CF_OK",
      "OP_CFH_OK",

      // Acelerômetro e giroscópio - Calibra ao ligar
      "OPC_FREQ_AG",
      "OPC_BW_AG",
      "OPC_ESC_AC",
      "OPC_ESC_GI",
      "OPC_QTD_AG",
      "LINE_0",

      // Acelerômetro, giroscópio e Mag - Operação
      "OP_FREQ_AG",
      "OP_BW_AG",
      "OP_ESC_AC",
      "OP_ESC_GI",
      // "OP_ESC_MG",
      "LINE_1",

      // Quem disparou
      "OP_MPU_ADR",
      "OP_GPS_ADR",
      "OP_DISP_TP",
      "LINE_2",
      "OP_BRK",
      "OP_ULT_ADR",
      "OP_AC_DATA",
      "OP_AC_HORA",
    };
    public override List<String> MetadataOrder => metadataOrder;

    private List<List<String>> metadataLinesOrdered = new List<List<String>>()
    {

      // LINE_0
      new List<String>{
        "OPC_AX",
        "OPC_AY",
        "OPC_AZ",
        "OPC_TP",
        "OPC_GX",
        "OPC_GY",
        "OPC_GZ",
      },

      // LINE_1
      new List<String>{
        "OP_LIM_AX",
        "OP_LIM_AY",
        "OP_LIM_AZ",
        "OP_LIM_GX",
        "OP_LIM_GY",
        "OP_LIM_GZ",
      },

      // LINE_2
      new List<String> {
        "OP_DISP_AX",
        "OP_DISP_AY",
        "OP_DISP_AZ",
        "OP_DISP_GX",
        "OP_DISP_GY",
        "OP_DISP_GZ",
      },

    };
    public override List<List<String>> MetadataLinesOrder => metadataLinesOrdered;

    private Dictionary<String, String> metadataTypes = new Dictionary<String, String>()
    {
      // Dados gerais da calibração ao ligar
      {"OP_OK", "String"},     // Deveria ser boolean (OK,NOK)
      {"OP_BATEU", "String"},  // Deveria ser boolean (OK,NOK)
      {"OP_ST_OK", "String"},  // Deveria ser boolean (OK,NOK)
      {"OP_STH_OK", "String"}, // Deveria ser boolean (OK,NOK)
      {"OP_CF_OK", "String"},  // Deveria ser boolean (OK,NOK)
      {"OP_CFH_OK", "String"}, // Deveria ser boolean (OK,NOK)

      // Acelerômetro e giroscópio - Calibra ao ligar
      {"OPC_FREQ_AG", "Int"},
      {"OPC_BW_AG", "Int"},
      {"OPC_ESC_AC", "Int"},
      {"OPC_ESC_GI", "Int"},
      {"OPC_QTD_AG", "Int"},
      // LINE_0
      {"OPC_AX", "Double"},
      {"OPC_AY", "Double"},
      {"OPC_AZ", "Double"},
      {"OPC_TP", "Double"},
      {"OPC_GX", "Double"},
      {"OPC_GY", "Double"},
      {"OPC_GZ", "Double"},

      // Acelerômetro, giroscópio e Mag - Operação
      {"OP_FREQ_AG", "Int"},
      {"OP_BW_AG", "Int"},
      {"OP_ESC_AC", "Int"},
      {"OP_ESC_GI", "Int"},
      // {"OP_ESC_MG", "String"},
      // LINE_1
      {"OP_LIM_AX", "Double"},
      {"OP_LIM_AY", "Double"},
      {"OP_LIM_AZ", "Double"},
      {"OP_LIM_GX", "Double"},
      {"OP_LIM_GY", "Double"},
      {"OP_LIM_GZ", "Double"},

      // Quem disparou
      {"OP_MPU_ADR", "Int"},
      {"OP_GPS_ADR", "Int"},
      {"OP_DISP_TP", "Int"},
      // LINE_2
      {"OP_DISP_AX", "Double"},
      {"OP_DISP_AY", "Double"},
      {"OP_DISP_AZ", "Double"},
      {"OP_DISP_GX", "Double"},
      {"OP_DISP_GY", "Double"},
      {"OP_DISP_GZ", "Double"},
      // /LINE_2
      {"OP_BRK", "String"}, // Deveria ser boolean (SS, NN)
      {"OP_ULT_ADR", "Int"},
      {"OP_AC_DATA", "String"},
      {"OP_AC_HORA", "String"},
    };
    public override Dictionary<String, String> MetadataTypes => metadataTypes;

    private List<String> valueParameters = new List<String>();
    public override List<string> ValueParameters => valueParameters;

    private Dictionary<String, String> valueTypes = new Dictionary<String, String>();
    public override Dictionary<string, string> ValueTypes => valueTypes;

    public override int ValuesCount => 0;
  }
}
