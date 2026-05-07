using UnityEngine;
using UnityEngine.UI;

public class Collectiblemanager : MonoBehaviour
{
    public int collectiblecount;
    public Text collectibleText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        collectibleText.text = "3/" +collectiblecount.ToString();
    }
}
