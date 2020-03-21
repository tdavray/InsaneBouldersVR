using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAround : MonoBehaviour
{

    public Transform player;
    float rotateX = 0f;

    // Start is called before the first frame update
    void Start()
    {
        //We can avoid the mouse doing something else, we can lock it in the game.
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        // mX and mY are the position after mouvement of the mouse cursor
        float mX = Input.GetAxis("Mouse X") * Time.deltaTime * 120f; //120 is the sensitivity
        float mY = Input.GetAxis("Mouse Y") * Time.deltaTime * 120f;

        rotateX -= mY;
        rotateX = Mathf.Clamp(rotateX, -90f, 90f);//This will force the camera to stay between -90 degrees and 90 degrees

        transform.localRotation = Quaternion.Euler(rotateX, 0f, 0f);

        player.Rotate(Vector3.up * mX);
    }
}
