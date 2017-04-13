using System.Collections;
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

  private bool _targetable = true;
  private int _set_untargetable_count = 0;
  private Character _targetable_character;
  private bool _damage_immune = false;
  private int _set_damage_immune_count = 0;
  private List<Character> _targetable_characters = new List<Character>();
  private List<Ability> _active_abilities = new List<Ability>();

  public bool Targetable {
    get {
      return _targetable;
    }
  }

  public void SetUntargetable() {
    _set_untargetable_count++;
    _targetable = false;
  }

  public void UnsetUntargetable() {
    _set_untargetable_count--;
    if (_set_untargetable_count == 0) {
      _targetable = true;
    }
  }

  public void SetDamageImmune() {
    _set_damage_immune_count++;
    _damage_immune = true;
  }

  public void UnsetDamageImmune() {
    _set_damage_immune_count--;
    if (_set_damage_immune_count == 0) {
      _damage_immune = false;
    }
  }

  public void SetTargetableCharacter(Character character) {
    _targetable_characters.Add(character);
    _targetable_character = character;
  }

  public void UnsetTargetableCharacter(Character character) {
    int index = _targetable_characters.LastIndexOf(character);
    if (index >= 0) {
      _targetable_characters.RemoveAt(index);
      if (_targetable_characters.Count > 0) {
        _targetable_character = _targetable_characters[_targetable_characters.Count - 1];
      } else {
        _targetable_character = this;
      }
    }
  }

  public Character AcquireTarget(Character targeter) {
    if (Targetable) {
      for (int i = 0; i < _active_abilities.Count; i++) {
        Character ability_target = _active_abilities[_active_abilities.Count - 1 - i].AcquireTarget(targeter);
        if (ability_target != null) {
          return ability_target;
        }
      }
      return _targetable_character;
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
    _targetable_character = this;
  }

  public void TakeDamage(float damage) {
    if (!_damage_immune) {
      Debug.Log(_name + " took " + damage + " damage");
      _health = Mathf.Max(_health - damage, 0);
    }
  }
}
