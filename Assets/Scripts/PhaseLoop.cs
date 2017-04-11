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

  public void GoToNext() {
    _index++;
    if (_index >= _phases.Length) {
      _index -= _phases.Length;
    }
  }

  public void GoTo(PhaseName name) {
    for (int i = 0; i < _phases.Length; i++) {
      if (_phases[i].Name == name) {
        _index = i;
        break;
      }
    }    
  }
}
