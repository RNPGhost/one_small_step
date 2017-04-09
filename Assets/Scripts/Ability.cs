using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour {
  [SerializeField]
  private Character _owner;

  public abstract string Name();
  public abstract bool Activate(out Ability state);
  public virtual bool SelectTarget(Character character, out Ability state) { state = null; return false; }
}
