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
  private List<Character> _targetable_characters = new List<Character>();
  private List<Ability> _active_abilities = new List<Ability>();

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

  public void SetTargetableCharacter(Character character) {
    _targetable_characters.Add(character);
    _targetable_character = character;
  }

  public void UnsetTargetableCharacter(Character character) {
    int index = _targetable_characters.LastIndexOf(character);
    if (index >= 0) {
      _targetable_characters.RemoveAt(index);
      if (_targetable_characters.Count > 0) {
        _targetable_character = _targetable_character[_targetable_characters.Count - 1];
      } else {
        _targetable_character = this;
      }
    }
  }

  public Character AcquireTarget() {
    if (_targetable) {
      return _targetable_character;
    }

    return null;
  }

  public void Interupt() {
    foreach (Ability ability in _active_abilities) {
      ability.Interupt();
    }
  }

  public void AddActiveAbility(Ability ability) {
    _active_abilities.Add(ability);
  }

  public bool RemoveActiveAbility(Ability ability) {
    return _active_abilities.Remove(ability);
  }

  public bool AbilityInProgress() {
    return _active_abilities.Count > 0;
  }

  public void Start() {
    _targetable_character = this;
  }

  public void TakeDamage(float damage) {
    Debug.Log(_name + " took " + damage + " damage");
    _health = Mathf.Max(_health - damage, 0);
  }
}
