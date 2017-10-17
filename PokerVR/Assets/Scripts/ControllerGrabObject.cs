using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HTC.UnityPlugin.Vive;

public class ControllerGrabObject : MonoBehaviour
{

    private SteamVR_TrackedObject trackedObj;  // referência para o controle

    private GameObject collidingObject; // referência para objeto que é intersectado

    private GameObject objectInHand; // referência para objeto que vai ser manuseado


	private GameObject cardInHand;
	private GameObject CardHolder;

    private SteamVR_Controller.Device Controller
    {  // Properties para o controle
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {                         // recupera referência para o controle
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }

    private void SetCollidingObject(Collider col)
    {  // guarda o objeto que está colidindo

        if (collidingObject || !col.GetComponent<Rigidbody>())
        {
            return;      // se já segura objeto ou objeto não possui rigidbody então saia
        }

        collidingObject = col.gameObject;  // salva como objeto a mover

    }
		
    public void OnTriggerEnter(Collider other)
    { // invocado se houver colisão
        SetCollidingObject(other);
    }

    public void OnTriggerStay(Collider other)
    { // invocado enquanto houver colisão
        SetCollidingObject(other);
    }

    public void OnTriggerExit(Collider other)
    { // invocado quando a colisão terminar
        if (!collidingObject)
        {
            return;
        }
        collidingObject = null;
    }

    private void GrabObject()
    {  // liga o objeto pego ao controle

        objectInHand = collidingObject;
        collidingObject = null;

        var joint = AddFixedJoint();
        joint.connectedBody = objectInHand.GetComponent<Rigidbody>();
    }
		

    private FixedJoint AddFixedJoint()
    { // cria uma junção para o objeto e controle
        FixedJoint fx = gameObject.AddComponent<FixedJoint>();
        fx.breakForce = 20000;
        fx.breakTorque = 20000;
        return fx;
    }

    private void ReleaseObject()
    { // solta objeto mantendo velocidade

        if (GetComponent<FixedJoint>())
        {

            GetComponent<FixedJoint>().connectedBody = null;
            Destroy(GetComponent<FixedJoint>());

            objectInHand.GetComponent<Rigidbody>().velocity = Controller.velocity;
            objectInHand.GetComponent<Rigidbody>().angularVelocity = Controller.angularVelocity;
        }

        objectInHand = null;
    }

    void Update()
    {
		if (ViveInput.GetPressDown (HandRole.RightHand, ControllerButton.Pad))
        { // caso botão inferior pressionado
            if (collidingObject)
            {
                GrabObject();
            }
        }

		if (ViveInput.GetPressDown (HandRole.RightHand, ControllerButton.Pad))
        { // caso botão inferior solto
            if (objectInHand)
            {
                ReleaseObject();
            }
        }
    }
}
