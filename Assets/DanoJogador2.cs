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
        if (jogador != null)
        {
            jogador.aAtacar = true;

            // Mantém a tua animação da abelha a atacar
            if (animacao != null)
            {
                animacao.Play("abelhaataquedirei");
            }

            Invoke(nameof(PararAtaque), 0.5f);
        }

        // Removeu-se a LayerMask para detetar por Tags (assim como no Jogador 1)
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, alcanceAtaque);

        foreach (Collider2D colisorAtingido in hits)
        {
            // 1. Se o colisor tiver a Tag de Inimigo Comum (terrestres e voadores pequenos)
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

            // 2. Se o colisor tiver a Tag do Boss
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