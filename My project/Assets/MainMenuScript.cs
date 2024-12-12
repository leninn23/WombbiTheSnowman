using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;


public class MainMenuScript : MonoBehaviour
{
    private bool isPaused = false;
    public GameObject pausa;
    public void PlayGame()
    {
        AudioManager.PlaySound(SoundType.BOTON);
        SceneManager.LoadScene("SampleScene"); 
    }

    void Update()
    {
        // Detectar tecla de pausa (por defecto Escape)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        isPaused = true;
        AudioManager.PlaySound(SoundType.BOTON);
        pausa.SetActive(true); // Mostrar el menú de pausa
        Time.timeScale = 0f; // Detener el tiempo del juego
    }

    public void ResumeGame()
    {
        isPaused = false;
        AudioManager.PlaySound(SoundType.BOTON);
        pausa.SetActive(false); // Ocultar el menú de pausa
        Time.timeScale = 1f; // Reanudar el tiempo del juego
    }

    public void QuitGame()
    {
        // Salir del juego
        Debug.Log("Quit");
        Application.Quit();
    }

    public void Retry()
    {
        AudioManager.PlaySound(SoundType.BOTON);
        SceneManager.LoadScene("SampleScene");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1f;
        AudioManager.PlaySound(SoundType.BOTON);
        SceneManager.LoadScene("MainMenu");

    }
}
