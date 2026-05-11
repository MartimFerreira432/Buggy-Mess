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

            // DANO (Mantido igual ao teu código anterior)
            if (atacante != null)
            {
                if (atacante.position.x > transform.position.x)
                    animacao.Play("Vacadanoesq");
                else
                    animacao.Play("Vacadanodirei");
            }
            else
            {
                if (jogador.direcao == 1)
                    animacao.Play("Vacadanoesq");
                else
                    animacao.Play("Vacadanodirei");
            }

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

        // MORTE (Invertida conforme pedido)
        if (atacante != null)
        {
            if (atacante.position.x > transform.position.x)
                animacao.Play("Vacamortadireita"); // Invertido aqui
            else
                animacao.Play("Vacamortaesquerda"); // Invertido aqui
        }
        else
        {
            if (jogador.direcao == 1)
                animacao.Play("Vacamortadireita"); // Invertido aqui
            else
                animacao.Play("Vacamortaesquerda"); // Invertido aqui
        }

        Destroy(gameObject, 1.0f);
    }

    void PararDano()
    {
        if (jogador != null && !estaMorto)
            jogador.aAtacar = false;
    }
}