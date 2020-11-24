using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class CSVEncoder {

    string fileName;
    string extension = ".csv";
    string path = "Assets\\Scenes\\Times\\"; 

    double firstTaskTime;
    double secondTaskTime;
    double thirdTaskTime;

    public CSVEncoder(string fileName) {
        this.fileName = fileName;
        CreateFile(fileName + extension);
    }

    private void CreateFile(string fileName) {
        if (!FileExists(path + fileName)) {
            Debug.Log("Creating file");
            StreamWriter outStream = File.CreateText(path + fileName);
            outStream.WriteLine("Task 1,Task 2,Task 3,Total");
            outStream.Close();
        }
    }

    // Adds a line to CSV file - It may be a good idea to call this method after SetThirdTaskTime(millis)
    public void UpdateFile() {
        Debug.Log("Updating file");
        File.AppendAllText(path + fileName + extension, GetFirstTaskTime() + "," + GetSecondTaskTime() + "," + GetThirdTaskTime() + "," + GetTotalTasksTime() + "\n" );
    }

    private bool FileExists(string fileName) {
        return File.Exists(fileName);
    }

    public void SetFirstTaskTime(double millis) {
        firstTaskTime = millis;
    }

    public void SetSecondTaskTime(double millis) {
        if (firstTaskTime <= 0) { Debug.Log("ALERT: You are inserting the time that the second task takes with the time of the first task equal to 0."); }
        secondTaskTime = millis;
    }

    public void SetThirdTaskTime(double millis) {
        if (firstTaskTime <= 0) { Debug.Log("ALERT: You are inserting the time that the third task takes with the time of the first task equal to 0."); }
        if (secondTaskTime <= 0) { Debug.Log("ALERT: You are inserting the time that the third task takes with the time of the second task equal to 0."); }
        thirdTaskTime = millis;
    }

    public double GetFirstTaskTime() {
        return firstTaskTime;
    }

    public double GetSecondTaskTime() {
        return secondTaskTime;
    }

    public double GetThirdTaskTime() {
        return thirdTaskTime;
    }

    public double GetTotalTasksTime() {
        return GetFirstTaskTime() + GetSecondTaskTime() + GetThirdTaskTime();
    }

}
