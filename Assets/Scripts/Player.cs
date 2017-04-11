using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  private string _id;
  [SerializeField]
  private World _world;
  [SerializeField]
  private UIController _ui_controller;

  private Ability _ability = null;
  
  private readonly object _sync_lock = new object();

  public string getId() {
    return _id;
  }

  public void MouseClicked(Vector3 mouse_position) {
    lock (_sync_lock) {
      if (!TrySelectButton(mouse_position)) {
        TrySelectCharacter(mouse_position);
      }
    }
  }

  private bool TrySelectButton(Vector3 mouse_position) {
    AbilityButton selected_button;
    if (_ui_controller.TrySelectAbilityButton(mouse_position, out selected_button)) {
      SelectAbility(selected_button.GetAbility());
      return true;
    }

    return false;
  }

  private void SelectAbility(Ability ability) {
    ability.Select(out _ability);
  }

  private bool TrySelectCharacter(Vector3 mouse_position) {
    Character selected_character;
    if (_world.TrySelectCharacter(mouse_position, out selected_character)) {
      SelectTarget(selected_character);
      return true;
    }

    return false;
  }

  private void SelectTarget(Character character) {
    if (_ability != null) {
      _ability.SelectTarget(character, out _ability);
    }
  }
}