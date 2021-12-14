## [Unreleased] - 2021-12-08
### Added
- Entity now has access to states Dictionary<Type, State> with getState method
- HitStunState added and hooked up to animator + single frame added to enemy1.png
  - keeps track of int comboChains if hits are within Combos.Instance.ChainIfWithinTime
  - keeps track of direction user was pressing on d-pad
- keyboard input rewritten / hitLines for collision detection working much better

### Dev

## Recall
- HitStunState -> 
  - after 2 comboChains, disable user movement for x time or if they finish the 3rd combo hit chain
  - on 3rd combo hit chain change entity into HitFlyState
- create HitFlyState 
  - in this state enemy should be sent flying in direction... 
  - would be cool if any other enemy hit by this entity in this state will be sent to HitStunState 
  - at the end of this HitFlyState should destroy enemy if health <= 0 OR should return entity to idleState

## bin
[AggressiveWeapon -> handleMeleeAttack]
- moving to use dispatcher from inputSystem primary attack to have better control of attacking to implement comboChains && combos
- removing the 'HitboxToAnimation etc. classes from tutorials'... these seem to work but 
look to be having issues on entering and exiting collision states (the actual melee checks were being called from within an Update / LogicUpdate and would sometimes overfire / leave a ghost entity inside the hitList collection)
going to try applying hits with Physics2D.Linecast to have more control of this.


