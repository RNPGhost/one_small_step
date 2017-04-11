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

  public override string Name() {
    return "Fireball";
  }

  private void Start() {
    _phases = new PhaseLoop(new Phase[] {
      new Phase(PhaseName.Ready),
      new Phase(PhaseName.Preparation, _preparation_duration),
      new Phase(PhaseName.Recovery, _recovery_duration),
      new Phase(PhaseName.Cooldown, _cooldown_duration)
      });
  }
  
  public override bool Select(out Ability state) {
    if (_current_phase == PhaseName.Ready) {
      state = this;
      return true;
    } else {
      state = null;
      return false;
    }
  }

  public override bool SelectTarget(Character character, out Ability state) {
    if (_current_phase == PhaseName.Ready) {
      UnpausePhaseTransition();
      state = null;
      return true;
    } else {
      state = this;
      return false;
    }
  }

  protected override void AbilitySpecificPhaseUpdate(Phase phase) {
    Debug.Log("Entered phase " + phase);
    if (phase.Name == PhaseName.Ready) {
      PausePhaseTransition();
    }
  }
}