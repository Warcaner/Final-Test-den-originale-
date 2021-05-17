using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using Oculus.Avatar;

public class spell : MonoBehaviour
{

    public AudioSource sndWandActivate;
    public AudioClip sndTrigPressed; // wand activate sound
    public AudioClip sndTrigHeld;
    public float trigActivate;
    public bool btnActivate; // button debug stuff
    public GameObject thatWhiteSquare;

    //public float projectileSpeed = 30;

    public GameObject projectile;
    public float shootForce;

    private Vector3 destination;


    public Camera cam;

    public float timer = 0;

    public bool firstFrameUpdate;

    public bool hasPlayed;

    [SerializeField]
    public GameObject SmokePrefab;
    public GameObject ProjectilePrefab;


    [SerializeField]
    public Transform SmokePoint;
    public Transform FirePoint;





    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("FUCKING PRINT SOMETHING YOU USELESS CUNT");
        firstFrameUpdate = false;
    }

    // Update is called once per frame
    void Update()
    {
        trigActivate = OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger);
        if (trigActivate >= 0.2 && hasPlayed == false)
        {
            timer += Time.deltaTime;
            firstFrameUpdate = true;
            goSound(sndTrigHeld);
            sndWandActivate.loop = false;
            firstFrameUpdate = false;
            hasPlayed = true;

            GameObject shot = GameObject.Instantiate(projectile, transform.position, transform.rotation);
            shot.GetComponent<Rigidbody>().AddForce(transform.forward * shootForce);

        }
        else if (trigActivate == 0 && hasPlayed == true)
        {
            sndWandActivate.loop = false;
            goSound(sndTrigPressed);
            hasPlayed = false;
            sndWandActivate.Play();
            var Smokey = Instantiate(SmokePrefab, SmokePoint.position, SmokePoint.rotation);
            Destroy(Smokey.gameObject, 5f);

        }
        btnActivate = OVRInput.Get(OVRInput.Button.One, OVRInput.Controller.RTouch);
        if (btnActivate == true)
        {
            Debug.Log(OVRInput.Get(OVRInput.Button.One));
            print("Status: " + OVRInput.Get(OVRInput.Button.One));
           
            print("FAK U!");
            //thatWhiteSquare.GetComponent<Renderer>().material.color = new Color(255,0,0);
        } else
        {
          //thatWhiteSquare.GetComponent<Renderer>().material.color = new Color(255, 255, 255);
        }

        /*void ShootProjectile()
        {


            Ray ray = camWand.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(transform.position, transform.forward,out hit))
                destination = hit.point;

            else
                destination = ray.GetPoint(1000f);

            InstantiateProjectile(FirePoint);

        }

        void InstantiateProjectile(Transform FirePoint)
        {
            var projectileObj = Instantiate(projectile, FirePoint.position, Quaternion.identity) as GameObject;
            projectileObj.GetComponent<Rigidbody>().velocity = (destination - FirePoint.position).normalized * projectileSpeed;
        }*/

        void goSound(AudioClip clip)
        {
            sndWandActivate.clip = clip;
            sndWandActivate.Play();

        }



        void stopSound(AudioClip clip)
        {
            sndWandActivate.Stop();
        }
    }
}
