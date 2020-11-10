using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRPlayerControllerJorge : MonoBehaviour
{
    public GameObject player;
    public GameObject eye;
    public GameObject leftController;
    public GameObject rightController;
    public GameObject leftRaycast;
    public GameObject rightRaycast;

    public float hitDistance = 2f;

    public float moveSpeedOVRController = 1f;
    public float moveSpeedTeleportOVRController = 10f;
    public float rotateAroundSpeed = 10f;

    public float sensitivity = 10f;

    private GameObject objHit = null;

    private bool playerStop = false;

    private Color orange = new Color(1.0f, 0.64f, 0.0f);

    private List<Color> colorsList = new List<Color>();
    private int colorListIterator = -1;

    private AudioSource audio;
    public AudioClip selectSound;
    public AudioClip markSound;

    public bool leftControllerRaycastOn;
    public bool rightControllerRaycastOn;

    private void Start()
    {
        colorsList.Add(Color.green);
        colorsList.Add(Color.cyan);
        colorsList.Add(Color.red);
        colorsList.Add(Color.yellow);

        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        ////////// CAMERA & PLAYER POSITIONS

        Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
        Vector2 secondaryAxis = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick);

        float xValue = primaryAxis.x * Time.deltaTime * moveSpeedOVRController;
        float zValue;

        if (secondaryAxis == Vector2.zero)
        {
            zValue = primaryAxis.y * Time.deltaTime * moveSpeedOVRController;
        }
        else
        {
            zValue = secondaryAxis.y * Time.deltaTime * moveSpeedTeleportOVRController;
        }

        transform.position += eye.transform.forward * zValue;
        transform.position += eye.transform.right * xValue;

        player.transform.position = transform.position;

        ////////// DECIDE RAYCAST SIDE

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            leftControllerRaycastOn = true;
            rightControllerRaycastOn = false;
        }
        else if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            leftControllerRaycastOn = false;
            rightControllerRaycastOn = true;
        }


        ////////// SHOW RAYCASTS
        
        if (leftControllerRaycastOn) 
        {
            leftRaycast.SetActive(true);
            rightRaycast.SetActive(false);
        } 
        else if (rightControllerRaycastOn) 
        {
            leftRaycast.SetActive(false);
            rightRaycast.SetActive(true);
        }


        ////////// SEND RAYCASTS
        
        RaycastHit hit;
        if ((leftControllerRaycastOn & Physics.Raycast(leftController.transform.position, leftController.transform.forward, out hit, hitDistance)) | (rightControllerRaycastOn & Physics.Raycast(rightController.transform.position, rightController.transform.forward, out hit, hitDistance)))
        {
            Debug.Log("Hitting sphere");
        }


    }
}
