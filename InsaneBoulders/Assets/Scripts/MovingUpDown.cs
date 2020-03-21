using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUpDown : MonoBehaviour
{
    private Vector3 startPosition;
    public AnimationCurve myCurve;

    private void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, startPosition.y + myCurve.Evaluate((Time.time % myCurve.length)), transform.position.z);
    }
}
