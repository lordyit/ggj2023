using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum AudioClipID
    {
        NONE = 0,

        MENU_MUSIC = 1,
        MENU_BUTTON_CLICK = 5,

        GAMEPLAY_MUSIC = 2,

        PLAYER_DAMAGED_3 = 3,

        ENEMY_DAMAGED = 4,
        ENEMY_DAMAGED_2 = 6,
        ENEMY_DAMAGED_3 = 7,

        TREE_DEATH = 8,
        
        MYSTERIOUS_VOICES = 9,

        ENEMY_DEATH_1 = 10,
        ENEMY_DEATH_2 = 11,

        ENEMY_SPELL = 12,

        PLAYER_DEATH_1 = 13,
        PLAYER_DEATH_2 = 14,

        ENEMY_1_ATTACK = 15,

        TOTAL = 16
    }

    [System.Serializable]
    private struct AudioClipRegister
    {
        [SerializeField]
        private AudioClipID id;
        [SerializeField]
        private AudioClip audioClip;

        public AudioClipID ID { get => id; }
        public AudioClip AudioClip { get => audioClip; }
    }

    [SerializeField]
    private AudioSource[] sfxAudioSources = null;

    [SerializeField]
    private AudioSource[] musicAudioSources = null;

    [SerializeField]
    private AudioClipRegister[] audioClipRegisters = null;

    private int currentSFXAudioSourceIndex;

    private static SoundManager instance;

    public static SoundManager Instance { get => instance; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySFX(AudioClipID clipID)
    {
        AudioSource audioSource = sfxAudioSources[currentSFXAudioSourceIndex];

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        AudioClip clip = FindAudioClip(clipID);
        audioSource.PlayOneShot(clip);
        currentSFXAudioSourceIndex = (currentSFXAudioSourceIndex + 1) % sfxAudioSources.Length;
    }

    public void PlayMusic(AudioClipID clipID)
    {
        AudioClip clip = FindAudioClip(clipID);
        // NOTE: in the future we may want to make a smooth transition using multiple audio sources.
        // But for now let's only use the first source.
        musicAudioSources[0].clip = clip;
        musicAudioSources[0].Play();
    }

    private AudioClip FindAudioClip(AudioClipID clipID)
    {
        AudioClip clip = null;
        for (int r = 0; r < audioClipRegisters.Length; r++)
        {
            AudioClipRegister register = audioClipRegisters[r];
            if (register.ID == clipID)
            {
                clip = register.AudioClip;
                break;
            }
        }

        if (clip == null)
        {
            Debug.LogError("[SoundManager] Couldn't find clip for "+clipID, this);
        }

        return clip;
    }
}
