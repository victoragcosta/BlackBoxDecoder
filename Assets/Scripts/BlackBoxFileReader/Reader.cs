using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System;
using UnityEngine;
using static Utils.Utils;

namespace BlackBoxFileReader
{
  public class Reader
  {

    private StreamReader reader;

    private MpuSectionReader mpuSectionReader = new MpuSectionReader();
    public MpuSectionReader MpuSectionReader => mpuSectionReader;
    private GpsSectionReader gpsSectionReader = new GpsSectionReader();
    public GpsSectionReader GpsSectionReader => gpsSectionReader;

    private bool finishedReading = false;
    public bool FinishedReading => finishedReading;

    public Reader(StreamReader reader)
    {
      this.reader = reader;

      String line = reader.ReadLine();

      while (line != null)
      {
        switch (line)
        {
          case "#[m":
            mpuSectionReader.ReadSection(reader);
            break;
          case "#[g":
            gpsSectionReader.ReadSection(reader);
            break;
          case "m]#":
          case "g]#":
          case "l]#":
          case "f]#":
            break;
          default:
            SectionDiscarder();
            break;
        }
        line = reader.ReadLine();
      }

      finishedReading = true;
    }

    // Must have this.reader set
    // Reads a section and ignores it
    private void SectionDiscarder()
    {
      String line = ReadNotEmptyLine(reader);
      while (line != null && !line.EndsWith("]#"))
        line = ReadNotEmptyLine(reader);
    }
  }

}
