## Input System
we're using updated Input System, however Input Actions are not added via ui

### InputActions
- reference -> MovementSM.cs -> AddInputBindings

## collectibles

### adding a new collectible
- add sprite
- tag as `collectible`
- layer as `Collectible`
- make sure Player has `ItemCollector` component on it


# States

## New Enemy State
- create new state class `states/NewState`
- create state data class (scriptable obj) `data/SO_NewState`
- create enemy specific state `EnemyName_NewState`

### states/NewState
- extend `State`
- create reference to our stateData: `protected SO_NewState stateData;`
- append to constructor -> `this.stateData = stateData;`

### data/SO_NewState
- add to asset menu: (first line before public class definition) 
  - `[CreateAssetMenu(fileName = "newNewStateData", menuName = "Data/StateData/NewState")]`
- extend `ScriptableObject`
- set public config params
- create `EnemyName_NewStateData` 
  - scripts/enemies/enemySpecific/enemyName/data/ -> right click -> create -> data -> state data -> EnemyName_NewStateData

### enemySpecific/EnemyName/EnemyName_NewState
- extend `NewState`
- create reference to our EnemyName: `private EnemyName enemy;`
- append to constructor -> `this.enemy = enemy`

### NewEnemy class
- create reference to new state: 
```
public EnemyName_NewState newState { get; private set; }
```
- create reference to newStateData:
```
[SerializeField]
private SO_NewState newStateData;
```
- create instance of new state in `Start()` method:
```
stateNameState = new EnemyName_NewState(this, stateMachine, "stateName", stateNameStateData, this);
// i.e. chargeState = new E1_ChargeState(this, stateMachine, "charge", chargeStateData, this);
```

### setup animator
  - select `Alive` component on enemy
  - open Animator window && select animation dropdown -> create new animation -> animations/enemy/enemyx_someAnimation
  - drag png(s) into it via Animator window (open Animator window -> Select 'Alive' component -> select animation from dropdown)