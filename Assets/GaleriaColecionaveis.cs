using UnityEngine;

// Put this on the parent panel that contains borb, gaf, and Lib
// (or any persistent object in the same Canvas).
public class GaleriaColecionaveis : MonoBehaviour
{
    [Header("Drag the 3 items here, in display order")]
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

    // Hook this to the ">" button's OnClick
    public void Proximo()
    {
        Debug.Log("Proximo chamado, indice antes: " + indiceAtual);
        indiceAtual = (indiceAtual + 1) % itens.Length;
        MostrarAtual();
        Debug.Log("Proximo chamado, indice depois: " + indiceAtual);
    }

    // Hook this to the "<" button's OnClick
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