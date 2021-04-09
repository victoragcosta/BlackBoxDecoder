
/* 
 * Script para rotacionar objeto de acordo com um arquivo que especifica os quaternions 
 * [period] é uma variável pública e pode ser modificada na interface do unity antes de executar a animação
 */

using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

public class QuaternionRotation : MonoBehaviour
{  
    public  int period = 10; //intervalo de tempo entre uma rotação e outra em milisegundos
    private float periodS; //periodo em segundos
    private float totalTime = 0f; //tempo que passou
    private int totalQuaternions; //quantidade total de quaternions no array
    private int quaternionArrayIndex; //indice atual no array de quaternions
    private ArrayList readings = new ArrayList(); //linhas do arquivo lido
    private ArrayList quaternions = new ArrayList();
    private int totalReadings = 0;
    private string path = "Assets/quaternions_volta.txt";

    Quaternion createQuaternion(float w, float x, float y, float z)
    {
        //para ficar igual ao do matlab: z unity = x, y unity = z, x unity = -y
        return new Quaternion(w, -y, z, x);      
    }

    void Start()
    {
        
        periodS = (float) period / 1000;     
        quaternionArrayIndex = 0;

        //lendo arquivo de quaternions
        StreamReader reader = new StreamReader(path);
        totalReadings = Int16.Parse(reader.ReadLine());
        Debug.Log("qtd leituras: " + totalReadings);

        for(int i = 0; i < totalReadings; i++)
        {
            for(int j = 0; j  < 4; j++)
            {            
                readings.Insert(i + j, float.Parse(reader.ReadLine()));
            }
            quaternions.Insert(i, createQuaternion((float) readings[i], (float)readings[i+1], (float)readings[i+2], (float)readings[i+3]));
        }

        reader.Close();
        Quaternion q = createQuaternion(1f, 0.0f, 0.0f, 0f);
        this.transform.rotation = q * this.transform.rotation;
        totalQuaternions = quaternions.Count;
    }

    void Update()
    {
        quaternionArrayIndex = (int) Math.Floor(totalTime / periodS);
        Debug.Log("quaternion: " + quaternionArrayIndex);

        if (quaternionArrayIndex < totalQuaternions)
        {
            this.transform.rotation = (Quaternion)quaternions[quaternionArrayIndex];
        }
        else
        {
            quaternionArrayIndex = 0;
            totalTime = 0;
        }

        totalTime += Time.deltaTime;
        //Debug.Log("tempo passado: " + totalTime);
        //Debug.Log("intervalo: " + periodS);
    }
}
