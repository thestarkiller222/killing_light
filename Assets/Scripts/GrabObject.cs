using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObject : MonoBehaviour
{
    // Punto en el que se colocar�n los objetos agarrados.
    public GameObject handPoint;

    // Objeto actualmente agarrado por esta mano.
    private GameObject pickedObject = null;

    // ----------------------------------------------------------------------------
    // M�todo Update
    // ----------------------------------------------------------------------------
    // Este m�todo se llama en cada frame y gestiona la l�gica de soltar el objeto
    // agarrado actualmente cuando se presiona la tecla "r".

    void Update()
    {
        if (pickedObject != null)
        {
            if (Input.GetKey("r"))
            {
                // Habilita la gravedad en el objeto agarrado.
                pickedObject.GetComponent<Rigidbody>().useGravity = true;

                // Desactiva la simulaci�n cinem�tica en el objeto agarrado.
                pickedObject.GetComponent<Rigidbody>().isKinematic = false;

                // Desvincula el objeto agarrado de cualquier padre.
                pickedObject.gameObject.transform.SetParent(null);

                // Establece el objeto agarrado como nulo para indicar que no se est� sosteniendo ning�n objeto.
                pickedObject = null;
            }
        }
    }

    // ----------------------------------------------------------------------------
    // M�todo OnTriggerStay
    // ----------------------------------------------------------------------------
    // Este m�todo se llama cuando otro objeto con un collider entra y permanece
    // dentro del collider asociado a esta mano. Se encarga de la l�gica para 
    // agarrar objetos cuando se presiona la tecla "e".

    private void OnTriggerStay(Collider other)
    {
        // Verifica si el objeto colisionado tiene la etiqueta "Object".
        if (other.gameObject.CompareTag("Object"))
        {
            // Verifica si la tecla "e" est� siendo presionada y no se est� sosteniendo actualmente ning�n objeto.
            if (Input.GetKey("e") && pickedObject == null)
            {
                // Deshabilita la gravedad en el objeto para que permanezca en la posici�n de la mano.
                other.GetComponent<Rigidbody>().useGravity = false;

                // Activa la simulaci�n cinem�tica para evitar problemas de f�sica mientras se sostiene el objeto.
                other.GetComponent<Rigidbody>().isKinematic = true;

                // Coloca el objeto en la posici�n del punto de agarre en la mano.
                other.transform.position = handPoint.transform.position;

                // Establece la mano como el padre del objeto para seguir su movimiento.
                other.gameObject.transform.SetParent(handPoint.gameObject.transform);

                // Asigna el objeto agarrado actualmente para realizar un seguimiento de �l.
                pickedObject = other.gameObject;
            }
        }
    }
}