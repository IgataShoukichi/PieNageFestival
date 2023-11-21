using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOption : MonoBehaviour
{
    [SerializeField] public GameObject canvas;
    [SerializeField] public GameObject option;

    bool flag;

    private void Start()
    {
        canvas.gameObject.SetActive(false);
        option.gameObject.SetActive(true);
        flag = false;
    }

    void Update()
    {

        var gamepad = Gamepad.all;

        for (int i = 0; i < gamepad.Count; i++)//ƒRƒ“ƒgƒ[ƒ‰‚Ìê‡
        {
            if (gamepad[i].startButton.wasPressedThisFrame)
            {
                {
                    if (!flag)
                    {
                        canvas.gameObject.SetActive(true);
                        option.gameObject.SetActive(false);
                        flag = true;
                    }

                    else if (flag)
                    {
                        canvas.gameObject.SetActive(false);
                        option.gameObject.SetActive(true);

                        flag = false;
                    }

                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            if (!flag)
            {
                canvas.gameObject.SetActive(true);
                option.gameObject.SetActive(false);
                flag = true;
            }

            else if (flag)
            {
                canvas.gameObject.SetActive(false);
                option.gameObject.SetActive(true);
                flag = false;
            }

        }
    }
}
