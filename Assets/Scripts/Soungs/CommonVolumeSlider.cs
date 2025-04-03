using UnityEngine;
using UnityEngine.UI;

public class CommonVolumeSlider : VolumeSlider
{
    private const string Master = nameof(Master);

    public void SetCommonVolume()
    {
        if (Mute.IsMute == false)
            Mixer.SetFloat(Master, Mathf.Log10(Slider.value) * ValueMultiplicationForMixerVolume);
    }
}

