using UnityEngine;

public class PhaseLoop {
  private int _index = 0;
  private Phase[] _phases;
  private float _last_left_ready_phase;

  public PhaseLoop(Phase[] phases) {
    _phases = phases;
  }

  public Phase Current() {
    return _phases[_index];
  }

  public void GoToNext() {
    if (Current().Name == PhaseName.Ready)
    {
      _last_left_ready_phase = Time.time;
    }
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

  public float GetTimeTillNext(PhaseName name)
  {
    float total = 0;
    for (int i = 1; i < _phases.Length; i++)
    {
      Phase phase = _phases[(_index + i) % _phases.Length];
      if (phase.Name != name)
      {
        total += phase.Duration;
      }
      else
      {
        break;
      }
    }

    return total;
  }

  public float GetTimeSinceLastReady()
  {
    return Time.time - _last_left_ready_phase;
  }
}
