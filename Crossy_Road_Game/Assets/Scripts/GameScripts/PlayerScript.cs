using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerScript : MonoBehaviour {

    [SerializeField] private TerrainGenerator tGenerator;
    private Animator playerAnimator;
    private Vector3 movement = new Vector3(0,0,0);
    private bool isHopping = false;
    private const string HOP_KEY = "Hop";

    private void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		
        if(Input.GetKeyDown(KeyCode.W) && !isHopping)
        {
            CharacterMove(new Vector3(1, 0, genWholeNumPosition(transform.position.z)));
        }
        else if (Input.GetKeyDown(KeyCode.S) && !isHopping)
        {
            CharacterMove(new Vector3(-1, 0, genWholeNumPosition(transform.position.z)));
        }
        else if (Input.GetKeyDown(KeyCode.A) && !isHopping)
        {
            CharacterMove(new Vector3(0, 0, 1));
        }
        else if (Input.GetKeyDown(KeyCode.D) && !isHopping)
        {
            CharacterMove(new Vector3(0, 0, -1));
        }
    }

    private float genWholeNumPosition(float notWholeZ)
    {
        float wholeZ = 0;
        if (transform.position.z % 1 != 0)
        {
            wholeZ = Mathf.Round(notWholeZ) - notWholeZ;
        }

        return wholeZ;
    }

    private void CharacterMove(Vector3 nextLocation)
    {
        
        playerAnimator.SetTrigger(HOP_KEY);
        isHopping = true;
        //translate this to DOTween movement.
        transform.position = transform.position + nextLocation;

        //Translate this to broadcasting.
        tGenerator.SpawnTerrain(false, this.transform.position);
    }


    public void FinishedHop()
    {
        isHopping = false;
    }
}
