using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

  [SerializeField]
  private LayerMask _character_layer;

  public bool TrySelectCharacter(Vector3 mouse_position, out Character character) {
    Ray ray = Camera.main.ScreenPointToRay(mouse_position);
    RaycastHit hit;
    if (Physics.Raycast(ray, out hit, 100f, _character_layer)) {
      if (hit.transform != null) {
        character = hit.transform.gameObject.GetComponent<Character>();
        return true;
      }
    }

    character = null;
    return false;
  }
}
