using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {
  [SerializeField]
  private Ability _ability;
  [SerializeField]
  private Text _text;

  private Button _button;

  public Ability GetAbility() {
    return _ability;
  }

  private void Start() {
    _text.text = _ability.GetName();
    _button = gameObject.GetComponent<Button>();
  }

  private void Update() {
    _button.interactable = _ability.OwningCharacter.IsReadyToActivateAbility();
  }
}
