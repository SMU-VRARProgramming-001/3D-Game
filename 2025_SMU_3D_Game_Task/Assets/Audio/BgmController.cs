using UnityEngine;

public class BgmController : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource source;

    [Header("Clips")]
    public AudioClip gameStart;
    public AudioClip cutsceneBat;
    public AudioClip cutsceneMush;
    public AudioClip cutsceneBoss;

    private AudioClip current;

    void Reset()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayGameStart()
    {
        Play(gameStart, true);
    }

    public void PlayCutsceneBat()
    {
        Play(cutsceneBat, true);
    }

    public void PlayCutsceneMush()
    {
        Play(cutsceneMush, true);
    }

    public void PlayCutsceneBoss()
    {
        Play(cutsceneBoss, true);
    }

    void Play(AudioClip clip, bool loop)
    {
        if (source == null || clip == null) return;
        if (current == clip && source.isPlaying) return; // 중복재생 방지

        current = clip;
        source.loop = loop;
        source.clip = clip;
        source.Play();
    }
}