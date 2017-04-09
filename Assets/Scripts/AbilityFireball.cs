using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFireball : Ability {
  [SerializeField]
  private float _damage;

  public override bool Activate(out Ability state) {
    state = this;
    return true;
  }
  
  public override bool SelectTarget(Character character, out Ability state) {
    character.TakeDamage(_damage);
    Debug.Log(character.Name + " took " + _damage + " damage");
    state = null;
    return true;
  }

  public override string Name() {
    return "Fireball";
  }
}
