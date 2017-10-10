using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;


public class CardHolder : MonoBehaviour
{

	private GameObject collidingObject; // referência para objeto que é intersectado

	private GameObject cardInHand; // referência para objeto que vai ser manuseado





	private void SetCollidingObject(Collider col)
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

	public void OnTriggerEnter(Collider other)
	{ // invocado se houver colisão
		if (other.gameObject.tag == "Card") {
			print ("Achou a carta");
			SetCollidingObject(other);
			
		}
	}

	public void OnTriggerStay(Collider other)
	{ // invocado se houver colisão
		if (other.gameObject.tag == "Card") {
			print ("Achou a carta");
			SetCollidingObject(other);

		}
	}

	public void OnTriggerExit(Collider other)
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

			cardInHand.GetComponent<Rigidbody>().velocity = this.GetComponent<Rigidbody>().velocity;
			cardInHand.GetComponent<Rigidbody>().angularVelocity = this.GetComponent<Rigidbody>().angularVelocity;
		}

		cardInHand = null;
	}

	void Update()
	{
		if (ViveInput.GetPressDown (HandRole.RightHand, ControllerButton.Trigger) || ViveInput.GetPressDown (HandRole.LeftHand, ControllerButton.Trigger)) {
			//print ("PRESSDOWN");

			if (collidingObject)
			{
				print ("COLLIDERRR");
				HoldCard();
			}
		}



		if (ViveInput.GetPressDown (HandRole.RightHand, ControllerButton.Trigger) || ViveInput.GetPressDown (HandRole.LeftHand, ControllerButton.Trigger)) {
		{ // caso botão inferior solto
			print ("PRESSDOWN");

			if (cardInHand)
			{
				print ("saaaiuuu");

				ReleaseObject();
			}
		}
	}
}

}