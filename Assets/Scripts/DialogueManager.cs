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

    public MeltdownEffect meltdownController;
    public EndingManager endingManager;

    public void DisplayDialogue(DialogueData data)
    {
        if (data == null)
        {
            uiDocument.rootVisualElement.style.display = DisplayStyle.None;
            return;
        }

        // Check node requirement
        if (!string.IsNullOrEmpty(data.requiredFlag))
        {
            if (GameStateManager.Instance.GetFlag(data.requiredFlag) != data.requiredValue)
            {
                uiDocument.rootVisualElement.style.display = DisplayStyle.None;
                return;
            }
        }

        uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        _characterNameLabel.text = data.characterName;
        _dialogueTextLabel.text = data.dialogueText;
        _choicesContainer.Clear();

        foreach (var choice in data.choices)
        {
            // Check choice requirement
            if (!string.IsNullOrEmpty(choice.requiredFlag))
            {
                if (GameStateManager.Instance.GetFlag(choice.requiredFlag) != choice.requiredValue)
                    continue;
            }

            var button = new Button();
            button.text = choice.text;
            button.AddToClassList("choice-button");
            
            button.clicked += () => 
            {
                // Apply Impacts
                if (!string.IsNullOrEmpty(choice.flagToSet))
                {
                    GameStateManager.Instance.SetFlag(choice.flagToSet, choice.flagValue);
                }
                if (!string.IsNullOrEmpty(choice.statToChange))
                {
                    GameStateManager.Instance.ChangeStat(choice.statToChange, choice.statDelta);
                }

                // Logic Triggers
                if (choice.triggerMeltdown && meltdownController != null)
                {
                    meltdownController.StartMeltdown();
                }
                if (choice.switchToObie)
                {
                    GameStateManager.Instance.SetFlag("IsPlayingAsObie", true);
                }
                if (!string.IsNullOrEmpty(choice.triggerEnding) && endingManager != null)
                {
                    endingManager.ShowEnding(choice.triggerEnding);
                }

                DisplayDialogue(choice.nextDialogue);
            };
            
            _choicesContainer.Add(button);
        }
    }
}
