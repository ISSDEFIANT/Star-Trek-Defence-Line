using System;
using UnityEngine;
using Utils;

namespace Settings
{
    /// <summary>
    /// This singleton is used for store all game settings(like sound volume, graphics level, etc)
    /// </summary>
    public class Settings : Singleton<Settings>
    {
        private GraphicsMode _graphicsMode;
        private float _soundLevel;

        public GraphicsMode Graphics
        {
            get { return _graphicsMode; }
            set
            {
                _graphicsMode = value;
                if (GraphicsChanged != null) GraphicsChanged(value);
            }
        }

        public float SoundLevel
        {
            get { return _soundLevel; }
            set
            {
                _soundLevel = value;
                if (SoundLevelChanged != null) SoundLevelChanged(value);
            }
        }

        public delegate void GraphicsChangedDelegate(GraphicsMode graphics);

        public delegate void SoundLevelChangedDelegate(float level);

        public event GraphicsChangedDelegate GraphicsChanged;
        public event SoundLevelChangedDelegate SoundLevelChanged;

        private void Awake()
        {
            _graphicsMode = (GraphicsMode) PlayerPrefs.GetInt("Graphic", QualitySettings.GetQualityLevel());
            _soundLevel = PlayerPrefs.GetFloat("Sound", AudioListener.volume);
            if (_graphicsMode != (GraphicsMode) QualitySettings.GetQualityLevel())
                QualitySettings.SetQualityLevel((int) _graphicsMode, true);
            if (Math.Abs(_soundLevel - AudioListener.volume) > 0.001)
                AudioListener.volume = _soundLevel;
            SoundLevelChanged += level =>
            {
                PlayerPrefs.SetFloat("Sound", level);
                PlayerPrefs.Save();
            };
            SoundLevelChanged += level => AudioListener.volume = level;
            GraphicsChanged += graphics =>
            {
                PlayerPrefs.SetInt("Graphic", (int) graphics);
                PlayerPrefs.Save();
            };
            GraphicsChanged += graphics => QualitySettings.SetQualityLevel((int) graphics, true);
        }

        public enum GraphicsMode
        {
            Fastest,
            Faster,
            Simple,
            Good,
            Beautiful,
            Fantastic
        }
    }
}