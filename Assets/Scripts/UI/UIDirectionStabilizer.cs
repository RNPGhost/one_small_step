using UnityEngine;

public class UIDirectionStabilizer : MonoBehaviour {
  
  private Quaternion _rotation;

  private void Start()
  {
    Camera camera = FindObjectOfType<Camera>();
    Vector3 look_direction = new Vector3(0, 0, camera.transform.forward.z);
    transform.rotation = Quaternion.LookRotation(look_direction);
    _rotation = transform.rotation;
  }

  private void LateUpdate()
  {
    transform.rotation = _rotation;
  }
}
