using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

namespace Chickoggo {

	public class AIStateNames {
		public static readonly AIStateNames NullStateID = new AIStateNames(0); // Use this ID to represent a non-existing State in your system
		public static readonly AIStateNames Idle = new AIStateNames(1);
		/// <summary>We are following.</summary>
		public static readonly AIStateNames Following = new AIStateNames(2);
		/// <summary>We are at combat range, but not attacking.</summary>
		public static readonly AIStateNames Combat = new AIStateNames(3);
		///// <summary>Aw shit, u going down!</summary>
		//public static readonly AIStateNames Attacking = new AIStateNames(4);
		/// <summary>Work work.</summary>
		public static readonly AIStateNames Working = new AIStateNames(5);
		/// <summary>Ded.</summary>
		public static readonly AIStateNames Dead = new AIStateNames(6);

		public int Value { get; protected set; }

		protected AIStateNames(int internalValue) {
			Value = internalValue;
		}
	}

	public class AIController : Controller {

		protected AIStateMachine stateMachine;

		[HideInInspector]
		public Vector3 currentTargetPos;

		protected override void AwakeInit() {
			base.AwakeInit();

			stateMachine = new AIStateMachine(this);

			SetAIStates();
		}

		private void Update() {
			if (stateMachine != null && stateMachine.currentState != null) {
				stateMachine.MainUpdate();
			}
		}

		protected virtual void SetAIStates() {
			/*stateMachine.AddState(new AIState_Idle(this));
			stateMachine.AddState(new AIState_Chasing(this));
			stateMachine.AddState(new AIState_Combat(this));
			stateMachine.AddState(new AIState_Attacking(this));
			stateMachine.AddState(new AIState_Working(this));
			stateMachine.AddState(new AIState_Dead(this));*/
		}

		protected override void StartInit() {
			base.StartInit();

			Pawn temPawn = GetComponent<Pawn>();
			Possess(temPawn);

			CallInitialState();
		}

		protected virtual void CallInitialState() {
			stateMachine.StartAIMachine(AIStateNames.Idle);
		}

		public override void OnPawnDied() {
			stateMachine.GoToState(AIStateNames.Dead);
		}

		public AIStateNames GetCurrentAIState() {
			return stateMachine.currentState.stateName;
		}
	}

	public sealed class AIStateMachine {

		public const float MAINUPDATETIMER = 0.33f;

		private AIController aiController;

		private List<AIState> availableStates = new List<AIState>();

		public AIState currentState;

		private float deltaTime = MAINUPDATETIMER;

		public float DeltaTime {
			get { return deltaTime; }
		}

		public AIStateMachine(AIController owner) {
			aiController = owner;
		}

		public void AddState(AIState newState) {
			availableStates.Add(newState);

			newState.aiStateMachine = this;
		}

		public void RemoveState(AIStateNames name) {
			for (var i = 0; i < availableStates.Count; i++) {
				if (availableStates[i].stateName == name) {
					availableStates[i].aiStateMachine = null;
					availableStates.RemoveAt(i);

					return;
				}
			}

			Debug.LogError("AIStateMachine RemoveState() no state named " + name.ToString() + " has been found.");
		}

		public AIState GetState(AIStateNames name) {
			for (var i = 0; i < availableStates.Count; i++) {
				if (availableStates[i].stateName == name) {
					return availableStates[i];
				}
			}

			return null;
		}

		public void GoToState(AIStateNames name) {
			AIState nextState = GetState(name);

			if (nextState == null) {
				Debug.LogError("AIStateMachine GoToState() nextState is null.");
				return;
			}

			if (nextState == currentState) {
				return;
			}

			currentState.OnExitState(nextState);

			AIState previousState = currentState;

			currentState = nextState;

			currentState.OnBeginState(previousState);
		}

		public void MainUpdate() {
			if (deltaTime > 0) {
				deltaTime -= Time.deltaTime;
			}

			if (deltaTime > 0) {
				return;
			} else {
				deltaTime = 0f;
			}

			if (currentState != null) {
				deltaTime = currentState.MainUpdate();
			} else {
				Debug.LogError("AIStateMachine _MainUpdate() currentState is null.");
				deltaTime = 0f;
			}

			deltaTime += MAINUPDATETIMER;
		}

		public void StartAIMachine(AIStateNames initialStateName) {
			currentState = GetState(initialStateName);

			if (currentState == null) {
				Debug.LogError("AIStateMachine StartAIMachine() currentState is null.");
				return;
			}

			currentState.OnBeginState(null);
		}
	}

	public abstract class AIState {

		protected AIController aiController;

		public AIStateMachine aiStateMachine;

		public AIStateNames stateName;

		public AIState(AIController owner) {
			aiController = owner;
		}

		/**<summary>Called after OnExitState of the previousState.</summary>*/
		public abstract void OnBeginState(AIState previousState);

		/**<summary>Called before OnBeginState of the nextState.</summary>*/
		public abstract void OnExitState(AIState nextState);

		public abstract float MainUpdate();
	}

	public class AIState_Idle : AIState {

		public AIState_Idle(AIController owner) : base(owner) {
			stateName = AIStateNames.Idle;
		}

		public override void OnBeginState(AIState previousState) {

		}

		public override void OnExitState(AIState previousState) {

		}

		public override float MainUpdate() {
			

			return 0.17f;
		}
	}

	public class AIState_Following : AIState {

		public AIState_Following(AIController owner) : base(owner) {
			stateName = AIStateNames.Following;
		}

		public override void OnBeginState(AIState previousState) {
			
		}

		public override void OnExitState(AIState previousState) {
			
		}

		public override float MainUpdate() {
			aiController.MyPawn.GoTo(PlayerController.instance.GetDoggoPosition());

			return 0f;

			//return (10f - aiController.AIPawn.aggressiveness) / 10f;
		}
	}

	public class AIState_Combat : AIState {

		public AIState_Combat(AIController owner) : base(owner) {
			stateName = AIStateNames.Combat;
		}

		public override void OnBeginState(AIState previousState) {
			
		}

		public override void OnExitState(AIState previousState) {
			
		}

		public override float MainUpdate() {
			return 0f;

			//return (10f - aiController.AIPawn.aggressiveness) / 10f;
		}
	}

	/*public class AIState_Attacking : AIState {

		public AIState_Attacking(AIController owner) : base(owner) {
			stateName = AIStateNames.Attacking;
		}

		public override void OnBeginState(AIState previousState) {
			
		}

		public override void OnExitState(AIState previousState) {

		}

		public override float MainUpdate() {
			return 0f;
		}
	}*/

	public class AIState_Working : AIState {

		public AIState_Working(AIController owner) : base(owner) {
			stateName = AIStateNames.Working;
		}

		public override void OnBeginState(AIState previousState) {

		}

		public override void OnExitState(AIState previousState) {

		}

		public override float MainUpdate() {
			return 0f;
		}
	}

	public class AIState_Dead : AIState {

		public AIState_Dead(AIController owner) : base(owner) {
			stateName = AIStateNames.Dead;
		}

		public override void OnBeginState(AIState previousState) {

		}

		public override void OnExitState(AIState previousState) {

		}

		public override float MainUpdate() {
			return 0f;
		}
	}
}