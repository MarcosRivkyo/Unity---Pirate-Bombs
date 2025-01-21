using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuertaSalida : MonoBehaviour
{
    private Animator animator;
    private bool jugadorCerca = false;
    private bool llaveRecogida = false;
    public string nombreEscenaDestino;
    public int nivelActual;
    public Animator jugadorAnimator;
    public Animator puertaAnimator; 

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void RecogerLlave()
    {
        llaveRecogida = true; 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador") && llaveRecogida)
        {
            animator.SetBool("Abierta", true);
            jugadorCerca = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Jugador"))
        {
            animator.SetBool("Abierta", false);
            jugadorCerca = false;
        }
    }

    void Update()
    {
        if (jugadorCerca && Input.GetKeyDown(KeyCode.W) && llaveRecogida)
        {
            jugadorAnimator.SetTrigger("entrarPuerta");

            if(nivelActual == 1)
            {
                GameManager.instance.NivelCompletado(1);
                StartCoroutine(CambiarEscenaConRetraso());

            }
            if (nivelActual == 2)
            {
                GameManager.instance.NivelCompletado(2);
                StartCoroutine(CambiarEscenaConRetraso());

            }
            if (nivelActual == 3)
            {
                GameManager.instance.NivelCompletado(3);
                StartCoroutine(CambiarEscenaConRetraso());

            }
        }
    }

    public void AnimarPuerta()
    {
        if (animator != null)
        {
            animator.SetBool("Abierta", true); 
        }
    }

    IEnumerator CambiarEscenaConRetraso()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(nombreEscenaDestino);
    }
}
