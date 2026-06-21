using UnityEngine;
using System;

public class Vidajogador1 : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 10;
    public Barravida barraDeVida; // a única barra, partilhada pelos dois

    private Animator animacao;
    private ControlaJogador jogador;
    private bool estaMorto = false;

    // A abelha "ouve" este evento para saber quando deve morrer também
    public event Action OnMorrer;

    void Awake()
    {
        vida = vidaMaxima;
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador>();
        barraDeVida?.AtualizarVida(vida, vidaMaxima);
    }

    public void Receberdano(int monte)
    {
        Receberdano(monte, null);
    }

    public void Receberdano(int monte, Transform atacante)
    {
        if (estaMorto) return;

        vida -= monte;
        barraDeVida?.AtualizarVida(vida, vidaMaxima);

        if (vida <= 0)
        {
            Morrer();
        }
        else if (jogador != null)
        {
            jogador.aAtacar = true;

            animacao.Play("Vacadanoesq");
            Invoke(nameof(PararDano), 0.4f);
        }
    }

    void Morrer()
    {
        if (estaMorto) return;
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

<<<<<<< HEAD
=======

>>>>>>> e50b1090ca460becc60015c6f0b88c831f11a0b6
        if (jogador != null && jogador.direcao == 1)
            animacao.Play("Vacamortadireita");
        else
            animacao.Play("Vacamortaesquerda");
<<<<<<< HEAD
        Destroy(gameObject, 1.0f);

        OnMorrer?.Invoke();
=======

        Invoke(nameof(AcionarRespawn), 1.0f);
>>>>>>> e50b1090ca460becc60015c6f0b88c831f11a0b6
    }

    void AcionarRespawn()
    {
        CheckpointManager.Instance.Respawn();
    }

    public void ResetVida()
    {
        vida = vidaMaxima;
        estaMorto = false;
        if (jogador != null) jogador.aAtacar = false;
        if (animacao != null) animacao.speed = 1f;
    }
    void PararDano()
    {
        if (jogador != null && !estaMorto)
            jogador.aAtacar = false;
    }

    public void Curar(int quantidade)
    {
        if (estaMorto) return;

        vida += quantidade;
        if (vida > vidaMaxima)
            vida = vidaMaxima;

        barraDeVida?.AtualizarVida(vida, vidaMaxima);
    }
}
