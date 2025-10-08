using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DevPeixoto.UI.GlobalUiEvents
{
    [AddComponentMenu("DevPeixoto/Global UI Events/Overriders/Selectable Event Overrider")]
    [RequireComponent(typeof(Selectable))]
    public class SelectableEventOverrider : MonoBehaviour
    {
        public bool overrideSelectEvent = true;
        public UnityEvent onSelect;

        public bool overrideSelectAudio = true;
        public AudioClip selectAudioClip;

        public bool overrideClickAudio = true;
        public AudioClip clickAudioClip;

        public bool overrideClickDisabledAudio = true;
        public AudioClip clickDisabledAudioClip;
    }
}