# Bomb Tactics
## Gameplay
https://youtu.be/G2HY7UPXZEQ
## How to play
Move with WASD.

Switch between move mode and bomb mode with spacebar.

Control the direction and angle of the bomb shooting with mouse.

Increase bomb shooting power with mouse scroll.

Click to shoot bomb.

Green is movable, red kills you.

## How was it made
### Implementation of movement with character controller.
First, we implemented a character moving via character controller.
### Added preview of shooting.
Using the parabolic trajectory formula to preview the shooting with placeholder values.
### Added input to the shooting.
Changed the placeholder values for actual values via input.
### Added bomb interaction.
Using AddExplosiveForce and OverlapSphere, we add an explosive force to every rigidbody close to the explosion.
### Created prefabs for puzzles.
Such as levers affected by explosions or presure plates affected by boxes.
### Built a level using the prefabs.
Also, modifying those prefabs to better suit certain purpouses.
### Added animations and a general visual layer.
Impelemented a basic animation tree and some VFX to make it a bit more inmersive.
