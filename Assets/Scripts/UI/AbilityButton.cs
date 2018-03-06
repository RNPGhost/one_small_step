using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {
  [SerializeField]
  private Ability _ability;
  [SerializeField]
  private Text _text;
  [SerializeField]
  private Slider _cooldown_bar;

  private Button _button;

  public Ability GetAbility() {
    return _ability;
  }

  private void Start() {
    _text.text = _ability.GetName();
    _button = gameObject.GetComponent<Button>();
  }

  private void Update() {
    bool can_select_ability = !_ability.IsInProgress() && _ability.OwningCharacter.IsReadyToActivateAbility(_ability);
    _button.interactable = can_select_ability;
    _cooldown_bar.gameObject.SetActive(!can_select_ability);
    UpdateCooldownBarValue();
  }

  private void UpdateCooldownBarValue()
  {
    float energy_progress = _ability.OwningCharacter.Energy / _ability.GetEnergyCost();
    float ability_progress = GetAbilityProgress(_ability);
    Ability active_ability = _ability.OwningCharacter.ActiveAbility;
    float active_ability_progress = 1f;
    if (active_ability != null)
    {
      active_ability_progress = GetAbilityProgress(_ability.OwningCharacter.ActiveAbility);
    }
    _cooldown_bar.value = Mathf.Min(energy_progress, ability_progress, active_ability_progress);
  }

  private float GetAbilityProgress(Ability ability)
  {
    float total_ability_time = ability.GetTotalAbilityTime();
    return (total_ability_time - ability.GetRemainingTime()) / total_ability_time;
  }
}
