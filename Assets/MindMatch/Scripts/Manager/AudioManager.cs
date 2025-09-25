using UnityEngine;

[DefaultExecutionOrder(-100)]

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    [SerializeField] private AudioData audioData;
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlayAudio(AudioType type)
    {
        AudioClip clip = audioData.GetClip(type);
        if (clip == null) return;

        float volume = audioData.GetVolume(type);

        if (type == AudioType.BackgroundMusic)
        {
            if (!musicSource.isPlaying || musicSource.clip != clip)
            {
                musicSource.clip = clip;
                musicSource.volume = volume;
                musicSource.loop = true;
                musicSource.Play();
            }
        }
        else
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
