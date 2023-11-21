using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


public class TitleDirector : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    float speed = 0.0001f;

    void Update()
    {
        var gamepad = Gamepad.all;
        for (int i = 0; i < gamepad.Count; i++)//コントローラの場合
        {
            if (gamepad[i].buttonEast.wasPressedThisFrame)
            {
                FadeManager.Instance.LoadScene("GameScene", 0.5f);
                while (audioSource.volume > 0)
                {
                    audioSource.volume -= speed * Time.deltaTime;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Return))//キーボードの場合
        {
            FadeManager.Instance.LoadScene("GameScene", 0.5f);
            while (audioSource.volume > 0)
            {
                audioSource.volume -= speed * Time.deltaTime;
            }
        }
    }
}

