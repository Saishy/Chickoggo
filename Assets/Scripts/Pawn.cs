using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Chickoggo {

	public enum PawnState { Idle, Following, Attacking, Working, Delivering };

	/// <summary>
	/// A Pawn is any object that is able to be controlled.
	/// </summary>
	public abstract class Pawn : Actor {

		protected NavMeshAgent _agent;
		protected Controller controller;

		protected float speed;

		protected PawnState _currentState = PawnState.Idle;

		public Controller MyController { get { return controller; } }

		public NavMeshAgent Agent {
			get { return _agent; }
		}

		public PawnState CurrentState {
			get { return _currentState; }
		}

		protected override void SetDefaultValues() {
			base.SetDefaultValues();

			speed = 3f;
		}

		protected override void AwakeInit() {
			base.AwakeInit();

			_agent = GetComponent<NavMeshAgent>();
		}

		public virtual bool Possess(Controller newController) {
			if (controller != null) {
				return false;
			}

			controller = newController;

			return true;
		}

		public virtual bool UnPossess() {
			if (controller != null) {
				controller = null;
				return true;
			}

			return false;
		}

		public virtual void ManualMovement(Vector3 newInput) {
			newInput = newInput.normalized * speed;
			_agent.velocity = newInput;
		}

		public override void OnDeath() {
			base.OnDeath();

			if (controller != null) {
				controller.OnPawnDied();
			}
		}

		public virtual void SetState(PawnState newState) {
			_currentState = newState;
		}

		public virtual void GoTo(Vector3 targetPosition) {
			_agent.SetDestination(targetPosition);
		}

		/// <summary>
		/// Call this every work tick to increase work done.
		/// </summary>
		/// <param name="nWork"></param>
		public virtual void DoWorkTickUpon(Workable nWork) {

		}
	}
}
