using UnityEngine;
using UnityEngine.InputSystem;

public class DanoJogador2 : MonoBehaviour
{
    public float alcanceAtaque = 2f;
    public LayerMask inimigos;

    private Animator animacao;
    private ControlaJogador jogador;

    void Start()
    {
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador>();
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

        if (jogador != null)
        {
            jogador.aAtacar = true;

            if (jogador.direcao == 1)
                animacao.Play("abelhaataquedirei");
            else
                animacao.Play("abelhaataqueesq");

            Invoke(nameof(PararAtaque), 0.5f); // ajusta ao tempo da animação
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, alcanceAtaque, inimigos);

        foreach (Collider2D inimigo in hits)
        {
            Debug.Log("Atingi: " + inimigo.name);

            var vidaTerrestre = inimigo.GetComponent<Vidainimigoterrestre>();
            if (vidaTerrestre != null)
            {
                vidaTerrestre.Receberdano(1);
                continue;
            }

            var vidaVoador = inimigo.GetComponent<Vidainimigovoador>();
            if (vidaVoador != null)
            {
                vidaVoador.Receberdano(1);
            }
        }
    }

    void PararAtaque()
    {
        if (jogador != null)
            jogador.aAtacar = false;
    }
}

