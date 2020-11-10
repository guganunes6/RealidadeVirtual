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
            objHit = hit.transform.gameObject;
            if (objHit.tag == "Sphere")
            {
                Outline outline = objHit.GetComponent<Outline>();
                outline.enabled = true;

                /////////// SELECT & DESELECT NODE

                if (Input.GetMouseButtonDown(0) & !playerStop)
                {
                    playerStop = true;
                    outline.OutlineColor = orange;
                    outline.OutlineWidth = 30;

                    if (objHit.GetComponent<Light>() != null)
                    {
                        Destroy(objHit.GetComponent<Light>());
                        objHit.GetComponent<Renderer>().material.color = Color.white;
                        Debug.Log("Time until select random node: " + Time.time);
                    }

                    audio.PlayOneShot(selectSound);

                }
                else if (Input.GetMouseButtonDown(0) & playerStop)
                {
                    playerStop = false;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 10;

                    audio.PlayOneShot(selectSound);
                }

                ////////// MARK NODES

                if (Input.GetMouseButtonDown(1) & outline.OutlineColor != orange)
                {
                    if (colorListIterator == 3)
                    {
                        outline.OutlineColor = Color.white;
                        outline.OutlineWidth = 10;
                        colorListIterator = -1;
                    }
                    else if (colorListIterator >= -1 & colorListIterator <= 2)
                    {
                        outline.OutlineColor = colorsList[colorListIterator + 1];
                        outline.OutlineWidth = 100;
                        colorListIterator++;
                    }

                    if (objHit.tag == "Sphere")
                    {
                        // get GraphManager component
                        //GraphManager.GetComponent<GraphManager>().OutlineNodeEdges(objHit.transform.position, outline.OutlineColor, 10, true);
                        // OutlineNodes(objHit.position)
                    }

                    audio.PlayOneShot(markSound);
                }

                ////////// ROTATE AROUND
                /*
                if (playerStop)
                {
                    float xValueRotateAround = Input.GetAxis("Horizontal") * Time.deltaTime * rotateAroundSpeed;
                    //float zValueRotate = Input.GetAxis("Vertical") * Time.deltaTime * rotateSpeed;
                    transform.RotateAround(objHit.transform.position, Vector3.up, -xValueRotateAround);
                    currentRotation.x = transform.localRotation.eulerAngles.y;
                    //transform.RotateAround(objHit.transform.position, Vector3.right, zValueRotate);
                    player.transform.position = transform.position;
                }
                */
            }
        }
        else if (objHit != null)
        {
            var outline = objHit.GetComponent<Outline>();
            if (outline.OutlineColor == Color.white)
            {
                outline.enabled = false;
                //GraphManager.GetComponent<GraphManager>().OutlineNodeEdges(objHit.transform.position, outline.OutlineColor, 10, false);
                objHit = null;

            }
            colorListIterator = -1;
        }


    }
}
