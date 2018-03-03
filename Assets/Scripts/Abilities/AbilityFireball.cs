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
  private float _speed_multiplier;

  public override string GetName() {
    return "Fireball";
  }

  public override bool Activate() {
    if (IsReady() && OwningCharacter.IsReadyToActivateAbility() && _selected_character != null) {
      UnpausePhaseTransition();
      return true;
    }

    return false;
  }

  public override void SetTarget(Character character) {
    if (IsReady() && IsValidTarget(character)) {
      _selected_character = character;
    }
  }

  public override void Reset() {
    if (IsReady()) {
      _selected_character = null;
    }
  }

  protected override void AbilitySpecificPhaseUpdate(Phase phase) {
    Debug.Log("Entered phase " + phase.Name);
    switch (phase.Name) {
      case PhaseName.Preparation:
        StartAnimation();
        _target = _selected_character.AcquireAsTargetBy(OwningCharacter);
        break;
      case PhaseName.Recovery:
        CreateFireball();
        break;
      default:
        break;
    }
  }

  private void Start() {
    _speed_multiplier = OwningCharacter.GetAbilitySpeedMultiplier();
    Debug.Log(_speed_multiplier);
    Animator.SetFloat("FireballAnimationSpeed", _speed_multiplier);
    SetPhases(new Phase[] {
      new Phase(PhaseName.Ready),
      new Phase(PhaseName.Preparation, 1.8f * (1/_speed_multiplier)),
      new Phase(PhaseName.Recovery, 1.5f * (1/_speed_multiplier)),
      });
  }

  private bool IsValidTarget(Character character) {
    return (character.OwningPlayer.Id != OwningCharacter.OwningPlayer.Id
            && character.Targetable);
  }

  private void StartAnimation() {
    Animator.Play("Fireball");
  }

  private void CreateFireball() {
    GameObject fireball = Instantiate(_fireball_prefab, _fireball_spawnpoint);
    TargetingMover fireballMover = fireball.GetComponent<TargetingMover>();
    fireballMover.SetTarget(_target.gameObject);
    fireballMover.SetSpeed(10);
    ExplodeOnContact fireballExploder = fireball.GetComponent<ExplodeOnContact>();
    fireballExploder.SetCaster(OwningCharacter);
  }
}