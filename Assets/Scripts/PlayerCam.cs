using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    //Controls the player rotation. TO be assigned to the Player Cam

    [Header("Controller Variables")]
    public float sensX;
    public float sensY;
    public bool invertedY;

    [Header("Editor Varitables")]
    public Transform orientation; //Orientation child of player.
    public Transform CameraPos;

    float xRotation;
    float yRotation;
    float invertion; //For invertion of Y

    // Start is called before the first frame update
    void Start()
    {
        //In Final Build
        /*Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;*/

        invertion = (invertedY) ? -1 : 1;
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxisRaw("Mouse X") * sensX * Time.deltaTime * invertion * 5f;
        float inputY = Input.GetAxisRaw("Mouse Y") * sensY * Time.deltaTime * 5f;

        yRotation += inputX;

        xRotation -= inputY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);// To keep within 90


        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);

        transform.position = CameraPos.position;
    }
}
