using System;
using System.IO;
using System.Text.RegularExpressions;
using UnityEngine;

namespace Utils
{
  public class Utils
  {
    private static Regex emptyLine = new Regex(@"^\s*$");
    public static String ReadNotEmptyLine(StreamReader reader) {
      try {
        String line = reader.ReadLine();

        if(line == null) throw new EndOfStreamException();

        while(emptyLine.IsMatch(line))
          line = reader.ReadLine();
        return line;
      } catch (EndOfStreamException) {
        return null;
      }
    }
  }

}
