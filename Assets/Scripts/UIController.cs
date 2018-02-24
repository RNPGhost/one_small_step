using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIController : MonoBehaviour {

  [SerializeField]
  private LayerMask _button_layer;

  public bool TrySelectAbilityButton(Vector3 mouse_position, out AbilityButton ability_button) {
    List<RaycastResult> results = new List<RaycastResult>();
    PointerEventData pointerData = new PointerEventData(EventSystem.current);
    pointerData.position = mouse_position;
    EventSystem.current.RaycastAll(pointerData, results);
    for (int i = 0; i < results.Count; i++) {
      GameObject gameObject = results[results.Count - i - 1].gameObject;
      if (gameObject.layer == Mathf.Log(_button_layer.value, 2)) {
        AbilityButton button = gameObject.GetComponent<AbilityButton>();
        if (button != null) {
          ability_button = button;
          return true;
        }
      }
    }

    ability_button = null;
    return false;
  }
}
