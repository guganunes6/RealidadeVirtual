using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq; 

public class CSVEncoder {

    string fileName;
    string extension = ".csv";
    string path = "Assets\\Scenes\\Times\\"; 

    List<double> times;

    public CSVEncoder(string fileName) {
        this.fileName = fileName;
        times = new List<double>(); // 15(task 1) + 5(task 2) + 1(task 3) + 1(total) 
        CreateFile(fileName + extension);
    }

    private void CreateFile(string fileName) {
        if (!FileExists(path + fileName)) {
            Debug.Log("Creating file");
            string firstLine = "";
            for (int i = 1; i <= 15; i++) { firstLine += "Task 1." + i + ","; }
            for (int i = 1; i <= 5; i++) { firstLine += "Task 2." + i + ","; }
            firstLine += "Task 3,Total";
            StreamWriter outStream = File.CreateText(path + fileName);
            outStream.WriteLine(firstLine);
            outStream.Close();
        }
    }

    // Adds a line to CSV file - It may be a good idea to call this method after SetThirdTaskTime(millis)
    public void UpdateFile() {
        Debug.Log("Updating file");
        File.AppendAllText(path + fileName + extension, String.Join(",", times) + "," + GetTotalTasksTime() + "\n" );
    }

    private bool FileExists(string fileName) {
        return File.Exists(fileName);
    }

    public double GetTotalTasksTime() {
        return times.Sum();
    }

    public void AddTime(double time) {
        times.Add(time);
    }

    public void PrintTimes() {
        string print = "";
        for (int i = 0; i < times.Count; i++) {
            print += times[i] + "  ";
        }
        Debug.Log(print);
    }

}
