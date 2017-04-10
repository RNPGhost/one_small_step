using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseLoop {
  private int _index = 0;
  private Phase[] _phases;

	public PhaseLoop(Phase[] phases) {
    _phases = phases;
  }

  public Phase Current() {
    return _phases[_index];
  }

  public Phase First() {
    _index = 0;
    return Current();
  }

  public Phase Next() {
    _index++;
    if (_index >= _phases.Length) {
      _index -= _phases.Length;
    }
    return Current();
  }

  public Phase Last() {
    _index = _phases.Length - 1;
    return Current();
  }
}
