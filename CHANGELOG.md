## [Unreleased] - 2021-12-08
### Added
- Entity now has access to states Dictionary<Type, State> with getState method
- HitStunState added and hooked up to animator + single frame added to enemy1.png
  - keeps track of int comboChains if hits are within Combos.Instance.ChainIfWithinTime
  - keeps track of direction user was pressing on d-pad
- keyboard input rewritten / hitLines for collision detection working much better
- fixed gamepad melee attack control / hit detection
- 3 hit animation added if punches are made within x time

### Dev

## Recall
- for some reason `Entity`'s `OnTriggerEnter2D` is not recognizing Player collision
- fix this and if player is in dashState call SuperKnockback on entity


## bin
[AggressiveWeapon -> handleMeleeAttack]
- moving to use dispatcher from inputSystem primary attack to have better control of attacking to implement comboChains && combos
- removing the 'HitboxToAnimation etc. classes from tutorials'... these seem to work but 
look to be having issues on entering and exiting collision states (the actual melee checks were being called from within an Update / LogicUpdate and would sometimes overfire / leave a ghost entity inside the hitList collection)
going to try applying hits with Physics2D.Linecast to have more control of this.


