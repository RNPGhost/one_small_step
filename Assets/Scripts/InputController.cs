using UnityEngine;

public class InputController : MonoBehaviour {
  [SerializeField]
  private World _world;
  [SerializeField]
  private UIController _ui_controller;

  private Ability _selected_ability = null;

  private void Update () {
    if (Input.GetMouseButtonDown(0))
    {
        Vector3 mouse_position = Input.mousePosition;
        AbilityButton selected_button;
        Character selected_character;
        if (_ui_controller.TrySelectAbilityButton(mouse_position, out selected_button))
        {
          SelectAbility(selected_button.GetAbility());
        }
        else if (_world.TrySelectCharacter(mouse_position, out selected_character))
        {
          SelectCharacter(selected_character);
        }
    }
	}

  private void SelectAbility(Ability ability) {
    ability.Reset();
    if (!ability.Activate())
    {
      _selected_ability = ability;
    }
  }

  private void SelectCharacter(Character character)
  {
    if (_selected_ability != null)
    {
      _selected_ability.SetTarget(character);
      if (_selected_ability.Activate())
      {
        _selected_ability = null;
      }
    }
  }
}
