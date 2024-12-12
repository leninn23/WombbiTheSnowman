using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public PlayerController player;
    public int puntosVidaPre;
    public int puntosVidaPost;
    public int puntosTotales;
    public TextMeshProUGUI vidas;
    public TextMeshProUGUI muni;
    public Transform mask;

    private float minY = -3.2f; // Posici�n m�nima vertical
    private float maxY = -1.21f; // Posici�n m�xima vertical
    private float minHealth = 1f; // Vida m�nima
    private float maxHealth = 30f; // Vida m�xima

    // Start is called before the first frame update
    void Start()
    {
        puntosVidaPre = (int)player.initialHealth;
        mask.localPosition = Vector3.zero;
        puntosTotales = player.vidas;

        /*TextMeshPro text = this.transform.Find("Munici�n").GetComponent<TextMeshPro>();
        if (text == null)
        {
            Debug.LogError("No se encontr� TextMeshPro en el objeto especificado.");
        }*/
    }
    //1.24
    //3.2
    // Update is called once per frame
    void Update()
    {
        puntosVidaPost = (int)player.initialHealth;
        float diferencia = puntosVidaPost - puntosVidaPre;
        //text.SetText(puntosVida.ToString());
        /*float newY = Mathf.Lerp(minY, maxY, (puntosVida - minHealth) / (maxHealth - minHealth));
        mask.position = new Vector3(mask.position.x, newY, mask.position.z);
        Debug.Log("Posici�n: " + mask.position);*/

        float cambio = diferencia * ((maxY - minY) / (maxHealth - minHealth));
        //mask.position += new Vector3(0, cambio, 0);
        //mask.anchoredPosition += new Vector2(0, cambio); // Cambia solo la posici�n en el Canvas
        //mask.localPosition = new Vector3(mask.localPosition.x, cambio, mask.localPosition.z);
        Debug.Log("Posici�n: " + mask.localPosition);

        puntosVidaPre = puntosVidaPost;




        muni.SetText(puntosVidaPost.ToString());

        puntosTotales = player.vidas;
        vidas.SetText(puntosTotales.ToString());

    }
}
