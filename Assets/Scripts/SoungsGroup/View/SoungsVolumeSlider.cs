using UnityEngine;
using UnityEngine.UI;

namespace SoungsGroup
{
    public class SoungsVolumeSlider : VolumeSlider
    {
        private const string Soungs = nameof(Soungs);

        public void SetSoungsVolume()
        {
            Mixer.SetFloat(Soungs, Mathf.Log10(Slider.value) * ValueMultiplicationForMixerVolume);
        }
    }
}