using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootMarker : MonoBehaviour
{

    //���������\����������̐�
    private int segmentCount = 60;

    //�����������b�Ԃ�v�Z���邩
    private float predictionTime = 6.0f;


    //�������̃}�e���A��
    [SerializeField, Tooltip("�������̃}�e���A��")]
    private Material arcMaterial;

    //�������̕�
    [SerializeField, Tooltip("�������̕�")]
    private float arcWidth = 0.02f;

    //���������\������LineRenderer
    private LineRenderer[] lineRenderers;

    //�e�̏����x�␶�����W�����R���|�[�l���g
    public PieThrow pieThrow;

    //�e�̏���
    private Vector3 initialVelocity;

    //�������̊J�n���W
    private Vector3 arcStartPosition;

    //���e�}�[�J�[�I�u�W�F�N�g��Prefab
    [SerializeField, Tooltip("���e�n�_�ɕ\������}�[�J�[��Prefab")]
    private GameObject pointerPrefab;

    //���e�_�̃}�[�J�[�̃I�u�W�F�N�g
    private GameObject pointerObject;

    void Start()
    {
        //��������LineRenderer�I�u�W�F�N�g��p��
        CreateLineRendererObjects();

        //�}�[�J�[�̃I�u�W�F�N�g��p��
        pointerObject = Instantiate(pointerPrefab, Vector3.zero, Quaternion.identity);
        pointerObject.SetActive(false);

        //�e�̏����x�␶�����W�����R���|�[�l���g
        pieThrow = gameObject.GetComponent<PieThrow>();
    }

    void LateUpdate()
    {
        //�����x�ƕ������̊J�n���W���X�V
        initialVelocity = pieThrow.follow_h.transform.forward * pieThrow.NageP;
        arcStartPosition = pieThrow.follow_HT.position;

        if (pieThrow.drawArc)
        {
            //��������\��
            float timeStep = predictionTime / segmentCount;
            bool draw = false;
            float hitTime = float.MaxValue;
            for (int i = 0; i < segmentCount; i++)
            {
                //���̍��W���X�V
                float startTime = timeStep * i;
                float endTime = timeStep + startTime;
                SetLineRendererPosition(i, startTime, endTime, !draw);

                //�Փ˔���
                if (!draw)
                {
                    hitTime = GetArcHitTime(startTime, endTime);
                    if (hitTime != float.MaxValue)
                    {
                        draw = true; //�Փ˂����炻�̐�̕������͕\�����Ȃ�
                    }
                }
            }

            //�}�[�J�[�̕\��
            if (hitTime != float.MaxValue)
            {
                Vector3 hitPosition = GetArcPositionAtTime(hitTime);
                ShowPointer(hitPosition);
            }
        }
        else
        {
            //�������ƃ}�[�J�[��\�����Ȃ�
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                lineRenderers[i].enabled = false;
            }
            pointerObject.SetActive(false);
        }
    }

    //�w�莞�Ԃɑ΂���A�[�`�̕�������̍��W��Ԃ�
    private Vector3 GetArcPositionAtTime(float time)
    {
        return (arcStartPosition + ((initialVelocity * time) + (0.5f * time * time) * Physics.gravity));

    }

    //LineRenderer�̍��W���X�V
    private void SetLineRendererPosition(int index, float startTime, float endTime, bool draw = true)
    {
        lineRenderers[index].SetPosition(0, GetArcPositionAtTime(startTime));
        lineRenderers[index].SetPosition(1, GetArcPositionAtTime(endTime));
        lineRenderers[index].enabled = draw;
    }

    //LineRenderer�I�u�W�F�N�g���쐬
    private void CreateLineRendererObjects()
    {
        //�e�I�u�W�F�N�g�����AlineRenderer�����q�I�u�W�F�N�g�����
        GameObject arcObjectsParent = new GameObject("ArcObject");

        lineRenderers = new LineRenderer[segmentCount];
        for (int i = 0; i < segmentCount; i++)
        {
            GameObject newObject = new GameObject("LineRenderer_" + 1);
            newObject.transform.SetParent(arcObjectsParent.transform);
            lineRenderers[i] = newObject.AddComponent<LineRenderer>();

            //�����֘A���g�p���Ȃ�
            lineRenderers[i].receiveShadows = false;
            lineRenderers[i].reflectionProbeUsage = UnityEngine.Rendering.ReflectionProbeUsage.Off;
            lineRenderers[i].lightProbeUsage = UnityEngine.Rendering.LightProbeUsage.Off;
            lineRenderers[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;

            // ���̕��ƃ}�e���A��
            lineRenderers[i].material = arcMaterial;
            lineRenderers[i].startWidth = arcWidth;
            lineRenderers[i].endWidth = arcWidth;
            lineRenderers[i].numCapVertices = 5;
            lineRenderers[i].enabled = false;

        }
    }

    //�w����W�Ƀ}�[�J�[��\��
    private void ShowPointer(Vector3 position)
    {
        pointerObject.transform.position = position;
        pointerObject.SetActive(true);
    }

    //2�_�Ԃ̐����ŏՓ˔��肵�A�Փ˂��鎞�Ԃ�Ԃ�
    private float GetArcHitTime(float startTime, float endTime)
    {
        //Linecast��������̏I�n�_�̍��W
        Vector3 startPosition = GetArcPositionAtTime(startTime);
        Vector3 endPosition = GetArcPositionAtTime(endTime);

        //�Փ˔���
        RaycastHit hitInfo;
        if (Physics.Linecast(startPosition, endPosition, out hitInfo))
        {
            pointerObject.transform.rotation = Quaternion.FromToRotation(Vector3.back, hitInfo.normal);
            //Debug.Log(pointerObject.transform.rotation);

            //�Փ˂���Collider�܂ł̋���������ۂ̏Փˎ��Ԃ��Z�o
            float distance = Vector3.Distance(startPosition, endPosition);
            return startTime + (endTime - startTime) * (hitInfo.distance / distance);
        }
        return float.MaxValue;
    }

}


