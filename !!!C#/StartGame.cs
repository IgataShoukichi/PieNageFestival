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


    float fadeSpeed = 0.01f;    //�����x���ς��X�s�[�h
    float alfa;                 //�s�����x���Ǘ�
    public bool isFadeOut = false;  //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O

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
        //�R���g���[�����q�����Ă���ꍇ
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

        //�R���g���[�����q�����Ă��Ȃ��ꍇ�́A�G���^�[�L�[�������ăX�^�[�g
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
            alfa -= fadeSpeed;         // b)�s�����x�����X�ɂ�����
            SetAlpha();               // c)�ύX���������x���p�l���ɔ��f����
            if (alfa <= 0)
            {             // d)���S�ɕs�����ɂȂ����珈���𔲂���
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
