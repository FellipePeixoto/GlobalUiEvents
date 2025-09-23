using UnityEngine;
using UnityEngine.Audio;

namespace DevPeixoto.UI.GlobalUiEvents
{
    public class AudioSrcPool : MonoBehaviour
    {
        static AudioSrcPool instance;

        [SerializeField] int audioPoolSize = 15;
        [SerializeField] AudioMixerGroup mixer;
        
        AudioSource[] _pooledAudionSource;

        public static AudioSrcPool Instance 
        { 
            get 
            { 
                if (instance == null)
                {
                    instance = new GameObject("AudioSrcPool", typeof(AudioSrcPool)).GetComponent<AudioSrcPool>();
                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance; 
            } 
        }

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Init();
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Init()
        {
            _pooledAudionSource = new AudioSource[audioPoolSize];
            for (int i = 0; i < _pooledAudionSource.Length; i++)
            {
                var audioSourceObj = new GameObject($"AudioPool_{i}", typeof(AudioSource));

                var audioSource = audioSourceObj.GetComponent<AudioSource>();
                audioSource.playOnAwake = false;
                audioSource.outputAudioMixerGroup = mixer;
                _pooledAudionSource[i] = audioSource;

                audioSource.transform.SetParent(transform);
            }
        }

        public void SetMixer(AudioMixerGroup mixer)
        {
            this.mixer = mixer;

            for (int i = 0; i < _pooledAudionSource.Length; i++)
            {
                _pooledAudionSource[i].outputAudioMixerGroup = mixer;
            }
        }

        public void PlayAudio(AudioClip audio)
        {
            bool played = false;
            for (int i = 0; !played && i < _pooledAudionSource.Length; i++)
            {
                var audioSrc = _pooledAudionSource[i];

                if (audioSrc.isPlaying)
                    continue;

                audioSrc.clip = audio;
                audioSrc.Play();
                played = true;
            }

            if (!played)
            {
                var audioSrc = _pooledAudionSource[0];
                audioSrc.Stop();

                audioSrc.clip = audio;
                audioSrc.Play();
            }

        }
    }
}