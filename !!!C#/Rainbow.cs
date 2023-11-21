using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rainbow : MonoBehaviour
{
    float span;
    [SerializeField] public Image image;

    void Update()
    {
        span += Time.deltaTime;
        if (span >= 0.3f)
        {
            image.color = new Color(Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f), Random.Range(0.3f, 1.0f), 1);
            span = 0.0f;
        }

    }
}
