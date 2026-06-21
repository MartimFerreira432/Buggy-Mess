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
        DefinirJogador1();
    }

    void Update()
    {
        if (Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("Q pressionado, jogadorativo atual: " + jogadorativo);
            TrocarJogador();
        }
    }

    public void TrocarJogador()
    {
        if (jogadorativo)
        {
            Vector3 pos = Jogador.transform.position;
            int dir = controlaJogador1.direcao;
            Jogador2.transform.position = pos;
            DefinirJogador2();
            controlaJogador2.direcao = dir;
            Jogador2.transform.localScale = new Vector3(dir, 1, 1);
        }
        else
        {
            Vector3 pos = Jogador2.transform.position;
            int dir = controlaJogador2.direcao;
            Jogador.transform.position = pos;
            DefinirJogador1();
            controlaJogador1.direcao = dir;
            Jogador.transform.localScale = new Vector3(dir, 1, 1);
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