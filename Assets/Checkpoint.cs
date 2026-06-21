using UnityEngine;

public class Checkpoint : MonoBehaviour
{

    public Animator animacao;        
    public string activateTrigger = "Activate";
    public GameObject indicadorAtivo; 
    private bool ativado = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (ativado) return;
        if (!other.CompareTag("Player") && !other.CompareTag("Player2")) return;
        Ativar();
    }
    void Ativar()
    {
        ativado = true;
        CheckpointManager.Instance.SetCheckpoint(transform.position);
        if (animacao != null) animacao.SetTrigger(activateTrigger);
        if (indicadorAtivo != null) indicadorAtivo.SetActive(true);
    }
}