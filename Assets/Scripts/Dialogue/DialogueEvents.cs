using UnityEngine;
using System;

public class DialogueEvents
{
    public event Action<string> OnEnterDialogue;

    public void EnterDialogue(string knotName)
    {
        OnEnterDialogue?.Invoke(knotName);
    }

    public event Action OnDialogueStarted;

    public void DialougeStarted()
    {
        if (OnDialogueStarted != null)
        {
            OnDialogueStarted();
        }
    }

    public event Action OnDialogueEnded;

    public void DialougeEnded()
    {
        if (OnDialogueEnded != null)
        {
            OnDialogueEnded();
        }
    }
    
    public event Action<string> OnDisplayLine; 
    
    public void DisplayLine(string dialogueLine)
    {
        if (OnDisplayLine != null)
        {
            OnDisplayLine(dialogueLine);
        }
    }
}