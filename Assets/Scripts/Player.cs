using UnityEngine;

public class Player : MonoBehaviour {
  [SerializeField]
  private string _id;
  public string Id {
    get {
      return _id;
    }
  }

  private Ability _selected_ability = null;

  public void SelectAbility(Ability ability) {
    if (ability.Select(out _selected_ability))
    {
      Debug.Log("Ability '" + _selected_ability.GetName() + "' selected");
    }
  }

  public bool SelectCharacter(Character character)
  {
    if (_selected_ability != null)
    {
      return _selected_ability.SelectTarget(character, out _selected_ability);
    }
    return false;
  }
}