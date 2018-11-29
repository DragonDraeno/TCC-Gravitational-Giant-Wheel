using UnityEngine;

public class ChangeThickness : MonoBehaviour {

	void Start () {
        ParticleSystem.ShapeModule shapeModule = gameObject.GetComponent<ParticleSystem>().shape;
        shapeModule.radiusThickness = 0;
    }
	
}
