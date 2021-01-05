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
    List<int> wrongSelectedNodes;

    public CSVEncoder(string fileName) {
        this.fileName = fileName;
        times = new List<double>(); // 15(task 1) + 5(task 2) + 1(task 3) + 1(total) 
        wrongSelectedNodes = new List<int>();
        CreateFile(fileName + extension);
    }

    private void CreateFile(string fileName) {
        if (!FileExists(path + fileName)) {
            Debug.Log("Creating file");
            string firstLine = "";
            for (int i = 1; i <= 15; i++) { firstLine += "Task 1." + i + ",Wrong Nodes 1."  + i + ","; }
            firstLine += "Task 1 Total,Task 1 Total Wrong Nodes,";
            for (int i = 1; i <= 5; i++) { firstLine += "Task 2." + i + ",Wrong Nodes 2."  + i + ","; }
            firstLine += "Task 2 Total,Task 2 Total Wrong Nodes,";
            firstLine += "Task 3,Wrong Nodes 3,Total,Total wrong nodes";
            StreamWriter outStream = File.CreateText(path + fileName);
            outStream.WriteLine(firstLine);
            outStream.Close();
        }
    }

    // Adds a line to CSV file - It may be a good idea to call this method after SetThirdTaskTime(millis)
    public void UpdateFile() {
        Debug.Log("Updating file");
        string line = "";
        for (int i = 0; i < times.Count(); i++) { 
            line += times[i] + "," + wrongSelectedNodes[i] + ","; 
        }
        line += GetTotalTasksTime() + "," + GetTotalWrongSelectedNodes() + "\n";
        File.AppendAllText(path + fileName + extension, line);
    }

    private bool FileExists(string fileName) {
        return File.Exists(fileName);
    }

    public double GetTotalTasksTime() {
        return times.Sum();
    }

    public double GetTotalWrongSelectedNodes() {
        return wrongSelectedNodes.Sum();
    }

    public void AddTime(double time, int wrongNodes) {
        times.Add(time);
        wrongSelectedNodes.Add(wrongNodes);
    }

    public void AddFinalTaskTime(int numTrials) {
        List<double> taskTrials = times.GetRange(times.Count - numTrials, numTrials);
        List<double> taskWrongNodes = wrongSelectedNodes.GetRange(wrongSelectedNodes.Count - numTrials, numTrials);
        times.Add(taskWrongNodes.Sum());
    }

    public void PrintTimes() {
        string print = "";
        for (int i = 0; i < times.Count; i++) {
            print += times[i] + "  ";
        }
        Debug.Log(print);
    }

}
