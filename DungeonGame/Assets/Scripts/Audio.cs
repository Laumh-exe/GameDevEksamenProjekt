using UnityEngine;

public class SoundManager : MonoBehaviour{
    public static SoundManager Instance;
    
    public AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip coinCollectSound;
    public AudioClip deadSound;
    public AudioClip jumpSound;
    public AudioClip buttonPressedSound;
    public AudioClip buttonReleasedSound;

    void Awake(){
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void PlaySound(AudioClip clip){
        if (clip != null) {
            audioSource.PlayOneShot(clip);
        }
    }

    public void PlayDamageSound(){
        PlaySound(damageSound);
    }

    public void PlayCoinCollectSound(){
        PlaySound(coinCollectSound);
    }

    public void PlayDeadSound(){
        PlaySound(deadSound);
    }

    public void PlayJumpSound(){
        PlaySound(jumpSound);
    }

    public void PlayButtonPressedSound(){
        PlaySound(buttonPressedSound);
    }

    public void PlayButtonReleasedSound(){
        PlaySound(buttonReleasedSound);
    }
}