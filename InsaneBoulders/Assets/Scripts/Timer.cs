using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Timer : MonoBehaviour
{
    public Text textTimer;
    public float currentTime = 0f;
    public float startingTime = 10f;
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentTime <= 0)
        {
            currentTime = 0;
        }
        else
        {
            currentTime -= 1 * Time.deltaTime;
            textTimer.text = currentTime.ToString("000");
        }
    }
}
