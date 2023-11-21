using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Result_stop : MonoBehaviour
{

    [SerializeField] public GameObject player1;
    [SerializeField] public GameObject player2;
    [SerializeField] public GameObject player3;
    [SerializeField] public GameObject player4;

    private Result_set RSE1;
    private Result_set RSE2;
    private Result_set RSE3;
    private Result_set RSE4;

    public static bool rs1;
    public static bool rs2;
    public static bool rs3;
    public static bool rs4;
    public static bool cm;

    void Start()
    {
        rs1 = false;
        rs2 = false;
        rs3 = false;
        rs4 = false;
        cm = false;

        RSE1 = player1.gameObject.GetComponent<Result_set>();
        RSE1.RST = this;
        RSE2 = player2.gameObject.GetComponent<Result_set>();
        RSE2.RST = this;
        RSE3 = player3.gameObject.GetComponent<Result_set>();
        RSE3.RST = this;
        RSE4 = player4.gameObject.GetComponent<Result_set>();
        RSE4.RST = this;
    }

    void Update()
    {
        if(rs1 && rs2 && rs3 && rs4 && cm)
        {
            RSE1.Rotation = true;
            RSE2.Rotation = true;
            RSE3.Rotation = true;
            RSE4.Rotation = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //1P
        if (other.gameObject.layer == 10 && gameObject.tag == "Set1")
        {
            rs1 = true;
            RSE1.ani = 0;
        }
        //2P
        else if (other.gameObject.layer == 11 && gameObject.tag == "Set2")
        {
            rs2 = true;
            RSE2.ani = 0;
        }
        //3P
        else if (other.gameObject.layer == 12 && gameObject.tag == "Set3")
        {
            rs3 = true;
            RSE3.ani = 0;
        }
        //4P
        else if (other.gameObject.layer == 6 && gameObject.tag == "Set4")
        {
            rs4 = true;
            RSE4.ani = 0;
        }
        //camera
        else if (other.gameObject.tag == "Setcamera")
        {
            cm = true;
        }
    }
}
