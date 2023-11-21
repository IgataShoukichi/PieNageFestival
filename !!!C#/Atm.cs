using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Atm : MonoBehaviour
{
    [System.NonSerialized] public PlayerController PC;

    // Update is called once per frame
    void Update()
    {
        if (PC.LeanNum == 1)
        {
            transform.DOLocalMove(new Vector3(2.55f, -1.45f, 2.5f), 0.1f);
        }

        if(PC.LeanNum == 2)
        {
            transform.DOLocalMove(new Vector3(-2.25f, -1.45f, 2.5f), 0.1f);
        }

        if(PC.LeanNum == 0)
        {
            transform.DOLocalMove(new Vector3(0.25f, -0.4f, 2.5f), 0.1f);
        }
    }
}
