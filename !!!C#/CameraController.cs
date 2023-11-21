using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class CameraController : MonoBehaviour
{
    //追いかける対象の情報
    [SerializeField] Transform follow;
    [System.NonSerialized] public PlayerController PC;

    //マウス感度
    [SerializeField] float mouseSensitivity = 1;

    public int num;

    //x軸中心の回転値、y軸中心の回転値
    float yaw, pitch;

    // Start is called before the first frame update
    void Start()
    {
        //カーソルの非表示
        Cursor.visible = false;

        //カーソルを中央に固定
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        //追従対象と同じpositionに瞬間移動
        transform.position = follow.position;

        var gamepad = Gamepad.all;

        if (num < gamepad.Count)//コントローラが繋がっている場合
        {
            //右スティックを動かして視点を操作する
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
            if (gamepad[PC.num].buttonWest.wasPressedThisFrame)//カメラ上下リセット
            {
                pitch = 0;
            }

        }
        else if(gamepad.Count <= 0 && num == 0)//コントローラが繋がっていない場合
        {
            yaw += Input.GetAxis("Mouse X") * mouseSensitivity;
            pitch += Input.GetAxis("Mouse Y") * mouseSensitivity;
        }

        //マウス・スティックの動かした距離をそのまま角度する
        transform.eulerAngles = new Vector3(pitch, yaw, 0);

        //数値の制限(下限が-60,上限が60)を設定し、pitchの値が制限を超えたり下回ったら制限値を代入する
        pitch = Mathf.Clamp(pitch, -60, 40);


    }
}
