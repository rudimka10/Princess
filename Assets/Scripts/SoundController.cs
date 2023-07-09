using System;
using UnityEngine;
using Utils.Singleton;


namespace Controllers.Sound
{
    public class SoundController : SingletonMonoBehaviour<SoundController>
    {
        [SerializeField] private AudioSource _audioSourceEmbient;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip _ambient;
        [SerializeField] private AudioClip _bossAmbient;
        
        private const string AudioValueKey = "Audio";
        private const string MusicValueKey = "Music";
        private const string SoundMutedKey = "SoundMuted";
        private const string MusicMutedKey = "MusicMuted";
        private const string VibroMutedKey = "Vibro";

        public event Action OnMusicValueChanged;
        public event Action OnAudioValueChanged;
        public event Action OnVibroValueChanged;

        private void Awake()
        {
            Debug.Log("Awake of controller");
            SetAudioVolume(GetAudioVolume());
            SetMusicVolume(GetMusicVolume());
            PlayEmbient();
            InitPrefs();
        }

        private static void InitPrefs()
        {
            if (!PlayerPrefs.HasKey(AudioValueKey))
                PlayerPrefs.SetFloat(AudioValueKey, 1);

            if (!PlayerPrefs.HasKey(MusicValueKey))
                PlayerPrefs.SetFloat(MusicValueKey, 1);

            if (!PlayerPrefs.HasKey(SoundMutedKey))
                PlayerPrefs.SetFloat(SoundMutedKey, 1);

            if (!PlayerPrefs.HasKey(MusicMutedKey))
                PlayerPrefs.SetFloat(MusicMutedKey, 1);

            if (!PlayerPrefs.HasKey(VibroMutedKey))
                PlayerPrefs.SetFloat(VibroMutedKey, 1);
        }

        public float GetAudioVolume()
        {
            return PlayerPrefs.GetFloat(AudioValueKey);
        }

        public float GetMusicVolume()
        {
            return PlayerPrefs.GetFloat(MusicValueKey);
        }
       
        public void SetAudioVolume(float value)
        {
            _audioSource.volume = value;
            PlayerPrefs.SetFloat(AudioValueKey, value);
            OnAudioValueChanged?.Invoke();
        }

        public void SetMusicVolume(float value)
        {
            _audioSourceEmbient.volume = value;
            PlayerPrefs.SetFloat(MusicValueKey, value);
            OnMusicValueChanged?.Invoke();
        }
        
        public void ChangeAudioState()
        {
            PlayerPrefs.SetInt(SoundMutedKey, !IsAudioMuted() ? 1 : 0);
            _audioSource.mute = IsAudioMuted();
            _audioSourceEmbient.mute = IsAudioMuted();
            OnMusicValueChanged?.Invoke();

        }
        
        public bool IsAudioMuted()
        {
            return PlayerPrefs.GetInt(SoundMutedKey) == 1;

        }
        
        public void ChangeVibroState()
        {
            PlayerPrefs.SetInt(VibroMutedKey, !IsVibroMuted() ? 1 : 0);
            OnVibroValueChanged?.Invoke();

        }

        public bool IsVibroMuted()
        {
            return PlayerPrefs.GetInt(VibroMutedKey) == 1;

        }
        
        public void PlaySound(AudioClip audioClip)
        {
            _audioSource.PlayOneShot(audioClip);
        }

        public void PlayRandomSoundOfType(AudioType audioType)
        {
            
        }
        
        public void PlayEmbient(bool isBoss = false)
        {
            _audioSourceEmbient.clip = isBoss ? _ambient : _bossAmbient;
            _audioSourceEmbient.Play();
        }

    }
}