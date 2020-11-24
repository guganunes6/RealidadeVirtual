using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject graphManager;
    public GameObject player;

    public float hitDistance = 2f;

    public float moveSpeedWASD = 10f;
    public float moveSpeedMouseWheel = 10f;
    public float rotateAroundSpeed = 10f;

    public float sensitivity = 10f;
    public float maxYAngle = 89.5f;
    private Vector2 currentRotation;

    private GameObject objHit = null;

    private bool playerStop = false;

    private Color orange = new Color(1.0f, 0.64f, 0.0f);

    private List<Color> colorsList = new List<Color>();
    private int colorListIterator = -1;

    private AudioSource audio;
    public AudioClip selectSound;
    public AudioClip markSound;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        colorsList.Add(Color.green);
        colorsList.Add(Color.cyan);
        colorsList.Add(Color.red);
        colorsList.Add(Color.yellow);

        audio = GetComponent<AudioSource>();
    }
    void Update()
    {
        if (!playerStop)
        {
            ////////// CAMERA & PLAYER POSITIONS

            float xValue = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeedWASD;
            float zValue;
            if (Input.mouseScrollDelta.y != 0)
            {
                zValue = Input.mouseScrollDelta.y * moveSpeedMouseWheel;
            }
            else
            {
                zValue = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeedWASD;
            }
            transform.Translate(xValue, 0, zValue);

            player.transform.position = transform.position;

            ////////// CAMERA ROTATION
            
            currentRotation.x += Input.GetAxis("Mouse X") * sensitivity;
            currentRotation.y -= Input.GetAxis("Mouse Y") * sensitivity;
            currentRotation.x = Mathf.Repeat(currentRotation.x, 360);
            currentRotation.y = Mathf.Clamp(currentRotation.y, -maxYAngle, maxYAngle);

            Camera.main.transform.rotation = Quaternion.Euler(currentRotation.y, currentRotation.x, 0);
            

        }

        ////////// RAYCASTS

        RaycastHit hit;
        var cameraCenter = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2f, Screen.height / 2f, Camera.main.nearClipPlane));
        if (Physics.Raycast(cameraCenter, transform.forward, out hit, hitDistance))
        {
            objHit = hit.transform.gameObject;
            if (objHit.tag == "Sphere")
            {
                Outline outline = objHit.GetComponent<Outline>();
                outline.enabled = true;

                /////////// SELECT & DESELECT NODE

                if (Input.GetMouseButtonDown(0) & !playerStop)
                {
                    graphManager.GetComponent<GraphManager>().ToggleMovieUI(true, objHit);

                    playerStop = true;
                    outline.OutlineColor = orange;
                    outline.OutlineWidth = 30;

                    //if (objHit.GetComponent<Light>() != null)
                    //{
                    //    List<GameObject> objsHit = new List<GameObject>();
                    //    objsHit.Add(objHit);
                    //    Task.currentTask.Stop(objsHit);
                    //    //Destroy(objHit.GetComponent<Light>());
                    //    //objHit.GetComponent<Renderer>().material.color = Color.white;
                    //    //Debug.Log("Time until select random node: " + Time.time);
                    //}
                    Task.currentTask.StopTask(objHit);

                    audio.PlayOneShot(selectSound);

                }
                else if (Input.GetMouseButtonDown(0) & playerStop)
                {
                    graphManager.GetComponent<GraphManager>().ToggleMovieUI(false, objHit);

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
                        graphManager.GetComponent<GraphManager>().OutlineNodeEdges(objHit, outline.OutlineColor, 10, colorListIterator != -1);
                    }

                    audio.PlayOneShot(markSound);
                }

                ////////// ROTATE AROUND

                if (playerStop)
                {
                    float xValueRotateAround = Input.GetAxis("Horizontal") * Time.deltaTime * rotateAroundSpeed;
                    //float zValueRotate = Input.GetAxis("Vertical") * Time.deltaTime * rotateSpeed;
                    transform.RotateAround(objHit.transform.position, Vector3.up, -xValueRotateAround);
                    currentRotation.x = transform.localRotation.eulerAngles.y;
                    //transform.RotateAround(objHit.transform.position, Vector3.right, zValueRotate);
                    player.transform.position = transform.position;
                }
            }
        }
        else if (objHit != null)
        {
            var outline = objHit.GetComponent<Outline>();
            if (outline.OutlineColor == Color.white)
            {
                outline.enabled = false;
                objHit = null;

            }
            colorListIterator = -1;
        }
        
    }

}
