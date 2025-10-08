using UnityEngine;
using UnityEngine.Audio;

namespace DevPeixoto.UI.GlobalUiEvents
{
    [CreateAssetMenu(fileName = "Audio Preset", menuName = "DevPeixoto/Global UI Events/Audio Preset")]
    public class AudioPresetSO : ScriptableObject
    {
        [SerializeField] AudioMixerGroup _mixer;
        public AudioClip SelectAudio;
        public AudioClip NoNextAudio;
        public AudioClip ClickAudio;
        public AudioClip ClickDisabledAudio;
        public AudioClip SlideAudio;

        public AudioMixerGroup Mixer { get => _mixer; }
    }
}