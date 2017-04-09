using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityButton : MonoBehaviour {
  [SerializeField]
  private Ability _ability;
  [SerializeField]
  private Text _text;

  private void Start() {
    _text.text = _ability.Name();
  }

  public Ability GetAbility() {
    return _ability;
  }
}
