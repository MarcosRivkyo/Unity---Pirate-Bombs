using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaExtra : MonoBehaviour
{
    protected Animator animator;
    private AudioSource audioSource;

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Jugador"))
        {
            Jugador jugador = other.GetComponent<Jugador>();

            if (jugador != null)
            {
                if (audioSource != null)
                {
                    audioSource.Play();
                }
                animator.SetTrigger("Recogido");

                jugador.AumentarVida();



                Destroy(gameObject, 1f); 
            }
        }
    }
}
