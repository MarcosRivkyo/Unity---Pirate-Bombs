using System.Collections;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public Transform posicionNivel1;
    public Transform posicionNivel2;
    public Transform posicionNivel3;
    public Transform posicionDefault;

    public GameObject jugador;

    void OnEnable()
    {
        if (jugador == null)
        {
            Debug.Log("El jugador no está asignado en el script Spawn.");
            return;
        }

        StartCoroutine(PosicionarJugador());
    }

    IEnumerator PosicionarJugador()
    {

        if (GameManager.instance == null)
        {
            Debug.LogError("GameManager no está inicializado.");
            yield break;
        }

        int ultimoNivel = GameManager.instance.ultimoNivelCompletado;

        Debug.Log("Posicionando jugador según el último nivel completado: " + ultimoNivel);

        if (ultimoNivel == 1)
        {
            jugador.transform.position = posicionNivel1.position;
        }
        else if (ultimoNivel == 2)
        {
            jugador.transform.position = posicionNivel2.position;
        }
        else if (ultimoNivel == 3)
        {
            jugador.transform.position = posicionNivel3.position;
        }
        else
        {
            jugador.transform.position = posicionDefault.position;
        }
    }
}
