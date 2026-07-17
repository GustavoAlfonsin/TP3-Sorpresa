using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrightnessController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Image brightnessOverlay;

    private void Start()
    {
        brightnessSlider.onValueChanged.AddListener(SetBrightness);

        SetBrightness(brightnessSlider.value);
    }

    private void SetBrightness(float value)
    {
        Color color = brightnessOverlay.color;

        // value = 1 -> pantalla brillante
        // value = 0 -> pantalla oscura

        color.a = 1f - value;

        brightnessOverlay.color = color;
    }
}

