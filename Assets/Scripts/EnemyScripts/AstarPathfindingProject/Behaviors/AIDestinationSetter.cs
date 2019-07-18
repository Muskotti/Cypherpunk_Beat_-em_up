using UnityEngine;
using System.Collections;

namespace Pathfinding {
	/// <summary>
	/// Sets the destination of an AI to the position of a specified object.
	/// This component should be attached to a GameObject together with a movement script such as AIPath, RichAI or AILerp.
	/// This component will then make the AI move towards the <see cref="target"/> set on this component.
	///
	/// See: <see cref="Pathfinding.IAstarAI.destination"/>
	///
	/// [Open online documentation to see images]
	/// </summary>
	[UniqueComponent(tag = "ai.destination")]
	[HelpURL("http://arongranberg.com/astar/docs/class_pathfinding_1_1_a_i_destination_setter.php")]
	public class AIDestinationSetter : VersionedMonoBehaviour {
		/// <summary>The object that the AI should move to</summary>
		public Transform target;
		IAstarAI ai;
        public bool inPosition = false;
        public bool canMove;
        public bool stop;

        void OnEnable () {
			ai = GetComponent<IAstarAI>();
			// Update the destination right before searching for a path as well.
			// This is enough in theory, but this script will also update the destination every
			// frame as the destination is used for debugging and may be used for other things by other
			// scripts as well. So it makes sense that it is up to date every frame.
			if (ai != null) ai.onSearchPath += Update;
            canMove = false;
            stop = false;
        }

		void OnDisable () {
			if (ai != null) ai.onSearchPath -= Update;
		}

        /// <summary>Updates the AI's destination every frame</summary>
        void Update () {
			if (target != null && ai != null && canMove && !stop)
            {
                moveTowardsPlayer();
            }
		}

        private void OnTriggerEnter(Collider other)
        {
            canMove = true;
        }

        public void EnableMovement()
        {
            stop = false;
        }
        public void DisableMovement()
        {
            stop = true;
        }

        public void moveTowardsPlayer()
        {
            if (target.transform.position.x < this.transform.position.x)
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                inPosition = false;
                ai.destination = new Vector3(target.transform.position.x + 0.7f, target.transform.position.y, target.transform.position.z);
            }
            else if (target.transform.position.x > this.transform.position.x)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
                }
                inPosition = false;
                ai.destination = new Vector3(target.transform.position.x - 0.7f, target.transform.position.y, target.transform.position.z);
            }

            if (ai.reachedEndOfPath && Vector3.Distance(transform.position, target.position) < 1f)
            {
                inPosition = true;
            }
        }
    }
}
