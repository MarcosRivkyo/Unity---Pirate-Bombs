using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioSource audioSource;
    public Slider volumenSlider;


    void Start()
    {
        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager no está inicializado.");
        }


        if(volumenSlider != null)
        {
            volumenSlider.value = audioSource.volume;

            volumenSlider.onValueChanged.AddListener(CambiarVolumen);
        }

    }
    void Update()
    {
        
    }

    public void CambiarVolumen(float nuevoVolumen)
    {
        audioSource.volume = nuevoVolumen;
    }

    public void EscenaJuego()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void EscenaAjustes()
    {
        SceneManager.LoadScene("Ajustes");
    }


    public void EscenaMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ReiniciarNiveles()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.ReiniciarProgreso();  
        }

        SceneManager.LoadScene("Lobby");
    }


    public void Salir()
    {
        Application.Quit();
    }
}
