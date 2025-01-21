using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NivelManager : MonoBehaviour
{
    public GameObject llave;
    public PuertaSalida puertaSalida;
    public TMP_Text textoPuntuacion;
    public TMP_Text textoInstruccion; 
    public int enemigosRestantes;
    private int puntuacion = 0;
    public Camera mainCamera;
    public Transform puntoDeEnfoque;
    public float velocidadDeMovimiento = 0.5f;

    private void Start()
    {
        if (GameManager.instance != null)
        {
            puntuacion = GameManager.instance.puntuacion;


        }
        else
        {
            Debug.LogError("GameManager no encontrado.");
        }

        llave.SetActive(false);
        ActualizarPuntuacionUI();
        ActualizarInstruccionUI("Derrota a todos los enemigos para obtener la llave.");
    }


    public void EnemigoDerrotado()
    {
        enemigosRestantes--;

        if (enemigosRestantes <= 0)
        {
            llave.SetActive(true);
            ActualizarInstruccionUI("¡Todos los enemigos han sido derrotados! La llave ha aparecido.");
        }
    }

    public void AgregarPuntos(int puntos)
    {
        puntuacion += puntos; 
        ActualizarPuntuacionUI(); 
    }

    private void ActualizarPuntuacionUI()
    {
        if (textoPuntuacion != null)
            textoPuntuacion.text = "Puntuación: " + puntuacion.ToString();
    }

    private void ActualizarInstruccionUI(string mensaje)
    {
        if (textoInstruccion != null)
            textoInstruccion.text = mensaje;
    }

    public void RecogerLlave()
    {
        Debug.Log("Llave recogida");
    }


}
