using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System;

public class Result_UI : MonoBehaviour
{
    [SerializeField] public GameObject canvas_R;//リザルト用のキャンパス

    [SerializeField] public Result_set[] RSE;//キャラクターの位置設定のスクリプト取得用
    public Result_set hold_r;

    [SerializeField] public Graph_manager[] GM;//棒グラフのスクリプト取得用
    public Graph_manager hold_gm;
    [SerializeField] public GameObject[] graph;//棒グラフ

    [SerializeField] public PlayerController[] PC;//PlayerController取得用

    public int rank_price;//順位

    [System.NonSerialized] public scoreManager SCM;//scoreManager取得用

    [SerializeField] public GameObject[] player;//プレイヤーを配列に入れる

    public GameObject hold_g;//並べ替え時、ゲームオブジェクトの一時保存用

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource bgm1;

    public bool Switch_can;

    //それぞれの汚れ度
    public float dirt_Red;
    public float dirt_Blue;
    public float dirt_Yellow;
    public float dirt_Green;

    //それぞれのパイを当てられた数（トータル）
    public float redT;
    public float blueT;
    public float yellowT;
    public float greenT;


    public float time;

    [SerializeField] public GameObject titleBack;//「タイトルに戻る」UI

    [SerializeField] public GameObject light;

    [SerializeField] public GameObject[] spotLight;

    //キレイ度表示するテキスト
    [SerializeField] public Text[] score_t;
    public Text hold_t;

    public float timeOutRed, timeOutBlue;
    public float timeRed, timeBlue;

    float span;
    //キレイ度入れる変数
    public float[] score = new float[4];
    public GameObject[] Score_p;

    //キレイ度のグラフ
    public GameObject[] graph_1;

    public AudioClip drum;
    public AudioClip close;
    public AudioClip rotation;
    AudioSource aud;

    //順位の配列
    int[] rank_array = new int[4];

    bool sound, sound1, sound2, sound3, sound4, sound5, sound6;

    bool flag1, flag2, flag3, flag4, flag5, flag6;

    float[] dirt;

    [SerializeField] public GameObject[] rank_1;
    [SerializeField] public GameObject[] rank_2;
    [SerializeField] public GameObject[] rank_3;
    [SerializeField] public GameObject[] rank_4;
    [SerializeField] public GameObject[] rank_5;

    GameObject[] rank_g;

    int gamep;//コントローラ接続判定用

    void Start()
    {
        this.aud = GetComponent<AudioSource>();

        for (int i = 0; i < player.Length; i++)
        {
            score[i] = 0;
            RSE[i] = player[i].gameObject.GetComponent<Result_set>();
            RSE[i].RU = this;
            GM[i] = graph[i].gameObject.GetComponent<Graph_manager>();
            GM[i].RU = this;
        }

        dirt_Red = 0.0f;
        dirt_Blue = 0.0f;
        dirt_Yellow = 0.0f;
        dirt_Green = 0.0f;

        canvas_R.SetActive(false);
        Switch_can = false;

        score_t[0].text = dirt_Red.ToString("F1");
        score_t[1].text = dirt_Blue.ToString("F1");
        score_t[2].text = dirt_Yellow.ToString("F1");
        score_t[3].text = dirt_Green.ToString("F1");

        sound = false;
        sound1 = true;
        sound2 = true;
        sound3 = false;
        sound4 = false;
        sound5 = false;
        sound6 = false;

        for (int i = 0; i < Score_p.Length; i++)
        {
            this.Score_p[i].SetActive(false);
            this.spotLight[i].SetActive(false);
            this.graph_1[i].SetActive(false);
        }
        flag1 = false;
        flag2 = false;
        flag3 = false;
        for (int i = 0; i < rank_1.Length; i++)
        {
            rank_1[i].SetActive(false);
            rank_2[i].SetActive(false);
            rank_3[i].SetActive(false);
            rank_4[i].SetActive(false);
            rank_5[i].SetActive(false);
        }
        rank_price = 4;
        gamep = 0;
    }

    void Update()
    {

        var gamepad = Gamepad.all;

        GameObject[] red = GameObject.FindGameObjectsWithTag("Red");
        GameObject[] blue = GameObject.FindGameObjectsWithTag("Blue");
        GameObject[] yellow = GameObject.FindGameObjectsWithTag("Yellow");
        GameObject[] green = GameObject.FindGameObjectsWithTag("Green");

        if (Switch_can)
        {
            //結果の計算
            //取得したタグを「パイを当てられた数」として変数に入れる
            redT = SCM.red_t.Length;
            blueT = SCM.blue_t.Length;
            yellowT = SCM.yellow_t.Length;
            greenT = SCM.green_t.Length;

            //それぞれの割合をだす
            dirt_Red = 100 - red.Length / redT * 100;
            dirt_Blue = 100 - blue.Length / blueT * 100;
            dirt_Yellow = 100 - yellow.Length / yellowT * 100;
            dirt_Green = 100 - green.Length / greenT * 100;

            if (!flag6)
            {
                //キレイ度の配列
                dirt = new float[] { dirt_Red, dirt_Blue, dirt_Yellow, dirt_Green };
                flag6 = true;
            }

            canvas_R.SetActive(true);

            titleBack.SetActive(false);
            light.SetActive(true);

            //回りだす
            if (time >= 1.5 && time < 12)
            {
                if (!sound3)
                {
                    this.aud.PlayOneShot(this.rotation);
                    sound3 = true;
                }
                if (!sound4 && time >= 4.7)
                {
                    this.aud.PlayOneShot(this.rotation);
                    sound4 = true;
                }

                for (int i = 0; i < spotLight.Length; i++)
                {
                    spotLight[i].SetActive(true);
                }
                light.SetActive(false);

                timeRed += Time.deltaTime;
                if (timeRed >= timeOutRed)
                {
                    timeRed = 0.0f;
                }

                timeBlue += Time.deltaTime;
                if (timeBlue >= timeOutBlue)
                {
                    timeBlue = 0.0f;
                }
            }

            //結果待ち
            if (time >= 7.5 && time < 12)
            {
                for (int i = 0; i < spotLight.Length; i++)
                {
                    spotLight[i].SetActive(false);
                    graph_1[i].SetActive(true);
                    RSE[i].wait = true;
                }
                if (!sound)
                {
                    this.aud.PlayOneShot(this.drum);
                    sound = true;
                }
            }

            //得点を出す
            if (time >= 12)
            {
                light.SetActive(false);
                int rank = 1;
                int count = 0;
                float hold;

                //降順に並び替える
                for (int j = 0; j < dirt.Length - 1 && !flag1; j++)
                {
                    for (int i = 0; i < dirt.Length - 1; i++)
                    {
                        if (dirt[i] < dirt[i + 1])
                        {
                            hold = dirt[i];
                            dirt[i] = dirt[i + 1];
                            dirt[i + 1] = hold;

                            hold_g = player[i];
                            player[i] = player[i + 1];
                            player[i + 1] = hold_g;

                            hold_g = spotLight[i];
                            spotLight[i] = spotLight[i + 1];
                            spotLight[i + 1] = hold_g;

                            hold_t = score_t[i];
                            score_t[i] = score_t[i + 1];
                            score_t[i + 1] = hold_t;

                            hold_r = RSE[i];
                            RSE[i] = RSE[i + 1];
                            RSE[i + 1] = hold_r;

                            hold_g = rank_1[i];
                            rank_1[i] = rank_1[i + 1];
                            rank_1[i + 1] = hold_g;

                            hold_g = rank_2[i];
                            rank_2[i] = rank_2[i + 1];
                            rank_2[i + 1] = hold_g;

                            hold_g = rank_3[i];
                            rank_3[i] = rank_3[i + 1];
                            rank_3[i + 1] = hold_g;

                            hold_g = rank_4[i];
                            rank_4[i] = rank_4[i + 1];
                            rank_4[i + 1] = hold_g;

                            hold_g = rank_5[i];
                            rank_5[i] = rank_5[i + 1];
                            rank_5[i + 1] = hold_g;

                            hold_gm = GM[i];
                            GM[i] = GM[i + 1];
                            GM[i + 1] = hold_gm;


                        }
                    }
                }

                //降順から順位を決めていく
                for (int i = 0; i < dirt.Length - 1 && !flag1; i++)
                {
                    if (dirt[i] == dirt[i + 1])     //同率のとき
                    {
                        rank_array[i] = rank;   //順位の配列のi番目にrankを代入
                        rank_array[i + 1] = rank;   //jにもiと同じ値を代入
                        count++;
                    }
                    else                       //同率ではないとき
                    {
                        rank_array[i] = rank;   //rankを代入
                        rank += 1 + count;          //同率の際カウントしていた分の数値+1
                        count = 0;                  //countをリセット
                        rank_array[i + 1] = rank;
                    }
                }

                for (int i = 0; i < dirt.Length && !flag1; i++)
                {
                    GM[i].gm_rank = rank_array[i];
                    RSE[i].rse_rank = rank_array[i];
                }

                //４位から順番に順位を発表する
                if (!flag2)
                {
                    //４位の表示
                    for (int i = 0; i < rank_array.Length; i++)
                    {
                        if (rank_array[i] == 4)
                        {
                            //キレイ度、順位を表示
                            score_t[i].text = dirt[i].ToString("F1") + "％";
                            rank_4[i].gameObject.SetActive(true);
                            rank_5[i].gameObject.SetActive(true);

                            rank_price = 4;
                            RSE[i].wait = false;
                            GM[i].current = dirt[i] / 2;
                            sound1 = false;
                        }
                    }
                    if (!sound1)
                    {
                        this.aud.PlayOneShot(this.close);
                        sound1 = true;
                    }

                    flag1 = true;
                    flag2 = true;
                }
                //３位
                if (time >= 13.5f && !flag3)
                {

                    for (int i = 0; i < rank_array.Length; i++)
                    {
                        if (rank_array[i] == 3)
                        {
                            score_t[i].text = dirt[i].ToString("F1") + "％";
                            rank_3[i].gameObject.SetActive(true);
                            rank_5[i].gameObject.SetActive(true);
                            RSE[i].wait = false;
                            rank_price = 3;
                            GM[i].current = dirt[i] / 2;
                            sound2 = false;
                        }
                    }

                    if (!sound2)
                    {
                        this.aud.PlayOneShot(this.close);
                        sound2 = true;
                    }

                    flag3 = true;

                }
                //２位
                if (time >= 16 && !flag4)
                {
                    if (!sound5)
                    {
                        this.aud.PlayOneShot(this.close);
                        sound5 = true;
                    }

                    for (int i = 0; i < rank_array.Length; i++)
                    {
                        if (rank_array[i] == 2)
                        {
                            score_t[i].text = dirt[i].ToString("F1") + "％";
                            rank_2[i].gameObject.SetActive(true);
                            rank_5[i].gameObject.SetActive(true);
                            RSE[i].wait = false;
                            rank_price = 2;
                            GM[i].current = dirt[i] / 2;
                        }

                    }
                    flag4 = true;
                }

                //１位
                if (time >= 16.03f && !flag5)
                {
                    if (!sound6)
                    {
                        this.aud.PlayOneShot(this.close);
                        sound6 = true;
                    }

                    for (int i = 0; i < rank_array.Length; i++)
                    {
                        if (rank_array[i] == 1)
                        {
                            score_t[i].text = dirt[i].ToString("F1") + "％";
                            rank_1[i].gameObject.SetActive(true);
                            rank_5[i].gameObject.SetActive(true);
                            spotLight[i].gameObject.SetActive(true);
                            RSE[i].wait = false;
                            rank_price = 1;
                            GM[i].current = dirt[i] / 2;
                        }

                    }
                    bgm.Play();

                    flag5 = true;
                }


                if (time >= 16.5)//すべて終わったタイミングで、タイトルに戻れるようにする
                {
                    light.SetActive(false);
                    titleBack.SetActive(true);
                    for (int i = 0; i < gamepad.Count; i++)//コントローラが繋がっている場合
                    {
                        if (gamepad[PC[i].num].buttonEast.wasPressedThisFrame)
                        {
                            FadeManager.Instance.LoadScene("Title", 0.5f);
                        }
                    }

                    if (gamepad.Count <= 0)//コントローラが繋がっていない場合
                    {
                        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Return))
                        {
                            FadeManager.Instance.LoadScene("Title", 0.5f);
                        }
                    }
                }
            }
        }
    }
}

