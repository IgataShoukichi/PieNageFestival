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
    Transform[] children; // �q�I�u�W�F�N�g�B������z��

    float fadeSpeed = 0.003f;    //�����x���ς��X�s�[�h
    float alfa;                 //�s�����x���Ǘ�
    public bool isFadeOut = false;  //�t�F�[�h�A�E�g�����̊J�n�A�������Ǘ�����t���O

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
        //�p�C�̃^�O����ɂ܂Ƃ߂ď���������
        if (other.gameObject.CompareTag("Pie1"))
        {
            //�p�C�ɕۑ����ꂽ�f�[�^�ɃA�N�Z�X���邽�߂ɃQ�b�g�R���|�[�l���g
            DecalController decal = other.gameObject.GetComponent<DecalController>();
            PieController pie = decal.defalt.GetComponent<PieController>();

            //�p�C�̓������v���C���[�L�����Ǝ����̐e�I�u�W�F�N�g����v���Ȃ��ꍇ�A���������������s��
            if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
            {
                decal_h.SetActive(true);
                pie.hitwait = true;
                //�m�[�}���p�C
                if (!pie.isSpecial)
                {
                    if (PC.betyaCount <= 3)
                    {
                        PC.betyaCount++;
                    }
                }
                //�X�y�V�����p�C
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
        else if (other.gameObject.CompareTag("Pie2"))   //�M�ɓ���������
        {
            PieController pie = other.gameObject.GetComponent<PieController>();

            if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
            {
                if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
                {
                    decal_h.SetActive(true);

                    pie.hitwait = true;
                    //�m�[�}���p�C
                    if (!pie.isSpecial)
                    {
                        if (PC.betyaCount <= 3)
                        {
                            PC.betyaCount++;
                        }
                    }
                    //�X�y�V�����p�C
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
