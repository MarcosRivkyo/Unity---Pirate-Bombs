using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomba : MonoBehaviour
{
    public float radio;

    void Start()
    {

    }
    void Update()
    {

    }
    public void destruir()
    {
        Collider2D[] collider2D = Physics2D.OverlapCircleAll(transform.position, radio);

        for (int i = 0; i < collider2D.Length; i++)
        {
            if (collider2D[i].gameObject.tag == "Enemigo")
            {
                EnemigoPirata enemigoPirata = collider2D[i].gameObject.GetComponent<EnemigoPirata>();

                enemigoPirata.explosionBomba();


            }


            if (collider2D[i].gameObject.tag == "Jugador")
            {
                Jugador vidaJugador = collider2D[i].gameObject.GetComponent<Jugador>();


                if (vidaJugador != null)
                {
                    vidaJugador.QuitarVida();
                }
            }
            Destroy(this.gameObject);
        }
    }
}
