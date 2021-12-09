## [Unreleased] - 2021-12-08
### Added
- Entity now has access to states Dictionary<Type, State> with getState method
- HitStunState added and hooked up to animator + single frame added to enemy1.png
  - keeps track of int comboChains if hits are within Combos.Instance.ChainIfWithinTime
  - keeps track of direction user was pressing on d-pad

## Recall
- HitStunState -> 
  - after 2 comboChains, disable user movement for x time or if they finish the 3rd combo hit chain
  - on 3rd combo hit chain change entity into HitFlyState
- create HitFlyState 
  - in this state enemy should be sent flying in direction... 
  - would be cool if any other enemy hit by this entity in this state will be sent to HitStunState 
  - at the end of this HitFlyState should destroy enemy if health <= 0 OR should return entity to idleState