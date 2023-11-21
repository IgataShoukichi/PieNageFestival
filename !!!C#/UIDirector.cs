using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIDirector : MonoBehaviour
{
    struct UI_PIE
    {
        public GameObject[] piece1;
        public GameObject[] piece2;
        public GameObject[] pie1;
        public GameObject[] pie2;
        public GameObject[] pie3;
    }
    [SerializeField] UI_PIE[] ui_pie = new UI_PIE[4];

    [SerializeField] GameObject[] piece1;
    [SerializeField] GameObject[] piece2;
    [SerializeField] GameObject[] pie1;
    [SerializeField] GameObject[] pie2;
    [SerializeField] GameObject[] pie3;

    [SerializeField] GameObject[] betya;
    [SerializeField] GameObject[] betya1;
    [SerializeField] GameObject[] betya2;

    [SerializeField] GameObject[] sPiece;
    [SerializeField] GameObject[] sPiece1;
    [SerializeField] GameObject[] BigPie;

    [SerializeField] GameObject[] human;
    [SerializeField] GameObject[] arrow2;
    [SerializeField] GameObject[] arrow3;

    [SerializeField] GameObject[] debuff1;
    [SerializeField] GameObject[] debuff2;
    [SerializeField] GameObject[] debuff3;

    [SerializeField] public GameObject[] glitter;

    [SerializeField] GameObject[] AllPlayers;

    public PlayerController[] PCs;
    public PieCounter[] PCounter;
    public ShotHead[] ShotHeads;
    public ShotBody[] ShotBodies;
    public PieThrow[] PieThrow;

    [System.NonSerialized] public TimeController TC;

    // Start is called before the first frame update
    void Start()
    {
        //すべてのプレイヤータグのオブジェクトを取得して保存(1Pから順番に取得できるわけではない)
        //4人分のスクリプトを保存する配列の箱を作る
        PCounter = new PieCounter[AllPlayers.Length];
        ShotBodies = new ShotBody[AllPlayers.Length];
        ShotHeads = new ShotHead[AllPlayers.Length];
        PieThrow = new PieThrow[AllPlayers.Length];


        for (int i = 0; i < AllPlayers.Length; i++)
        {
            //さきほど取得したプレイヤーからPlayerController2を取得
            PCs[i] = AllPlayers[i].GetComponent<PlayerController>();
            //取得したPlayerController2の中にあるプレイヤーナンバーの箱にShotBodyとShotHeadを保存する(こうしないと1Pから順番に保存できない)
            PCounter[PCs[i].num] = PCs[i].PCounter;
            //ShotBodies[PCs[i].num] = PCs[i].SB;
            ShotHeads[PCs[i].num] = PCs[i].SH[i];
            PieThrow[PCs[i].num] = PCs[i].PT;

            //4人分のUIをfor分でfalseにする
            this.piece1[i].SetActive(false);
            this.piece2[i].SetActive(false);
            this.pie1[i].SetActive(false);
            this.pie2[i].SetActive(false);
            this.pie3[i].SetActive(false);

            this.betya[i].SetActive(false);
            this.betya1[i].SetActive(false);
            this.betya2[i].SetActive(false);

            this.sPiece[i].SetActive(false);
            this.sPiece1[i].SetActive(false);
            this.BigPie[i].SetActive(false);

            this.human[i].SetActive(false);
            this.arrow2[i].SetActive(false);
            this.arrow3[i].SetActive(false);

            this.debuff1[i].SetActive(false);
            this.debuff2[i].SetActive(false);
            this.debuff3[i].SetActive(false);

            this.glitter[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {




        for (int i = 0; i < AllPlayers.Length; i++)
        {
            if (PCounter[i].PiePiece == 0)
            {
                this.piece1[i].SetActive(false);
                this.piece2[i].SetActive(false);
            }

            if (PCounter[i].PiePiece == 1)
            {
                this.piece1[i].SetActive(true);
                this.piece2[i].SetActive(false);

            }

            if (PCounter[i].PiePiece == 2)
            {
                this.piece1[i].SetActive(true);
                this.piece2[i].SetActive(true);

            }


            if (PCounter[i].Pie == 0)
            {
                this.pie1[i].SetActive(false);
                this.pie2[i].SetActive(false);
                this.pie3[i].SetActive(false);

            }

            if (PCounter[i].Pie == 1)
            {
                this.pie1[i].SetActive(true);
                this.pie2[i].SetActive(false);
                this.pie3[i].SetActive(false);

            }

            if (PCounter[i].Pie == 2)
            {
                this.pie1[i].SetActive(true);
                this.pie2[i].SetActive(true);
                this.pie3[i].SetActive(false);

            }
            if (PCounter[i].Pie == 3)
            {
                this.pie1[i].SetActive(true);
                this.pie2[i].SetActive(true);
                this.pie3[i].SetActive(true);
            }

            if (ShotHeads[i].PC.betyaCount == 0)
            {
                this.betya[i].SetActive(false);
                this.betya1[i].SetActive(false);
                this.betya2[i].SetActive(false);
            }

            if (ShotHeads[i].PC.betyaCount == 1)
            {
                this.betya[i].SetActive(true);
                this.betya1[i].SetActive(false);
                this.betya2[i].SetActive(false);
            }

            if (ShotHeads[i].PC.betyaCount == 2)
            {
                this.betya[i].SetActive(true);
                this.betya1[i].SetActive(true);
                this.betya2[i].SetActive(false);
            }

            if (ShotHeads[i].PC.betyaCount == 3)
            {
                this.betya[i].SetActive(true);
                this.betya1[i].SetActive(true);
                this.betya2[i].SetActive(true);
            }

            if (PCounter[i].sPiece == 0)
            {
                this.sPiece[i].SetActive(false);
                this.sPiece1[i].SetActive(false);

            }

            if (PCounter[i].sPiece == 1)
            {
                this.sPiece[i].SetActive(true);
                this.sPiece1[i].SetActive(false);

            }

            if (PCounter[i].sPiece == 2)
            {
                this.sPiece[i].SetActive(true);
                this.sPiece1[i].SetActive(true);

            }

            if (PCounter[i].sPie == false)
            {
                this.BigPie[i].SetActive(false);
            }

            if (PCounter[i].sPie == true)
            {
                this.BigPie[i].SetActive(true);

            }

            if (PCs[i].speed == 7 || TC.countdown <= 0)
            {
                this.human[i].SetActive(false);
                this.arrow2[i].SetActive(false);
                this.arrow3[i].SetActive(false);

                this.debuff1[i].SetActive(false);
                this.debuff2[i].SetActive(false);
                this.debuff3[i].SetActive(false);
            }

            if (PCs[i].speed == 5)
            {
                this.human[i].SetActive(true);

                this.debuff1[i].SetActive(true);

            }

            if (PCs[i].speed == 3)
            {
                this.arrow2[i].SetActive(true);

                this.debuff2[i].SetActive(true);
            }

            if (PCs[i].speed <= 1 && !PCs[i].wipeON)
            {
                this.human[i].SetActive(true);
                this.arrow2[i].SetActive(true);
                this.arrow3[i].SetActive(true);

                this.debuff1[i].SetActive(true);
                this.debuff2[i].SetActive(true);
                this.debuff3[i].SetActive(true);
            }

            if (TC.countdown <= 100 && TC.countdown > 20)
            {
                GameObject[] red = GameObject.FindGameObjectsWithTag("Red");
                GameObject[] blue = GameObject.FindGameObjectsWithTag("Blue");
                GameObject[] yellow = GameObject.FindGameObjectsWithTag("Yellow");
                GameObject[] green = GameObject.FindGameObjectsWithTag("Green");

                if (red.Length <= blue.Length && red.Length <= yellow.Length && red.Length <= green.Length)
                {
                    if (PCs[i].num == 0)
                    {
                        glitter[i].SetActive(true);
                    }
                    else
                    {
                        glitter[i].SetActive(false);
                    }
                }

                if (blue.Length <= green.Length && blue.Length <= yellow.Length && blue.Length <= red.Length)
                {
                    if (PCs[i].num == 1)
                    {
                        glitter[i].SetActive(true);
                    }
                    else
                    {
                        glitter[i].SetActive(false);
                    }

                }

                if (yellow.Length <= green.Length && yellow.Length <= blue.Length && yellow.Length <= red.Length)
                {
                    if (PCs[i].num == 2)
                    {
                        glitter[i].SetActive(true);
                    }
                    else
                    {
                        glitter[i].SetActive(false);
                    }

                }

                if (green.Length <= yellow.Length && green.Length <= blue.Length && green.Length <= red.Length)
                {
                    if (PCs[i].num == 3)
                    {
                        glitter[i].SetActive(true);
                    }
                    else
                    {
                        glitter[i].SetActive(false);
                    }

                }
            }

            if (TC.countdown <= 20 && TC.countdown > 0)
            {
                glitter[i].SetActive(false);
            }

        }
    }
}
