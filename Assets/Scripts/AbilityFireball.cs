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

  private Character _target;

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
    Character target = character.AcquireTarget();
    if (_current_phase == PhaseName.Ready && IsValidTarget(target)) {
      _target = target;
      UnpausePhaseTransition();
      state = null;
      return true;
    }

    state = this;
    return false;
  }

  private bool IsValidTarget(Character character) {
    return (character != null &&
            character.Owner.Id != _owner.Owner.Id);
  }

  protected override void AbilitySpecificPhaseUpdate(Phase phase) {
    Debug.Log("Entered phase " + phase.Name);
    switch (phase.Name) {
      case PhaseName.Ready:
        PausePhaseTransition();
        break;
      case PhaseName.Preparation:
        break;
      case PhaseName.Recovery:
        _target.TakeDamage(_damage);
        break;
      case PhaseName.Cooldown:
        break;
      default:
        break;
    }
  }
}