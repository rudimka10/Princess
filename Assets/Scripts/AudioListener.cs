using Controllers.Sound;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Audio
{
    [RequireComponent(typeof(Button))]
    public class AudioListener : MonoBehaviour
    {
        [SerializeField] private AudioClip _audioClip;
        private Button _button;
        
        private void Awake()
        {
            _button = gameObject.GetComponent<Button>();
            _button.onClick.AddListener(Play);
        }

        public void Play()
        {
            SoundController.Instance.PlaySound(_audioClip);
        }

    }
}