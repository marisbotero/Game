using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Tesoro : MovingObject2
{

    public Vector3 posicionInicial;
    public int resistencia = 3;
    private bool robado = false;
    public TextMesh contador;


    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        posicionInicial = transform.position;
    }

    public void Update()
    {
        SetContador();
    }

    private void SetContador()
    {
        if (resistencia > 0)
        {
            contador.text = resistencia.ToString();
        } else
        {
            contador.text = "";
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladron"))
        {
            StopAllCoroutines();
            Defender(collision.transform);
        } else if (collision.CompareTag("Salida"))
        {
            ManagerLevelsNight.DetenerCorrutinas();
            SceneManager.LoadScene("GameOver");
        }
    }

    // Regresa a la posicion inicial del objeto
    public void Regresar()
    {
        robado = false;
        float distanciaCuadrada = (transform.position - posicionInicial).sqrMagnitude;
        if (distanciaCuadrada > float.Epsilon)
        {
            StartCoroutine(Desplazar(posicionInicial));
        }
    }

    // Se desplaza a la posicion objetivo determinada por un Vector3
    private IEnumerator Desplazar(Vector3 posicionObjetivo)
    {
        Move(posicionObjetivo);
        yield return null;
    }

    private void Defender(Transform ladron)
    {
        if (resistencia > 0)
        {
            resistencia -= 1;
        } else
        {
            if (!robado)
            {
                robado = true;
                transform.SetParent(ladron);
                transform.position = new Vector3(ladron.position.x + 1, ladron.position.y + 1, 0);
            }
        }
    }
}
