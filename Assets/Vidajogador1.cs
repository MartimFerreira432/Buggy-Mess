using UnityEngine;
using System;

public class Vidajogador1 : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 10;
    public Barravida barraDeVida;
    private Animator animacao;
    private ControlaJogador jogador;
    private bool estaMorto = false;
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

    public void MorteInstantanea()
    {
        if (estaMorto) return;
        vida = 0;
        barraDeVida?.AtualizarVida(vida, vidaMaxima);
        Morrer();
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
        if (jogador != null && jogador.direcao == 1)
            animacao.Play("Vacamortadireita");
        else
            animacao.Play("Vacamortaesquerda");
        OnMorrer?.Invoke();
        Invoke(nameof(AcionarRespawn), 1.0f);
    }

    void AcionarRespawn()
    {
        if (CheckpointManager.Instance != null)
        {
            CheckpointManager.Instance.Respawn();
        }
    }

    public void ResetVida()
    {
        vida = vidaMaxima;
        estaMorto = false;
        if (jogador != null)
            jogador.aAtacar = false;
        if (animacao != null)
            animacao.speed = 1f;
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        barraDeVida?.AtualizarVida(vida, vidaMaxima);
    }

    void PararDano()
    {
        if (jogador != null && !estaMorto)
            jogador.aAtacar = false;
    }

    public void Curar(int quantidade)
    {
        if (estaMorto) return;
        if (Sonsemcomum.Instance != null)
        {
            Sonsemcomum.Instance.TocarCura();
        }
        vida += quantidade;
        if (vida > vidaMaxima)
            vida = vidaMaxima;
        barraDeVida?.AtualizarVida(vida, vidaMaxima);
    }
}