using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using TMPro;

public class EscenaFinalManager : MonoBehaviour
{
    public TMP_Text puntuacionFinalText;
    public TMP_Text tiempoFinalText;
    public string siguienteEscena = "MainMenu";

    void Start()
    {
        if (GameManager.instance != null)
        {
            int puntuacionFinal = GameManager.instance.puntuacion;

            if (puntuacionFinalText != null)
            {
                puntuacionFinalText.text = "Puntuación Final = " + puntuacionFinal.ToString();
            }
            else
            {
                Debug.LogError("El campo puntuacionFinalText no está asignado en el inspector.");
            }


            if (GameManager.instance.temporizadorActivo)
            {
                GameManager.instance.DetenerTemporizador();
            }

            float tiempoFinal = GameManager.instance.tiempoJugado; 

            if (tiempoFinalText != null)
            {
                tiempoFinalText.text = "Tiempo Jugado = " + FormatearTiempo(tiempoFinal);
            }
            else
            {
                Debug.LogError("El campo tiempoFinalText no está asignado en el inspector.");
            }
        }
        else
        {
            Debug.LogError("GameManager no encontrado.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            if (!string.IsNullOrEmpty(siguienteEscena))
            {
                SceneManager.LoadScene(siguienteEscena);
            }
            else
            {
                Debug.LogError("No se ha asignado un nombre de escena en el campo 'siguienteEscena'.");
            }
        }
    }

    private string FormatearTiempo(float tiempo)
    {
        int minutos = Mathf.FloorToInt(tiempo / 60);
        int segundos = Mathf.FloorToInt(tiempo % 60);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }
}
