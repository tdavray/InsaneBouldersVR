using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public Text textEnd;
    public Text textInstructions;
    public Image EndBG;
    public AudioSource WinSound;
    public AudioSource LoseSound;
    private bool over = false;
    private bool win = false;


    // Update is called once per frame
    void Update()
    {

        GameObject[] targetsContainers = GameObject.FindGameObjectsWithTag("TargetsContainer");
        int countTargets = 0;
        foreach(GameObject tc in targetsContainers)
        {
            Transform[] childrenTransforms = tc.GetComponentsInChildren<Transform>(true);
            countTargets += childrenTransforms.Length - 1;
        }
        //GameObject[] targets = GameObject.FindGameObjectsWithTag("target");
        
        float curr = GameObject.Find("Timer").GetComponent<Timer>().currentTime;
        float lives = GameObject.Find("OVRPlayerController").GetComponent<EventsVR>().lives;
        if (((countTargets > 0 && curr <= 0) || lives <= 0) && !over)
        {
            over = true;
            LoseSound.Play();
            textEnd.color = Color.red;
            win = false;
            setEndGame(win, "You Lose");
        }
        else if(countTargets <= 0 && curr > 0 && !over)
        {
            over = true;
            WinSound.Play();
            textEnd.color = Color.green;
            win = true;
            setEndGame(win, "You Win");
        }
        else if(!over)
        {
            textEnd.enabled = false;
            textInstructions.enabled = false;
            EndBG.enabled = false;
        }
        else
        {
            setEndGame(win);
        }

    }

    void setEndGame(bool win, string txt = "")
    {
        textEnd.enabled = true;
        textInstructions.enabled = true;
        EndBG.enabled = true;
        //GameObject.Find("Pistol").GetComponent<Gun>().bulletsLeft = 0;
        if (txt != "")
            textEnd.text = txt;
        Time.timeScale = 0;//We pause the game
        /*GameObject[] crossElems = GameObject.FindGameObjectsWithTag("cross");
        foreach(GameObject crossElem in crossElems)
        {
            crossElem.SetActive(false);
        }*/
        textInstructions.text = "Reload to open menu";
        if (Input.GetButtonDown("Cancel") || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            SceneManager.LoadScene("MenuStartVR");
            Time.timeScale = 1;
        }

        /*if (win)
        {
            int index = SceneManager.GetActiveScene().buildIndex;
            Debug.Log(index);
            if(index < 3)//MAX level
                textInstructions.text = "Fire to load next level\nReload to quit";
            else textInstructions.text = "Fire to restart last level\nReload to quit";
            if (Input.GetButtonDown("Fire1") || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                if (index < 3)
                    SceneManager.LoadScene(index + 1);
                else SceneManager.LoadScene(index);
                Time.timeScale = 1;
            }
            else if (Input.GetButtonDown("Cancel") || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
                Application.Quit();
        }
        else
        {
            textInstructions.text = "Fire to retry \nReload to quit";
            if (Input.GetButtonDown("Fire1") || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
            }
            else if (Input.GetButtonDown("Cancel") || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
                Application.Quit();
        }*/

    }
}
