# Vertical Slice Plan: Reusable Dialog & Transitions

This plan outlines the implementation of a robust, reusable Vertical Slice for the "AI_theGame2" project, focusing on a sophisticated Dialog Decision Tree and polished Title Screen transitions.

## 1. Core Architecture: Reusable Dialog System
The focus is on a decoupled system that can be dropped into any 2D project.

### 1.1 Dialogue Data (ScriptableObject)
- **Node-Based Structure**: Each `DialogueData` represents a node in the tree.
- **Extended Metadata**:
    - `characterName`: Name of the speaker.
    - `dialogueText`: The message to display.
    - `portrait`: Optional Sprite for the character.
    - `audioClip`: Optional voice/blip sound.
    - `emotion`: Enum or string to trigger animation changes.
- **Branching Logic**:
    - `Choice` objects with conditions (flags/stats) and results.
- **Action System**: A generic list of "Actions" to trigger (e.g., Play Sound, Give Item, Start Meltdown) to remove hardcoded dependencies.

### 1.2 Dialogue Manager (Logic)
- **Text Typing**: Component-based typing effect with speed control and skip functionality.
- **UI Binding**: Generic UI Toolkit binding to `DialogueData`.
- **Action Executor**: Processes the "Actions" defined in the dialogue nodes.

### 1.3 UI Toolkit (Visuals)
- **Generic UXML**: A standard layout for dialogue (Name, Text, Choices, Portrait).
- **Reusable USS**: Theme-able styles for easy visual changes.

## 2. Title Screen & Transitions
Creating a "professional" feel through smooth scene changes.

### 2.1 Transition Manager
- **Cross-Scene Persistence**: A singleton or persistent prefab.
- **Animation Controller**: Handles the "In" and "Out" states of the transition.
- **Types**: Standard Fade (Black/White), Blur, or Wipe.

### 2.2 Title Screen Manager
- **Visual Polish**: Background parallax or subtle animations.
- **Integration**: Use the `TransitionManager` for starting the game instead of immediate scene loading.

## 3. Reusability Strategy
- **Namespace isolation**: Keep these systems in a specific folder (e.g., `Assets/Framework`).
- **Minimal Dependencies**: Use interfaces or generic events to communicate with game-specific logic (like `GameStateManager`).

## 4. Implementation Steps
1. **Refactor `DialogueData`**: Add generic actions and portrait support.
2. **Implement `TransitionManager`**: Create the prefab and script for scene fades.
3. **Update `TitleScreenManager`**: Hook it up to the `TransitionManager`.
4. **Develop `TextTyper`**: Add typewriter effect to `DialogueManager`.
5. **Generic UI Templates**: Create reusable UXML/USS files.
6. **Integration Demo**: Build a simple "Vertical Slice" scene showing a transition from Title to a Dialog-heavy gameplay moment.

## 5. Validation
- Verify transitions are frame-rate independent.
- Ensure the Dialog Tree handles deep nesting without stack overflow.
- Confirm flags are correctly updated in `GameStateManager`.
