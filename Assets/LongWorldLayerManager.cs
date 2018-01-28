using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongWorldLayerManager : MonoBehaviour {

    public float paralaxFactor = 1.0f; // 1->0, Front->Back
    public GameObject blockToCopy = null;

    public float frameSize = 10.0f;
    public float blockSize = 0.0f;

    private GameObject _blockA;
    private GameObject _blockB;
    // ABABAB...

	void Start () { Initialize(); }

    private void Initialize() {
        _blockA = GameObject.Instantiate(blockToCopy);
        _blockB = GameObject.Instantiate(blockToCopy);
    }

    private void Update() { updateForFrames(); }
    
    void updateForFrames() {
        float theFrameBegin = (Camera.main.transform.position.x - frameSize/2)*paralaxFactor;
        float theFrameEnd = theFrameBegin + frameSize;

        //Create objects by frame
        int theFirstBlockIndex = Mathf.FloorToInt(theFrameBegin/blockSize);

        if (0 == theFirstBlockIndex % 2) {
           placeBlocks(theFirstBlockIndex*blockSize, _blockA, _blockB);
        } else {
           placeBlocks(theFirstBlockIndex*blockSize, _blockB, _blockA);
        }
    }

    void placeBlocks(float inFirstBlockPosition, GameObject inFirstBlock, GameObject inSecondBlock) {
        Vector3 thePosition = new Vector3(inFirstBlockPosition, transform.position.y, transform.position.z);

        float theCorrection = (Camera.main.transform.position.x - frameSize/2) * (1.0f - paralaxFactor);

        thePosition.x = theCorrection + inFirstBlockPosition;
        inFirstBlock.transform.position = thePosition;
        
        thePosition.x = theCorrection + inFirstBlockPosition + blockSize;
        inSecondBlock.transform.position = thePosition;
    }
}
