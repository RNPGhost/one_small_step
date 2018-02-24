using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  private string _id;
  [SerializeField]
  private World _world;
  [SerializeField]
  private UIController _ui_controller;

  private Ability _selected_ability = null;

  public string Id {
    get {
      return _id;
    }
  }

  public void SelectAbility(Ability ability) {
    if (ability.Select(out _selected_ability))
    {
      Debug.Log("Ability '" + _selected_ability.GetName() + "' selected");
    }
  }

  public bool TrySelectCharacter(Vector3 mouse_position) {
    Character selected_character;
    if (_selected_ability != null && _world.TrySelectCharacter(mouse_position, out selected_character)) {
      _selected_ability.SelectTarget(selected_character, out _selected_ability);
      return true;
    }

    return false;
  }
}