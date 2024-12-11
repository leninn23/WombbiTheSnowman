/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    LANZAR_NIEVE,
    LANZAR_FUEGO,
    PRENDER,
    RODAR_BOLA,
    SALTAR,
    PASOS,
    BOTON
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    /*[Header("-----Audio Manager-----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SFXSource;

    [Header("-----Audio Clip-----")]
    public AudioClip background;
    public AudioClip paso;
    public AudioClip saltar;
    public AudioClip lanzar_nieve;
    public AudioClip lanzar_fuego;
    public AudioClip rodar;
    public AudioClip encender;
    public AudioClip boton;*/

/*    [SerializeField] private AudioClip[] soundList;
    private static AudioManager instance;
    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        //musicSource.clip = background;
        //musicSource.Play();
        audioSource = GetComponent<AudioSource>();
    }

    public static void PlaySound(SoundType sound, float volume = 1.0f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    /*public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SoundType
{
    LANZAR_NIEVE,
    LANZAR_FUEGO,
    PRENDER,
    RODAR_BOLA,
    SALTAR,
    PASOS,
    BOTON
}
public enum MusicType
{
    WOMBI,
}


[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    [SerializeField] private AudioClip[] musicList;
    private static AudioManager instance;
    public AudioSource audioSource;
    public AudioSource musicSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Mantener entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(MusicType.WOMBI);
    }

    public static void PlaySound(SoundType sound, float volume = 1.0f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    public static void PlayMusic(MusicType music, float transitionTime = 1.0f)
    {
        if (instance == null)
        {
            Debug.LogError("AudioManager instance is null!");
            return;
        }
        if (instance.musicSource == null)
        {
            Debug.LogError("AudioManager musicSource is null!");
            return;
        }
        if (instance == null || instance.musicSource == null) return;
        Debug.Log("Esto suena");
        instance.StartCoroutine(instance.TransitionToNewMusic(instance.musicList[(int)music], transitionTime));
    }

    public static void StopMusic(float fadeOutTime = 1.0f)
    {
        if (instance == null || instance.musicSource == null) return;

        instance.StartCoroutine(instance.FadeOutMusic(fadeOutTime));
    }

    private IEnumerator TransitionToNewMusic(AudioClip newMusic, float transitionTime)
    {
        // Si ya hay m�sica sonando, realiza un fade-out
        if (musicSource.isPlaying)
        {
            yield return StartCoroutine(FadeOutMusic(transitionTime));
        }

        // Cambiar la m�sica y hacer fade-in
        musicSource.clip = newMusic;
        musicSource.Play();
        yield return StartCoroutine(FadeInMusic(transitionTime));
    }

    private IEnumerator FadeOutMusic(float duration)
    {
        float startVolume = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVolume; // Restaurar volumen original para futuras m�sicas
    }

    private IEnumerator FadeInMusic(float duration)
    {
        float startVolume = 0.0f;
        musicSource.volume = startVolume;

        while (musicSource.volume < 0.5f) // Volumen m�ximo predeterminado
        {
            musicSource.volume += Time.deltaTime / duration;
            yield return null;
        }
    }
}
