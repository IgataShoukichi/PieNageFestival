using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transparent : MonoBehaviour
{
    MeshRenderer mesh;
    bool flag;
    float hitcount;
    private Rigidbody rb;
    float fadeSpeed = 0.05f;        // �����x���ς��X�s�[�h
    float fadeSpeed1 = 0.02f;        // �����x���ς��X�s�[�h
    float red, green, blue, alfa;   // Material�̐F
    private GameObject myself;
    public int num;
    float time, time1;

    [SerializeField] public PieController PCT;

    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        myself = GetComponent<GameObject>();
        flag = false;
        rb = GetComponent<Rigidbody>();
        red = mesh.material.color.r;
        green = mesh.material.color.g;
        blue = mesh.material.color.b;
        alfa = mesh.material.color.a;
        alfa = 1;
        time = 0;
        time1 = 0;
    }


    void Update()
    {
        if (PCT.trFlag)
        {
            if (alfa > 0 && PCT.trFlag2)
            {
                time += Time.deltaTime;
                if (time >= 1 && time < 2.2 && this.num == 0)
                {
                    alfa -= fadeSpeed;         // �s�����x��������
                    SetAlpha();               // �ύX���������x�𔽉f����
                }

                if (time >= 1.1 && time < 2.3 && this.num == 1)
                {
                    alfa -= fadeSpeed1;         // �s�����x��������
                    SetAlpha();               // �ύX���������x�𔽉f����
                }
            }

            if (alfa > 0 && !PCT.trFlag2)
            {

                if (this.num == 0)
                {
                    time1 += Time.deltaTime;
                    if (time1 >= 1 && time1 < 2.2)
                    {
                        alfa -= fadeSpeed;         // �s�����x��������
                        SetAlpha();               // �ύX���������x�𔽉f����
                    }

                }

                if (this.num == 1 && PCT.trFlag1)
                {
                    alfa -= fadeSpeed1;         // �s�����x��������
                    SetAlpha();               // �ύX���������x�𔽉f����
                }

            }

            if (time >= 2.3 || alfa <= 0 || time1 >= 2.2)
            {
                time = 0;
                time1 = 0;
                flag = false;
                mesh.enabled = false;  // Material�̕`����I�t�ɂ���
            }
        }

        if (!PCT.trFlag)
        {
            alfa = 1;
            SetAlpha();
            mesh.enabled = true;
        }
    }

    void SetAlpha()
    {
        // �ύX�����s�����x���܂�Material�̃J���[�𔽉f����
        mesh.material.color = new Color(red, green, blue, alfa);
    }

    private void OnTriggerEnter(Collider other)
    {
        rb.isKinematic = true;

    }
}
