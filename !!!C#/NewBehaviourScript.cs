using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    GameObject player;

    public Vector3 cameraRotation = new Vector3();
    Vector3 currentCamRotation = new Vector3();

    public float dist = 4.0f;
    Vector3 currentLookAtPos = new Vector3();

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // �Q�[���p�b�h�E�X�e�B�b�N����̒l�����Z����
        cameraRotation += new Vector3(-Input.GetAxis("Vertical2"), -Input.GetAxis("Horizontal2"), 0) * 2.0f * Time.deltaTime;

        // X����]�̐���
        cameraRotation.x = Mathf.Clamp(cameraRotation.x, 15 * Mathf.Deg2Rad, 60 * Mathf.Deg2Rad);

        // �x���p�̊p�x�Ƃ̍������Ƃ�
        Vector3 diff = cameraRotation - currentCamRotation;
        currentCamRotation += WrapAngle(diff) * 0.2f;

        // �p�x����x�N�g�����v�Z����
        Vector3 craneVec = new Vector3
        (
            Mathf.Cos(currentCamRotation.x) * Mathf.Cos(currentCamRotation.y),
            Mathf.Sin(currentCamRotation.x),
            Mathf.Cos(currentCamRotation.x) * Mathf.Sin(currentCamRotation.y)
        );

        // �����_�̍��W
        Vector3 lookAtPos = player.transform.position + new Vector3(0, 1, 0);

        currentLookAtPos += (lookAtPos - currentLookAtPos) * 0.2f;

        // �J�����̍��W���X�V����
        this.transform.position = currentLookAtPos + craneVec * dist;

        // �v���C���[�̍��W�ɃJ������������i����͍Ō�ɂ���j
        this.transform.LookAt(currentLookAtPos);
    }

    // �p�x��0�`360���Ɏ��߂�֐�
    Vector3 WrapAngle(Vector3 vector)
    {
        vector.x %= Mathf.PI * 2;
        vector.y %= Mathf.PI * 2;
        vector.z %= Mathf.PI * 2;

        return vector;
    }
}