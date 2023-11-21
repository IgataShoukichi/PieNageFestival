using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalController_Title : MonoBehaviour
{
    //�ڐG�������W��ۑ�����ϐ��ƁA�ڐG�����ʂƂ̊p�x��ۑ�����ϐ�
    Vector3 hitPos, hitAngle;

    //��������f�J�[���v���n�u
    [SerializeField] GameObject decal;

    //������Rigidbody�����Ă���
    private Rigidbody rb;


    private Collider myCol;


    //������Ray��������Ȃ��悤�ɁA������Layer�𓖂���Ȃ��Ώۂɂ��Ă���
    LayerMask layerMask = ~(1 << 7);

    //�ڐG��������̃R���C�_�[��ۑ����Ă����ϐ�
    Collider col;
    [System.NonSerialized] public GameObject ThrowPlayer;

    private Vector3 initalPos;
    private Quaternion initalRot;
    [SerializeField] Transform parentPos;

    public bool switchD = false;

    private void Start()
    {
        myCol = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();

        initalPos = transform.position;
        initalRot = transform.rotation;
    }

    public void PosReset()
    {
        transform.position = initalPos;
        transform.rotation = initalRot;
        rb.isKinematic = true;
        myCol.isTrigger = false;
        switchD = false;
    }

    public void ShootChildren()
    {
       rb.isKinematic = false;
        rb.AddForce((transform.position - parentPos.position).normalized * 300);
    }


    private void OnTriggerEnter(Collider other)
    {
        /*
        if (!switchD && !other.gameObject.CompareTag("Pie1") && other.transform.root.gameObject != ThrowPlayer && other.gameObject.CompareTag("Player"))
        {
            Vector3 hitPos = other.ClosestPointOnBounds(this.transform.position);
            //�ڐG��������̃R���C�_�[��ۑ�
            col = other;

            rb.velocity = Vector3.zero;

            //�����Ńf�J�[���𐶐����Ă������̂����A�߂荞��ŐڐG����Ɛ��������W�Ɩ@���x�N�g�����擾�ł��Ȃ����Ƃ��������̂ŁA
            //�R���[�`�������p���Đ��m�ȍ��W�Ɩ@���x�N�g�����擾����
            StartCoroutine(Ray());
        }
        else if (other.gameObject.CompareTag("Decal"))
        {
            switchD = true;
        }
        */
    }

    private void OnCollisionEnter(Collision other)
    {
        //�ڐG�������肪�p�C�ȊO�������Ƃ��̏���
        if (!switchD && !other.gameObject.CompareTag("Pie1") && other.transform.root.gameObject != ThrowPlayer)
        {
            PieController pie = other.gameObject.GetComponent<PieController>();

            //foreach (ContactPoint point in other.contacts)�ŐڐG�������W��ڐG�����ʂ̖@���x�N�g�����擾�ł���
            foreach (ContactPoint point in other.contacts)
            {
                //�ڐG�������W��ۑ�
                hitPos = point.point;

                //�ڐG��������̃R���C�_�[��ۑ�
                col = other.collider;
            }

            //�����Ńf�J�[���𐶐����Ă������̂����A�߂荞��ŐڐG����Ɛ��������W�Ɩ@���x�N�g�����擾�ł��Ȃ����Ƃ��������̂ŁA
            //�R���[�`�������p���Đ��m�ȍ��W�Ɩ@���x�N�g�����擾����
            StartCoroutine(Ray());
        }
    }

    //���m�ȐڐG�ʒu�ƐڐG�ʂ̖@���x�N�g�����擾���A�f�J�[���𐶐�����R���[�`��
    IEnumerator Ray()
    {
        //While�����~�߂邽�߂�bool�ŁA���ꂪfalse���Ɖ��ɂ���While�������[�v����
        bool hitRay = false;

        //Raycast�Ŕ�΂���ray�̐ڐG����ۑ�����ϐ�
        RaycastHit hit;

        //ray���΂��AOnCollisionEnter�ŐڐG���������ray���ڐG����܂Ń��[�v���鏈��
        while (!hitRay)
        {
            //�����̌��_����OnCollisionEnter�ŐڐG�������W�Ɍ�������ray���΂�(�������g�ɂ͓�����Ȃ�)
            if (Physics.Raycast(transform.position, hitPos - transform.position, out hit, Mathf.Infinity, layerMask))
            {
                //����OnCollisionEnter�ŐڐG���������ray���ڐG������
                if (hit.collider == col)
                {
                    
                    //���[�v���~�߂邽�߂�bool��true�ɂ���
                    hitRay = true;

                    //�������W��ray�������������W�A�����p�x��ray�����������ʂ̖@���x�N�g��(hit.normal)�ƁAScene��̌������Ƃ̊p�x���v�Z���đ��(�ڍׂ͎�����)
                    var DE = Instantiate(decal, hit.point, Quaternion.FromToRotation(Vector3.back, hit.normal));

                    //�f�J�[����ڐG��������̎q���ɂ���
                    //��������Ȃ��Ƒ��肪�������Ƃ��Ƀf�J�[���������c����Ă��܂�
                    DE.transform.parent = col.transform;

                    //�f�J�[�������񃉃��_���Ɍ�����ς��Ăق����̂ŁA�f�J�[���̐��ʂł���Z����-180~180�̃����_���Ȓl�ŉ�]������
                    DE.transform.localEulerAngles += new Vector3(0, 0, Random.Range(-180f, 180f));

                    //�f�J�[�����A�N�e�B�u�ɂ���
                    DE.SetActive(true);

                    //����͐ڐG�����ꏊ���瓮���Ȃ��悤�ɁAisKinematic��true�ɂ���
                    rb.isKinematic = true;
                }
                else
                {
                    //ray��OnCollisionEnter�ŐڐG��������ӊO�ƐڐG�����ꍇ�A1�t���[���ҋ@���Ă��烋�[�v����
                    yield return null;
                }
            }
            else
            {
                //ray���ڐG���Ȃ������ꍇ�A1�t���[���ҋ@���Ă��烋�[�v����
                yield return null;
            }
        }
    }
}