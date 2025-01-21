using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camara2D : MonoBehaviour
{
    public GameObject gameObjectSeguir; 
    public float margenX = 1.5f;
    public float margenY = 1.5f; 
    public float suavidad = 0.1f; 

    private float currentVelocityX;
    private float currentVelocityY; 

    void Start()
    {
        
    }

    void Update()
    {
        moverCamara();
    }

    void moverCamara()
    {
        Vector2 vector2min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 vector2max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        float desplazamientoJugadorX = Mathf.Clamp(gameObjectSeguir.transform.position.x, vector2min.x + margenX, vector2max.x - margenX);
        float desplazamientoJugadorY = Mathf.Clamp(gameObjectSeguir.transform.position.y, vector2min.y + margenY, vector2max.y - margenY);

        Vector3 nuevaPosicion = new Vector3(desplazamientoJugadorX, desplazamientoJugadorY, transform.position.z);

        nuevaPosicion.x = Mathf.SmoothDamp(transform.position.x, nuevaPosicion.x, ref currentVelocityX, suavidad);
        nuevaPosicion.y = Mathf.SmoothDamp(transform.position.y, nuevaPosicion.y, ref currentVelocityY, suavidad);

        transform.position = nuevaPosicion;
    }
}

