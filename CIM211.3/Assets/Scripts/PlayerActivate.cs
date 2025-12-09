using UnityEngine;


public enum ZoneType
{
    None,
    A,
    B
}



public class PlayerActivate : MonoBehaviour
{


    [SerializeField] private GameObject objectToActivate;


    private ZoneType currentZone = ZoneType.None;

    private void Start()
    {

        if (objectToActivate != null)
        {

            objectToActivate.SetActive(false);

        }

    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {

            if (currentZone == ZoneType.B && objectToActivate != null)
            {

                if (!objectToActivate.activeSelf)
                {

                    objectToActivate.SetActive(true);

                }


            }


        }


    }

    public void SetZone(ZoneType zone)
    {

        currentZone = zone;


    }

}
