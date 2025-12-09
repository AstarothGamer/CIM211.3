using UnityEngine;

[RequireComponent(typeof(Collider))]

public class RampActivation : MonoBehaviour
{

    [SerializeField] private ZoneType zoneType = ZoneType.A;

    private void OnTriggerEnter(Collider other)
    {


        var player = other.GetComponent<PlayerActivate>();

        if (player != null)
        {

            player.SetZone(zoneType);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerActivate>();

        if (player != null)
        {

            player.SetZone(ZoneType.None);

        }
    }

    



}
