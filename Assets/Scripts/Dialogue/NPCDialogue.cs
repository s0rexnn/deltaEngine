using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea] public string text;
    public Sprite portrait;
    public float typingSpeed = 0.05f;
    public AudioClip typingSound;
}

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "Dialogue/NPCDialogue")]
public class NPCDialogue : ScriptableObject
{
    public DialogueLine[] lines;
    public Sprite portrait; // fallback portrait
}