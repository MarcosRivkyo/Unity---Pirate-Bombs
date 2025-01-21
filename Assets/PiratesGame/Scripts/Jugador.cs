using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Jugador : MonoBehaviour
{
    public float velocidad = 5f;
    public float salto = 15f;

    protected Rigidbody2D rigidBody2D;
    protected Animator animator;
    protected Collider2D collider2d;
    public AudioSource sonidoGolpe;
    public AudioSource sonidoMuerte;


    public GameObject gameObjectBomba;

    [SerializeField] private int vidas = 3;
    public Image vida1, vida2, vida3;
    public Image armadura;
    private float tiempoRestanteCooldown;
    public Slider sliderBombaCooldown;


    protected bool disparar = false;
    private bool puedeSaltar = true;
    private bool invencible = false;


    public float cooldownDisparo = 2f;
    public float duracionInvencibilidad = 2f;
    public float fuerzaEmpuje = 150f;

    private int vidasInicio;




    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider2d = GetComponent<Collider2D>();

        armadura.enabled = false;

        if (sliderBombaCooldown != null)
        {
            sliderBombaCooldown.value = 1f; 
            sliderBombaCooldown.gameObject.SetActive(false); 
        }

        vidas = GameManager.instance.CargarVidas();

        vidasInicio = GameManager.instance.CargarVidas();

        animator.SetTrigger("salirPuerta");

        StartCoroutine(EsperarAnimacion());
        ActualizarUIVidas();
    }

    IEnumerator EsperarAnimacion()
    {
        yield return new WaitForSeconds(1.5f);
    }


    void Update()
    {
        if (Input.GetAxisRaw("Fire1") != 0 && !disparar)
        {
            StartCoroutine(DispararBomba());
        }

        if (Input.GetButtonDown("Jump") && puedeSaltar)
        {
            Saltar();
        }

        if (rigidBody2D.velocity.y < 0 && !animator.GetBool("caer"))
        {
            animator.SetBool("saltar", false);
            animator.SetBool("caer", true);
        }
        if (sliderBombaCooldown.gameObject.activeSelf)
        {
            ActualizarBarraCooldown();
        }

    }

    private IEnumerator DispararBomba()
    {
        disparar = true;
        tiempoRestanteCooldown = cooldownDisparo;


        if (sliderBombaCooldown != null)
        {
            sliderBombaCooldown.value = 1f;
            sliderBombaCooldown.gameObject.SetActive(true);
        }

        Vector3 posicionBomba = transform.position + transform.right;
        Instantiate(gameObjectBomba, posicionBomba, Quaternion.identity);

        yield return new WaitForSeconds(cooldownDisparo);

        disparar = false;


        if (sliderBombaCooldown != null)
        {
            sliderBombaCooldown.gameObject.SetActive(false); 
        }
    }


    private void ActualizarBarraCooldown()
    {
        if (tiempoRestanteCooldown > 0)
        {
            tiempoRestanteCooldown -= Time.deltaTime;
            float progreso = Mathf.Clamp01(tiempoRestanteCooldown / cooldownDisparo);
            sliderBombaCooldown.value = progreso;
        }
    }


    void FixedUpdate()
    {
        rigidBody2D.velocity = new Vector2(velocidad * Input.GetAxis("Horizontal"), rigidBody2D.velocity.y);

        animator.SetBool("correr", rigidBody2D.velocity.x != 0);

        if (rigidBody2D.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (rigidBody2D.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        bool enSuelo = Physics2D.OverlapBox(
            transform.position + new Vector3(0, -collider2d.bounds.extents.y, 0),
            new Vector2(collider2d.bounds.size.x * 0.8f, 0.05f),                   
            0,                                                                   
            LayerMask.GetMask("suelo")                                            
        );

        if (enSuelo)
        {
            animator.SetBool("saltar", false);
            animator.SetBool("caer", false);
            puedeSaltar = true; 
        }
    }

    void Saltar()
    {
        puedeSaltar = false;

        animator.SetBool("saltar", true);
        animator.SetBool("caer", false);

        rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
        rigidBody2D.AddForce(new Vector2(0, salto), ForceMode2D.Impulse);

    }



    public bool QuitarVida()
    {
        if (!invencible)
        {

            vidas = vidas - 2;

            GameManager.instance.RegistrarVidas(vidas);

            sonidoGolpe.Play();

            animator.SetTrigger("hit");

            Debug.Log("Vida perdida! Vidas restantes: " + vidas);

            ActualizarUIVidas();

            float direccionEmpuje = transform.localScale.x > 0 ? -1 : 1;  
            Vector2 empuje = new Vector2(direccionEmpuje * 5f, salto); 

            rigidBody2D.AddForce(empuje, ForceMode2D.Impulse);

            StartCoroutine(ActivarInvencibilidad());

            if (vidas <= 0)
            {
                Debug.Log("Jugador ha perdido todas las vidas.");

                StartCoroutine(EsperarYDestruir());
            }

            return true;

        }

        return false;
    }



    public IEnumerator EsperarYDestruir()
    {

        GameManager.instance.RegistrarVidas(vidasInicio);

        sonidoMuerte.Play();

        animator.SetBool("morirHit", true);

        yield return new WaitForSeconds(2f); 

        destruir();

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void destruir()
    {

        if (transform.parent != null)
        {
            Destroy(transform.parent.gameObject);
        }
        Destroy(this.gameObject);
    }

    IEnumerator ActivarInvencibilidad()
    {
        invencible = true;
        armadura.enabled = true;


        yield return new WaitForSeconds(duracionInvencibilidad);

        invencible = false;
        armadura.enabled = false;

    }

    public void AumentarVida()
    {
        vidas++;
        if (vidas > 3)
        {
            Debug.Log("Jugador ya tiene todas las vidas.");
        }
        else
        {
            GameManager.instance.RegistrarVidas(vidas);
            Debug.Log("Vida ganada! Vidas restantes: " + vidas);
            ActualizarUIVidasGanadas();
        }
    }

    public void ActualizarUIVidas()
    {
        if (vidas == 2)
        {
            vida3.gameObject.SetActive(false);
        }
        else if (vidas == 1)
        {
            vida2.gameObject.SetActive(false);
            vida3.gameObject.SetActive(false);
        }
        else if (vidas == 0)
        {
            vida1.gameObject.SetActive(false);
            vida2.gameObject.SetActive(false);
            vida3.gameObject.SetActive(false);
        }
    }

    public void ActualizarUIVidasGanadas()
    {
        if (vidas == 2)
        {
            vida2.gameObject.SetActive(true);
        }
        else if (vidas == 3)
        {
            vida3.gameObject.SetActive(true);
        }
    }



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovil")
            transform.SetParent(collision.transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "PlataformaMovil")
            transform.SetParent(null);
    }
}
