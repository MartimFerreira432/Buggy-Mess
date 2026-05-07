using UnityEngine;
using UnityEngine.InputSystem;

public class Jogadormanager : MonoBehaviour
{
    public ControlaJogador controlaJogador1;
    public ControlaJogador2 controlaJogador2;
    public bool jogadorativo = true;
    public GameObject Jogador;
    public GameObject Jogador2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Jogador.SetActive(true);
        Jogador2.SetActive(false);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
            {
            TrocarJogador();
        }
    }
    public void TrocarJogador()
    {
        if (jogadorativo)
        {
            Vector3 pos = Jogador.transform.position;
            controlaJogador1.enabled = false;
            controlaJogador2.enabled = true;
            Jogador.SetActive(false);
            Jogador2.transform.position = pos;
            Jogador2.SetActive(true);
            jogadorativo = false;
        }
        else {
            Vector3 pos = Jogador2.transform.position;
            controlaJogador1.enabled = true;
            controlaJogador2.enabled = false;
            Jogador.transform.position = pos;
            Jogador.SetActive(true);
            Jogador2.SetActive(false);
            jogadorativo = true;
         
        }
    }
}
