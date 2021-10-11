using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBlock : Block
{
    // Start is called before the first frame update
    override protected void Start()
    {
        pointsPerBlock = 50;
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
