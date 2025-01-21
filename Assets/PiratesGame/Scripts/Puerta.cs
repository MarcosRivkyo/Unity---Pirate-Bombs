using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class Puerta : MonoBehaviour
{
    private Animator animator;
    private bool jugadorCerca = false;
    public string nombreEscenaDestino;
    public Animator jugadorAnimator;
    private bool puertaInteractuable = false;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (nombreEscenaDestino == "EscenaNivel1")
        {
            if (!GameManager.instance.EsNivelCompletado(1))
            {
                puertaInteractuable = true;  
            }
            if (animator != null) animator.SetBool("Abierta", true);

        }
        else if (nombreEscenaDestino == "EscenaNivel2")
        {
            if (GameManager.instance.EsAbierta(2) && !GameManager.instance.EsNivelCompletado(2))
            {
                puertaInteractuable = true;  
                if (animator != null) animator.SetBool("Abierta", true);
            }
            if (GameManager.instance.EsAbierta(2))
            {
                if (animator != null) animator.SetBool("Abierta", true);

            }
        }
        else if (nombreEscenaDestino == "EscenaNivel3")
        {
            if (GameManager.instance.EsAbierta(3) && !GameManager.instance.EsNivelCompletado(3))
            {
                puertaInteractuable = true;  
                if (animator != null) animator.SetBool("Abierta", true);
            }
            if (GameManager.instance.EsAbierta(3))
            {
                if (animator != null) animator.SetBool("Abierta", true);

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador") && puertaInteractuable)
        {
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            jugadorCerca = false;
        }
    }

    void Update()
    {
        if (jugadorCerca && puertaInteractuable && Input.GetKeyDown(KeyCode.W))
        {
            jugadorAnimator.SetTrigger("entrarPuerta");
            StartCoroutine(CambiarEscenaConRetraso());
        }
    }

    IEnumerator CambiarEscenaConRetraso()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nombreEscenaDestino);
    }
}
