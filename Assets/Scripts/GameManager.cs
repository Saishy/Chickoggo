using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	public class GameManager : MonoBehaviour {

		public static GameManager instance;

		public GameObject egg;
		public GameObject chick;

		private void Awake() {
			instance = this;
		}
	}
}