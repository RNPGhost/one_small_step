using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
  [SerializeField]
  private Character _owner;

  public abstract string Name();
  public abstract void Activate();
  public abstract void SelectTarget(Character character);
}
