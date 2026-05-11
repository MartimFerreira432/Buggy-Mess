
using UnityEngine;

public class Vidajogador2 : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 10;

    private Animator animacao;
    private ControlaJogador2 jogador;
    private bool estaMorto = false;

    void Start()
    {
        vida = vidaMaxima;
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador2>();
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

       
            if (atacante != null)
            {
                if (atacante.position.x < transform.position.x)
                    animacao.Play("Abelhadanoesq");
                else
                    animacao.Play("Abelhadanodireita");
            }
            else
            {
                if (jogador.direcao == -1)
                    animacao.Play("Abelhadanoesq");
                else
                    animacao.Play("Abelhadanodireita");
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

      
        if (atacante != null)
        {
            if (atacante.position.x > transform.position.x)
                animacao.Play("Abelhamortadireita"); 
            else
                animacao.Play("Abelhamortaesquerda"); 
        }
        else
        {
            if (jogador.direcao == 1)
                animacao.Play("Abelhamortadireita");
            else
                animacao.Play("Abelhamortaesquerda");
        }

        Destroy(gameObject, 1.0f);
    }

    void PararDano()
    {
        if (jogador != null && !estaMorto)
            jogador.aAtacar = false;
    }
}