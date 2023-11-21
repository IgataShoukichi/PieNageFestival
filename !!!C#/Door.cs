using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [System.NonSerialized] public TimeController TC;

    float y;

    public bool door;

    public float open;
    public float close;

    // Start is called before the first frame update
    void Start()
    {
        y = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Transform myTransform = transform;
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.y = y;
        myTransform.localEulerAngles = localAngle;


        if (door)
        {
            if (TC.countdown <= 75 && TC.countdown >= 55 && y <= open)
            {
                y++;
            }
            else if (TC.countdown <= 55 && y > close)
            {
                y--;
            }
        }

        if (!door)
        {
            if (TC.countdown <= 75 && TC.countdown >= 55 && y >= open)
            {
                y--;
            }
            else if (TC.countdown <= 55 && TC.countdown > 0 && y < close)
            {
                y++;
            }

            if(TC.countdown <= 0 && TC.countdown > -20 && y >= open)
            {
                y--;
            }
            else if(TC.countdown <= -20 && y < close)
            {
                y++;
            }
        }
    }
}
