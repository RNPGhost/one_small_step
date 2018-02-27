using UnityEngine;

public class AbilityFireball : Ability {
  [SerializeField]
  private float _damage;
  [SerializeField]
  private Transform _fireball_spawnpoint;
  [SerializeField]
  private GameObject _fireball_prefab;

  private Character _selected_character;
  private Character _target;

  public override string GetName() {
    return "Fireball";
  }

  private void Start() {
    SetPhases(new Phase[] {
      new Phase(PhaseName.Ready),
      new Phase(PhaseName.Preparation, 3.4f),
      new Phase(PhaseName.Recovery, 2.1f),
      });
  }
  
  public override bool Select(out Ability state) {
    if (!OwningCharacter.AbilityInProgress()
        && GetCurrentPhaseName() == PhaseName.Ready) {
      state = this;
      return true;
    }

    state = null;
    return false;
  }

  public override bool SelectTarget(Character character, out Ability state) {
    if (!OwningCharacter.AbilityInProgress()
        && GetCurrentPhaseName() == PhaseName.Ready
        && IsValidTarget(character)) {
      _selected_character = character;
      UnpausePhaseTransition();
      state = null;
      return true;
    }

    state = this;
    return false;
  }

  private bool IsValidTarget(Character character) {
    return (character.OwningPlayer.Id != OwningCharacter.OwningPlayer.Id
            && character.Targetable);
  }

  protected override bool Interruptable() {
    return GetCurrentPhaseName() == PhaseName.Preparation;
  }

  protected override void AbilitySpecificPhaseUpdate(Phase phase) {
    Debug.Log("Entered phase " + phase.Name);
    switch (phase.Name) {
      case PhaseName.Ready:
        OwningCharacter.UnsetActiveAbility(this);
        PausePhaseTransition();
        break;
      case PhaseName.Preparation:
        OwningCharacter.SetActiveAbility(this);
        _target = _selected_character.AcquireAsTargetBy(OwningCharacter);
        StartAnimation();
        break;
      case PhaseName.Recovery:
        CreateFireball();
        break;
      default:
        break;
    }
  }

  private void StartAnimation() {
    Animator.Play("Fireball");
  }

  private void CreateFireball() {
    GameObject fireball = Instantiate(_fireball_prefab, _fireball_spawnpoint);
    TargetingMover script = fireball.GetComponent<TargetingMover>();
    script.SetTarget(_target.gameObject);
    script.SetSpeed(7);
  }
}