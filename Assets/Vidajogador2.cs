using UnityEngine;

public class Vidajogador2 : MonoBehaviour
{
    public Vidajogador1 vidaDaVaca;
    private Animator animacao;
    private ControlaJogador2 jogador;
    private bool estaMorto = false;

    void Awake()
    {
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador2>();
        if (vidaDaVaca != null)
            vidaDaVaca.OnMorrer += Morrer;
    }

    void OnDestroy()
    {
        if (vidaDaVaca != null)
            vidaDaVaca.OnMorrer -= Morrer;
    }

    public void Receberdano(int monte)
    {
        Receberdano(monte, null);
    }

    public void Receberdano(int monte, Transform atacante)
    {
        if (estaMorto || vidaDaVaca == null) return;
        vidaDaVaca.Receberdano(monte, atacante);
        if (!estaMorto && jogador != null)
        {
            jogador.aAtacar = true;
            animacao.Play("Abelhadanodireita");
            Invoke(nameof(PararDano), 0.4f);
        }
    }

    public void MorteInstantanea()
    {
        if (estaMorto || vidaDaVaca == null) return;
        vidaDaVaca.MorteInstantanea();
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
        Vector3 escala = transform.localScale;
        escala.x = Mathf.Abs(escala.x);
        transform.localScale = escala;
        animacao.Play("Abelhamortadireita");
    }

    public void ResetVida()
    {
        estaMorto = false;
        if (jogador != null)
            jogador.aAtacar = false;
        if (animacao != null)
            animacao.speed = 1f;
        if (TryGetComponent<Rigidbody2D>(out Rigidbody2D rb))
        {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void PararDano()
    {
        if (jogador != null && !estaMorto)
            jogador.aAtacar = false;
    }

    public void Curar(int quantidade)
    {
        vidaDaVaca?.Curar(quantidade);
    }
}