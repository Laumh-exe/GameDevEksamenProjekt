using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource audioSource;

    // Forskellige lyde
    public AudioClip damageSound;
    public AudioClip coinCollectSound;
    public AudioClip deadSound;
    public AudioClip jumpSound;
    public AudioClip buttonPressedSound;
    public AudioClip buttonReleasedSound;
    // Tilf�j flere lyde her efter behov

    void Awake()
    {
        // Singleton m�nster for at sikre, at der kun er �n instans af SoundManager
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    // Funktion til at afspille en specifik lyd
    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    // Du kan ogs� lave specifikke funktioner for hver lyd, hvis du �nsker
    public void PlayDamageSound()
    {
        PlaySound(damageSound);
    }

    public void PlayCoinCollectSound()
    {
        PlaySound(coinCollectSound);
    }

    public void PlayDeadSound()
    {
        PlaySound(deadSound);
    }

    public void PlayJumpSound()
    {
        PlaySound(jumpSound);
    }
    
    public void PlayButtonPressedSound()
    {
        PlaySound(buttonPressedSound);
    }
    
    public void PlayButtonReleasedSound()
    {
        PlaySound(buttonReleasedSound);
    }

}
