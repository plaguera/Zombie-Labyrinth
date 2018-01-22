using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyScript : MonoBehaviour {

	public void Destroy() {
		GameController.destroyInstances -= this.Destroy;
		GameObject.Destroy (gameObject);
	}

	void OnDestroy() {
		GameController.destroyInstances -= this.Destroy;
	}
}
