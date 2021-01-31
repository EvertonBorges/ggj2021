using UnityEngine;
using System.Collections;

public class MoveObjectController : MonoBehaviour 
{
	public float reachRange = 1.8f;
	[SerializeField] private bool _needKey = false;

	private Animator anim;
	private Camera fpsCam;
	private GameObject player;

	private const string animBoolName = "isOpen_Obj_";

	private bool playerEntered;
	private bool showInteractMsg;
	private GUIStyle guiStyle;
	private string msg;

	private int rayLayerMask;

	private MoveableObject _moveableObjectReference = null;
	private MoveableObject _lastMoveableObjectReference = null;

	void Start()
	{
		//Initialize moveDrawController if script is enabled.
		player = GameObject.FindGameObjectWithTag("Player");

		fpsCam = Camera.main;
		if (fpsCam == null)	//a reference to Camera is required for rayasts
		{
			Debug.LogError("A camera tagged 'MainCamera' is missing.");
		}

		//create AnimatorOverrideController to re-use animationController for sliding draws.
		anim = GetComponent<Animator>(); 
		anim.enabled = true;  //enable animation states by default.  

		//the layer used to mask raycast for interactable objects only
		LayerMask iRayLM = LayerMask.NameToLayer(Layers.InteractRaycast.ToString());
		rayLayerMask = 1 << iRayLM.value;
	}
		
	void OnTriggerEnter(Collider other)
	{		
		if (other.gameObject == player)		//player has collided with trigger
		{			
			playerEntered = true;
		}
	}

	void OnTriggerExit(Collider other)
	{		
		if (other.gameObject == player)		//player has exited trigger
		{			
			playerEntered = false;
			//hide interact message as player may not have been looking at object when they left
			showInteractMsg = false;		
		}
	}

	void Update()
	{		
		if (playerEntered)
		{
			//center point of viewport in World space.
			Vector3 rayOrigin = fpsCam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0f));
			RaycastHit hit;

			//if raycast hits a collider on the rayLayerMask
			if (Physics.Raycast(rayOrigin,fpsCam.transform.forward, out hit, reachRange, rayLayerMask))
			{
				MoveableObject moveableObject = null;
				//is the object of the collider player is looking at the same as me?
				if (!isEqualToParent(hit.collider, out moveableObject))
				{	//it's not so return;
					return;
				}
				
				if (moveableObject != null && FirstPersonMovement.Instance.CanInteract())
				{
					if (_needKey && !FirstPersonMovement.Instance.HasKey)
						return;

					_moveableObjectReference = moveableObject;
					if (_lastMoveableObjectReference != _moveableObjectReference)
						_lastMoveableObjectReference?.SetCanInteract(false);
					_lastMoveableObjectReference = _moveableObjectReference;

					string animBoolNameNum = animBoolName + _moveableObjectReference.objectNumber.ToString();
					bool isOpen = anim.GetBool(animBoolNameNum);
					_moveableObjectReference?.SetCanInteract(true);

					if (Input.GetButtonDown(InputButton.Interact.ToString()))
					{
						anim.enabled = true;
						anim.SetBool(animBoolNameNum, !isOpen);
					}
				}
			}
			else
			{
				_moveableObjectReference?.SetCanInteract(false);
				_moveableObjectReference = null;
			}
		}
		else
		{
			_moveableObjectReference?.SetCanInteract(false);
			_moveableObjectReference = null;
		}
	}

	//is current gameObject equal to the gameObject of other.  check its parents
	private bool isEqualToParent(Collider other, out MoveableObject draw)
	{
		draw = null;
		bool rtnVal = false;
		try
		{
			int maxWalk = 6;
			draw = other.GetComponent<MoveableObject>();

			GameObject currentGO = other.gameObject;
			for(int i=0; i < maxWalk; i++)
			{
				if (currentGO.Equals(this.gameObject))
				{
					rtnVal = true;	
					if (draw== null) draw = currentGO.GetComponentInParent<MoveableObject>();
					break;			//exit loop early.
				}

				//not equal to if reached this far in loop. move to parent if exists.
				if (currentGO.transform.parent != null)		//is there a parent
				{
					currentGO = currentGO.transform.parent.gameObject;
				}
			}
		} 
		catch (System.Exception e)
		{
			Debug.Log(e.Message);
		}
			
		return rtnVal;

	}
}
