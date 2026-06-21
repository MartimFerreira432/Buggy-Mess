using UnityEngine;
using UnityEngine.InputSystem;

public class DanoJogador1 : MonoBehaviour
{
    public float alcanceAtaque = 2f;

    private Animator animacao;
    private ControlaJogador jogador;
    private Rigidbody2D rb;

    void Start()
    {
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Keyboard.current.fKey.wasPressedThisFrame)
        {
            Atacar();
        }
    }

    void Atacar()
    {
        Debug.Log("Ataque feito");

   
        if (Sonsemcomum.Instance != null)
        {
            Sonsemcomum.Instance.TocarAtaque();
        }

        if (jogador != null)
        {
            jogador.aAtacar = true;

            if (animacao != null)
            {
                animacao.Play("Vacaataquedireita");
            }

            if (rb != null)
            {
                rb.linearVelocity = new Vector2(0, rb.linearVelocity.y);
            }

            Invoke(nameof(PararAtaque), 0.5f);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, alcanceAtaque);

        foreach (Collider2D colisorAtingido in hits)
        {
            if (colisorAtingido.CompareTag("Inimigo"))
            {
                Debug.Log("Atingi Inimigo Comum: " + colisorAtingido.name);

                var vidaTerrestre = colisorAtingido.GetComponent<Vidainimigoterrestre>();
                if (vidaTerrestre != null)
                {
                    vidaTerrestre.Receberdano(1);
                    continue;
                }

                var vidaVoador = colisorAtingido.GetComponent<Vidainimigovoador>();
                if (vidaVoador != null)
                {
                    vidaVoador.Receberdano(1);
                    continue;
                }
            }

            if (colisorAtingido.CompareTag("Boss"))
            {
                Debug.Log("Atingi o Boss: " + colisorAtingido.name);

                var vidaMiniboss = colisorAtingido.GetComponent<Vidaminiboss>();
                if (vidaMiniboss != null)
                {
                    vidaMiniboss.Receberdano(1);
                }
            }
        }
    }

    void PararAtaque()
    {
        if (jogador != null)
        {
            jogador.aAtacar = false;
        }
    }
}