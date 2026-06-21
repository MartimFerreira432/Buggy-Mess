using UnityEngine;
using UnityEngine.UI;

// Coloca este script no mesmo GameObject que tem o componente Image da barra de vida.
public class Barravida : MonoBehaviour
{
    [Header("Sprites da barra (do MAIS CHEIO para o MAIS VAZIO)")]
    public Sprite[] sprites; // arrasta os 8 sprites aqui, por ordem

    [Header("Referência")]
    public Image imagemBarra; // se ficar vazio, tenta encontrar automaticamente

    void Awake()
    {
        if (imagemBarra == null)
            imagemBarra = GetComponent<Image>();
    }

    // Chama este método sempre que a vida mudar (incluindo no Start, para mostrar a barra correta no início)
    public void AtualizarVida(int vidaAtual, int vidaMaxima)
    {
        if (sprites == null || sprites.Length == 0 || imagemBarra == null) return;

        float percentagem = Mathf.Clamp01((float)vidaAtual / vidaMaxima);

        // percentagem 1 -> indice 0 (cheia) | percentagem 0 -> ultimo indice (vazia)
        int indice = Mathf.RoundToInt((1f - percentagem) * (sprites.Length - 1));
        indice = Mathf.Clamp(indice, 0, sprites.Length - 1);

        imagemBarra.sprite = sprites[indice];
    }
}