using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameFromMenu : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            SceneManager.LoadScene("MainSceneVR");
        }
    }
}
