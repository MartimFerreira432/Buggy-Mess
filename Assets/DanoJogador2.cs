using UnityEngine;
using UnityEngine.InputSystem;

public class DanoJogador2 : MonoBehaviour
{
    public float alcanceAtaque = 2f;
    public LayerMask inimigos;

    private Animator animacao;
    private ControlaJogador2 jogador; 

    void Start()
    {
        animacao = GetComponentInChildren<Animator>();
        jogador = GetComponent<ControlaJogador2>(); 
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
        if (jogador != null)
        {
            jogador.aAtacar = true;

            if (jogador.direcao == 1)
                animacao.Play("abelhaataquedirei");
            else
                animacao.Play("abelhaataqueesq");

            Invoke(nameof(PararAtaque), 0.5f);
        }

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, alcanceAtaque, inimigos);
        foreach (Collider2D inimigo in hits)
        {
            var vidaTerrestre = inimigo.GetComponent<Vidainimigoterrestre>();
            if (vidaTerrestre != null) { vidaTerrestre.Receberdano(1); continue; }

            var vidaVoador = inimigo.GetComponent<Vidainimigovoador>();
            if (vidaVoador != null) { vidaVoador.Receberdano(1); }
        }
    }

    void PararAtaque()
    {
        if (jogador != null)
            jogador.aAtacar = false;
    }
}