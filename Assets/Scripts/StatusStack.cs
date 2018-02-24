using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusStack<T> {
  T _default_status;
  T _current_status;
  Stack<T> _status_stack = new Stack<T>();

  public StatusStack(T default_status)
  {
    _default_status = default_status;
    _current_status = _default_status;
  }

	public void SetStatus (T new_status) {
    _status_stack.push(new_status);
    _current_status = new_status;
	}
	
  public T GetStatus()
  {
    return _current_status;
  }

  public void UnsetStatus(T old_status)
  {
    _status_stack.remove(old_status);
    _current_status = _status_stack.Count > 0 ? _status_stack.Peek() : _default_status;
  }
}
