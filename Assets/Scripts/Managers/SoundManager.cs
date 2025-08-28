using UnityEngine;
public enum SoundType
{
    MENU_MOVE,
    MENU_CLICK,
    MENU_CLOSE,
}

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip[] soundList;
    public static SoundManager instance { get; private set; }
    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
       audioSource = GetComponent<AudioSource>();
    }


    // This function plays a specific sound from the enum //
    public static void PlaySound(SoundType sound, float volume = 1f)
    {
        instance.audioSource.PlayOneShot(instance.soundList[(int)sound], volume);
    }

    // This function plays a custom sound clip from the inspector [Used in other scripts] //
    public static void PlayCustomSound(AudioClip clip, float volume = 1f)
    {
        if (clip != null)
        {
            instance.audioSource.PlayOneShot(clip, volume);
        }
    }
}
