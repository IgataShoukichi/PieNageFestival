using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scale_button : MonoBehaviour
{
    [System.NonSerialized] public TimeController TC;
    public float time, changeSpeed;
    public bool enlarge;
    public GameObject circle;
    // Start is called before the first frame update
    void Start()
    {
        enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        changeSpeed = Time.unscaledDeltaTime * 1.0f;

        if (time < 0)
        {
            enlarge = true;
        }
        if (time > 0.5f)
        {
            enlarge = false;
        }

        if (enlarge == true)
        {
            time += Time.unscaledDeltaTime;
            transform.localScale += new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }
        else
        {
            time -= Time.unscaledDeltaTime;
            transform.localScale -= new Vector3(changeSpeed, changeSpeed, changeSpeed);
        }




    }
}
