using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DevPeixoto.UI.GlobalUiEvents
{
    [AddComponentMenu("DevPeixoto/Global UI Events/Overriders/Slider Event Overrider")]
    [RequireComponent(typeof(Slider))]
    public class SliderEventOverrider : SelectableEventOverrider
    {
        public UnityEvent onMove;

        public bool overrideSlideAudio = true;
        public AudioClip slideAudioClip;
    }
}