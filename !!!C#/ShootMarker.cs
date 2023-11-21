using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMarker : MonoBehaviour
{

    //放物線を構成する線分の数
    private int segmentCount = 60;

    //放物線を何秒ぶん計算するか
    private float predictionTime = 6.0f;


    //放物線のマテリアル
    [SerializeField, Tooltip("放物線のマテリアル")]
    private Material arcMaterial;

    //放物線の幅
    [SerializeField, Tooltip("放物線の幅")]
    private float arcWidth = 0.02f;

    //放物線を構成するLineRenderer
    private LineRenderer[] lineRenderers;

    //弾の初速度や生成座標をもつコンポーネント
    public PieThrow pieThrow;

    //弾の初速
    private Vector3 initialVelocity;

    //放物線の開始座標
    private Vector3 arcStartPosition;

    //着弾マーカーオブジェクトのPrefab
    [SerializeField, Tooltip("着弾地点に表示するマーカーのPrefab")]
    private GameObject pointerPrefab;

    //着弾点のマーカーのオブジェクト
    private GameObject pointerObject;

    void Start()
    {
        //放物線のLineRendererオブジェクトを用意
        CreateLineRendererObjects();

        //マーカーのオブジェクトを用意
        pointerObject = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity);
        pointerObject.SetActive(false);

        //弾の初速度や生成座標を持つコンポーネント
        pieThrow = gameObject.GetComponent<PieThrow>();
    }

    void LateUpdate()
    {
        //初速度と放物線の開始座標を更新
        initialVelocity = pieThrow.follow_h.transform.forward * pieThrow.NageP;
        arcStartPosition = pieThrow.follow_HT.position;

        if (pieThrow.drawArc)
        {
            //放物線を表示
            float timeStep = predictionTime / segmentCount;
            bool draw = false;
            float hitTime = float.MaxValue;
            for (int i = 0; i < segmentCount; i++)
            {
                //線の座標を更新
                float startTime = timeStep * i;
                float endTime = timeStep + startTime;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                //衝突判定
                if (!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; //衝突したらその先の放物線は表示しない
                    }
                }
            }

            //マーカーの表示
            if (hitTime != float.MaxValue)
            {
                Vector3 hitPosition = GetArcPositionAtTime(hitTime);
                ShowPointer(hitPosition);
            }
        }
        else
        {
            //放物線とマーカーを表示しない
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].enabled = false;
            }
            pointerObject.SetActive(false);
        }
    }

    //指定時間に対するアーチの放物線上の座標を返す
    private Vector3 GetArcPositionAtTime(float time)
    {
        return (arcStartPosition + ((initialVelocity * time) + (0.5f * time * time) * Physics.gravity));

    }

    //LineRendererの座標を更新
    private void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {
        lineRenderers[index].SetPosition(0, GetArcPositionAtTime(startTime));
        lineRenderers[index].SetPosition(1, GetArcPositionAtTime(endTime));
        lineRenderers[index].enabled = draw;
    }

    //LineRendererオブジェクトを作成
    private void CreateLineRendererObjects()
    {
        //親オブジェクトを作り、lineRendererを持つ子オブジェクトを作る
        GameObject arcObjectsParent = new GameObject("ArcObject");

        lineRenderers = new LineRenderer[segmentCount];
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject newObject = new GameObject("LineRenderer_" + 1);
            newObject.transform.SetParent(arcObjectsParent.transform);
            lineRenderers[i] = newObject.AddComponent<LineRenderer>();

            //光源関連を使用しない
            lineRenderers[i].receiveShadows = false;
            lineRenderers[i].reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            lineRenderers[i].lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            lineRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            // 線の幅とマテリアル
            lineRenderers[i].material = arcMaterial;
            lineRenderers[i].startWidth = arcWidth;
            lineRenderers[i].endWidth = arcWidth;
            lineRenderers[i].numCapVertices = 5;
            lineRenderers[i].enabled = false;

        }
    }

    //指定座標にマーカーを表示
    private void ShowPointer(Vector3 position)
    {
        pointerObject.transform.position = position;
        pointerObject.SetActive(true);
    }

    //2点間の線分で衝突判定し、衝突する時間を返す
    private float GetArcHitTime(float startTime, float endTime)
    {
        //Linecastする線分の終始点の座標
        Vector3 startPosition = GetArcPositionAtTime(startTime);
        Vector3 endPosition = GetArcPositionAtTime(endTime);

        //衝突判定
        RaycastHit hitInfo;
        if (Physics.Linecast(startPosition, endPosition, out hitInfo))
        {
            pointerObject.transform.rotation = Quaternion.FromToRotation(Vector3.back, hitInfo.normal);
            //Debug.Log(pointerObject.transform.rotation);

            //衝突したColliderまでの距離から実際の衝突時間を算出
            float distance = Vector3.Distance(startPosition, endPosition);
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        return float.MaxValue;
    }

}


