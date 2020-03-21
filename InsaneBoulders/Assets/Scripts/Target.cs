using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 20f;
    public GameObject Explode;
    public Material MatHigh;
    public Material MatMid;
    public Material MatLow;

    public AudioSource Bip1Sound;
    public AudioSource Bip2Sound;
    public AudioSource ExplodeSound;


    public void Start()
    {
        Renderer rend = GetComponent<Renderer>();
        if (health > 25f)
            rend.material = MatHigh;
        else if (health > 10f)
            rend.material = MatMid;
        else
            rend.material = MatLow;
    }

    public void Update()
    {
        if(transform.position.y < -300f)
        {
            Destroy(gameObject);
        }
    }

    public void isDamaged(float damage)
    {
        Renderer rend = GetComponent<Renderer>();
        health -= damage;
        if (health > 25f)
            rend.material = MatHigh;
        else if (health > 10f)
            rend.material = MatMid;
        else
            rend.material = MatLow;

        if (health > 0)
        {
            int randForSound = Random.Range(0, 2);
            if (randForSound == 0)
            {
                Bip1Sound.Play();
            }
            if (randForSound == 1)
            {
                Bip2Sound.Play();
            }
            transform.localScale -= new Vector3(damage / 50f, damage / 50f, damage / 50f);
        }
        else
        {
            ExplodeSound.Play();
            GameObject ex = Instantiate(Explode,transform.position,transform.rotation);
            Destroy(ex, 3f);
            Destroy(gameObject,0.1f);
        }
    }
}
