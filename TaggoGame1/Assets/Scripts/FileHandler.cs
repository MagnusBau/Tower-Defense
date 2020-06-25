using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

class FileHandler
{

    private List<string> wave;
    public List<string> ReadWave(int waveNumber)
    {
        wave = new List<string>();
        try
        {
            // The using statement also closes the StreamReader.
            using (StreamReader sr = new StreamReader(Directory.GetCurrentDirectory() + "/Assets/Waves/Wave" + waveNumber.ToString() + ".txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    wave.Add(line);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("The file could not be read:");
            Debug.LogError(e.Message);
        }
        return wave;
    }

    public void WriteWave(int waveNumber, string[] waveData)
    {
        try
        {
            using (StreamWriter sw = File.CreateText(Directory.GetCurrentDirectory() + "/Assets/Waves/Wave" + waveNumber.ToString() + ".txt"))
            {

                foreach (string s in waveData)
                {
                    sw.WriteLine(s);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Could not Write to file:");
            Debug.LogError(e.Message);
        }
    }
}