using UnityEngine;
using System;

public class Vidaminiboss : MonoBehaviour
{

    public static event Action OnBossMorreu;

    public int vida;
    public int vidaMaxima = 10;
    public float tempoAteDestruir = 1f;

    private Animator animacao;
    private bool morto = false;

    void Start()
    {
        vida = vidaMaxima;
        animacao = GetComponentInChildren<Animator>();
    }

    public void Receberdano(int monte)
    {
        if (morto) return;

        vida -= monte;

        if (vida <= 0)
        {
            Morrer();
        }
        else
        {
            TocarAnimacaoDano();
        }
    }

    void TocarAnimacaoDano()
    {
        if (animacao == null) return;
        animacao.Play("MinibossDano");
    }

    void Morrer()
    {
        morto = true;

        if (animacao != null)
            animacao.Play("Minibossmorte");

        if (OnBossMorreu != null)
        {
            OnBossMorreu.Invoke();
        }

     
        Miniboss movimento = GetComponent<Miniboss>();
        if (movimento != null)
            movimento.enabled = false;

    
        Collider2D colisor = GetComponent<Collider2D>();
        if (colisor != null)
            colisor.enabled = false;

        Destroy(gameObject, tempoAteDestruir);
    }
}