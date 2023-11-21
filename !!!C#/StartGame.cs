using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class StartGame : MonoBehaviour
{
    public GameObject[] Con = new GameObject[4];
    public GameObject[] Che = new GameObject[4];
    [SerializeField] public GameObject Ready;
    [SerializeField] public GameObject Go;
    [SerializeField] public Image Go_t;
    bool[] stanby = new bool[4];
    bool[] wait = new bool[4];

    [System.NonSerialized] public PlayerController[] PC = new PlayerController[4];


    float fadeSpeed = 0.01f;    //透明度が変わるスピード
    float alfa;                 //不透明度を管理
    public bool isFadeOut = false;  //フェードアウト処理の開始、完了を管理するフラグ

    static int flag;
    float time;
    bool[] sound = new bool[4];
    bool sound1;

    public AudioClip betya;
    public AudioClip start;
    [SerializeField] AudioSource aud;

    [SerializeField] private AudioSource bgm;

    void Start()
    {
        Time.timeScale = 0;

        for (int i = 0; i < stanby.Length; i++)
        {
            stanby[i] = false;
            Con[i].SetActive(true);
            Che[i].SetActive(false);
            sound[i] = false;
        }
        flag = 0;
        Ready.SetActive(true);
        Go.SetActive(false);
        alfa = Go_t.color.a;
        isFadeOut = true;
        sound1 = false;
    }

    void Update()
    {
        var gamepad = Gamepad.all;
        //コントローラが繋がっている場合
        for (int i = 0; i < PC.Length && PC[i].num < gamepad.Count; i++)
        {
            if (gamepad[i].buttonEast.wasPressedThisFrame)
            {
                stanby[i] = true;
            }

            if (stanby[i])
            {
                Con[i].SetActive(false);
                Che[i].SetActive(true);
                if (!wait[i])
                {
                    if (!sound[i])
                    {
                        this.aud.PlayOneShot(this.betya, 0.5f);
                        sound[i] = true;
                    }
                    flag += 1;
                    wait[i] = true;
                }
                stanby[i] = false;
            }

        }

        //コントローラが繋がっていない場合は、エンターキーを押してスタート
        if (Input.GetKeyDown(KeyCode.Return))
        {
            flag = 4;
            for (int i = 0; i < stanby.Length; i++)
            {
                stanby[i] = true;
                Con[i].SetActive(false);
                Che[i].SetActive(true);
                sound[i] = true;
            }
        }


        if (flag == 4)
        {
            time += Time.unscaledDeltaTime;
            if (time >= 3)
            {
                for (int i = 0; i < Che.Length; i++)
                {
                    if (!sound1)
                    {
                        this.aud.PlayOneShot(this.start, 0.5f);
                        sound1 = true;
                        bgm.Play();
                    }
                    Ready.SetActive(false);
                    Che[i].SetActive(false);
                    Go.SetActive(true);
                }
                Time.timeScale = 1.0f;
                if (isFadeOut)
                {
                    StartFadeOut();
                }
            }
        }

        void StartFadeOut()
        {
            alfa -= fadeSpeed;         // b)不透明度を徐々にあげる
            SetAlpha();               // c)変更した透明度をパネルに反映する
            if (alfa <= 0)
            {             // d)完全に不透明になったら処理を抜ける
                isFadeOut = false;
                this.gameObject.SetActive(false);
            }
        }
        void SetAlpha()
        {
            Go_t.color = new Color(1, 0, 0, alfa);
        }
    }
}
