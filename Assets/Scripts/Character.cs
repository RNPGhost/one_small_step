﻿using System.Collections.Generic;
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
  private List<Ability> _active_abilities = new List<Ability>();

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
      for (int i = 0; i < _active_abilities.Count; i++) {
        Character ability_target = _active_abilities[_active_abilities.Count - 1 - i].AcquireTarget(targeter);
        if (ability_target != null) {
          return ability_target;
        }
      }
      return _targetable_character.GetValue();
    }

    return null;
  }

  public void Interrupt() {
    foreach (Ability ability in _active_abilities) {
      ability.Interrupt();
    }
  }

  public void AddActiveAbility(Ability ability) {
    _active_abilities.Add(ability);
  }

  public bool RemoveActiveAbility(Ability ability) {
    return _active_abilities.Remove(ability);
  }

  public int AbilitiesInProgressCount() {
    return _active_abilities.Count;
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
