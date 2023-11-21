using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;



public class PlayerController : MonoBehaviour
{
     public float jumpPower;//�W�����v�p���[
    Rigidbody rb;
    public int num;//�v���C���[�̃i���o�[

    [SerializeField] Transform myCamera;//�J�����̃g�����X�t�H�[��
    [SerializeField] Transform thirdPos;//�J�����̈ʒu�i�O�l�̎��_�j
    [SerializeField] Transform firstPos;//�J�����̈ʒu�i��l�̎��_�j

    [SerializeField] GameObject head_position;//�L�����N�^�[�̓��̈ʒu
    [SerializeField] GameObject camera_obj;//�J�����擾

    public bool jumpNow;//�W�����v����
    bool landing;//�v���C���[�̃W�����v��A���n������

    public bool wipeON;//�@���Ă��邩
    public int Wiper;//�@�����̃J�E���g

    float shoL = 0;//�R���g���[�����V�����_�[�̓���
    float triL = 0;//�R���g���[�����g���K�[�̓���

    [System.NonSerialized] public List<ShotBody> SB = new List<ShotBody>();//�̂Ƀp�C�������������̃X�N���v�g�擾�p
    [System.NonSerialized] public List<ShotHead> SH = new List<ShotHead>();//���Ƀp�C�������������̃X�N���v�g�擾�p

    [System.NonSerialized] public PieCounter PCounter;//�p�C�̂�������J�E���g����X�N���v�g�擾�p
    [System.NonSerialized] public PieThrow PT;//�p�C�𓊂���X�N���v�g�擾�p
    [System.NonSerialized] public TimeController TC;//TimeController�擾�p

    [SerializeField] public PieController[] PController;//PieController�擾�p
    [SerializeField] public GameObject[] pcontroller;

    [SerializeField] public GameObject sg;
    private StartGame SG;//�Q�[���X�^�[�g���̃X�N���v�g�擾�p

    private Atm AT;//�v���C���[�̓��̃X�N���v�g�擾�p
    private Camera_Lean CL;//�J�������[�����̃X�N���v�g�擾�p
    private bool switchResult;//���U���g�̃X�C�b�`

    [SerializeField] public GameObject cc;//CameraController�擾�p
    private CameraController CC;

    public int LeanNum;//���[���̃i���o�[

    Animator animator;

    //�ړ����x
    [SerializeField] public float speed = 7;
    public float speed_hold;
    public int betyaCount;//���Ƀp�C���������Ă����

    void Start()
    {
        switchResult = true;
        jumpNow = false;
        wipeON = false;
        landing = false;

        speed = 7;
        LeanNum = 0;
        betyaCount = 0;
        speed_hold = 0;

        rb = GetComponent<Rigidbody>();
        this.animator = GetComponent<Animator>();

        //���ꂼ��̃X�N���v�g�擾
        AT = head_position.gameObject.GetComponent<Atm>();
        AT.PC = this;
        CL = camera_obj.gameObject.GetComponent<Camera_Lean>();
        CL.PC = this;
        SG = sg.gameObject.GetComponent<StartGame>();
        SG.PC[num] = this;
        CC = cc.gameObject.GetComponent<CameraController>();
        CC.PC = this;

        for (int i = 0; i < PController.Length; i++)
        {
            PController[i] = pcontroller[i].gameObject.GetComponent<PieController>();
            if (num == 0)
            {
                PController[i].PC[0] = this;
            }
            else if (num == 1)
            {
                PController[i].PC[1] = this;
            }
            else if (num == 2)
            {
                PController[i].PC[2] = this;
            }
            else if (num == 3)
            {
                PController[i].PC[3] = this;
            }

        }
    }

    void Update()
    {
        var gamepad = Gamepad.all;

        if (num < gamepad.Count)//�R���g���[�����q�����Ă���ꍇ
        {
            triL = gamepad[num].leftTrigger.ReadValue();
            shoL = gamepad[num].leftShoulder.ReadValue();

            if (gamepad[num].buttonSouth.wasPressedThisFrame && !jumpNow)//���{�^������������W�����v������
            {
                animator.SetBool("Jamp", true);
                rb.AddForce(new Vector3(0, jumpPower, 0));
                jumpNow = true;
                speed = speed / 2;
                landing = true;
            }

            if (shoL > 0)//���V�����_�[�������Ă���ԁA�J�������O�l�̎��_�ɂ���
            {
                myCamera.position = thirdPos.position;
            }
            else if (shoL <= 0)
            {
                myCamera.position = firstPos.position;
            }

            //���E�X�e�B�b�N�̉������݂Ń��[������
            if (gamepad[num].rightStickButton.wasPressedThisFrame && (LeanNum == 0 || LeanNum == 2))
            {
                animator.SetBool("Lean_L", false);
                animator.SetBool("Lean_R", true);
                LeanNum = 1;
            }
            else if (gamepad[num].leftStickButton.wasPressedThisFrame && (LeanNum == 0 || LeanNum == 1))
            {
                animator.SetBool("Lean_R", false);
                animator.SetBool("Lean_L", true);
                LeanNum = 2;
            }

            else if ((gamepad[num].rightStickButton.wasPressedThisFrame || gamepad[num].leftStickButton.wasPressedThisFrame) && (LeanNum == 1 || LeanNum == 2))
            {
                animator.SetBool("Lean_R", false);
                animator.SetBool("Lean_L", false);
                LeanNum = 0;
            }

        }

        if(gamepad.Count <= 0 && num == 0)//�R���g���[�����q�����Ă��炸�A�PP�̏ꍇ
        {
            if (Input.GetKeyDown(KeyCode.Space) && !jumpNow)//�W�����v�����Ƃ�
            {
                animator.SetBool("Jamp", true);
                rb.AddForce(new Vector3(0, jumpPower, 0));
                jumpNow = true;
                speed = speed / 2;
                landing = true;
            }

            if (Input.GetKey(KeyCode.LeftShift))//���V�t�g�L�[�������Ă���ԁA�J�������O�l�̎��_�ɂ���
            {
                myCamera.position = thirdPos.position;
            }
            else
            {
                myCamera.position = firstPos.position;
            }
        }

        if (!jumpNow && landing)//�W�����v���A���n�����Ƃ�
        {
            animator.SetBool("Jamp", false);
            speed = speed * 2;
            speed -= speed_hold;
            speed_hold = 0;
            landing = false;
        }

        if (PCounter.Pie >= 1 && !wipeON && TC.countdown > 0.3f && !PT.Switch)
        {
            animator.SetBool("BigPie", false);
            animator.SetBool("Throw", true);
        }
        if (PCounter.Pie == 0)
        {
            animator.SetBool("Throw", false);
        }
        if (PT.Switch == false)
        {
            animator.SetBool("BigPie", false);
        }
    }

    private void FixedUpdate()
    {

        //�@��
        if ((triL > 0 || (Input.GetKeyDown(KeyCode.E) && num == 0))&& (betyaCount > 0 || speed < 7) && jumpNow == false && wipeON == false)
        {
            jumpNow = true;//�W�����v���ł��Ȃ�����
            speed = 0;//�����Ȃ�����

            Wiper++;
            animator.SetInteger("Wipe", 1);
            animator.SetBool("Throw", false);
            PT.handPie.SetActive(false);

            wipeON = true;
            if (Wiper >= 100)//�J�E���g��100�ɂȂ�����A�@���I���
            {
                betyaCount = 0;
                speed = 7;
                animator.SetInteger("Wipe", 0);

                if (PCounter.sPie == false)
                {
                    PCounter.sPiece++;
                }
                Wiper = 0;
                wipeON = false;
                jumpNow = false;
            }
        }
        else if (wipeON == true)//�����{�^�������𗣂��Ă��A�@���I���܂Ő@��������
        {
            PT.handPie.SetActive(false);
            jumpNow = true;
            Wiper++;
            animator.SetInteger("Wipe", 1);
            animator.SetBool("Throw", false);

            if (Wiper >= 100)
            {
                betyaCount = 0;
                speed = 7;
                animator.SetInteger("Wipe", 0);

                if (PCounter.sPie == false)
                {
                    PCounter.sPiece++;
                }
                Wiper = 0;
                wipeON = false;
                jumpNow = false;
            }
        }

        var gamepad = Gamepad.all;

        if (TC.countdown < 0.3f)//���ʔ��\�O�A���ׂĂ̓������~�߂�
        {
            switchResult = false;
            animator.SetFloat("Walk_F", 0);
            animator.SetFloat("Walk_B", 0);
            animator.SetInteger("Wipe", 0);
            animator.SetBool("Jamp", false);
            animator.SetBool("Lean_R", false);
            animator.SetBool("Lean_L", false);
            animator.SetBool("Throw", false);
            PT.handPie.SetActive(false);
            PT.handBigpie.SetActive(false);
            animator.SetBool("Throw", false);
            animator.SetBool("BigPie", false);

        }

        if (num < gamepad.Count && TC.countdown > 0.3f && switchResult)
        {
            //�R���g���[�����X�e�B�b�N�̓��͂�Vector3�ɑ��
            var input = new Vector3(gamepad[num].leftStick.ReadValue().x, 0f, gamepad[num].leftStick.ReadValue().y);

            //�J���������Ă���y������Ƃ�������(����)���擾
            //Quaternion.AngleAxis��Vector3.up(y��)�����ɂ����J������y���̊p�x��Quaternion�^�ɕϊ�
            var horizontalRotation = Quaternion.AngleAxis(myCamera.transform.eulerAngles.y, Vector3.up);

            //�u�J�����̐��ʂ���Ƃ������͕����v���v�Z
            var velocity = horizontalRotation * input;

            //�J�����ƃL�����N�^�[�̌����𓯂��ɂ���
            transform.localEulerAngles = new Vector3(0f, myCamera.transform.eulerAngles.y, 0f);

            //�΂߈ړ��̍ۂ�velocity�̒������P�𒴂��Ă��܂��̂ŁA���̎��͂P�ɒ��߂���
            if (velocity.magnitude > 1)
            {
                velocity = velocity.normalized;
            }

            rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y, velocity.z * speed);

            animator.SetFloat("Walk_F", gamepad[num].leftStick.ReadValue().x);
            animator.SetFloat("Walk_B", gamepad[num].leftStick.ReadValue().y);
        }

        //�Q�[���p�b�g���q�����Ă��Ȃ�+1P�̂Ƃ��̓L�[�{�[�h�œ�����
        else if (gamepad.Count <= 0 && num == 0 && TC.countdown > 0.3f && switchResult)
        {
            var input = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            var horizontalRotation = Quaternion.AngleAxis(myCamera.transform.eulerAngles.y, Vector3.up);
            var velocity = horizontalRotation * input;

            transform.localEulerAngles = new Vector3(0f, myCamera.transform.eulerAngles.y, 0f);

            if (velocity.magnitude > 1)
            {
                velocity = velocity.normalized;
            }

            rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y, velocity.z * speed);

            animator.SetFloat("Walk_F", Input.GetAxis("Horizontal"));
            animator.SetFloat("Walk_B", Input.GetAxis("Vertical"));
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            jumpNow = false;
        }
    }
}
