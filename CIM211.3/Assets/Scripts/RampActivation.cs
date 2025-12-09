using UnityEngine;

[RequireComponent(typeof(Collider))]

public class RampActivation : MonoBehaviour
{

    [SerializeField] private ZoneType zoneType = ZoneType.A;
    [SerializeField] private GameObject rampObject;
    [SerializeField] private GameObject rampPanel;

    private void OnTriggerEnter(Collider other)
    {


        var player = other.GetComponent<PlayerActivate>();

        if (player != null)
        {
            if(!rampObject.activeSelf)
            {
                rampPanel.SetActive(true);
            }
            
            player.SetZone(zoneType);

        }

    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerActivate>();

        if (player != null)
        {
            rampPanel.SetActive(false);
            player.SetZone(ZoneType.None);
        }
    }

    



}
