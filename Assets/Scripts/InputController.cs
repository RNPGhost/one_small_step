using UnityEngine;

public class InputController : MonoBehaviour {
  [SerializeField]
  private Player _player;
  [SerializeField]
  private World _world;
  [SerializeField]
  private UIController _ui_controller;

  private bool _mouse_was_down = false;
  
  void Update () {
    if (Input.GetMouseButtonDown(0))
    {
      if (!_mouse_was_down) 
      {
        Vector3 mouse_position = Input.mousePosition;
        AbilityButton selected_button;
        Character selected_character;
        if (_ui_controller.TrySelectAbilityButton(mouse_position, out selected_button))
        {
          _player.SelectAbility(selected_button.GetAbility());
        }
        else if (_world.TrySelectCharacter(mouse_position, out selected_character))
        {
          _player.SelectCharacter(selected_character);
        }

        _mouse_was_down = true;
      }
    }
    else if (_mouse_was_down)
    {
      _mouse_was_down = false;
    }
	}
}
