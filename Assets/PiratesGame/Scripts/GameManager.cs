using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; 

    public int vidas;
    public int puntuacion;

    public TMP_Text puntuacionText;

    public bool nivel1Completado = false;
    public bool nivel2Completado = false;
    public bool nivel3Completado = false;

    public bool puerta1Abierta = true;
    public bool puerta2Abierta = false;
    public bool puerta3Abierta = false;


    public GameObject PuertaNivel1;
    public GameObject PuertaNivel2;
    public GameObject PuertaNivel3;

    public int ultimoNivelCompletado = -1;


    public float tiempoJugado;   
    public bool temporizadorActivo;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            PlayerPrefs.SetInt("vidas", 3);
            PlayerPrefs.SetInt("nivel1Completado", 0);
            PlayerPrefs.SetInt("nivel2Completado", 0);
            PlayerPrefs.SetInt("nivel3Completado", 0);
            PlayerPrefs.SetInt("puerta1Abierta", 1);
            PlayerPrefs.SetInt("puerta2Abierta", 0);
            PlayerPrefs.SetInt("puerta3Abierta", 0);
            
            PlayerPrefs.SetInt("puntuacion", 0);

            if (puntuacionText != null)
                ActualizarPuntuacionUI();

            PlayerPrefs.Save();
            CargarNivelesCompletados();

            tiempoJugado = 0f;
            temporizadorActivo = true;

            StartCoroutine(Temporizador());

        }
        else
        {
            Destroy(gameObject); 

            CargarVidas();
            puntuacion = PlayerPrefs.GetInt("puntuacion", 0); 
            ActualizarPuntuacionUI();
            CargarNivelesCompletados();
        }
    }


    private IEnumerator Temporizador()
    {
        Debug.Log("Comienza el temporizador: " + tiempoJugado);

        while (temporizadorActivo)
        {
            tiempoJugado += Time.deltaTime;


            yield return null;
        }
    }


    public void DetenerTemporizador()
    {
        temporizadorActivo = false;
        PlayerPrefs.SetFloat("tiempoJugado", tiempoJugado);
        PlayerPrefs.Save();
    }

    private string FormatearTiempo()
    {
        int minutos = Mathf.FloorToInt(tiempoJugado / 60);
        int segundos = Mathf.FloorToInt(tiempoJugado % 60);
        return string.Format("{0:00}:{1:00}", minutos, segundos);
    }

    public void RegistrarVidas(int vidasJugador)
    {
        vidas = vidasJugador;
        PlayerPrefs.SetInt("vidas", vidas);  
        PlayerPrefs.Save(); 
    }


    public int CargarVidas()
    {
        if (PlayerPrefs.HasKey("vidas"))
        {
            vidas = PlayerPrefs.GetInt("vidas");  
            Debug.Log("Vidas cargadas desde PlayerPrefs: " + vidas);
            return vidas;
        }
        return 0;
    }

    public void CargarNivelesCompletados()
    {
        nivel1Completado = PlayerPrefs.GetInt("nivel1Completado", 0) == 1;
        nivel2Completado = PlayerPrefs.GetInt("nivel2Completado", 0) == 1;
        nivel3Completado = PlayerPrefs.GetInt("nivel3Completado", 0) == 1;

        Debug.Log("Niveles cargados: Nivel 1: " + nivel1Completado + ", Nivel 2: " + nivel2Completado + ", Nivel 3: " + nivel3Completado);
        
        if (nivel1Completado && nivel2Completado && nivel3Completado)
        {
            Debug.Log("Todos los niveles están completados. gg");
        }

    }


    public void NivelCompletado(int nivel)
    {
        switch (nivel)
        {
            case 1:
                nivel1Completado = true;
                puerta2Abierta = true; 
                PlayerPrefs.SetInt("nivel1Completado", 1);
                PlayerPrefs.SetInt("puerta2Abierta", 1);
                SetUltimoNivelCompletado(1);
                break;
            case 2:
                nivel2Completado = true;
                puerta3Abierta = true; 
                PlayerPrefs.SetInt("nivel2Completado", 1);
                PlayerPrefs.SetInt("puerta3Abierta", 1);
                SetUltimoNivelCompletado(2);
                break;
            case 3:
                nivel3Completado = true;
                PlayerPrefs.SetInt("nivel3Completado", 1);
                SetUltimoNivelCompletado(3);
                break;
            default:
                Debug.LogError("Nivel no válido: " + nivel);
                break;
        }

        PlayerPrefs.Save();
    }


    public bool EsNivelCompletado(int nivel)
    {
        switch (nivel)
        {
            case 1:
                return nivel1Completado;
            case 2:
                return nivel2Completado;
            case 3:
                return nivel3Completado;
            default:
                Debug.LogError("Nivel no válido: " + nivel);
                return false;
        }
    }

    public bool EsAbierta(int puerta)
    {
        switch (puerta)
        {
            case 1:
                return puerta1Abierta;
            case 2:
                return puerta2Abierta;
            case 3:
                return puerta3Abierta;
            default:
                Debug.LogError("Nivel no válido: " + puerta);
                return false;
        }
    }

    public void ActualizarPuntuacionUI()
    {
        if (puntuacionText != null)
            puntuacionText.text = "Puntuación: " + puntuacion.ToString();
    }

    public void AgregarPuntos(int puntos)
    {
        puntuacion += puntos;
        PlayerPrefs.SetInt("puntuacion", puntuacion); // Guarda la puntuación en PlayerPrefs
        PlayerPrefs.Save();
        ActualizarPuntuacionUI();
    }

    public void SetUltimoNivelCompletado(int nivel)
    {
        ultimoNivelCompletado = nivel;
        PlayerPrefs.SetInt("ultimoNivelCompletado", ultimoNivelCompletado);
        PlayerPrefs.Save();
        Debug.LogError("últmo nivel: " + ultimoNivelCompletado);

    }

    public void ReiniciarProgreso()
    {
        vidas = 3;
        puntuacion = 0;
        PlayerPrefs.SetInt("vidas", vidas); 
        PlayerPrefs.SetInt("puntuacion", puntuacion); 
        PlayerPrefs.SetInt("nivel1Completado", 0); 
        PlayerPrefs.SetInt("nivel2Completado", 0); 
        PlayerPrefs.SetInt("nivel3Completado", 0); 
        PlayerPrefs.SetInt("puerta1Abierta", 1);
        PlayerPrefs.SetInt("puerta2Abierta", 0);
        PlayerPrefs.SetInt("puerta3Abierta", 0); 
        PlayerPrefs.SetInt("ultimoNivelCompletado", -1); 

        PlayerPrefs.Save();

        CargarNivelesCompletados();

        if (puntuacionText != null)
        {
            puntuacion = 0; 
            ActualizarPuntuacionUI();
        }

        puerta1Abierta = true; 
        puerta2Abierta = false;
        puerta3Abierta = false;
    }

}
