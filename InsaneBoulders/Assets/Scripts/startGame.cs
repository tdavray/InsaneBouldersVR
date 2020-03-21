using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public GameObject StartCanvas;
    public GameObject GameCanvas;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        StartCanvas.SetActive(true);
        GameCanvas.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Oculus_GearVR_RThumbstickY") || Input.GetButtonDown("space"))
        {
            Time.timeScale = 1;
            StartCanvas.SetActive(false);
            GameCanvas.SetActive(true);
        }
    }
}
