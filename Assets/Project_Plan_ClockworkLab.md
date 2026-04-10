# Project Plan: The Clockwork Lab (2D Narrative Horror)

This document outlines the current architecture, narrative logic, and asset setup for "The Clockwork Lab". Use this as a reference to ensure all systems are correctly wired.

## 1. Core Systems (C# Scripts)
The game uses a modular approach where logic is separated from narrative data.

- **`GameStateManager.cs` (Singleton):** Tracks global "Impact Flags" (e.g., `HasBrassKey`, `KnowsSecret`) and player stats (`Inquisitive`, `Mysterious`, `DarkComedy`).
- **`DialogueManager.cs`:** The UI controller. It reads `DialogueData` assets and populates the dialogue box. It also triggers special events like "Meltdowns" or "Endings" based on choices.
- **`DialogueData.cs`:** A ScriptableObject template. Each file represents one "node" of the story.
- **`PointAndClickManager.cs`:** Uses the New Input System to handle left-clicks. It shoots a ray into the scene to find objects with an `IInteractable` interface.
- **`RoomNavigation.cs`:** Moves the camera between the **Lab Room** and **Obie's Apartment**.
- **`MeltdownEffect.cs`:** Handles the red/teal flickering light routine during the climax.
- **`EndingManager.cs`:** Swaps visuals (Real Elara vs. Old Laptop) based on the final decision.
- **`ItemPickup.cs`:** Specialized interaction for the **Brass Key** that sets the `HasBrassKey` flag.

## 2. Scene Architecture

### **Scene 1: TitleScreen**
- **Type:** Sprite-based (Legacy UI fallback).
- **Background:** `TitleBackground` sprite.
- **Buttons:** Physical objects with `BoxCollider2D` and `SimpleButton.cs`.
- **Failsafe:** `SpaceToStart.cs` allows pressing the Spacebar to enter the game if mouse interaction fails.

### **Scene 2: SampleScene (Main Game)**
- **Room 1: The Clockwork Lab (Factory)**
    - Contains: Ivan's workstation, the **Empty Pedestal**, the **Crime Scene Wrench**, and the **Steam Valve**.
    - Objective: Find the **Brass Key** hidden behind the valve.
- **Room 2: Obie's Apartment**
    - Contains: **Obie NPC**, the **Steam-Mind Console**, and **Cryptic Wall Circles**.
    - Interaction: Automatically locked until Ivan has the key.

## 3. The Narrative Decision Tree (The "Impact" System)
The story is built on choices that lead to different narrative nodes:

1.  ** Backstory:** Ivan discovers the theft and his scorched hands (establishing the "Good Ivan" vs "Obsessive Obie" grey area).
2.  **The Confrontation:** Three styles of response:
    - **Inquisitive:** Learns about the `MAP, CULT, LIE` words. (Triggers Meltdown).
    - **Mysterious:** Ivan and Obie see Elara become solid. (Triggers Meltdown).
    - **Dark Comedy (Hank Style):** Ivan reveals his wife's tragic backstory involving the Circle Cult. (Triggers Meltdown after a pause).
3.  **The Resolution (Elara's Freedom):**
    - **Choice A (Break the Seal):** Elara manifests as a real person (Anime-style V4). Grid collapses. Psychosis ends.
    - **Choice B (Hesitate):** Ivan takes the machine back. Obie finds a remnant of Elara on an old laptop.

## 4. Key Assets to Verify
- **Input:** `Assets/Settings/GameInputs.inputactions`. Action map "Game" with action "Click".
- **UI:** `Assets/UI/DialogueLayout.uxml` and `Assets/UI/DialogueStyle.uss`.
- **Audio:** `Assets/Audio/Ambient_Lab.wav` (Industrial) and `Assets/Audio/Ambient_Apartment.wav` (Ethereal).
- **Visuals:** `Assets/Sprites/Elara_Real_Final_V4.png` is the canonical version of Elara.

## 5. Instructions for Cursor / Developer:
- **Verify Wiring:** Ensure the `Managers` object in `SampleScene` has all scripts attached and that the `DialogueManager` has a reference to the `UIDocument`.
- **Check PanelSettings:** Ensure `Assets/Settings/StableUIConfig.asset` is assigned to the `PanelSettings` slot of all `UIDocument` components.
- **Input System:** Ensure the **Input System Package** is installed and the `GameInputs` asset is correctly generated.
- **Sorting Layers:** Background should be at -100, Characters at 0, and UI/Prompts at 10.

---
*Created by Unity AI Assistant for "The Clockwork Lab"* ◍
