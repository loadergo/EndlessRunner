using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Common;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public List<GameObject> BlockGroupPrefabs;
    public int MinBlockPosY;
    public int MaxBlockPosY;
    public Sprite NightSprite;

    public GameObject CoinPrefab;
    public GameObject RhumPrefab;
    public float RhumSpawnProbability = 0.2f;
    public int RhumMinimalPeriod = 20;

    private Vector2 _nextBlockPosition;
    private List<BlockGroup> _blockGroups;

    private bool _isNight;

    private int _rhumPeriodValue;

    private void Awake()
    {
        _blockGroups = BlockGroupPrefabs.Select(x => x.GetComponent<BlockGroup>()).ToList();
        _isNight = false;
    }

	private void Start()
	{
	    _rhumPeriodValue = RhumMinimalPeriod;
	}

    public void GenerateStartWay()
    {
        GenerateNextBlockGroup();
        GenerateNextBlockGroup();
        GenerateNextBlockGroup();
        GenerateNextBlockGroup();
        GenerateNextBlockGroup();
        GenerateNextBlockGroup();
    }

    public void ChangeDay(bool isNight)
    {
        _isNight = isNight;
    }

    public void GenerateNextBlockGroup()
    {
        //int nextStepY = Random.Range(-1, 2);
        //if (_nextBlockPosition.y + nextStepY > MaxBlockPosY)
        //{
        //    nextStepY = -nextStepY;
        //}
        //else if (_nextBlockPosition.y + nextStepY < MinBlockPosY)
        //{
        //    nextStepY = -nextStepY;
        //}

        //_nextBlockPosition = new Vector2(_nextBlockPosition.x, _nextBlockPosition.y + nextStepY);

        var maxUpValue = MaxBlockPosY - _nextBlockPosition.y;
        var maxDownValue = _nextBlockPosition.y - MinBlockPosY;

        //StairType stair = (StairType) nextStepY;

        //var listToCheckGroup = _blockGroups.Where(x => x.CanStairTo.Contains(stair)).ToList();
        var listToCheckGroup = _blockGroups.Where(x => x.BlocksHeightUp <= maxUpValue && x.BlocksHeightDown <= maxDownValue).ToList();
        int nextBlockGroupNum = Random.Range(0, listToCheckGroup.Count);
        BlockGroup checkedGroupBlock = listToCheckGroup[nextBlockGroupNum];
        BlockGroup newBlockGroup = InstantiateNextBlockGroup(checkedGroupBlock.gameObject);
        
        //calc custom random value
        int rand = Random.Range(0, 500);
        int randValue;
        if (rand >= 300)
        {
            randValue = 0;
        }
        else if (rand >= 150)
        {
            randValue = 1;
        }
        else if (rand >= 20)
        {
            randValue = 2;
        }
        else
        {
            randValue = 3;
        }

        //Debug.Log("rand = " + rand);
        //Debug.Log("randValue = " + randValue);
        SpawnBonuses(newBlockGroup.GetRandomPointsToSpawnObjects(randValue));

        _nextBlockPosition += new Vector2(checkedGroupBlock.BlocksLenght, checkedGroupBlock.ExitStairPos);
    }

    private BlockGroup InstantiateNextBlockGroup(GameObject blockGroupPrefab)
    {
        var newBlockGroup = Instantiate(blockGroupPrefab);
        newBlockGroup.transform.position = _nextBlockPosition;
        newBlockGroup.transform.localScale = Vector3.one;
        BlockGroup blockGroupScript = newBlockGroup.GetComponent<BlockGroup>();
        if (_isNight)
        {
            blockGroupScript.SetBlockSprites(NightSprite);
        }

        return blockGroupScript;
    }

    private void SpawnBonuses(List<Vector3> points)
    {
        points.ForEach(x =>
        {
            if (_rhumPeriodValue >= RhumMinimalPeriod)
            {
                RandTryToSpawnRhum(x);
            }
            else
            {
                SpawnCoin(x); 
            }
            
        });
    }

    private void RandTryToSpawnRhum(Vector3 spawnPoint)
    {
        var rand = Random.Range(0, 100);
        if (rand < RhumSpawnProbability * 100)
        {
            SpawnRhum(spawnPoint);
        }
        else
        {
            SpawnCoin(spawnPoint);
        }
    }

    private void SpawnRhum(Vector3 spawnPoint)
    {
        _rhumPeriodValue = 0;
        Instantiate(RhumPrefab, spawnPoint, Quaternion.identity);
    }

    private void SpawnCoin(Vector3 spawnPoint)
    {
        _rhumPeriodValue++;
        Instantiate(CoinPrefab, spawnPoint, Quaternion.identity);
    }


}
