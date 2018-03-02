using UnityEngine;

public abstract class Ability : MonoBehaviour {
  [SerializeField]
  private Character _owning_character;
  public Character OwningCharacter {
    get {
      return _owning_character;
    }
  }
  [SerializeField]
  private Animator _animator;
  protected Animator Animator
  {
    get
    {
      return _animator;
    }
  }

  private PhaseLoop _phases;
  private PhaseName _current_phase_name;
  private float _next_phase_change;
  private bool _phase_transition_allowed = false;

  public abstract string GetName();

  // returns whether this ability was activated
  public virtual bool Activate()
  {
    if (IsReady() && !OwningCharacter.AbilityInProgress()) {
      UnpausePhaseTransition();
      return true;
    }

    return false;
  }

  public virtual void SelectTarget(Character character) {}

  public virtual void Reset() {}

  // returns whether this ability was interrupted
  public virtual bool Interrupt() {
    if (Interruptable()) {
      PausePhaseTransition();
      ResetPhase();
      return true;
    }

    return false;
  }

  protected virtual bool IsReady()
  {
    return !OwningCharacter.AbilityInProgress() && _current_phase_name == PhaseName.Ready;
  }

  protected abstract void AbilitySpecificPhaseUpdate(Phase phase);

  protected void SetPhases(Phase[] phases) {
    _phases = new PhaseLoop(phases);
  }

  protected virtual bool Interruptable() {
    return (!(GetCurrentPhaseName() == PhaseName.Ready));
  }

  protected virtual void ResetPhase() {
    _next_phase_change = Time.time;
    GoToPhase(PhaseName.Ready);
  }

  protected void UnpausePhaseTransition() {
    _next_phase_change = Time.time;
    _phase_transition_allowed = true;
  }

  protected void PausePhaseTransition() {
    _phase_transition_allowed = false;
  }


  protected PhaseName GetCurrentPhaseName() {
    return _current_phase_name;
  }

  private void Update() {
    while (_phase_transition_allowed && Time.time >= _next_phase_change) {
      GoToNextPhase();
    }
  }

  private void GoToNextPhase() {
    _phases.GoToNext();
    PhaseUpdate(_phases.Current());
  }

  private void GoToPhase(PhaseName name) {
    _phases.GoTo(name);
    PhaseUpdate(_phases.Current());
  }

  private void PhaseUpdate(Phase phase) {
    UpdatePhaseVariables(phase);
    AbilitySpecificPhaseUpdate(phase);
  }

  private void UpdatePhaseVariables(Phase phase) {
    _current_phase_name = phase.Name;
    _next_phase_change += phase.Duration;
  }
}
