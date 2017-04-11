using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
  [SerializeField]
  private Character _owner;
  
  protected PhaseLoop _phases;
  protected PhaseName _current_phase;
  private float _next_phase_change;
  private bool _phase_transition_allowed = false;

  public abstract string Name();
  
  public abstract bool Select(out Ability state);

  public virtual bool SelectTarget(Character character, out Ability state) {
    state = null;
    return false;
  }
  
  public virtual bool Interrupt(out Ability state) {
    if (Interruptable()) {
      Deactivate();
      PutOnCooldown();
      state = null;
      return true;
    } else {
      state = this;
      return false;
    }
  }

  protected virtual bool Interruptable() {
    return (!(_current_phase == PhaseName.Ready || _current_phase == PhaseName.Cooldown));
  }

  protected virtual void PutOnCooldown() {
    Phase phase = _phases.Last();
    _current_phase = phase.Name;
    EnterNewPhase();
  }

  protected void Activate() {
    _next_phase_change = Time.time;
    TransitionPhase();
    _phase_transition_allowed = true;
  }
  
  protected void Deactivate() {
    _phase_transition_allowed = false;
  }

  private void Update() {
    while (_phase_transition_allowed && Time.time >= _next_phase_change) {
      TransitionPhase();
    }
  }

  private void TransitionPhase() {
    Phase phase = _phases.Next();
    _current_phase = phase.Name;
    _next_phase_change += phase.Duration;
    EnterNewPhase();
  }

  protected abstract void EnterNewPhase();
}
