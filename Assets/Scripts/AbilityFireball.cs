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
  private float _action_duration;
  [SerializeField]
  private float _cooldown_duration;

  private Character _selected_character;
  private Character _target;

  public override string Name() {
    return "Fireball";
  }

  private void Start() {
    SetPhases(new Phase[] {
      new Phase(PhaseName.Ready),
      new Phase(PhaseName.Preparation, _preparation_duration),
      new Phase(PhaseName.Action, _action_duration),
      new Phase(Phasename.Effects, 0),
      new Phase(PhaseName.Recovery, _recovery_duration),
      new Phase(PhaseName.Cooldown, _cooldown_duration)
      });
  }
  
  public override bool Select(out Ability state) {
    if (!OwningCharacter.AbilityInProgress() && GetCurrentPhaseName() == PhaseName.Ready) {
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
        PausePhaseTransition();
        break;
      case PhaseName.Preparation:
        OwningCharacter.AddActiveAbility(this);
        break;
      case PhaseName.Action:
        _target = _selected_character.AcquireTarget();
        break;
      case PhaseName.Effects:
        if (_target != null) {
          target.TakeDamage(_damage);
        }
        break;
      case PhaseName.Recovery:
        break;
      case PhaseName.Cooldown:
        OwningCharacter.RemoveActiveAbility(this);
        break;
      default:
        break;
    }
  }
}