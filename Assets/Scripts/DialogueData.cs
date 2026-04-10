using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialogue", menuName = "HorrorGame/Dialogue")]
public class DialogueData : ScriptableObject
{
    public string characterName;
    public string dialogueText;
    public List<Choice> choices;
}

[System.Serializable]
public class Choice
{
    public string text;
    public DialogueData nextDialogue;
}
