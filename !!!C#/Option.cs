using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire10_1"))
        {
            FadeManager.Instance.LoadScene("Title", 0.5f);
        }

        if (Input.GetButtonDown("Fire10_2"))
        {
            FadeManager.Instance.LoadScene("Title", 0.5f);
        }

        if (Input.GetButtonDown("Fire10_3"))
        {
            FadeManager.Instance.LoadScene("Title", 0.5f);
        }

        if (Input.GetButtonDown("Fire10_4"))
        {
            FadeManager.Instance.LoadScene("Title", 0.5f);
        }

    }
}
