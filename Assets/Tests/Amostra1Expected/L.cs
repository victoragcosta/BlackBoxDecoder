using System;
using System.Collections.Generic;
using Utils;

namespace Amostra1Expected
{
  public class L : ExpectedData {
    private static Dictionary<String, dynamic> _metadata = new Dictionary<String, dynamic>()
    {
      // Dados gerais da calibração ao ligar
      {"OP_OK", "21331"},
      {"OP_BATEU", "21331"},
      {"OP_ST_OK", "20046"},
      {"OP_STH_OK", "21331"},
      {"OP_CF_OK", "255"},
      {"OP_CFH_OK", "65296"},

      // Acelerômetro e giroscópio - Calibra ao ligar
      {"OPC_FREQ_AG", 100},
      {"OPC_BW_AG", 5},
      {"OPC_ESC_AC", 2},
      {"OPC_ESC_GI", 250},
      {"OPC_QTD_AG", 8},
      // LINE_0
      {"OPC_AX", -08871.0},
      {"OPC_AY", +00350.0},
      {"OPC_AZ", +13286.0},
      {"OPC_TP", +05607.0},
      {"OPC_GX", +03439.0},
      {"OPC_GY", +03006.0},
      {"OPC_GZ", +02930.0},

      // Acelerômetro, giroscópio e Mag - Operação
      {"OP_FREQ_AG", 100},
      {"OP_BW_AG", 5},
      {"OP_ESC_AC", 2},
      {"OP_ESC_GI", 250},
      // {"OP_ESC_MG", "String"},
      // LINE_1
      {"OP_LIM_AX", +00004.0},
      {"OP_LIM_AY", +00004.0},
      {"OP_LIM_AZ", +00004.0},
      {"OP_LIM_GX", +01000.0},
      {"OP_LIM_GY", +01000.0},
      {"OP_LIM_GZ", +01000.0},

      // Quem disparou
      {"OP_MPU_ADR", 49878},
      {"OP_GPS_ADR", 233088},
      {"OP_DISP_TP", 6525},
      // LINE_2
      {"OP_DISP_AX", +21331.0},
      {"OP_DISP_AY", +20046.0},
      {"OP_DISP_AZ", +20046.0},
      {"OP_DISP_GX", +20046.0},
      {"OP_DISP_GY", +20046.0},
      {"OP_DISP_GZ", +20046.0},
      // /LINE_2
      {"OP_BRK", "21331"},
      {"OP_ULT_ADR", 51318},
      {"OP_AC_DATA", "041020"},
      {"OP_AC_HORA", "145330"},
    };
    private static List<List<dynamic>> _data = new List<List<dynamic>>();

    public override Dictionary<string, dynamic> Metadata => _metadata;
    public override List<List<dynamic>> Data => _data;
  }
}
