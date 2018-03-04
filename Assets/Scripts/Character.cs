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
  public float Health
  {
    get
    {
      return _health;
    }
  }
  [SerializeField]
  private float _max_health;
  public float MaxHealth
  {
    get
    {
      return _max_health;
    }
  }
  [SerializeField]
  private float _energy;
  public float Energy
  {
    get
    {
      return _energy;
    }
  }
  [SerializeField]
  private float _max_energy;
  public float MaxEnergy
  {
    get
    {
      return _max_energy;
    }
  }
  [SerializeField]
  private float _speed; // 0 <= speed <= 10

  private StatusStack<bool> _targetable = new StatusStack<bool>(true);
  private StatusStack<bool> _damage_immune = new StatusStack<bool>(false);
  private StatusStack<Character> _targetable_character;
  private StatusStack<Character> _direction_target = new StatusStack<Character>(null);
  private Ability _active_ability;
  private Quaternion _initial_rotation;

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

  public void SetDirectionTarget(Character target)
  {
    _direction_target.SetValue(target);
  }

  public void UnsetDirectionTarget(Character target)
  {
    _direction_target.UnsetValue(target);
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

  public bool IsReadyToActivateAbility() {
    return !AbilityInProgress();
  }

  public float GetAbilitySpeedMultiplier()
  {
    return 0.8f + _speed * 0.04f;
  }

  public void TakeDamage(float damage) {
    if (!_damage_immune.GetValue()) {
      Debug.Log(_name + " took " + damage + " damage");
      _health = Mathf.Max(_health - damage, 0);
    }
  }
  
  private void Start()
  {
    _targetable_character = new StatusStack<Character>(this);
    _initial_rotation = transform.rotation;
  }

  private void Update()
  {
    LookTowardsTarget();
  }

  private void LookTowardsTarget()
  {
    Character target = _direction_target.GetValue();
    if (target != null)
    {
      Vector3 look_direction = new Vector3(target.gameObject.transform.position.x - gameObject.transform.position.x, 0, target.gameObject.transform.position.z - gameObject.transform.position.z);
      transform.rotation = Quaternion.LookRotation(look_direction);
    }
    else if (transform.rotation != _initial_rotation)
    {
      transform.rotation = _initial_rotation;
    }
  }
}
