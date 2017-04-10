using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFireball : Ability {
  [SerializeField]
  private float _damage;
  [SerializeField]
  private float _preparation_duration;
  [SerializeField]
  private float _recovery_duration;
  [SerializeField]
  private float _cooldown_duration;
  
  private PhaseLoop _phases;
  private float _next_phase_change;
  private bool _allowed_phase_transition = false;

  public AbilityFireball() {
    _phases = new PhaseLoop(new Phase[] {
      new Phase(PhaseName.Ready),
      new Phase(PhaseName.Preparation, _preparation_duration),
      new Phase(PhaseName.Recovery, _recovery_duration),
      new Phase(PhaseName.Cooldown, _cooldown_duration)
      });

    _next_phase_change = Time.time;
  }

  public override bool Activate(out Ability state) {
    state = this;
    return true;
  }
  
  public override bool SelectTarget(Character character, out Ability state) {
    if (_phases.Current().Name == PhaseName.Ready) {
      _next_phase_change = Time.time;
      _allowed_phase_transition = true;
      state = null;
      return true;
    } else {
      state = this;
      return false;
    }
  }

  public override string Name() {
    return "Fireball";
  }

  public void Update() {
    while (_allowed_phase_transition && Time.time >= _next_phase_change) {
      TransitionPhase();
    }
  }

  private void TransitionPhase() {
    Phase phase = _phases.Next();
    _next_phase_change += phase.Duration;
  }
}
