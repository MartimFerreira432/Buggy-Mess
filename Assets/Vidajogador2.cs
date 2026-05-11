using UnityEngine;

public class Vidajogador2 : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 10;

    private Animator animacao;
    private ControlaJogador2 jogador;

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
        vida -= monte;

        if (jogador != null)
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

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

    void PararDano()
    {
        if (jogador != null)
            jogador.aAtacar = false;
    }
}