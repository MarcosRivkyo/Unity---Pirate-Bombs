using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaidaMortal : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            Jugador jugador = other.GetComponent<Jugador>();

            if (jugador != null)
            {
                jugador.StartCoroutine(jugador.EsperarYDestruir());
            }
            else
            {
                Debug.LogError("No se encontró el script Jugador en el objeto.");
            }
        }
    }
}
