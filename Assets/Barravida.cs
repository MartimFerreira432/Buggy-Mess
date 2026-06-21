using UnityEngine;
using UnityEngine.UI;


public class Barravida : MonoBehaviour
{

    public Sprite[] sprites; 

  
    public Image imagemBarra; 

    void Awake()
    {
        if (imagemBarra == null)
            imagemBarra = GetComponent<Image>();
    }

   
    public void AtualizarVida(int vidaAtual, int vidaMaxima)
    {
        if (sprites == null || sprites.Length == 0 || imagemBarra == null) return;

        float percentagem = Mathf.Clamp01((float)vidaAtual / vidaMaxima);

      
        int indice = Mathf.RoundToInt((1f - percentagem) * (sprites.Length - 1));
        indice = Mathf.Clamp(indice, 0, sprites.Length - 1);

        imagemBarra.sprite = sprites[indice];
    }
}