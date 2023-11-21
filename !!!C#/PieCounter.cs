using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PieCounter : MonoBehaviour
{
    private void Awake()
    {
        transform.root.GetComponent<PlayerController>().PCounter = this;
    }

    public int num;
    public int PiePiece = 0;
    public int Pie = 0;

    public int sPiece = 0;
    public bool sPie = false;

    private void Start()
    {
        PiePiece = 0;
        Pie = 0;

        sPiece = 0;
        sPie = false;
    }



    private void FixedUpdate()
    {
        var gamepad = Gamepad.all;
    }

    void OnCollisionEnter(Collision other)
    {
        //パイのかけらに当たった時
        if (other.gameObject.tag == "Piece" && Pie < 3)
        {

            PiePiece++;　　　　//かけら+1
            if (PiePiece == 3)//かけらがパイになる処理
            {
                Pie++;
                PiePiece = 0;

            }

            Destroy(other.gameObject);
            PieGenerator.count--;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        //パイのかけらに当たった時
        if (other.gameObject.tag == "Piece" && Pie < 3)
        {
            PiePiece++;　　　　//かけら+1
            if (PiePiece == 3)//かけらがパイになる処理
            {
                Pie++;
                PiePiece = 0;

            }

            Destroy(other.gameObject);
            PieGenerator.count--;
        }
    }


    private void Update()
    {

        if (sPie == false)//パイのかけらに当たった時
        {
            if (sPiece == 3)//かけらがパイになる処理
            {
                sPie = true;
                sPiece = 0;
            }
        }
    }
}
