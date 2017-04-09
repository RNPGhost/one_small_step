using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityFireball : Ability {
  [SerializeField]
  private float _damage;

  public override void Activate() {}
  
  public override void SelectTarget(Character character) {
    character.TakeDamage(_damage);
    Debug.Log(character.Name + " took " + _damage + " damage");
  }

  public override string Name() {
    return "Fireball";
  }
}
