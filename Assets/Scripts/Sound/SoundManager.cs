using UnityEngine;
using Utils;

namespace Sound
{
    public enum SoundGroup
    {
        Music,
        Game
    }
    
    public class SoundManager : Singleton<SoundManager>
    {
        private float _globalSoundLevel;

        public float GlobalSoundLevel
        {
            get { return _globalSoundLevel; }
            set { _globalSoundLevel = value; }
        }

        public float GetSoundLevel(SoundGroup group)
        {
            
        }

        public void SetSoundLevel(SoundGroup group, float value)
        {
            
        }

        public void PlayAudioAtPoint(AudioClip clip, SoundGroup group, Vector3 pos)
        {
            AudioSource source
        }

        public AudioSource GetAudioSource()
        {
            AudioSource source;
            
        }
    }
}