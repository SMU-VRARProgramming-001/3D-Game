using UnityEngine;

public class BgmController : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource source;

    [Header("Scene BGM Clips")]
    public AudioClip gameStart;
    public AudioClip village;
    public AudioClip battleBat;
    public AudioClip battleMush;
    public AudioClip battleBoss;
    public AudioClip ending;
    public AudioClip gameOver;

    [Header("Cutscene Clips")]
    public AudioClip cutsceneBat;
    public AudioClip cutsceneMush;
    public AudioClip cutsceneBoss;

    private AudioClip current;

    public static BgmController Instance;

    void Reset()
    {
        source = GetComponent<AudioSource>();
    }

    // ===== Scene BGM =====
    public void PlayGameStart() => Play(gameStart, true);
    public void PlayVillage() => Play(village, true);
    public void PlayBattleBat() => Play(battleBat, true);
    public void PlayBattleMush() => Play(battleMush, true);
    public void PlayBattleBoss() => Play(battleBoss, true);
    public void PlayEnding() => Play(ending, true);
    public void PlayGameOver() => Play(gameOver, true);

    // ===== Cutscene BGM =====
    public void PlayCutsceneBat() => Play(cutsceneBat, true);
    public void PlayCutsceneMush() => Play(cutsceneMush, true);
    public void PlayCutsceneBoss() => Play(cutsceneBoss, true);

    // ===== Core =====
    void Play(AudioClip clip, bool loop)
    {
        if (source == null || clip == null) return;
        if (current == clip && source.isPlaying) return; // Prevent Audio duplicate

        current = clip;
        source.loop = loop;
        source.clip = clip;
        source.Play();
    }

    public void StopBgm()
    {
        if (source == null) return;
        source.Stop();
        current = null;
    }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (source == null) source = GetComponent<AudioSource>();
    }
}