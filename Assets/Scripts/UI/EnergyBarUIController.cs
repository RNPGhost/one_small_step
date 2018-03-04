using UnityEngine;
using UnityEngine.UI;

public class EnergyBarUIController : MonoBehaviour {
  [SerializeField]
  private Character _character;

  private Slider _slider;

  protected virtual void Start()
  {
    _slider = gameObject.GetComponent<Slider>();
  }

  protected virtual void Update()
  {
    _slider.value = _character.Energy / _character.MaxEnergy;
  }
}
