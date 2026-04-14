using UnityEngine;
using UnityEngine.UIElements;
using System.Collections;
using System.Collections.Generic;

public class DialogueManager : MonoBehaviour
{
    public UIDocument uiDocument;
    public DialogueData initialDialogue;
    public TextTyper textTyper;

    private Label _characterNameLabel;
    private Label _dialogueTextLabel;
    private VisualElement _choicesContainer;
    private VisualElement _portraitImage;
    private VisualElement _dialogueContainer;

    public AudioSource audioSource;
    public MeltdownEffect meltdownController; 
    public EndingManager endingManager; 

    void Start()
    {
        var root = uiDocument.rootVisualElement;
        _characterNameLabel = root.Q<Label>("character-name");
        _dialogueTextLabel = root.Q<Label>("dialogue-text");
        _choicesContainer = root.Q<VisualElement>("choices-container");
        _portraitImage = root.Q<VisualElement>("portrait-image");
        _dialogueContainer = root.Q<VisualElement>("dialogue-container");

        if (_dialogueContainer != null)
        {
            _dialogueContainer.RegisterCallback<ClickEvent>(evt => 
            {
                if (textTyper != null && textTyper.isTyping)
                {
                    textTyper.Skip();
                }
            });
        }

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

        // Node requirement check
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
        
        // Portrait
        if (_portraitImage != null && data.characterPortrait != null)
        {
            _portraitImage.style.backgroundImage = new StyleBackground(data.characterPortrait);
            _portraitImage.style.display = DisplayStyle.Flex;
        }
        else if (_portraitImage != null)
        {
            _portraitImage.style.display = DisplayStyle.None;
        }

        // Use TextTyper
        if (textTyper != null)
        {
            textTyper.TypeText(data.dialogueText, _dialogueTextLabel, () => ShowChoices(data.choices));
        }
        else
        {
            _dialogueTextLabel.text = data.dialogueText;
            ShowChoices(data.choices);
        }

        // Voice Blip
        if (audioSource != null && data.voiceBlip != null)
        {
            audioSource.PlayOneShot(data.voiceBlip);
        }
    }

    private void ShowChoices(List<Choice> choices)
    {
        _choicesContainer.Clear();
        _choicesContainer.style.display = DisplayStyle.Flex;

        foreach (var choice in choices)
        {
            // Choice requirement check
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
                // Persistent State Changes
                if (!string.IsNullOrEmpty(choice.flagToSet))
                {
                    GameStateManager.Instance.SetFlag(choice.flagToSet, choice.flagValue);
                }
                if (!string.IsNullOrEmpty(choice.statToChange))
                {
                    GameStateManager.Instance.ChangeStat(choice.statToChange, choice.statDelta);
                }

                // Transient Events (Generic Triggering)
                if (choice.eventTags != null)
                {
                    foreach (var tag in choice.eventTags)
                    {
                        ProcessEvent(tag);
                    }
                }

                DisplayDialogue(choice.nextDialogue);
            };
            
            _choicesContainer.Add(button);
        }
    }

    private void ProcessEvent(string eventTag)
    {
        // For Vertical Slice, we map tags to specific functions
        // In a more complex system, this could use a UnityEvent map or BroadcastMessage
        switch (eventTag)
        {
            case "triggerMeltdown":
                if (meltdownController != null) meltdownController.StartMeltdown();
                break;
            case "switchToObie":
                GameStateManager.Instance.SetFlag("IsPlayingAsObie", true);
                break;
            case "triggerEnding_Freed":
                if (endingManager != null) endingManager.ShowEnding("Freed");
                break;
            case "triggerEnding_Caged":
                if (endingManager != null) endingManager.ShowEnding("Caged");
                break;
            default:
                Debug.Log($"[DialogueManager] Event Tag triggered: {eventTag}");
                break;
        }
    }
}
