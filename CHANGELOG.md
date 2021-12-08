## [Unreleased] - 2021-12-07
### Added
- Entity now has access to states Dictionary<Type, State> with getState method
- HitStunState added and hooked up to animator + single frame added to enemy1.png

## Todo
- HitStunState implementation -> enemy should apply knockback
- should be able to start to combo off each hit (Ro3)
- on each TryHit (via AggressiveWeapon) -> should be able to accept / read the user
  input (i.e. direction user was pressing on d-pad + button user pressed)