using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoPirata : MonoBehaviour
{
    public NivelManager nivelManager;
    public Animator animator;
    public Image enemigoIcono;
    private AudioSource audioSource; 

    void Start()
    {
        if (nivelManager == null)
        {
            nivelManager = FindObjectOfType<NivelManager>();
        }

        if (enemigoIcono != null)
        {
            enemigoIcono.gameObject.SetActive(true);
        }
        
        animator = GetComponent<Animator>();

        audioSource = GetComponent<AudioSource>(); 
    }


    public void explosionBomba()
    {
        animator.SetBool("morir", true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Jugador"))
        {

            Jugador vidaJugador = collision.collider.GetComponent<Jugador>();

            if (vidaJugador != null)
            {
                bool vidaQuitada = vidaJugador.QuitarVida();

                if (vidaQuitada)
                {
                    if (animator != null)
                    {
                        animator.SetTrigger("attack");
                    }
                }
            }
        }
    }

    public void destruir()
    {
        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(this.gameObject);

        if (enemigoIcono != null)
        {
            enemigoIcono.gameObject.SetActive(false);
        }

        nivelManager.EnemigoDerrotado();
        nivelManager.AgregarPuntos(50);
        AgregarPuntos();
    }

    private void AgregarPuntos()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.AgregarPuntos(50);
        }
    }
}
