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
        for (int i = 0; i < gamepad.Count; i++)//�R���g���[���̏ꍇ
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

        if (Input.GetKeyDown(KeyCode.B) || Input.GetKeyDown(KeyCode.Return))//�L�[�{�[�h�̏ꍇ
        {
            FadeManager.Instance.LoadScene("GameScene", 0.5f);
            while (audioSource.volume > 0)
            {
                audioSource.volume -= speed * Time.deltaTime;
            }
        }
    }
}

