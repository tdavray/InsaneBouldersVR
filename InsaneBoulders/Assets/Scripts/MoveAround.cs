using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This script is to make the target move around "randomly"
public class MoveAround : MonoBehaviour
{
    //We can chage this values to increase the difficulty, Level system ?
    public float movingSpeed = 5f;
    public float rotationSpeed = 200f;

    private bool isWandering = false;
    private bool isRotationRight = false;
    private bool isRotationLeft = false;
    private bool isMoving = false;
    public bool freeze = false;

    // Update is called once per frame
    void Update()
    {
        if (!freeze)
        {
            if (isWandering == false)
            {
                StartCoroutine(Wander());
            }
            if (isRotationRight == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * rotationSpeed);
            }
            if (isRotationLeft == true)
            {
                transform.Rotate(transform.up * Time.deltaTime * -rotationSpeed);
            }
            if (isMoving == true)
            {
                transform.position += transform.forward * movingSpeed * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "wall")
        {
            int rotation = Random.Range(150, 210);
            transform.RotateAround(transform.position, transform.up, rotation);
        }
    }

    IEnumerator Wander()
    {
        //int rotTime = Random.Range(1, 3);
        //int rotWait = Random.Range(1, 3);
        int rotLeftOrRight = Random.Range(0, 2);
        //int moveWait = Random.Range(1, 3);
        int moveTime = Random.Range(4, 10);

        isWandering = true;

        //yield return new WaitForSeconds(moveWait);
        isMoving = true;
        yield return new WaitForSeconds(moveTime);
        isMoving = false;
        //yield return new WaitForSeconds(rotWait);
        if(rotLeftOrRight == 0)
        {
            isRotationRight = true;
            //yield return new WaitForSeconds(rotTime);
            isRotationRight = false;
        }
        if (rotLeftOrRight == 1)
        {
            isRotationLeft = true;
            //yield return new WaitForSeconds(rotTime);
            isRotationLeft = false;
        }
        isWandering = false;

    }
}
