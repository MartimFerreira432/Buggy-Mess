using UnityEngine;

public class Vidajogador1 : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 10;
    private Animator animacao;
    private ControlaJogador jogador;
    private bool estaMorto = false;

    void Start()
    {
        vida = vidaMaxima;
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador>();
    }

    public void Receberdano(int monte)
    {
        Receberdano(monte, null);
    }

    public void Receberdano(int monte, Transform atacante)
    {
        if (estaMorto) return;

        vida -= monte;

        if (vida <= 0)
        {
            Morrer(atacante);
        }
        else if (jogador != null)
        {
            jogador.aAtacar = true;

            
            animacao.Play("Vacadanoesq");

            Invoke(nameof(PararDano), 0.4f);
        }
    }

    void Morrer(Transform atacante)
    {
        estaMorto = true;

        if (jogador != null)
        {
            jogador.aAtacar = true;
            if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
            {
                rb.linearVelocity = Vector2.zero;
                rb.bodyType = RigidbodyType2D.Static;
            }
        }

        
        if (jogador != null && jogador.direcao == 1)
            animacao.Play("Vacamortadireita");
        else
            animacao.Play("Vacamortaesquerda");

        Destroy(gameObject, 1.0f);
    }

    void PararDano()
    {
        if (jogador != null && !estaMorto)
            jogador.aAtacar = false;
    }
}