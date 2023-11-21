using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotate : MonoBehaviour
{
    float y_axis;
    bool rotate_change;

    void Start()
    {
        y_axis = 0;
        rotate_change = false;
    }

    void FixedUpdate()
    {
        if (!rotate_change)
        {
            y_axis++;
            if (y_axis > 30)
            {
                rotate_change = true;
            }
        }
        else
        {
            y_axis--;
            if (y_axis < -30)
            {
                rotate_change = false;
            }
        }
        Transform myTransform = this.transform;
        Vector3 localAngle = myTransform.localEulerAngles;
        localAngle.y = y_axis;
        myTransform.localEulerAngles = localAngle;
    }
}
