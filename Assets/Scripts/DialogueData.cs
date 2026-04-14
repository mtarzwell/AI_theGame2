using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "Framework/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string characterName;
    public Sprite characterPortrait;
    public AudioClip voiceBlip;
    [TextArea(3, 10)]
    public string dialogueText;
    public List<Choice> choices;
    
    // Node-level requirement
    public string requiredFlag;
    public bool requiredValue = true;
}

[System.Serializable]
public class Choice
{
    public string text;
    public DialogueData nextDialogue;
    
    // Impact: Persistent State
    public string flagToSet;
    public bool flagValue = true;
    
    public string statToChange;
    public int statDelta = 0;

    // Requirement: Persistent State
    public string requiredFlag;
    public bool requiredValue = true;

    // Narrative Logic: Transient Events (Replaces hardcoded booleans)
    public List<string> eventTags;
}
