using UnityEngine;
using UnityEngine.SceneManagement;


public class FecharGaleria : MonoBehaviour
{
    public string cenaPrincipal = "Messy bug saves"; 

    public void Fechar()
    {
        SceneManager.LoadScene(cenaPrincipal);
    }
}