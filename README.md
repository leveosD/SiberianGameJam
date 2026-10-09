# KOLOBOK (Siberian GameJam 2025)

A first-person action game developed within one week as part of Siberian GameJam. Developed in a team of three: myself as the programmer, along with a sound designer and an artist.

Gameplay Trailer: [Watch on YouTube](https://youtu.be/EmPOPHG4WN0)

## Technology Stack
- C#
- Unity
- Unity Input System
- DOTween
- Unity ScriptableObjects
- Unity Physics
- Unity UI
- Unity Audio Mixer

## Architecture and Technical Implementation

### Object-Oriented Design
Gameplay logic is organized into dedicated components with clearly defined responsibilities. Shared enemy behavior is implemented through a common base controller, while specialized controllers extend it with entity-specific logic.

### Data-Driven Configuration
Weapons use ScriptableObject assets to separate configuration data from runtime behavior. Parameters such as damage, ammunition, firing delays, and attack range can be configured in the Unity Editor without modifying gameplay code.

### Event-Driven Communication
C# events are used to communicate important gameplay state changes, including player death, falling, and pause-related events. This reduces direct dependencies between components and allows other systems to respond to state changes.

### Unity Physics and Combat Systems
Player movement is implemented using Rigidbody-based physics. Raycasting is used for target detection, while weapon and enemy controllers handle combat behavior and projectile-based attacks.

### Input and State Management
The Unity Input System handles player actions and input callbacks. Gameplay state and settings are managed through dedicated controllers, with PlayerPrefs used for local persistence of selected settings and state values.

### Localization
The localization implementation supports English and Russian text, with dedicated language assets and a language-switching component for updating interface text.

### UI and Runtime Feedback
Gameplay controllers communicate with interface components to update relevant information. DOTween is used to implement camera effects and transitions without manually managing every animation frame.
