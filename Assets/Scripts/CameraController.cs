using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Reference to the player GameObject.
    public GameObject player;

    // The distance between the camera and the player.
    private Vector3 offset;
    private float angle;

    [SerializeField]
    private float rotationSpeed = 10f;

    [SerializeField]
    private float camVerticalOffset = 15f;

    // Start is called before the first frame update.
    void Start()
    {
        // Calculate the initial offset between the camera's position and the player's position.
        offset = transform.position - player.transform.position;
    }

    // LateUpdate is called once per frame after all Update functions have been completed.
    void LateUpdate()
    {

        if (Input.GetKey(KeyCode.Q))
        {
            angle += rotationSpeed * Time.deltaTime;
            Debug.Log("Left turn");
        }
        if (Input.GetKey(KeyCode.E))
        {
            angle -= rotationSpeed * Time.deltaTime;
            Debug.Log("Right turn");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            angle = 0;
            Debug.Log("Reset angle");
        }

        // Maintain the same offset between the camera and player throughout the game.
        Vector3 rotatedOffset = Quaternion.AngleAxis(angle, Vector3.up) * offset;
        
        transform.position = player.transform.position + rotatedOffset;

        //Debug.Log(angle);
        //Debug.Log(offset);
        //Debug.Log(rotatedOffset);

        transform.LookAt(player.transform);
        transform.rotation = Quaternion.AngleAxis(camVerticalOffset, transform.right) * transform.rotation;

        //vector3 pointdirection = player.transform.position - transform.position;
        //quaternion camrotation = quaternion.lookrotation(pointdirection, vector3.up);
        //quaternion adjustedcamrotation = quaternion.angleaxis(camoffset, transform.right);
        //transform.rotation = adjustedcamrotation;
    }



}