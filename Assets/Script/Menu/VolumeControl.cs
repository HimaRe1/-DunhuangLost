using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    AudioSource audiosource;
    public Slider slider;
    private void Start()
    {
        slider.onValueChanged.AddListener(OnSliderValueChanged);
        audiosource = GameObject.FindGameObjectWithTag("bgm").GetComponent<AudioSource>();
    }
    public void OnSliderValueChanged(float value)
    {
        audiosource.volume = value;
    }
}
