using UnityEngine;
namespace MindMatch.Gameplay
{
    public enum GameAudioType
    {
        ButtonClick,
        CardClick,
        CardFlip,
        MatchSuccess,
        BackgroundMusic
    }


    [CreateAssetMenu(fileName = "AudioData", menuName = "MindMatch/AudioData", order = 1)]
    public class AudioData : ScriptableObject
    {
        [System.Serializable]
        public struct AudioClipEntry
        {
            public GameAudioType type;
            public AudioClip clip;
            public float volume;
        }

        public AudioClipEntry[] audioClips;

        public AudioClip GetClip(GameAudioType type)
        {
            foreach (var entry in audioClips)
            {
                if (entry.type == type)
                    return entry.clip;
            }
            return null;
        }

        public float GetVolume(GameAudioType type)
        {
            foreach (var entry in audioClips)
            {
                if (entry.type == type)
                    return entry.volume;
            }
            return 1f;
        }
    }
}