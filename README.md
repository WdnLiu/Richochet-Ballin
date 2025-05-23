# Richochet ballin

This game is inspired in American western cowboy duels, where they start by walking in opposite directions, and once a certain distance is reached they start to shoot one another.

Our game follows a similar concept, the players start by positioning themselves in a circle and face opposite directions, a visual and sound cue will be given after 2 seconds of positioning themselves like that. Then the duel starts, the players can shoot balls. Each ball will bounce once against the circular limit.

Each player has 3 lives, which will be shown as 3 fragments of sphere inside the user's hitbox. A live can only be substracted if a player is hit with a ball. After that, a wave will appear with the same color as the player that was hit. This wave eliminates all the balls in the map and stops shooting for a few seconds, allowing both players to reposition. Then the game will resume. The game ends once a player's health reaches 0.

Powerups and healing items will appear around the map every few seconds:

- A box with a heart heals the player by 1, it will not add to the maximum of 3 lives.
- An item with three bullets will allow the player to shoot 3 bullets at once one time.
- An item with the shape of a shield will protect the player if they are hit once.

These powerups cannot be accumulated.

When the game ends it will restart, the initial circles will appear once again, and they will be able to start another duel.

## Environment

- 3D Meshes added to the background.
- Real-time render of the area of play.
- Tumbleweeds spawn randomly every few seconds with their own animation.
- Wind particles spawn randomly every few seconds with two kinds of animation.
- Power Up: Every 8 seconds a power up spawns and any player can take it by walking into it.
- Only 1 power up can be hold at the same time.
- Power Ups vanish after 10 seconds of no one taking them.
- 3 seconds after vanishing power ups will start to blink to denote it.
- Power Ups will only spawn during play time and will deactivate once the game ends.
- Power Ups have an standard animation that rotates them.
- MultipleShoot Power Up: Represented as 3 bullets and a yellow ring on the player that holds it. The next shoot will spawn 3 bullets.
- Heart Power Up: Represented as a box with a heart. The player that takes it heals 1 life.
- Shield Power Up: Represented as a shield and a grey ring on the player that holds it. The next bullet taken by the player is ignored and the shield is lost.

## Player logic

- Shooting detection
- Player interface
- Bullet bounce

## Managers

- Game State Manager: Implemented State pattern to transition between different game states, prepare state (where players need to position themselves within a circle and in opposite directions), playing state (state where players can shoot and be hurt), pause state (state where players cannot fire nor be hurt, giving a few seconds to reposition after being hit), gameover state (state where the winner is announced and everything is removed)
- Sound Manager: Implemented sound manager to give independent sound sources to an object, or scene. Allows you to add different sounds with a name, where you can also edit their pitch and volume from the inspector.
