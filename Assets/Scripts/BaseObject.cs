using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	public abstract class BaseObject : MonoBehaviour {

		private void Awake() {
			SetDefaultValues();
			AwakeInit();
		}

		protected virtual void SetDefaultValues() {
		}

		protected virtual void AwakeInit() {
		}

		private void Start() {
			StartInit();
		}

		protected virtual void StartInit() {
		}

		public virtual void InitializeFromPool() {
			SetDefaultValues();
		}

		public virtual void DisableForPool() {
			gameObject.SetActive(false);
		}
	}
}
