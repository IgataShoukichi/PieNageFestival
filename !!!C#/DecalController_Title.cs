using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecalController_Title : MonoBehaviour
{
    //接触した座標を保存する変数と、接触した面との角度を保存する変数
    Vector3 hitPos, hitAngle;

    //生成するデカールプレハブ
    [SerializeField] GameObject decal;

    //自分のRigidbodyを入れておく
    private Rigidbody rb;


    private Collider myCol;


    //自分にRayが当たらないように、自分のLayerを当たらない対象にしておく
    LayerMask layerMask = ~(1 << 7);

    //接触した相手のコライダーを保存しておく変数
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
            //接触した相手のコライダーを保存
            col = other;

            rb.velocity = Vector3.zero;

            //ここでデカールを生成してもいいのだが、めり込んで接触すると正しい座標と法線ベクトルを取得できないことがあったので、
            //コルーチンを活用して正確な座標と法線ベクトルを取得する
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
        //接触した相手がパイ以外だったときの処理
        if (!switchD && !other.gameObject.CompareTag("Pie1") && other.transform.root.gameObject != ThrowPlayer)
        {
            PieController pie = other.gameObject.GetComponent<PieController>();

            //foreach (ContactPoint point in other.contacts)で接触した座標や接触した面の法線ベクトルを取得できる
            foreach (ContactPoint point in other.contacts)
            {
                //接触した座標を保存
                hitPos = point.point;

                //接触した相手のコライダーを保存
                col = other.collider;
            }

            //ここでデカールを生成してもいいのだが、めり込んで接触すると正しい座標と法線ベクトルを取得できないことがあったので、
            //コルーチンを活用して正確な座標と法線ベクトルを取得する
            StartCoroutine(Ray());
        }
    }

    //正確な接触位置と接触面の法線ベクトルを取得し、デカールを生成するコルーチン
    IEnumerator Ray()
    {
        //While文を止めるためのboolで、これがfalseだと下にあるWhile文がループする
        bool hitRay = false;

        //Raycastで飛ばしたrayの接触情報を保存する変数
        RaycastHit hit;

        //rayを飛ばし、OnCollisionEnterで接触した相手とrayが接触するまでループする処理
        while (!hitRay)
        {
            //自分の原点からOnCollisionEnterで接触した座標に向かってrayを飛ばす(自分自身には当たらない)
            if (Physics.Raycast(transform.position, hitPos - transform.position, out hit, Mathf.Infinity, layerMask))
            {
                //もしOnCollisionEnterで接触した相手とrayが接触したら
                if (hit.collider == col)
                {
                    
                    //ループを止めるためにboolをtrueにする
                    hitRay = true;

                    //生成座標はrayが当たった座標、生成角度はrayが当たった面の法線ベクトル(hit.normal)と、Scene上の後ろ方向との角度を計算して代入(詳細は資料に)
                    var DE = Instantiate(decal, hit.point, Quaternion.FromToRotation(Vector3.back, hit.normal));

                    //デカールを接触した相手の子供にする
                    //これをしないと相手が動いたときにデカールだけ取り残されてしまう
                    DE.transform.parent = col.transform;

                    //デカールが毎回ランダムに向きを変えてほしいので、デカールの正面であるZ軸を-180~180のランダムな値で回転させる
                    DE.transform.localEulerAngles += new Vector3(0, 0, Random.Range(-180f, 180f));

                    //デカールをアクティブにする
                    DE.SetActive(true);

                    //今回は接触した場所から動かないように、isKinematicをtrueにする
                    rb.isKinematic = true;
                }
                else
                {
                    //rayがOnCollisionEnterで接触した相手意外と接触した場合、1フレーム待機してからループする
                    yield return null;
                }
            }
            else
            {
                //rayが接触しなかった場合、1フレーム待機してからループする
                yield return null;
            }
        }
    }
}