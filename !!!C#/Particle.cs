using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] ParticleSystem effect1;
    [SerializeField] ParticleSystem effect2;

    [SerializeField] Transform follow1;
    [SerializeField] Transform follow2;
    [SerializeField] Transform follow3;
    [SerializeField] Transform follow4;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = follow1.position;

    }

    public void Player1()
    {
        Instantiate(effect1, transform.position,Quaternion.identity);
        effect1.Play();
    }

}
