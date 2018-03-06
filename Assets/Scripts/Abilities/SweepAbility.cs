using UnityEngine;

public class SweepAbility : Ability
{
  private Character _selected_character;
  private Character _target;

  public override string GetName()
  {
    return "Sweep";
  }

  public override float GetEnergyCost()
  {
    return 40f;
  }

  public override void SetTarget(Character character)
  {
    if (!IsInProgress() && IsValidTarget(character))
    {
      _selected_character = character;
    }
  }

  public override void Reset()
  {
    if (!IsInProgress())
    {
      _selected_character = null;
    }
  }

  protected override bool IsReadyToBeActivated()
  {
    return base.IsReadyToBeActivated() && _selected_character != null;
  }

  protected override void AbilitySpecificPhaseUpdate(Phase phase)
  {
    switch (phase.Name)
    {
      case PhaseName.Ready:
        OwningCharacter.UnsetDirectionTarget(_target);
        break;
      case PhaseName.Preparation:
        _target = _selected_character.AcquireAsTargetBy(OwningCharacter);
        OwningCharacter.SetDirectionTarget(_target);
        break;
      case PhaseName.Recovery:
        _target.Interrupt();
        break;
      default:
        break;
    }
  }

  private void Start()
  {
    SetPhases(new Phase[] {
      new Phase(PhaseName.Ready),
      new Phase(PhaseName.Preparation, 0.7f),
      new Phase(PhaseName.Recovery, 1.4f),
      });
  }

  private bool IsValidTarget(Character character)
  {
    return (character.OwningPlayer.Id != OwningCharacter.OwningPlayer.Id
            && character.Targetable);
  }
}