using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
  [SerializeField]
  private Player _owning_player;
  public Player OwningPlayer {
    get {
      return _owning_player;
    }
  }
  [SerializeField]
  private string _name;
  public string Name {
    get {
      return _name;
    }
  }
  [SerializeField]
  private float _health;

  private StatusStack<bool> _targetable = new StatusStack<bool>(true);
  private StatusStack<bool> _damage_immune = new StatusStack<bool>(false);
  private StatusStack<Character> _targetable_character;
  private Ability _active_ability;

  public bool Targetable {
    get {
      return _targetable.GetValue();
    }
  }

  public void SetUntargetable() {
    _targetable.SetValue(false);
  }

  public void UnsetUntargetable() {
    _targetable.UnsetValue(false);
  }

  public void SetDamageImmune() {
    _damage_immune.SetValue(true);
  }

  public void UnsetDamageImmune() {
    _damage_immune.UnsetValue(true);
  }

  public void SetTargetableCharacter(Character character) {
    _targetable_character.SetValue(character);
  }

  public void UnsetTargetableCharacter(Character character) {
    _targetable_character.UnsetValue(character);
  }

  public Character AcquireAsTargetBy(Character targeter) {
    if (Targetable) {
      return _targetable_character.GetValue();
    }

    return null;
  }

  public void Interrupt() {
    _active_ability.Interrupt();
  }

  public void SetActiveAbility(Ability ability) {
    _active_ability = ability;
  }

  public bool UnsetActiveAbility(Ability ability) {
    if (_active_ability == ability)
    {
      _active_ability = null;
      return true;
    }
    return false;
  }

  public bool AbilityInProgress() {
    return _active_ability != null;
  }

  public void Start() {
    _targetable_character = new StatusStack<Character>(this);
  }

  public void TakeDamage(float damage) {
    if (!_damage_immune.GetValue()) {
      Debug.Log(_name + " took " + damage + " damage");
      _health = Mathf.Max(_health - damage, 0);
    }
  }
}
