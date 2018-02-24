using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {
  [SerializeField]
  private Ability _ability;
  [SerializeField]
  private Text _text;

  private void Start() {
    _text.text = _ability.GetName();
  }

  public Ability GetAbility() {
    return _ability;
  }
}
