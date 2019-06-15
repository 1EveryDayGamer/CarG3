using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ScrollingScript : MonoBehaviour
{
    public Vector2 speed = new Vector2(2, 2);

    public Vector2 direction = new Vector2(0, -1);

    public bool isLinkedToCamera = false;

    public bool isLooping = false;

    private List<SpriteRenderer> backgroundPart;


    private void Start()
    {

            if(isLooping)
            {
                backgroundPart = new List<SpriteRenderer>();

                for (int i = 0; i < transform.childCount; i++)
                {
                    Transform child = transform.GetChild(i);
                    SpriteRenderer r = child.GetComponent<SpriteRenderer>();
                    if (r != null)
                    {
                        backgroundPart.Add(r);
                    }
                 }
                backgroundPart = backgroundPart.OrderBy(t => t.transform.position.x).ToList();
            }
    }




    // Update is called once per frame
    void Update()
    {
        Vector3 movement = new Vector3(speed.x * direction.x, speed.y * direction.y, 0);

        movement *= Time.deltaTime;
        transform.Translate(movement);

        if (isLinkedToCamera)
        {
            Camera.main.transform.Translate(movement);
        }

        if (isLooping)
        {
            SpriteRenderer firstChild = backgroundPart.FirstOrDefault();
            if (firstChild != null)
            {
                if (firstChild.transform.position.y < Camera.main.transform.position.y)
                {
                    if (firstChild.IsVisibleFrom(Camera.main) == false)
                    {
                        SpriteRenderer lastChild = backgroundPart.LastOrDefault();

                        Vector3 lastPosition = lastChild.transform.position;

                        Vector3 lastSize = (lastChild.bounds.max - lastChild.bounds.min);

                        firstChild.transform.position = new Vector3(firstChild.transform.position.x, lastPosition.y + lastSize.y, firstChild.transform.position.z);

                        backgroundPart.Remove(firstChild);
                        backgroundPart.Add(firstChild);

                    }
                }
            }
        }
    }
}

