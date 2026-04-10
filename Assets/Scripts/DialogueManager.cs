using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public UIDocument uiDocument;
    public DialogueData initialDialogue;

    private Label _characterNameLabel;
    private Label _dialogueTextLabel;
    private VisualElement _choicesContainer;

    void Start()
    {
        var root = uiDocument.rootVisualElement;
        _characterNameLabel = root.Q<Label>("character-name");
        _dialogueTextLabel = root.Q<Label>("dialogue-text");
        _choicesContainer = root.Q<VisualElement>("choices-container");

        if (initialDialogue != null)
        {
            DisplayDialogue(initialDialogue);
        }
    }

    public void DisplayDialogue(DialogueData data)
    {
        if (data == null)
        {
            uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            return;
        }

        uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        _characterNameLabel.text = data.characterName;
        _dialogueTextLabel.text = data.dialogueText;
        _choicesContainer.Clear();

        foreach (var choice in data.choices)
        {
            var button = new Button();
            button.text = choice.text;
            button.AddToClassList("choice-button");
            button.clicked += () => DisplayDialogue(choice.nextDialogue);
            _choicesContainer.Add(button);
        }
    }
}
