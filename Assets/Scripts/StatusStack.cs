using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusStack<T> {
  T _default_status;
  T _current_status; // Most recently assigned status
  List<T> _status_stack = new List<T>();

  public StatusStack(T default_status)
  {
    _default_status = default_status;
    _current_status = _default_status;
  }

	public void SetStatus (T new_status) {
    _status_stack.Add(new_status);
    _current_status = new_status;
	}
	
  public T GetStatus()
  {
    return _current_status;
  }

  public void UnsetStatus(T old_status)
  {
    _status_stack.Remove(old_status);
    int stack_size = _status_stack.Count;
    _current_status = stack_size > 0 ? _status_stack[stack_size - 1] : _default_status;
  }
}
