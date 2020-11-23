using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OVRPlayerControllerJorge : MonoBehaviour
{
    public GameObject graphManager;
    public GameObject player;
    public GameObject eye;
    public GameObject leftController;
    public GameObject rightController;
    public GameObject leftRaycast;
    public GameObject rightRaycast;

    public float hitDistance;

    public float moveSpeedOVRController = 1f;
    public float moveSpeedTeleportOVRController = 10f;
    public float rotateAroundSpeed = 10f;

    public float sensitivity = 10f;

    public static GameObject objHit = null;

    public static bool playerStop = false;

    private Color orange = new Color(1.0f, 0.64f, 0.0f);

    private List<Color> colorsList = new List<Color>();
    private int colorListIterator = -1;

    private AudioSource audio;
    public AudioClip selectSound;
    public AudioClip markSound;

    public bool leftControllerRaycastOn;
    public bool rightControllerRaycastOn;

    public static int colCount = 0;

    private GameObject selectedNode;

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
        if (!playerStop)
        {
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
        }

        ////////// DECIDE RAYCAST SIDE

        if (((!leftControllerRaycastOn & !rightControllerRaycastOn) | rightControllerRaycastOn) & OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            leftControllerRaycastOn = true;
            rightControllerRaycastOn = false;

            
            if (!playerStop & objHit != null)
            {
                Outline outline = objHit.GetComponent<Outline>();
                outline.enabled = false;
                objHit = null;
                colCount = 0;   
            }
            
        }
        else if (((!leftControllerRaycastOn & !rightControllerRaycastOn) | leftControllerRaycastOn) & OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
        {
            leftControllerRaycastOn = false;
            rightControllerRaycastOn = true;
            
            
            if (!playerStop & objHit != null)
            {
                Outline outline = objHit.GetComponent<Outline>();
                outline.enabled = false;
                objHit = null;
                colCount = 0;   
            }
            
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
        
        if (colCount > 0)
        {
            
            // TODO - verificar apenas o mais perto do controller ?????
            if (objHit.tag == "Sphere")
            {
                Outline outline = objHit.GetComponent<Outline>();
                outline.enabled = true;

                /////////// SELECT & DESELECT NODE

                if ((selectedNode == null) & !playerStop & ((leftControllerRaycastOn & OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) | (rightControllerRaycastOn & OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))))
                {
                    graphManager.GetComponent<GraphManager>().ToggleMovieUI(true, objHit);

                    playerStop = true;
                    outline.OutlineColor = orange;
                    outline.OutlineWidth = 30;

                    if (objHit.GetComponent<Light>() != null)
                    {
                        Destroy(objHit.GetComponent<Light>());
                        objHit.GetComponent<Renderer>().material.color = Color.white;
                        Debug.Log("Time until select random node: " + Time.time);
                    }

                    selectedNode = objHit;

                    audio.PlayOneShot(selectSound);

                }
                else if ((objHit == selectedNode) & playerStop & ((leftControllerRaycastOn & OVRInput.GetDown(OVRInput.Button.PrimaryHandTrigger)) | (rightControllerRaycastOn & OVRInput.GetDown(OVRInput.Button.SecondaryHandTrigger))))
                {
                    graphManager.GetComponent<GraphManager>().ToggleMovieUI(false, objHit);

                    playerStop = false;
                    outline.OutlineColor = Color.white;
                    outline.OutlineWidth = 10;

                    selectedNode = null;

                    audio.PlayOneShot(selectSound);
                }

                ////////// MARK NODES

                if (OVRInput.GetDown(OVRInput.Button.One) & outline.OutlineColor != orange)
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
                        graphManager.GetComponent<GraphManager>().OutlineNodeEdges(objHit.transform.position, outline.OutlineColor, 10, true);
                        // OutlineNodes(objHit.position)
                    }

                    audio.PlayOneShot(markSound);
                }

                ////////// ROTATE AROUND
                
                if (playerStop)
                {
                    Vector2 primaryAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);

                    float xValueRotateAround = primaryAxis.x * Time.deltaTime * rotateAroundSpeed;
                    //float zValueRotate = Input.GetAxis("Vertical") * Time.deltaTime * rotateSpeed;
                    transform.RotateAround(objHit.transform.position, Vector3.up, -xValueRotateAround);
                    //currentRotation.x = transform.localRotation.eulerAngles.y;
                    //transform.RotateAround(objHit.transform.position, Vector3.right, zValueRotate);
                    player.transform.position = transform.position;
                }
                
            }
        }
        else if (colCount == 0 & objHit != null)
        {
            var outline = objHit.GetComponent<Outline>();
            if (outline.OutlineColor == Color.white)
            {
                outline.enabled = false;
                graphManager.GetComponent<GraphManager>().OutlineNodeEdges(objHit.transform.position, outline.OutlineColor, 10, false);
                objHit = null;

            }
            colorListIterator = -1;
        }
    }
}
