using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouvmentPlayer : MonoBehaviour
{

    public CharacterController contr;
    public GameObject Player;

    public float gravity = -9.81f;
    public float jumpH = 4f;
    public float speed = 17f;

    public Transform checkGround;
    public LayerMask maskGround;

    Vector3 velocity;
    bool isGrounded;

    public int lives = 3;
    public Text textLives;
    public Text textEvent;

    public AudioSource HurtSound;
    public AudioSource FizzleSound;
    public AudioSource EventSound;
    public AudioSource Music;

    public void Start()
    {
        textLives.text = lives + " lives left";
        textEvent.enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(checkGround.position, 0.5f, maskGround);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -10f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        contr.Move(move * Time.deltaTime * speed);

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpH * -2f * gravity);//Formula of a falling object
        }

        velocity.y += gravity * Time.deltaTime;

        contr.Move(velocity * Time.deltaTime);

        /*if (OVRInput.GetDown(OVRInput.Button.Two) || Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("FPressed");
            Time.timeScale = 0f;
            textEvent.enabled = true;
            textEvent.text = "Fire to continue\nB for menu";
            if (OVRInput.GetDown(OVRInput.Button.Two))
            {
                SceneManager.LoadScene("MenuStartVR");
            }
            else if (OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
            {
                Time.timeScale = 1f;
                textEvent.enabled = false;
                textEvent.text = "";
            }
        }*/
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "target")
        {
            HurtSound.Play();
            FizzleSound.Play();
            lives -= 1;
            textLives.text = lives + " lives left";
        }
        else if(other.tag == "event")
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

    void launchRandEvent()
    {
        int randevent = Random.Range(0, 4);
        if(randevent == 0)
        {
            StartCoroutine(ShowEventText("+15 seconds"));
            GameObject.Find("Timer").GetComponent<Timer>().currentTime += 15;
        }
        else if(randevent == 1)
        {
            StartCoroutine(ShowEventText("+1 max bullet in the barrel"));
            Gun gun = GameObject.Find("Pistol").GetComponent<Gun>();
            gun.maxBullets += 1;
            gun.bulletsLeft += 1;
            gun.textBullets.text = gun.bulletsLeft + " bullets left";
        }
        else if (randevent == 2)
        {
            StartCoroutine(ShowEventText("Slow motion!"));
            StartCoroutine(SlowMotion());
        }
        else if (randevent == 3)
        {
            StartCoroutine(ShowEventText("Super speed!"));
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
        speed = 33f;
        Music.pitch = 1.3f;
        yield return new WaitForSeconds(10);
        speed = 17f;
        Music.pitch = 1;
    }
}
