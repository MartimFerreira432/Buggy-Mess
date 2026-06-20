using UnityEngine;
using UnityEngine.InputSystem;

public class Jogadormanager : MonoBehaviour
{
    public ControlaJogador controlaJogador1;
    public ControlaJogador2 controlaJogador2;
    public bool jogadorativo = true;
    public GameObject Jogador;
    public GameObject Jogador2;

    void Start()
    {
     
        int jogadorSalvo = PlayerPrefs.GetInt("JogadorAtivoSalvo", 1);

        if (jogadorSalvo == 1) { DefinirJogador1(); }
        else { DefinirJogador2(); }
    }

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame) { TrocarJogador(); }
    }

    public void TrocarJogador()
    {
        if (jogadorativo)
        {
            Vector3 pos = Jogador.transform.position;
            Jogador2.transform.position = pos;
            DefinirJogador2();
        }
        else
        {
            Vector3 pos = Jogador2.transform.position;
            Jogador.transform.position = pos;
            DefinirJogador1();
        }
    }

    void DefinirJogador1()
    {
        controlaJogador1.enabled = true;
        controlaJogador2.enabled = false;
        Jogador.SetActive(true);
        Jogador2.SetActive(false);
        jogadorativo = true;
        PlayerPrefs.SetInt("JogadorAtivoSalvo", 1);
        PlayerPrefs.Save();
    }

    void DefinirJogador2()
    {
        controlaJogador1.enabled = false;
        controlaJogador2.enabled = true;
        Jogador.SetActive(false);
        Jogador2.SetActive(true);
        jogadorativo = false;
        PlayerPrefs.SetInt("JogadorAtivoSalvo", 2);
        PlayerPrefs.Save();
    }
}