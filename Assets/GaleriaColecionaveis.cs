using UnityEngine;


public class GaleriaColecionaveis : MonoBehaviour
{

    public GameObject borb;
    public GameObject gaf;
    public GameObject lib;

    private GameObject[] itens;
    private int indiceAtual = 0;

    void Awake()
    {
        itens = new GameObject[] { borb, gaf, lib };
    }

    void OnEnable()
    {
        MostrarAtual();
    }


    public void Proximo()
    {
        Debug.Log("Proximo chamado, indice antes: " + indiceAtual);
        indiceAtual = (indiceAtual + 1) % itens.Length;
        MostrarAtual();
        Debug.Log("Proximo chamado, indice depois: " + indiceAtual);
    }


    public void Anterior()
    {
        indiceAtual = (indiceAtual - 1 + itens.Length) % itens.Length;
        MostrarAtual();
    }

    private void MostrarAtual()
    {
        for (int i = 0; i < itens.Length; i++)
        {
            if (itens[i] != null)
                itens[i].SetActive(i == indiceAtual);
        }
    }
}