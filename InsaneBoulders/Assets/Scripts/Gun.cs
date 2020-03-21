using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public float gunRange = 200f;
    public float gunDamage = 1.5f;
    public int bulletsLeft = 6;
    public int maxBullets = 6;
    public int totalBullets = 24;
    public Text textBullets;

    public GameObject GunObject;

    public ParticleSystem Flash;
    public GameObject Impact;
    public AudioSource GunSound;
    public AudioSource ReloadSound;

    void Start()
    {
        textBullets.text = bulletsLeft + " / " + totalBullets;
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") || OVRInput.GetDown(OVRInput.RawButton.RIndexTrigger))
        {
            if(bulletsLeft > 0)
            {
                ShootGun();
                bulletsLeft--;
                if(bulletsLeft > 0)
                    textBullets.text = bulletsLeft + " / " + totalBullets;
                else
                    textBullets.text = "Reload / " + totalBullets;
            }
        }
        if (Input.GetButtonDown("Fire2") || OVRInput.GetDown(OVRInput.RawButton.RHandTrigger))
        {
            //totalBullets = (totalBullets - maxBullets) + bulletsLeft;
            //bulletsLeft = 0;
            StartCoroutine(reloadCoroutine());
        }
    }

    void ShootGun()
    {
        Flash.Play();
        GunSound.Play();
        RaycastHit hit;
        if(Physics.Raycast(GunObject.transform.position, GunObject.transform.forward, out hit, gunRange))
        {
            //Debug.Log(hit.distance);
            Target target = hit.transform.GetComponent<Target>();
            if(target != null)
            {
                //Let's make the damage dependant of the distance
                float dmg = gunDamage + ((gunDamage / hit.distance) * 3);
                Debug.Log(dmg);
                target.isDamaged(dmg);
            }

            GameObject im = Instantiate(Impact, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(im, 2f);
        }
    }

    IEnumerator reloadCoroutine()
    {
        ReloadSound.Play();
        int temp = bulletsLeft;
        bulletsLeft = 0;
        yield return new WaitForSeconds(1.5f);
        if (maxBullets <= totalBullets)
        {
            bulletsLeft = maxBullets;
            totalBullets = (totalBullets - maxBullets) + temp;
        }
        else
        {
            bulletsLeft = totalBullets;
            totalBullets = 0;
        }

        textBullets.text = bulletsLeft + " / " + totalBullets;
    }
}
