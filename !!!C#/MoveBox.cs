using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBox : MonoBehaviour
{
    Transform myTransform;
    Vector3 pos;
    int point;
    float speed;
    void Start()
    {
        speed = 0.13f;
    }

    void FixedUpdate()
    {
        myTransform = this.transform;

        // 座標を取得
        pos = myTransform.position;
        if (point == 0)
        {
            pos.x += speed;    // x座標へ0.01加算
        }
        else if (point == 1)
        {
            pos.z += speed;    // x座標へ0.01加算
        }
        else if (point == 2)
        {
            pos.x -= speed;    // x座標へ0.01加算
        }
        else if (point == 3)
        {
            pos.z -= speed;
        }
        myTransform.position = pos;  // 座標を設定
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Shell")
        {
            point = 1;
        }
        if (other.gameObject.tag == "Pie2")
        {
            point = 2;
        }
        if (other.gameObject.tag == "Pie3")
        {
            point = 3;
        }
        if (other.gameObject.tag == "Pie4")
        {
            point = 0;
        }
    }
}
