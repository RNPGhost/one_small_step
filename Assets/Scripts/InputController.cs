using UnityEngine;

public class InputController : MonoBehaviour {
  [SerializeField]
  private Player _player;
  [SerializeField]
  private UIController _ui_controller;
  
  void Update () {
		if (Input.GetMouseButtonDown(0)) {
      Vector3 mouse_position = Input.mousePosition;
      AbilityButton selected_button;
      if (_ui_controller.TrySelectAbilityButton(mouse_position, out selected_button))
      {
        _player.SelectAbility(selected_button.GetAbility());
      }
      else
      {
        _player.TrySelectCharacter(mouse_position);
      }
    }
	}
}
