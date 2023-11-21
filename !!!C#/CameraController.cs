using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    //�ǂ�������Ώۂ̏��
    [SerializeField] Transform follow;
    [System.NonSerialized] public PlayerController PC;

    //�}�E�X���x
    [SerializeField] float mouseSensitivity = 1;

    public int num;

    //x�����S�̉�]�l�Ay�����S�̉�]�l
    float yaw, pitch;

    // Start is called before the first frame update
    void Start()
    {
        //�J�[�\���̔�\��
        Cursor.visible = false;

        //�J�[�\���𒆉��ɌŒ�
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //�Ǐ]�ΏۂƓ���position�ɏu�Ԉړ�
        transform.position = follow.position;

        var gamepad = Gamepad.all;

        if (num < gamepad.Count)//�R���g���[�����q�����Ă���ꍇ
        {
            //�E�X�e�B�b�N�𓮂����Ď��_�𑀍삷��
            if(num == 0 || num == 2)
            {
                yaw += gamepad[num].rightStick.ReadValue().x * Time.deltaTime * 200;
                pitch -= gamepad[num].rightStick.ReadValue().y * Time.deltaTime * 200;
            }
            else if(num == 1 || num == 3)
            {
                yaw += gamepad[num].rightStick.ReadValue().x * Time.deltaTime * 200;
                pitch += gamepad[num].rightStick.ReadValue().y * Time.deltaTime * 200;
            }
            if (gamepad[PC.num].buttonWest.wasPressedThisFrame)//�J�����㉺���Z�b�g
            {
                pitch = 0;
            }

        }
        else if(gamepad.Count <= 0 && num == 0)//�R���g���[�����q�����Ă��Ȃ��ꍇ
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        //�}�E�X�E�X�e�B�b�N�̓����������������̂܂܊p�x����
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        //���l�̐���(������-60,�����60)��ݒ肵�Apitch�̒l�������𒴂����艺������琧���l��������
        pitch = Mathf.Clamp(pitch, -60, 40);


    }
}
