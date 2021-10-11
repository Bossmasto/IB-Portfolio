using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBuilder : MonoBehaviour
{

    [SerializeField] GameObject prefabPaddle;

    [SerializeField] GameObject prefabStdBlock;

    [SerializeField] GameObject bonusBlock;

    [SerializeField] GameObject prefabPickup;

    // Start is called before the first frame update
    void Start()
    {
        //instantiabting a paddle
        Instantiate(prefabPaddle);

        GenerateLevel();

    }

    // Update is called once per frame
    void Update()
    {

    }

    void GenerateLevel()
    {
        //retreiving a block size
        GameObject temp = Instantiate(prefabStdBlock);
        float blockWidth = temp.GetComponent<BoxCollider2D>().size.x;
        float blockHeights = temp.GetComponent<BoxCollider2D>().size.y;
        Destroy(temp);


        Vector2 position = new Vector2(0, ScreenUtils.ScreenTop - (ScreenUtils.ScreenTop - ScreenUtils.ScreenBottom) / 5);

        for (var y = 0; y < 3; y++)
        {
            position.x = 0;
            PlaceBlock(position) ;
            for (var i = 0; i < 4; i++)
            {

                position.x = position.x + blockWidth/2;
                PlaceBlock(position);
            }
            position.x = 0;
            for (var i = 0; i < 4; i++)
            {
                position.x = position.x - blockWidth/2;
                PlaceBlock(position);
            }
            position.y = position.y - blockHeights / 2;
            
            
        }
    }
    void PlaceBlock(Vector2 position)
    {
        float randomBlockType = Random.value;
        if (randomBlockType < 0.5)
        {
            Instantiate(prefabStdBlock, position, Quaternion.identity);
        }
        else if (randomBlockType <
                 (0.5 + 0.2))
        {
            Instantiate(bonusBlock, position, Quaternion.identity);
        }
        else
        {
            // pickup block selected
            GameObject pickupBlock = Instantiate(prefabPickup, position, Quaternion.identity);
            PickupBlock pickupBlockScript = pickupBlock.GetComponent<PickupBlock>();

            // set pickup effect  (probabilities)
            float freezerThreshold = 0.7f + 0.1f + 0.1f;

            if (randomBlockType < freezerThreshold)
            {
                pickupBlockScript.Effect = PickupEffect.Freezer;
            }
            else
            {
                pickupBlockScript.Effect = PickupEffect.Speedup;
            }
        }
    }
}
