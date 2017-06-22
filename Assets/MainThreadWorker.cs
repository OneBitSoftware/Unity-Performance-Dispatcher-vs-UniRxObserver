using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainThreadWorker : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        Dispatcher.Instance.InvokeAllPending();
	}
}
