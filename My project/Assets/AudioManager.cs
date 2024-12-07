using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("-----Audio Manager-----")]
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
    public AudioClip boton;

    private void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
