using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	public class ObjectPoolHandler {

		private static ObjectPoolHandler _instance;

		public static ObjectPoolHandler instance {
			get {
				if (_instance == null) {
					_instance = new ObjectPoolHandler();
				}

				return _instance;
			}
		}

		private Dictionary<string, Stack<BaseObject>> prefabs = new Dictionary<string, Stack<BaseObject>>();

		private Stack<BaseObject> current;

		public GameObject Spawn(GameObject objToSpawn) {
			GameObject returnObj = null;
			BaseObject baseObj;

			if (prefabs.TryGetValue(objToSpawn.name, out current)) {
				if (current.Count != 0) {
					baseObj = current.Pop();
					baseObj.InitializeFromPool();
					returnObj = baseObj.gameObject;
				}
			}

			if (returnObj == null) {
				returnObj = GameObject.Instantiate<GameObject>(objToSpawn);
			}

			return returnObj;
		}

		public void Unspawn(GameObject objToUnspawn) {
			BaseObject obj = objToUnspawn.GetComponent<BaseObject>();

			if (prefabs.TryGetValue(objToUnspawn.name, out current)) {
				current.Push(obj);
			} else {
				current = new Stack<BaseObject>();
				current.Push(obj);
				prefabs.Add(objToUnspawn.name, current);
			}

			obj.DisableForPool();
		}
	}
}
