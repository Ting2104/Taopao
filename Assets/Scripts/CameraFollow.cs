using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject playerFollow;
    public GameObject limitI, limitD;

    public float xMin, xMax, yMin, yMax;
    private float camY, camX;
    private float playerX, playerY;
    public float speed;

    private void Start()
    {
        xMin = limitI.transform.position.x;
        xMax = limitD.transform.position.x;
        yMin = limitI.transform.position.y;
        yMax = limitD.transform.position.y;

        camX = playerX + xMin;
        camY = playerY + yMin;
        transform.position = Vector3.Lerp(transform.position, new Vector3(camX, camY, -1), 1);
    }
    //Movimiento de la cámara 
    void MoveCam()
    {
        if (playerFollow)
        {
            playerX = playerFollow.transform.position.x;
            playerY = playerFollow.transform.position.y;
            if (playerX > xMin && playerX < xMax)
                camX = playerX;
            float mejorCam = camY - camY / 4;
            if (mejorCam > 0) { mejorCam = -mejorCam; }
            if (playerY > yMin && playerY < yMax)
                camY = playerY;
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(camX, camY, -1), speed * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        MoveCam();
    }

}