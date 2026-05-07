using UnityEngine;

public class Vidajogador2 : MonoBehaviour
{
    public int vida;
    public int vidaMaxima = 10;

    private Animator animacao;
    private ControlaJogador jogador;

    void Start()
    {
        vida = vidaMaxima;

        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador>();
    }

    public void Receberdano(int monte)
    {
        vida -= monte;


        if (jogador != null)
        {
            jogador.aAtacar = true;

            if (jogador.direcao == 1)
                animacao.Play("Abelhadanoesq");
            else
                animacao.Play("abelhadanodireita");

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