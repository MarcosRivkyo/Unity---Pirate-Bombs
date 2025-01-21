using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Llave : MonoBehaviour
{
    public PuertaSalida puertaSalida;
    public GameObject plataforma; 
    public Camera mainCamera;
    public Transform puntoDeEnfoque;
    public float velocidadDeMovimiento = 1f;
    public NivelManager nivelManager;


    public AudioSource audioSource;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            nivelManager.RecogerLlave();

            gameObject.GetComponent<Renderer>().enabled = false;

            if (audioSource != null)
            {
                audioSource.Play();
            }

            StartCoroutine(EsperarYDestruir());


            puertaSalida.RecogerLlave();
            puertaSalida.AnimarPuerta();

            if (plataforma != null)
            {
                Plataforma plataformaScript = plataforma.GetComponent<Plataforma>();
                if (plataformaScript != null)
                {
                    plataformaScript.Desaparecer(); 
                }
            }

        }
    }



    private IEnumerator EsperarYDestruir()
    {
        yield return new WaitForSeconds(1.7f); 
        gameObject.SetActive(false);

    }

}
