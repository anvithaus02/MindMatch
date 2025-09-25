using UnityEngine;

public enum AudioType
{
    ButtonClick,
    CardClick,
    CardFlip,
    MatchSuccess,
    LevelComplete,
    BackgroundMusic
}


[CreateAssetMenu(fileName = "AudioData", menuName = "MindMatch/AudioData", order = 1)]
public class AudioData : ScriptableObject
{
    [System.Serializable]
    public struct AudioClipEntry
    {
        public AudioType type;
        public AudioClip clip;
        public float volume;
    }

    public AudioClipEntry[] audioClips;

    public AudioClip GetClip(AudioType type)
    {
        foreach (var entry in audioClips)
        {
            if (entry.type == type)
                return entry.clip;
        }
        Debug.LogWarning($"Audio clip for {type} not found!");
        return null;
    }

    public float GetVolume(AudioType type)
    {
        foreach (var entry in audioClips)
        {
            if (entry.type == type)
                return entry.volume;
        }
        return 1f;
    }
}
