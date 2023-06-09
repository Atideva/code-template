using UnityEngine;
using UnityEngine.Audio;
using Utilities.AudioSystem;

namespace SO.ConfigsSO
{
    [CreateAssetMenu(fileName = "Audio", menuName = "Config/Audio")]
    public class AudioSO : ScriptableObject
    {
        [Header("Mixers")]
        [SerializeField] AudioMixerGroup musicMixer;
        [SerializeField] AudioMixerGroup sfxMixer;
        [SerializeField] AudioMixerSnapshot normal;
        [SerializeField] AudioMixerSnapshot subdued;
        [SerializeField] AudioMixerSnapshot timeSlow;
        [SerializeField] AudioPrefab prefab;
        [SerializeField] float musicFadeTransitionMult = 0.6f;
        [Header("Keys")]
        [SerializeField] string soundKey = "VolumeSFX";
        [SerializeField] string musicKey = "VolumeMusic";
        public AudioMixerGroup MusicMixer => musicMixer;

        public AudioMixerGroup SfxMixer => sfxMixer;

        public AudioMixerSnapshot Normal => normal;

        public AudioMixerSnapshot TimeSlow => timeSlow;

        public AudioMixerSnapshot Subdued => subdued;

        public float MusicFadeTransitionMult => musicFadeTransitionMult;

        public AudioPrefab Prefab => prefab;

        public string MusicKey => musicKey;

        public string SoundKey => soundKey;
    }
}