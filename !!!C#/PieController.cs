using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PieController : MonoBehaviour
{
    public float hitcount = 0;
    public int hc = 0;

    private Vector3 initalPos;
    private Quaternion initalRot;
    bool enterCheck = false;

    // �O�t���[���̃��[���h�ʒu
    private Vector3 _prevPosition;
    private Rigidbody rb;

    public AudioClip hit;
    public AudioClip hit1;
    [SerializeField] public AudioSource aud;

    Collider col;

    public Transparent[] TR;
    [SerializeField] public GameObject[] tr;
    public bool trFlag;
    public bool trFlag1;
    public bool trFlag2;
    [SerializeField] public GameObject defalt;


    [SerializeField] public PlayerController[] PC;

    public GameObject parent;
    public void Shoot(Vector3 dir, GameObject player,bool isSP)
    {
        this.gameObject.transform.SetParent(defalt.transform);
        trFlag = false;
        trFlag1 = false;
        trFlag2 = false;

        //�������v���C���[�̃L�����N�^�[��ۑ����Ă���(���C���[��������ƊǗ�����ςȂ̂ŁA�N���������p�C�Ȃ̂����Q�Ƃł���悤�ɂ��ē����蔻����Ǘ�����)
        throwPlayer = player;
        for (int i = 0; i < childrenDecal.Length; i++)
        {
            childrenDecal[i].ThrowPlayer = player;
        }
        //�������l���擾����O�Ƀp�C�������ɓ�����Ȃ��悤��bool�Ń^�C�~���O���Ǘ�
        canHit = true;

        //�X�y�V�������ǂ����ۑ����Ă���(�傫���ȂǒP���Ȕ\�͂����ς��Ȃ��ꍇ�́A���̃X�N���v�g�Ńm�[�}�����X�y�V������������)
        isSpecial = isSP;

        enterCheck = false;

        //����Q�b�g�R���|�[�l���g����͖̂��ʂȂ̂ŁA��x�擾������i�[���Ă���
        if (rb == null)
        {
            rb = GetComponent<Rigidbody>();
        }
        rb.isKinematic = false;
        rb.AddForce(dir * rb.mass, ForceMode.Impulse);
    }

    //public ParticleSystem[] pieEffect;
    //private Vector3 hitPos;

    //[SerializeField] DecalController[] children;
    [SerializeField] DecalController[] childrenDecal;
    [System.NonSerialized] public GameObject throwPlayer;
    private bool canHit = false;
    [System.NonSerialized] public int PlayerID;
    [System.NonSerialized] public bool isSpecial = false;
    public bool hitwait;
    MeshRenderer mesh;
    float transparency;
    public Vector3 pieScale;
    public int hitpoint;

    public int p_num;

    private void OnTriggerEnter(Collider other)
    {
        //���������Ώۂ̈�Ԑe�̃I�u�W�F�N�g���A�������v���C���[�L�����ƈ�v���Ȃ��Ƃ��������������Ƃ��̏������s��
        if (other.transform.root.gameObject != throwPlayer && canHit && !other.gameObject.CompareTag("Piece"))
        {

            enterCheck = true;
            canHit = false;
            rb.isKinematic = true;

            for (int i = 0; i < childrenDecal.Length; i++)
            {
                childrenDecal[i].ShootChildren();
            }

            if (other.transform.root.gameObject != throwPlayer && (other.gameObject.layer == 10 || other.gameObject.layer == 11 || other.gameObject.layer == 12 || other.gameObject.layer == 6))
            {
                trFlag = true;
                parent = other.transform.root.gameObject;
                PlayerController parent_PC = parent.GetComponent<PlayerController>();
                p_num = parent_PC.num;
                this.aud.PlayOneShot(this.hit);
                //���������I�u�W�F�N�g�̎q���ɂ���
                //�q�ǂ��ɂ͂ł��邪�A�\�ꂽ��ό`�����肵�Ă��܂�
                col = other;
                this.gameObject.transform.SetParent(col.gameObject.transform);
            }
            else
            {
                this.aud.PlayOneShot(this.hit1);
                hc = 1;
                trFlag = true;
                trFlag2 = true;
            }
        }
    }

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
        mesh = GetComponent<MeshRenderer>();
        transparency = 0;
        for (int i = 0; i < tr.Length; i++)
        {
            TR[i] = tr[i].gameObject.GetComponent<Transparent>();
            TR[i].PCT = this;

        }
        trFlag = false;
        trFlag1 = false;
        trFlag2 = false;

        pieScale = transform.lossyScale;

        //�ǂ̃v���C���[�ԍ��ł��Ȃ����̂�ݒ�
        p_num = 4;
    }

    void Update()
    {
        for (int i = 0; i < PC.Length; i++)
        {
            if (PC[i].wipeON && p_num == PC[i].num)
            {
                hc = 1;
                trFlag1 = true;
            }
        }


        if (hc == 1)
        {
            hitcount += Time.deltaTime;
            this.gameObject.transform.Translate(0, -0.005f, 0);
            transparency += 1f;
            mesh.material.color = new Color(0, 0, 0, transparency);

            if (hitcount >= 2.6f)
            {
                hitwait = false;
                transform.position = initalPos;
                transform.rotation = initalRot;

                for (int i = 0; i < childrenDecal.Length; i++)
                {
                    childrenDecal[i].SetChildren();
                    childrenDecal[i].PosReset();
                }
                gameObject.SetActive(false);
                hc = 0;
                hitcount = 0f;
                throwPlayer = null;
                canHit = false;
                transparency = 0;
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
