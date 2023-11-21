using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using DG.Tweening;



public class PlayerController : MonoBehaviour
{
     public float jumpPower;//ジャンプパワー
    Rigidbody rb;
    public int num;//プレイヤーのナンバー

    [SerializeField] Transform myCamera;//カメラのトランスフォーム
    [SerializeField] Transform thirdPos;//カメラの位置（三人称視点）
    [SerializeField] Transform firstPos;//カメラの位置（一人称視点）

    [SerializeField] GameObject head_position;//キャラクターの頭の位置
    [SerializeField] GameObject camera_obj;//カメラ取得

    public bool jumpNow;//ジャンプ中か
    bool landing;//プレイヤーのジャンプ後、着地したか

    public bool wipeON;//拭っているか
    public int Wiper;//拭う時のカウント

    float shoL = 0;//コントローラ左ショルダーの入力
    float triL = 0;//コントローラ左トリガーの入力

    [System.NonSerialized] public List<ShotBody> SB = new List<ShotBody>();//体にパイが当たった時のスクリプト取得用
    [System.NonSerialized] public List<ShotHead> SH = new List<ShotHead>();//頭にパイが当たった時のスクリプト取得用

    [System.NonSerialized] public PieCounter PCounter;//パイのかけらをカウントするスクリプト取得用
    [System.NonSerialized] public PieThrow PT;//パイを投げるスクリプト取得用
    [System.NonSerialized] public TimeController TC;//TimeController取得用

    [SerializeField] public PieController[] PController;//PieController取得用
    [SerializeField] public GameObject[] pcontroller;

    [SerializeField] public GameObject sg;
    private StartGame SG;//ゲームスタート時のスクリプト取得用

    private Atm AT;//プレイヤーの頭のスクリプト取得用
    private Camera_Lean CL;//カメラリーン時のスクリプト取得用
    private bool switchResult;//リザルトのスイッチ

    [SerializeField] public GameObject cc;//CameraController取得用
    private CameraController CC;

    public int LeanNum;//リーンのナンバー

    Animator animator;

    //移動速度
    [SerializeField] public float speed = 7;
    public float speed_hold;
    public int betyaCount;//頭にパイが当たっている回数

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

        //それぞれのスクリプト取得
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

        if (num < gamepad.Count)//コントローラが繋がっている場合
        {
            triL = gamepad[num].leftTrigger.ReadValue();
            shoL = gamepad[num].leftShoulder.ReadValue();

            if (gamepad[num].buttonSouth.wasPressedThisFrame && !jumpNow)//下ボタンを押したらジャンプをする
            {
                animator.SetBool("Jamp", true);
                rb.AddForce(new Vector3(0, jumpPower, 0));
                jumpNow = true;
                speed = speed / 2;
                landing = true;
            }

            if (shoL > 0)//左ショルダーを押している間、カメラを三人称視点にする
            {
                myCamera.position = thirdPos.position;
            }
            else if (shoL <= 0)
            {
                myCamera.position = firstPos.position;
            }

            //左右スティックの押し込みでリーンする
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

        if(gamepad.Count <= 0 && num == 0)//コントローラが繋がっておらず、１Pの場合
        {
            if (Input.GetKeyDown(KeyCode.Space) && !jumpNow)//ジャンプしたとき
            {
                animator.SetBool("Jamp", true);
                rb.AddForce(new Vector3(0, jumpPower, 0));
                jumpNow = true;
                speed = speed / 2;
                landing = true;
            }

            if (Input.GetKey(KeyCode.LeftShift))//左シフトキーを押している間、カメラを三人称視点にする
            {
                myCamera.position = thirdPos.position;
            }
            else
            {
                myCamera.position = firstPos.position;
            }
        }

        if (!jumpNow && landing)//ジャンプ中、着地したとき
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

        //拭う
        if ((triL > 0 || (Input.GetKeyDown(KeyCode.E) && num == 0))&& (betyaCount > 0 || speed < 7) && jumpNow == false && wipeON == false)
        {
            jumpNow = true;//ジャンプをできなくする
            speed = 0;//動けなくする

            Wiper++;
            animator.SetInteger("Wipe", 1);
            animator.SetBool("Throw", false);
            PT.handPie.SetActive(false);

            wipeON = true;
            if (Wiper >= 100)//カウントが100になったら、拭い終わる
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
        else if (wipeON == true)//もしボタンから手を離しても、拭い終わるまで拭い続ける
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

        if (TC.countdown < 0.3f)//結果発表前、すべての動きを止める
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
            //コントローラ左スティックの入力をVector3に代入
            var input = new Vector3(gamepad[num].leftStick.ReadValue().x, 0f, gamepad[num].leftStick.ReadValue().y);

            //カメラが見ているy軸を基準とした方向(正面)を取得
            //Quaternion.AngleAxisでVector3.up(y軸)を軸にしたカメラのy軸の角度をQuaternion型に変換
            var horizontalRotation = Quaternion.AngleAxis(myCamera.transform.eulerAngles.y, Vector3.up);

            //「カメラの正面を基準とした入力方向」を計算
            var velocity = horizontalRotation * input;

            //カメラとキャラクターの向きを同じにする
            transform.localEulerAngles = new Vector3(0f, myCamera.transform.eulerAngles.y, 0f);

            //斜め移動の際にvelocityの長さが１を超えてしまうので、その時は１に調節する
            if (velocity.magnitude > 1)
            {
                velocity = velocity.normalized;
            }

            rb.velocity = new Vector3(velocity.x * speed, rb.velocity.y, velocity.z * speed);

            animator.SetFloat("Walk_F", gamepad[num].leftStick.ReadValue().x);
            animator.SetFloat("Walk_B", gamepad[num].leftStick.ReadValue().y);
        }

        //ゲームパットが繋がっていない+1Pのときはキーボードで動かす
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
