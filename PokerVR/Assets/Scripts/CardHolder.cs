using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CardHolder : MonoBehaviour
{
	public SteamVR_TrackedObject trackedObj;  // referência para o controle

	private GameObject collidingObject; // referência para objeto que é intersectado

	private GameObject cardInHand; // referência para objeto que vai ser manuseado


	private SteamVR_Controller.Device Controller
	{  // Properties para o controle
		get { return SteamVR_Controller.Input((int)trackedObj.index); }
	}

	void Awake()
	{                         // recupera referência para o controle
		trackedObj = GetComponent<SteamVR_TrackedObject>();
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
			print ("Achou a carta");
			SetCollidingObject(other);
			
		}
	}

	public void OnCollisionStay(Collision other)
	{ // invocado se houver colisão
		if (other.gameObject.GetComponent<CardValue> ()) {
			print ("Achou a carta");
			SetCollidingObject(other);

		}
	}

	public void OnCollisionExit(Collision other)
	{ // invocado quando a colisão terminar
		if (!collidingObject)
		{
			return;
		}
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

			cardInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
			cardInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
		}

		cardInHand = null;
	}

	void Update()
	{
		if (collidingObject)
		{
			HoldCard();
		}


		if (Controller.GetHairTriggerUp())	
		{ // caso botão inferior solto
			if (cardInHand)
			{
				ReleaseObject();
			}
		}
	}
}