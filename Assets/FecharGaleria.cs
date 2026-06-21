using UnityEngine;
using UnityEngine.SceneManagement;

// Put this on the FECHAR button itself, or any persistent object,
// and wire the button's OnClick to call Fechar().
public class FecharGaleria : MonoBehaviour
{
    public string cenaPrincipal = "Messy bug saves"; // must match exact scene name

    public void Fechar()
    {
        SceneManager.LoadScene(cenaPrincipal);
    }
}