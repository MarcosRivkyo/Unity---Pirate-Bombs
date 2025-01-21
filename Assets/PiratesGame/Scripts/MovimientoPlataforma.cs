using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPlataforma : MonoBehaviour
{
    [SerializeField] protected Transform origen;
    [SerializeField] protected Transform destino;
    public float velocidad = 0.005f;
    protected bool invertir = false;
    protected Vector2 posicionInicial;
    protected Vector2 posicionOrigen;
    protected Vector2 posicionDestino;

    private void Start()
    {
        posicionInicial = transform.position;
        posicionOrigen = origen.position;
        posicionDestino = destino.position;
    }

    void OnDrawGizmos()
    {
        if (origen != destino)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(origen.position, destino.position);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (origen != destino)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(origen.position, 0.5f);
            Gizmos.DrawWireSphere(destino.position, 0.5f);
        }
    }

    private void Update()
    {
        if (!invertir)
        {
            transform.position = Vector2.MoveTowards(transform.position, posicionDestino, velocidad);
            if (Mathf.Abs(transform.position.x - posicionDestino.x) < 0.1f)
                invertir = true;
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, posicionOrigen, velocidad);
            if (Mathf.Abs(transform.position.x - posicionOrigen.x) < 0.1f)
                invertir = false;
        }
    }
}
