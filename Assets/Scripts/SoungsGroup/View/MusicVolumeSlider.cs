using UnityEngine;
using UnityEngine.UI;

namespace SoungsGroup
{
    public class MusicVolumeSlider : VolumeSlider
    {
        private const string Music = nameof(Music);

        public void SetMusicVolume()
        {
            Mixer.SetFloat(Music, Mathf.Log10(Slider.value) * ValueMultiplicationForMixerVolume);
        }
    }
}
