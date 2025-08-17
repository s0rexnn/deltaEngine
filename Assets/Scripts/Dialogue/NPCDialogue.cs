using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea] public string text;
    public Sprite portrait;
}

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Dialogue/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public DialogueLine[] lines;
    public float typingSpeed = 0.05f;
    public AudioClip typingSound;
    public Sprite portrait; // fallback portrait
}