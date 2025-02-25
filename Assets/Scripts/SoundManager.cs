using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    [SerializeField]
    private SoundLibrary sfxLibrary;
    [SerializeField] private AudioSource sfx2DSource;
    [SerializeField] private AudioSource boostAudioSource;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    public void PlaySound3D(AudioClip clip, Vector3 pos)
    {
        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, pos);
        }
    }

    public void PlaySound3D(string soundName, Vector3 pos)
    {
        PlaySound3D(sfxLibrary.GetClipFromName(soundName), pos);
    }

    public void PlaySound2D(string soundName)
    {
        sfx2DSource.PlayOneShot(sfxLibrary.GetClipFromName(soundName));
    }
    public void PlayLoopingSound(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null && sfx2DSource.clip != clip)
        {
          sfx2DSource.clip = clip;
            sfx2DSource.Play();
        }
    }

    public void StopLoopingSound()
    {
        if (sfx2DSource.isPlaying)
        {
            sfx2DSource.Stop();
           sfx2DSource.clip = null;
        }
    }
    public void PlayLoopingBoostSound(string soundName)
    {
        AudioClip clip = sfxLibrary.GetClipFromName(soundName);
        if (clip != null && boostAudioSource.clip != clip)
        {
            boostAudioSource.clip = clip;
            boostAudioSource.loop = true;
            boostAudioSource.Play();
        }
    }

    public void StopLoopingBoostSound()
    {
        if (boostAudioSource.isPlaying)
        {
            boostAudioSource.Stop();
            boostAudioSource.clip = null;
        }
    }
}

