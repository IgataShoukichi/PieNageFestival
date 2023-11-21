using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Foot_sound : MonoBehaviour
{
    [SerializeField] public AudioClip step;
    [SerializeField] public AudioClip step1;
    [SerializeField] public AudioClip wipe;
    //[SerializeField] public AudioClip step2;
    //[SerializeField] public AudioClip step3;
    [SerializeField] public AudioSource aud;
    [SerializeField] public AudioSource aud1;

    int rnd;
    // Start is called before the first frame update
    void Start()
    {
        //this.aud = GetComponent<AudioSource>();

    }

    public void FootSound()
    {

        rnd = Random.Range(0, 2);
        if(rnd == 0)
        {
            aud.PlayOneShot(step);
        }
        else if(rnd == 1)
        {
            aud.PlayOneShot(step1);
        }
        /*
        else if(rnd == 2)
        {
            aud.PlayOneShot(step2);
        }
        else
        {
            aud.PlayOneShot(step3);
        }
        */
    }
    public void FootSound1()
    {
        rnd = Random.Range(0, 2);
        if (rnd == 0)
        {
            aud.PlayOneShot(step);
        }
        else if (rnd == 1)
        {
            aud.PlayOneShot(step1);
        }
    }

    public void Wipe()
    {
        aud1.PlayOneShot(wipe,1f);
    }

    /*
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Floor")
        {
            Debug.Log("“®‚¢‚Ä‚é");
            this.aud.PlayOneShot(this.step);
        }
    }
    */
}
