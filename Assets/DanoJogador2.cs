using UnityEngine;
using UnityEngine.InputSystem;

public class DanoJogador2 : MonoBehaviour
{
    public float alcanceAtaque = 2f;

    private Animator animacao;
    private ControlaJogador2 jogador;

    void Start()
    {
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador2>();
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
        // Toca o som de ataque globalmente
        if (Sonsemcomum.Instance != null)
        {
            Sonsemcomum.Instance.TocarAtaque();
        }

        if (jogador != null)
        {
            jogador.aAtacar = true;

            if (animacao != null)
            {
                animacao.Play("abelhaataquedirei");
            }

            Invoke(nameof(PararAtaque), 0.5f);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, alcanceAtaque);

        foreach (Collider2D colisorAtingido in hits)
        {
            if (colisorAtingido.CompareTag("Inimigo"))
            {
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
                }
            }

            if (colisorAtingido.CompareTag("Boss"))
            {
                var vidaMiniboss = colisorAtingido.GetComponent<Vidaminiboss>();
                if (vidaMiniboss != null)
                {
                    vidaMiniboss.Receberdano(1);
                    Debug.Log("Jogador 2 (Abelha) deu dano no Miniboss!");
                }
            }
        }
    }

    void PararAtaque()
    {
        if (jogador != null)
            jogador.aAtacar = false;
    }
}