using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chickoggo {
	/// <summary>
	/// A generic Controller class.
	/// </summary>
	public abstract class Controller : BaseObject {

		protected Pawn pawn;

		public Pawn MyPawn { get { return pawn; } }

		public virtual bool Possess(Pawn newPawn) {
			if (newPawn.Possess(this)) {
				pawn = newPawn;

				return true;
			}

			return false;
		}

		public virtual bool UnPossess() {
			if (pawn.UnPossess()) {
				pawn = null;

				return true;
			}

			pawn = null;
			return false;
		}

		public virtual void OnPawnDied() {
		}
	}
}
