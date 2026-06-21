using System.Collections; 
using System.Collections.Generic; 
using UnityEngine; 
using UnityEngine.SceneManagement; 

public class GeneMenu : MonoBehaviour
{
    public void Sai() 
    { 
        print("Aplicação terminou!"); 

        Application.Quit(); 
    } 

    public void AbreJogo() 
    { 
        SceneManager.LoadScene("Messy bug saves"); 
    } 

}
