using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mob_Slide : MonoBehaviour
{
    [System.NonSerialized] public TimeController TC;
    private CanvasGroup canvasGroup;
    void Start()
    {
        this.canvasGroup = this.GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0f;
    }
    void FixedUpdate()
    {
        if (TC.countdown <= 70 && TC.countdown > 65)
        {
            canvasGroup.alpha += 0.05f;
        }
        if (TC.countdown <= 65 && canvasGroup.alpha >= 0)
        {
            canvasGroup.alpha -= 0.05f;
        }
    }
}
