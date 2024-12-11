using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class levelChange : MonoBehaviour
{
    public string nextLevel;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica si el objeto que entra al trigger tiene el tag "Player"
        if (other.CompareTag("Player"))
        {
            Debug.Log("Jugador ha alcanzado el punto de cambio de nivel.");
            ChangeLevel();
        }
    }

    private void ChangeLevel()
    {
        // Aseg�rate de que el nombre del nivel est� correctamente escrito
        if (!string.IsNullOrEmpty(nextLevel))
        {
            Debug.Log("Estamos aqu�");
            SceneManager.LoadScene(nextLevel);
        }
        else
        {
            Debug.LogError("El nombre del siguiente nivel no est� configurado.");
        }

    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
