using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioEscena : MonoBehaviour
{
    public Animator jugadorAnimator; 

    void Start()
    {
        jugadorAnimator.SetTrigger("salirPuerta");

        StartCoroutine(EsperarAnimacion());

    }

    IEnumerator EsperarAnimacion()
    {
        yield return new WaitForSeconds(1.5f);

    }
}
