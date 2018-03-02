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

  public override bool Activate() {
    if (IsReady() && _selected_character != null) {
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
      case PhaseName.Ready:
        OwningCharacter.UnsetActiveAbility(this);
        Reset();
        PausePhaseTransition();
        break;
      case PhaseName.Preparation:
        OwningCharacter.SetActiveAbility(this);
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
    SetPhases(new Phase[] {
      new Phase(PhaseName.Ready),
      new Phase(PhaseName.Preparation, 3.3f),
      new Phase(PhaseName.Recovery, 2.2f),
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