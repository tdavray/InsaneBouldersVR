using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EventsVR : MonoBehaviour
{
    public int lives = 3;
    public Text textLives;
    public Text textEvent;

    public AudioSource HurtSound;
    public AudioSource FizzleSound;
    public AudioSource EventSound;
    public AudioSource Music;

    public GameObject GunGO;

    private bool invincible = false;
    private bool onPause = false;

    public void Start()
    {
        textLives.text = lives + " lives left";
        textEvent.enabled = false;

    }

    public void Update()
    {
        if (OVRInput.GetDown(OVRInput.RawButton.LHandTrigger))
        {
            GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().Acceleration *= 1.5f;
        }
        if (OVRInput.GetUp(OVRInput.RawButton.LHandTrigger))
        {
            GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().Acceleration /= 1.5f;
        }

        if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.F) || onPause)
        {
            Debug.Log("FPressed");
            Time.timeScale = 0f;
            textEvent.enabled = true;
            textEvent.text = "B to keep playing\nReload for menu";
            if (OVRInput.GetDown(OVRInput.RawButton.RHandTrigger) || Input.GetButtonDown("Fire1"))
            {
                SceneManager.LoadScene("MenuStartVR");
                Time.timeScale = 1f;
            }
            
            if ((OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.F)) && onPause)
            {
                onPause = false;
                Time.timeScale = 1f;
                textEvent.enabled = false;
                textEvent.text = "";
            }
            else
            {
                onPause = true;
            }

        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "target")
        {
            if (!invincible)
            {
                HurtSound.Play();
                FizzleSound.Play();
                lives -= 1;
                textLives.text = lives + " lives left";
                invincible = true;
                StartCoroutine(InvincibleTime());
            }
            
        }
        else if (other.tag == "event")
        {
            EventSound.Play();
            launchRandEvent();
            other.gameObject.SetActive(false);
            StartCoroutine(EventRespawn(other.gameObject));
        }
    }

    IEnumerator EventRespawn(GameObject go)
    {
        yield return new WaitForSeconds(30);
        go.SetActive(true);
    }

    IEnumerator InvincibleTime()
    {
        yield return new WaitForSeconds(5);
        invincible = false;
    }

    void launchRandEvent()
    {
        int randevent = Random.Range(0, 4);
        if (randevent == 0)
        {
            StartCoroutine(ShowEventText("+15 seconds"));
            GameObject.Find("Timer").GetComponent<Timer>().currentTime += 15;
        }
        else if (randevent == 1)
        {
            StartCoroutine(ShowEventText("+12 bullets"));
            Gun gun = GunGO.GetComponent<Gun>();
            //gun.maxBullets += 1;
            //gun.bulletsLeft += 1;
            gun.totalBullets += 12;
            gun.textBullets.text = gun.bulletsLeft + " / " + gun.totalBullets;
        }
        else if (randevent == 2)
        {
            StartCoroutine(ShowEventText("Slow motion!"));
            StartCoroutine(SlowMotion());
        }
        else if (randevent == 3)
        {
            StartCoroutine(ShowEventText("Invincible!"));
            StartCoroutine(SuperSpeed());
        }
    }

    IEnumerator ShowEventText(string txt)
    {
        textEvent.enabled = true;
        textEvent.text = txt;
        yield return new WaitForSeconds(2.2f);
        textEvent.enabled = false;
    }

    IEnumerator SlowMotion()
    {
        Time.timeScale = 0.5f;
        Music.pitch = 0.5f;
        yield return new WaitForSeconds(5);
        Music.pitch = 1;
        Time.timeScale = 1;
    }

    IEnumerator SuperSpeed()
    {
        //speed = 33f;
        invincible = true;
        GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().Acceleration *= 2;
        Music.pitch = 1.3f;
        yield return new WaitForSeconds(10);
        GameObject.Find("OVRPlayerController").GetComponent<OVRPlayerController>().Acceleration /= 2;
        yield return new WaitForSeconds(2);
        invincible = false;
        //speed = 17f;
        Music.pitch = 1;
    }
}
