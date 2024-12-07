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

    [SerializeField] private AudioClip[] soundList;
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
}
