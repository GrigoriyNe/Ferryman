using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace SoungsGroup
{
    public class Mute : MonoBehaviour
    {
        private const string Master = nameof(Master);

        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Button _button;

        private bool _isMute = false;
        private int _minVolume = -60;
        private int _maxVolume = 0;

        public bool IsMute => _isMute;

        public void OnEnable()
        {
            _button.onClick.AddListener(OnValueChanged);
        }

        private void OnDisable()
        {
            _button.onClick.AddListener(OnValueChanged);
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                _mixer.SetFloat(Master, _minVolume);
            }
            else
            {
                if (_isMute != true)
                    _mixer.SetFloat(Master, _maxVolume);
            }
        }

        public void OnValueChanged()
        {
            _isMute = !_isMute;
            _mixer.SetFloat(Master, _maxVolume);

            if (_isMute)
                _mixer.SetFloat(Master, _minVolume);
        }
    }
}