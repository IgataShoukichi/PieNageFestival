using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieController_Title : MonoBehaviour
{
    private float hitcount = 0;
    int hc = 0;

    private Vector3 initalPos;
    private Quaternion initalRot;
    bool enterCheck = false;

    // �O�t���[���̃��[���h�ʒu
    private Vector3 _prevPosition;
    private Rigidbody rb;


    public void Shoot(Vector3 dir,GameObject player, bool isSP)
    {
        //�������v���C���[�̃L�����N�^�[��ۑ����Ă���(���C���[��������ƊǗ�����ςȂ̂ŁA�N���������p�C�Ȃ̂����Q�Ƃł���悤�ɂ��ē����蔻����Ǘ�����)
        throwPlayer = player;
        for (int i = 0; i < childrenDecal.Length; i++)
        {
            childrenDecal[i].ThrowPlayer = player;
        }
        //�������l���擾����O�Ƀp�C�������ɓ�����Ȃ��悤��bool�Ń^�C�~���O���Ǘ�
        canHit = true;
        enterCheck = false;

        //����Q�b�g�R���|�[�l���g����͖̂��ʂȂ̂ŁA��x�擾������i�[���Ă���
        if(rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.AddForce(dir * rb.mass, ForceMode.Impulse);
    }

    [System.NonSerialized] public GameObject throwPlayer;
    [SerializeField] DecalController[] childrenDecal;
    private bool canHit = false;
    public bool hitwait;

    private void OnCollisionEnter(Collision other)
    {
        //���������Ώۂ̈�Ԑe�̃I�u�W�F�N�g���A�������v���C���[�L�����ƈ�v���Ȃ��Ƃ��������������Ƃ��̏������s��
        if (other.transform.root.gameObject != throwPlayer && canHit )
        {
            canHit = false;
            rb.isKinematic = true;
            hc = 1;

            for (int i = 0; i < childrenDecal.Length; i++)
            {
                childrenDecal[i].ShootChildren();
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody>();
        rb.isKinematic = false;

        _prevPosition = transform.position;
        hitcount = 0;

        initalPos = transform.position;
        initalRot = transform.rotation;
        this.gameObject.SetActive(false);
        hitwait = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (hc == 1)
        {
            hitcount += Time.deltaTime;
            if (hitcount >= 1.5f)
            {
                hitwait = false;
                transform.position = initalPos;
                transform.rotation = initalRot;
                for(int i = 0; i < childrenDecal.Length; i++)
                {
                    childrenDecal[i].PosReset();
                }
                gameObject.SetActive(false);
                hc = 0;
                hitcount = 0f;
                throwPlayer = null;
                canHit = false;
            }
        }


        if (enterCheck == false)
        {
            Vector3 position = transform.position;

            // �ړ��ʂ��v�Z
            Vector3 delta = position - _prevPosition;

            // ����Update�Ŏg�����߂̑O�t���[���ʒu�X�V
            _prevPosition = position;

            // �Î~���Ă����Ԃ��ƁA�i�s���������ł��Ȃ����߉�]���Ȃ�
            if (delta == Vector3.zero)
            {
                return;
            }

            // �i�s�����i�ړ��ʃx�N�g���j�Ɍ����悤�ȃN�H�[�^�j�I�����擾
            Quaternion rotation = Quaternion.LookRotation(delta, Vector3.up);

            // �I�u�W�F�N�g�̉�]�ɔ��f
            transform.rotation = rotation;
        }
    }
}
