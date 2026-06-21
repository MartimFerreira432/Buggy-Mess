using UnityEngine;

public class Vidajogador2 : MonoBehaviour
{
    public Vidajogador1 vidaDaVaca; // arrasta aqui o GameObject da vaca (o que tem o Vidajogador1)

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
        Destroy(gameObject, 1.0f);
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