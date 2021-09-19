using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

public class Tester : MonoBehaviour {

  public String filePath = "Assets/amostra1.txt";
  private int i = 0;
  private DataReader dataReader;
  void Awake() {
    var streamReader = new StreamReader(filePath);
    dataReader = new DataReader(streamReader);
  }

  void Update() {
    if(dataReader.FinishedReading){
      Debug.Log(i);
      Debug.Log(dataReader.MpuSectionReader.ValuesCount);
      var a = dataReader.MpuSectionReader.GetValue(i);
      i++;
      foreach (var entry in a)
      {
        Debug.Log($"{entry.Key}: {entry.Value}");
      }
      if(i >= dataReader.MpuSectionReader.ValuesCount) {
        i = 0;
      }
    }
  }
}
