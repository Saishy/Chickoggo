using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {

	/// <summary>
	/// An Actor is any object that has graphical representation.
	/// </summary>
	public class Actor : BaseObject {

		protected float health;
		protected float maxHealth;

		public virtual void OnDeath() {
		}
	}
}
