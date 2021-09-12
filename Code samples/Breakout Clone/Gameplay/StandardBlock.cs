using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlock : Block

{

    //Prefabs for standard blocks

    [SerializeField] List<Sprite> prefabsBlocks;

    // Start is called before the first frame update
    override protected void Start()
    {
        //picking up a random prefab
        int prefabNumber = Random.Range(0, 3);
        gameObject.GetComponent<SpriteRenderer>().sprite = prefabsBlocks[prefabNumber];

        //setting points
        pointsPerBlock = 1;

        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
