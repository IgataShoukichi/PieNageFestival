using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBody : MonoBehaviour
{
    public int Pointcount = 0;�@//�|�C���g�J�E���g
    private PlayerController PC;
    [SerializeField] public GameObject BodyDecal;

    private float time = 0;
    [System.NonSerialized] public scoreManager SCM;

    private void Awake()
    {
        PC = transform.root.gameObject.GetComponent<PlayerController>();
        PC.SB.Add(this);
    }

    private void Start()
    {
        time = 0;
        Pointcount = 0;
        BodyDecal.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        //�p�C�̃^�O����ɂ܂Ƃ߂ď���������
        if (other.gameObject.layer == 7)
        {

            Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);

            if (other.gameObject.CompareTag("Pie1"))    //���ɓ���������
            {
                //�p�C�ɕۑ����ꂽ�f�[�^�ɃA�N�Z�X���邽�߂ɃQ�b�g�R���|�[�l���g
                DecalController decal = other.gameObject.GetComponent<DecalController>();
                PieController pie = decal.defalt.GetComponent<PieController>();

                if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
                {
                    BodyDecal.SetActive(true);
                    pie.hitwait = true;
                    if (PC.speed > 1)
                    {
                        if (PC.jumpNow)
                        {
                            PC.speed_hold += 2;
                        }
                        else
                        {
                            PC.speed -= 2f;    //�ݑ�       
                            if (PC.speed < 0)
                            {
                                PC.speed = 0.5f;
                            }
                        }
                    }
                }
            }
            else if (other.gameObject.CompareTag("Pie2"))   //�M�ɓ���������
            {
                PieController pie = other.gameObject.GetComponent<PieController>();

                if (pie.throwPlayer != transform.root.gameObject && pie.hitwait == false)
                {
                    BodyDecal.SetActive(true);
                    pie.hitwait = true;
                    if (PC.speed > 1)
                    {
                        if (PC.jumpNow)
                        {
                            PC.speed_hold += 2;
                        }
                        else
                        {
                            PC.speed -= 2f;    //�ݑ�       
                            if (PC.speed < 0)
                            {
                                PC.speed = 0.5f;
                            }
                        }
                    }
                }
            }
        }
    }
}
