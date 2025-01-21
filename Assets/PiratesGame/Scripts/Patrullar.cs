using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;

    private int numeroAleatorio;
    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private Vector3 posicionAnterior;

    void Start()
    {
        numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        posicionAnterior = transform.position; 
        Girar();
    }

    void Update()
    {
        if (spriteRenderer.isVisible)
        {
            transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);

            float velocidadActual = (transform.position - posicionAnterior).magnitude / Time.deltaTime;

            animator.SetFloat("Velocidad", velocidadActual);

            posicionAnterior = transform.position;

            if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
            {
                numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
                Girar();
            }
        }
        else
        {
            animator.SetFloat("Velocidad", 0);
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
    }
}


