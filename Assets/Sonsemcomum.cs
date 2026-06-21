using UnityEngine;

public class Sonsemcomum : MonoBehaviour
{
    public static Sonsemcomum Instance;

    private AudioSource audioSourcePassos;
    private AudioSource audioSourcePulo;

    [Header("Configurações de Áudio")]
    public AudioClip somPulo;
    public AudioClip somAtaque;
    public AudioClip somCura;
    public AudioClip somComer;
    public AudioClip somRespawn; // Arraste o som de renascer/respawn aqui no Inspector

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        AudioSource[] sources = GetComponents<AudioSource>();

        if (sources.Length > 0) audioSourcePassos = sources[0];
        if (sources.Length > 1) audioSourcePulo = sources[1];
    }

    public void TocarPassos(bool tocar)
    {
        if (audioSourcePassos == null) return;

        if (tocar)
        {
            if (!audioSourcePassos.isPlaying) audioSourcePassos.Play();
        }
        else
        {
            if (audioSourcePassos.isPlaying) audioSourcePassos.Stop();
        }
    }

    public void TocarPulo()
    {
        if (audioSourcePulo != null && somPulo != null)
        {
            audioSourcePulo.PlayOneShot(somPulo);
        }
        else if (audioSourcePassos != null && somPulo != null)
        {
            audioSourcePassos.PlayOneShot(somPulo);
        }
    }

    public void TocarAtaque()
    {
        if (audioSourcePulo != null && somAtaque != null)
        {
            audioSourcePulo.PlayOneShot(somAtaque);
        }
        else if (audioSourcePassos != null && somAtaque != null)
        {
            audioSourcePassos.PlayOneShot(somAtaque);
        }
    }

    public void TocarCura()
    {
        if (audioSourcePulo != null && somCura != null)
        {
            audioSourcePulo.PlayOneShot(somCura);
        }
        else if (audioSourcePassos != null && somCura != null)
        {
            audioSourcePassos.PlayOneShot(somCura);
        }
    }

    public void TocarComer()
    {
        if (audioSourcePulo != null && somComer != null)
        {
            audioSourcePulo.PlayOneShot(somComer);
        }
        else if (audioSourcePassos != null && somComer != null)
        {
            audioSourcePassos.PlayOneShot(somComer);
        }
    }

    // Nova função para tocar o som quando o jogador volta à vida no checkpoint
    public void TocarRespawn()
    {
        if (audioSourcePulo != null && somRespawn != null)
        {
            audioSourcePulo.PlayOneShot(somRespawn);
        }
        else if (audioSourcePassos != null && somRespawn != null)
        {
            audioSourcePassos.PlayOneShot(somRespawn);
        }
    }
}