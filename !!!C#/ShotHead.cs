using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class ShotHead : MonoBehaviour
{
    public int Pointcount = 0;
    [System.NonSerialized] public ShotBody SB;
    public PlayerController PC;

    [SerializeField] public GameObject decal_h;
    Transform[] children; // 子オブジェクト達を入れる配列

    float fadeSpeed = 0.003f;    //透明度が変わるスピード
    float alfa;                 //不透明度を管理
    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ

    DecalProjector[] decal_c;
    DecalProjector decal;
    Vector3[] origin_size_c;
    Vector3 origin_size;

    [SerializeField] public GameObject[] upper;

    [System.NonSerialized] public scoreManager SCM;

    private void Awake()
    {
        PC = transform.root.gameObject.GetComponent<PlayerController>();
        PC.SH.Add(this);
    }
    private void Start()
    {
        Pointcount = 0;
        decal_h.SetActive(false);
        isFadeOut = true;
        for (int i = 0; i < upper.Length; i++)
        {
            upper[i].gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        //パイのタグを一つにまとめて処理をする
        if (other.gameObject.CompareTag("Pie1"))
        {
            //パイに保存されたデータにアクセスするためにゲットコンポーネント
            DecalController decal = other.gameObject.GetComponent<DecalController>();
            PieController pie = decal.defalt.GetComponent<PieController>();

            //パイの投げたプレイヤーキャラと自分の親オブジェクトが一致しない場合、当たった処理を行う
            if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
            {
                decal_h.SetActive(true);
                pie.hitwait = true;
                //ノーマルパイ
                if (!pie.isSpecial)
                {
                    if (PC.betyaCount <= 3)
                    {
                        PC.betyaCount++;
                    }
                }
                //スペシャルパイ
                else
                {
                    PC.betyaCount = 3;
                    PC.speed = 0;
                    for (int i = 0; i < upper.Length; i++)
                    {
                        upper[i].gameObject.SetActive(true);
                    }
                }
            }
        }
        else if (other.gameObject.CompareTag("Pie2"))   //皿に当たった時
        {
            PieController pie = other.gameObject.GetComponent<PieController>();

            if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
            {
                if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
                {
                    decal_h.SetActive(true);

                    pie.hitwait = true;
                    //ノーマルパイ
                    if (!pie.isSpecial)
                    {
                        if (PC.betyaCount <= 3)
                        {
                            PC.betyaCount++;
                        }
                    }
                    //スペシャルパイ
                    else
                    {
                        PC.betyaCount = 3;
                        PC.speed = 0;
                        for (int i = 0; i < upper.Length; i++)
                        {
                            upper[i].gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    private void Update()
    {
        if (PC.Wiper >= 50)
        {
            decal_h.SetActive(false);
        }
    }
}
