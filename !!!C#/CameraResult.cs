using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraResult : MonoBehaviour
{
    [System.NonSerialized] public TimeController TC;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(TC.countdown < -2.5f)
        {
            this.transform.DOMove(new Vector3(1.5f, 5f, -25f), 3.5f);
            transform.DOLocalRotate(new Vector3(20, 0, 0f), 3.5f);
        }
    }
}
