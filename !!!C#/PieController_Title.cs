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

    // 前フレームのワールド位置
    private Vector3 _prevPosition;
    private Rigidbody rb;


    public void Shoot(Vector3 dir,GameObject player, bool isSP)
    {
        //投げたプレイヤーのキャラクターを保存しておく(レイヤーが増えると管理が大変なので、誰が投げたパイなのかを参照できるようにして当たり判定を管理する)
        throwPlayer = player;
        for (int i = 0; i < childrenDecal.Length; i++)
        {
            childrenDecal[i].ThrowPlayer = player;
        }
        //投げた人を取得する前にパイが何かに当たらないようにboolでタイミングを管理
        canHit = true;
        enterCheck = false;

        //毎回ゲットコンポーネントするのは無駄なので、一度取得したら格納しておく
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
        //当たった対象の一番親のオブジェクトが、投げたプレイヤーキャラと一致しないときだけ当たったときの処理を行う
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

            // 移動量を計算
            Vector3 delta = position - _prevPosition;

            // 次のUpdateで使うための前フレーム位置更新
            _prevPosition = position;

            // 静止している状態だと、進行方向を特定できないため回転しない
            if (delta == Vector3.zero)
            {
                return;
            }

            // 進行方向（移動量ベクトル）に向くようなクォータニオンを取得
            Quaternion rotation = Quaternion.LookRotation(delta, Vector3.up);

            // オブジェクトの回転に反映
            transform.rotation = rotation;
        }
    }
}
