using UnityEngine;
// Put this on each checkpoint asset/prefab.
// Requires a Collider2D on the same object, with "Is Trigger" checked.
public class Checkpoint : MonoBehaviour
{
    [Header("Optional visual feedback")]
    public Animator animacao;          // e.g. a flag-raise clip, optional
    public string activateTrigger = "Activate";
    public GameObject indicadorAtivo;  // glow/particle shown once activated, optional
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