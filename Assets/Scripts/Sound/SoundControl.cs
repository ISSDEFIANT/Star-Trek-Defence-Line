using UnityEngine;
using UnityEngine.Audio;

namespace Sound
{
    /// <summary>
    /// This class initializes audio mixer with global audio volume. It also provides methods to control global audio volume
    /// </summary>
    /// <seealso cref="Assets/AudioMixer"/>
    public sealed class SoundControl : MonoBehaviour
    {
        public AudioMixer mixer;

        public delegate void VolumeChanged(float value);

        public event VolumeChanged OnMasterVolumeChanged = delegate {  };
        public event VolumeChanged OnMusicVolumeChanged = delegate {  };
        public event VolumeChanged OnSfxVolumeChanged = delegate { };
        public event VolumeChanged OnVoiceVolumeChanged = delegate { };

        public void SetMasterVolume(float value)
        {
            mixer.SetFloat("masterVolume", value);
            OnMasterVolumeChanged(value);
        }

        public void SetMusicVolume(float value)
        {
            mixer.SetFloat("musicVolume", value);
            OnMusicVolumeChanged(value);
        }

        public void SetSfxVolume(float value)
        {
            mixer.SetFloat("sfxVolume", value);
            OnSfxVolumeChanged(value);
        }

        public void SetVoiceVolume(float value)
        {
            mixer.SetFloat("voiceVolume", value);
            OnVoiceVolumeChanged(value);
        }

        private void Awake()
        {
            mixer.SetFloat("masterVolume", PlayerPrefs.GetFloat("sound_master", 1.0f));
            mixer.SetFloat("musicVolume", PlayerPrefs.GetFloat("sound_music", 1.0f));
            mixer.SetFloat("sfxVolume", PlayerPrefs.GetFloat("sound_sfx", 1.0f));
            mixer.SetFloat("voiceVolume", PlayerPrefs.GetFloat("sound_voice", 1.0f));

            OnSfxVolumeChanged += value => PlayerPrefs.SetFloat("sound_sfx", value);
            OnMusicVolumeChanged += value => PlayerPrefs.SetFloat("sound_music", value);
            OnMasterVolumeChanged += value => PlayerPrefs.SetFloat("sound_master", value);
            OnVoiceVolumeChanged += value => PlayerPrefs.SetFloat("sound_voice", value);
        }
    }
}