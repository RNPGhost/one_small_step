using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
  [SerializeField]
  private Player _owner;
  [SerializeField]
  private string _name;
  [SerializeField]
  private float _health;

  public string Name {
    get {
      return _name;
    }
  }

  public void TakeDamage(float damage) {
    _health = Mathf.Max(_health - damage, 0);
  }
}
