using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TimeController : MonoBehaviour
{

    GameObject timer;

    //BGM�ASE
    public AudioClip finish;
    public AudioClip last;
    public AudioClip warning;
    AudioSource aud;
    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource lastb;

    [SerializeField] public GameObject[] camera;//�J������z��Ŏ擾

    [SerializeField] public GameObject[] player;//�v���C���[��z��Ŏ擾

    [SerializeField] public GameObject[] Set;//�����I������ɃZ�b�g�������

    [SerializeField] public GameObject pgenerator;

    [SerializeField] public Image timefill;

    [SerializeField] public GameObject cameraR;

    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject sc;

    [SerializeField] public GameObject[] spotLight;//�X�|�b�g���C�g��z��Ŏ擾

    [SerializeField] public GameObject[] mob;
    [SerializeField] public MobController[] MC;
    private CameraResult CR;

    private PieThrow PT;

    private PlayerController PC;

    //�J�E���g�_�E��
    public float countdown = 180.0f;

    //���Ԃ�\������Text�^�̕ϐ�
    public Text timeText;

    private TimerScale SC;

    bool sound, sound1;

    public GameObject pd;
    private UIDirector PD;

    public GameObject[] door;
    public Door[] DR;

    [SerializeField] PieDirector pieD;

    [SerializeField] public GameObject ms;
    private Mob_Slide MS;

    private void Start()
    {
        this.aud = GetComponent<AudioSource>();

        CR = cameraR.gameObject.GetComponent<CameraResult>();
        CR.TC = this;

        for (int i = 0; i < player.Length; i++)
        {
            PT = player[i].gameObject.GetComponent<PieThrow>();
            PT.TC = this;
            PC = player[i].gameObject.GetComponent<PlayerController>();
            PC.TC = this;
            spotLight[i].SetActive(false);
            camera[i].SetActive(true);
            player[i].gameObject.GetComponent<Result_set>().enabled = false;
        }
        for (int i = 0; i < Set.Length; i++)
        {
            Set[i].SetActive(false);
        }

        PD = pd.gameObject.GetComponent<UIDirector>();
        PD.TC = this;

        MS = ms.gameObject.GetComponent<Mob_Slide>();
        MS.TC = this;

        for (int i = 0; i < door.Length; i++)
        {
            DR[i] = door[i].gameObject.GetComponent<Door>();
            DR[i].TC = this;
        }

        for (int i = 0; i < mob.Length; i++)
        {
            MC[i] = mob[i].gameObject.GetComponent<MobController>();
            MC[i].TC = this;
        }

        SC = sc.gameObject.GetComponent<TimerScale>();
        SC.TC = this;

        this.timer = GameObject.Find("Timer");

        cameraR.SetActive(false);
        canvas.SetActive(true);

        pgenerator.SetActive(true);
        sound = false;

    }


    void Update()
    {
        //���Ԃ��J�E���g�_�E������
        countdown -= Time.deltaTime;

        //���Ԃ�\������
        timeText.text = countdown.ToString("f0");


        if (countdown <= 29 && countdown > 28.5)//�ʏ�BGM���~�߂�
        {
            bgm.Stop();
        }

        if (countdown <= 28.5 && countdown > 28)//���X�g�X�p�[�gBGM�Đ�
        {
            lastb.Play();
        }

        if (countdown <= 70 && !sound1)//���u����SE
        {
            this.aud.PlayOneShot(this.warning, 7.0f);
            sound1 = true;
        }

        //countdown��0�ȉ��ɂȂ����Ƃ�
        if (countdown <= 0)
        {
            if (!sound)
            {
                this.aud.PlayOneShot(this.finish);
                sound = true;
                //�̂ɂ��Ă��邷�ׂẴp�C������
                for (int i = 0; i < pieD.pieCons1.Length; i++)
                {
                    if (pieD.pieCons1[i].gameObject.activeSelf)
                    {
                        pieD.pieCons1[i].gameObject.SetActive(false);
                    }
                }
                for (int i = 0; i < pieD.bigpieCons.Length; i++)
                {
                    if (pieD.bigpieCons[i].gameObject.activeSelf)
                    {
                        pieD.bigpieCons[i].gameObject.SetActive(false);
                    }
                }


                lastb.Stop();

                for (int i = 0; i < player.Length; i++)
                {
                    player[i].gameObject.GetComponent<PlayerController>().enabled = false;
                    player[i].gameObject.GetComponent<PieThrow>().enabled = false;
                    player[i].gameObject.GetComponent<Outline>().enabled = false;
                    player[i].gameObject.GetComponent<Result_set>().enabled = true;
                    camera[i].SetActive(false);
                }
                for (int i = 0; i < Set.Length; i++)
                {
                    Set[i].SetActive(true);

                }
                cameraR.SetActive(true);

                canvas.SetActive(false);

                pgenerator.SetActive(false);

                obstacleDestroy();

            }

        }
    }

    void obstacleDestroy()
    {
        GameObject[] piepiece = GameObject.FindGameObjectsWithTag("Piece");
        foreach (GameObject obs in piepiece)
        {
            obs.gameObject.SetActive(false);
        }
    }
    private void FixedUpdate()
    {
        //UI�ɓ���
        timefill.fillAmount -= Time.deltaTime * 0.00556f;
    }
}
