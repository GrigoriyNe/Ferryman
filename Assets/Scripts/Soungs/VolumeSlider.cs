using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.UI;

public abstract class VolumeSlider : MonoBehaviour
{
    [SerializeField] protected AudioMixer Mixer;
    [SerializeField] protected Mute Mute;

    protected Slider Slider;
    protected int ValueMultiplicationForMixerVolume = 20;

    private void Start()
    {
        Slider = GetComponent<Slider>();
    }
}
