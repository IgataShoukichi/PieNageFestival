using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScale : MonoBehaviour
{
    [System.NonSerialized] public TimeController TC;
    public float time, changeSpeed;
    public bool enlarge;
    public Text text;

    void Start()
    {
        enabled = true;
    }

    void Update()
    {
        if(TC.countdown <= 9)
        {
            text.color = new Color(1, 0, 0, 1);
            changeSpeed = Time.deltaTime * 1.0f;

            if(time < 0)
            {
                enlarge = true;
            }
            if (time > 0.3f)
            {
                enlarge = false;
            }

            if(enlarge == true)
            {
                time += Time.deltaTime;
                transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
            }
            else
            {
                time -= Time.deltaTime;
                transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
            }



        }

    }
}
