using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Phase {
  private PhaseName _name;
  public PhaseName Name {
    get {
      return _name;
    }
  }
  private float _duration;
  public float Duration {
    get {
      return _duration;
    }
  }

  public Phase(PhaseName name, float duration) {
    _name = name;
    _duration = duration;
  }

  public Phase(PhaseName name) : this(name, -1) {}
}

public enum PhaseName {
  Ready,
  Preparation,
  Action,
  Effects,
  Recovery,
  Cooldown
}
