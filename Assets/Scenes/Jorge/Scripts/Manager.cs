using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Manager : MonoBehaviour
{

    private GameObject spheres;
    private bool explore = true;
    public static bool testing = false;

    void OnEnable()
    {
        spheres = GameObject.Find("Spheres");
        int numberSpheres = spheres.transform.childCount;
        /*
        Task1 task1 = new Task1();
        Task.currentTask = task1;
        */

        Debug.Log("Press ENTER to start the tests");
    }


    void Update()
    {
        if (!explore & testing) {
            if(Task.currentTask.ToContinue() & Input.GetKeyDown(KeyCode.Return)) {
                Task.currentTask.StartNextTask();
            } else if(Task.currentTask.GetTaskId() == "2" & Input.GetKeyDown(KeyCode.Return)) {
                // Might not have been the right node selected 
                Task.currentTask.StopTask();
            } else if(Task.currentTask.GetTaskId() == "3" & Input.GetKeyDown(KeyCode.Return)) {
                // Might not have been the right node selected 
                Task.currentTask.StopTask();
            }
        } else if (explore & !testing & Input.GetKeyDown(KeyCode.Return)) {
            explore = false;
        } else if (!explore & !testing) {
            //int random = Random.Range(0, numberSpheres);
            //GameObject firstSelectedNode = spheres.transform.GetChild(random).gameObject;
            //NodeFeedback firstTest = firstSelectedNode.AddComponent<NodeFeedback>();
            CSVEncoder encoder = null;
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                encoder = new CSVEncoder("tasks_time_kbm");
            }
            else if (SceneManager.GetActiveScene().buildIndex == 1)
            {
                encoder = new CSVEncoder("tasks_time_vr");
            }
            else if (SceneManager.GetActiveScene().buildIndex == 2)
            {
                encoder = new CSVEncoder("tasks_time_vr_kbm");
            }
            Task task = new Task3(encoder, spheres);
            Task.currentTask = task; 
            task.Start();
            testing = true;
        }
    }
}
