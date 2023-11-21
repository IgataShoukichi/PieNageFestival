using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WiperSlider : MonoBehaviour
{
    public GameObject slider;
    public Slider slider1;
    private PlayerController PC;

    void Start()
    {
        slider1.value = 100;
        PC = transform.root.gameObject.GetComponent<PlayerController>();

    }

    void FixedUpdate()
    {
        if (PC.Wiper <= 100 && PC.Wiper > 0 )
        {
            this.slider.SetActive(true);
            slider1.value --;
        }

        if (PC.Wiper == 0)
        {
            slider1.value = 100;
            this.slider.SetActive(false);

        }
    }
}
