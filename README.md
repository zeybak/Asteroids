# Asteroids

## GENERAL

 - Editor: Unity 2020.2.7f1
 - IDE Recommended: Rider
 - Packages used:
   - Burst (1.4.11)
   - Entities (0.17.0-preview.42)
   - Hybrid Renderer (0.11.0-preview.44)
   - Unity Physics (0.6.0-preview.3)
 - Folder Assets/_Asteroids/Diagrams contains ECS diagrams for the most important elements.

## HOW TO INSTALL

- Install Unity Editor: https://download.unity3d.com/download_unity/c53830e277f1/Windows64EditorInstaller/UnitySetup64-2020.2.7f1.exe
- Install any GIT IDE you like, I recommend SourceTree or Github Desktop. You can also use the console if you desire.
- Fork the project
- Clone the project to your hard drive

## HOW TO PLAY

- Open project with Unity
- Search for scene: Assets/_Asteroids/Scenes/Game.unity
- Double-click scene
- Hit play

## SYSTEMS

- CollisionsDetectionSystem:
  - Handles entities collisions, uses new Unity Physics for DOTS
- ConstantMovementSpeedSystem:
  - Handles entities that moves to a specific direction constantly
- DestructionSystem:
  - Handles entities destruction with help of DestroyTag, entities doesn't get destroyed instantly on collision. Instead they get marked for destruction to add extra features on destruction.
- EdgeDetectionSystem:
  - Manages entities moving towards the edge of the screen
- FollowPlayerSystem:
  - Moves entities with FollowPlayerTag so they can follow the player
- LifeSpanSystem:
  - Controls entities with LifeSpanData to destroy them after time
- PickupsRewardSystem:
  - Handles pickups rewards to the player
- PlayerAimingSystem:
  - Controls player aiming direction
- PlayerHyperSpaceSystem:
  - Controls player hyper space teleportation mechanic
- PlayerInvulnerabilitySystem:
  - Handles player invulnerability visuals and time
- PlayerMovementSystem:
  - Controls player movement
- PlayerShootingSystem:
  - Controls player shooting
- SpawnOverTimeSystem:
  - Handles spawning entities over time, uses SpawnOverTimeData