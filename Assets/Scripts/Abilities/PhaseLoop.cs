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

  public void GoToNext(PhaseName name) {
    for (int i = 0; i < _phases.Length; i++) {
      int phase_index = (_index + i) % _phases.Length;
      if (_phases[phase_index].Name == name) {
        _index = phase_index;
        break;
      }
    }
  }
}
