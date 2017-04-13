using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
  [SerializeField]
  private Character _owning_character;
  public Character OwningCharacter {
    get {
      return _owning_character;
    }
  }

  private PhaseLoop _phases;
  private PhaseName _current_phase_name;
  private float _next_phase_change;
  private bool _phase_transition_allowed = false;

  public abstract string Name();

  // returns whether this action was successful
  // if more input is required, state is set to this, otherwise state is set to null
  public abstract bool Select(out Ability state);

  // returns whether this action was successful
  // if more input is required, state is set to this, otherwise state is set to null
  public virtual bool SelectTarget(Character character, out Ability state) {
    state = null;
    return false;
  }

  // returns the character that should be targetted 
  // when the owner of this ability is targetted 
  // while this ability is active
  public Character AcquireTarget(Character targeter) {
    return null;
  }

  // returns whether this action was successful
  public virtual bool Interrupt() {
    if (Interruptable()) {
      PausePhaseTransition();
      PutOnCooldown();
      return true;
    }

    return false;
  }

  protected virtual bool Interruptable() {
    return (!(GetCurrentPhaseName() == PhaseName.Ready || GetCurrentPhaseName() == PhaseName.Cooldown));
  }

  protected virtual void PutOnCooldown() {
    _next_phase_change = Time.time;
    GoToPhase(PhaseName.Cooldown);
  }

  protected void UnpausePhaseTransition() {
    _next_phase_change = Time.time;
    _phase_transition_allowed = true;
  }
  
  protected void PausePhaseTransition() {
    _phase_transition_allowed = false;
  }

  private void Update() {
    while (_phase_transition_allowed && Time.time >= _next_phase_change) {
      GoToNextPhase();
    }
  }

  protected PhaseName GetCurrentPhaseName() {
    return _current_phase_name;
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

  protected abstract void AbilitySpecificPhaseUpdate(Phase phase);

  protected void SetPhases(Phase[] phases) {
    _phases = new PhaseLoop(phases);
  }
}
