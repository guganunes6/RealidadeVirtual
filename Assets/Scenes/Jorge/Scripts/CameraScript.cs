﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject player;

    public float hitDistance = 2f;

    public float moveSpeedWASD = 10f;
    public float moveSpeedMouseWheel = 10f;

    public float sensitivity = 10f;
    public float maxYAngle = 89.5f;
    private Vector2 currentRotation;

    private GameObject objHit = null;

    private bool playerStop = false;

    private Color orange = new Color(1.0f, 0.64f, 0.0f);

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
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
            var outline = objHit.GetComponent<Outline>();
            outline.enabled = true;
            if (Input.GetMouseButtonDown(0) & !playerStop)
            {
                playerStop = true;
                outline.OutlineColor = orange;
                outline.OutlineWidth = 30;
            }
            else if (Input.GetMouseButtonDown(0) & playerStop)
            {
                playerStop = false;
                outline.OutlineColor = Color.white;
                outline.OutlineWidth = 10;
            }
        }
        else if (objHit != null)
        {
            var outline = objHit.GetComponent<Outline>();
            outline.enabled = false;
            objHit = null;
        }

    }

}
