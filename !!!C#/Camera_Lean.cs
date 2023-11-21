using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera_Lean : MonoBehaviour
{
    [System.NonSerialized] public PlayerController PC;

    void Update()
    {
        if (PC.LeanNum == 1)
        {
            if (PC.num == 0 || PC.num == 2)
                transform.DOLocalRotate(new Vector3(0f, 0, -10f), 0.1f);

            else if (PC.num == 1 || PC.num == 3)
                transform.DOLocalRotate(new Vector3(0f, 180, -10f), 0.1f);
        }

        if (PC.LeanNum == 2)
        {
            if (PC.num == 0 || PC.num == 2)
                transform.DOLocalRotate(new Vector3(0f, 0, 10f), 0.1f);

            else if (PC.num == 1 || PC.num == 3)
                transform.DOLocalRotate(new Vector3(0f, 180, 10f), 0.1f);

        }

        if (PC.LeanNum == 0)
        {
            if (PC.num == 0 || PC.num == 2)
                transform.DOLocalRotate(new Vector3(0f, 0, 0f), 0.1f);

            else if (PC.num == 1 || PC.num == 3)
                transform.DOLocalRotate(new Vector3(0f, 180, 0f), 0.1f);

        }

    }
}
