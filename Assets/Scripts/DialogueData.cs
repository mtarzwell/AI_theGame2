using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "HorrorGame/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string characterName;
    [TextArea(3, 10)]
    public string dialogueText;
    public List<Choice> choices;
    
    // Optional: Only show this node if a flag is met
    public string requiredFlag;
    public bool requiredValue = true;
}

[System.Serializable]
public class Choice
{
    public string text;
    public DialogueData nextDialogue;
    
    // Impact
    public string flagToSet;
    public bool flagValue = true;
    
    public string statToChange;
    public int statDelta = 0;

    // Requirement
    public string requiredFlag;
    public bool requiredValue = true;

    // Narrative Logic
    public bool triggerMeltdown;
    public bool switchToObie;
}
