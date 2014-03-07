using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip FailedClip;
    public AudioClip SuccessClip;
    public AudioClip WinClip;
    public AudioClip LoseClip;

    public static SoundManager Instance;

    void Start()
    {
        Instance = this;
    }

    public void PlaySoundClip(AudioClip audioClip)
    {
        audio.Stop();
        audio.PlayOneShot(audioClip);
    }
}
