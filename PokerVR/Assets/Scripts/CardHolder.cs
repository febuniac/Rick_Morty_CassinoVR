using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardHolder : MonoBehaviour
{
	private SteamVR_TrackedObject trackedObjLeft;  // referência para o controle
	private SteamVR_TrackedObject trackedObjRight;  // referência para o controle

	private GameObject collidingObject; // referência para objeto que é intersectado

	private GameObject cardInHand; // referência para objeto que vai ser manuseado

	public GameObject leftController;
	public GameObject rightController;

	private SteamVR_Controller.Device ControllerLeft
	{  // Properties para o controle
		get { return SteamVR_Controller.Input((int)trackedObjLeft.index); }
	}

	private SteamVR_Controller.Device ControllerRight
	{  // Properties para o controle
		get { return SteamVR_Controller.Input((int)trackedObjRight.index); }
	}

	void Awake()
	{                         // recupera referência para o controle
		trackedObjLeft = leftController.GetComponent<SteamVR_TrackedObject>();
		trackedObjRight = rightController.GetComponent<SteamVR_TrackedObject>();

	}

	private void SetCollidingObject(Collision col)
	{  // guarda o objeto que está colidindo

		if (collidingObject || !col.gameObject.GetComponent<Rigidbody>())
		{
			return;      // se já segura objeto ou objeto não possui rigidbody então saia
		}


		collidingObject = col.gameObject;  // salva como objeto a mover

	}

	private FixedJoint AddFixedJoint()
	{ // cria uma junção para o objeto e controle

		FixedJoint fx = gameObject.AddComponent<FixedJoint>();
		fx.breakForce = 20000;
		fx.breakTorque = 20000;
		return fx;
	}

	public void OnCollisionEnter(Collision other)
	{ // invocado se houver colisão
		if (other.gameObject.GetComponent<CardValue>()) {
			//print ("Achou a carta");
			SetCollidingObject(other);
			
		}
	}

	public void OnCollisionStay(Collision other)
	{ // invocado se houver colisão
		if (other.gameObject.GetComponent<CardValue> ()) {
			//print ("Achou a carta");
			SetCollidingObject(other);

		}
	}

	public void OnCollisionExit(Collision other)
	{ // invocado quando a colisão terminar
		if (!collidingObject)
		{
			return;
		}
		print ("Saiu da carta");
		collidingObject = null;
	}

	private void HoldCard()
	{  // liga o objeto pego ao controle

		cardInHand = collidingObject;
		collidingObject = null;

		var joint = AddFixedJoint();
		joint.connectedBody = cardInHand.GetComponent<Rigidbody>();
		collidingObject.transform.position.Set (transform.position.x, transform.position.y + 2.0F, transform.position.z);
		collidingObject.transform.eulerAngles = this.transform.eulerAngles;
	}

	private void ReleaseObject()
	{ // solta objeto mantendo velocidade

		if (GetComponent<FixedJoint>())
		{
			GetComponent<FixedJoint>().connectedBody = null;
			Destroy(GetComponent<FixedJoint>());

			cardInHand.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
			cardInHand.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity;
		}

		cardInHand = null;
	}

	void Update()
	{
		if (ControllerLeft.GetHairTriggerUp () || ControllerRight.GetHairTriggerUp ()) {
			if (collidingObject)
			{
				HoldCard();
			}
		}



		if (ControllerLeft.GetHairTriggerDown() || ControllerRight.GetHairTriggerDown())	
		{ // caso botão inferior solto
			if (cardInHand)
			{
				ReleaseObject();
			}
		}
	}
}