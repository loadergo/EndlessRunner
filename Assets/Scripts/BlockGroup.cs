using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Assets.Scripts.Common;
using UnityEngine;

public class BlockGroup : MonoBehaviour
{

    public List<StairType> CanStairTo;
    public int StairHeight;
    public int BlocksLenght;
    public int ExitStairPos;
    public int BlocksHeightUp;
    public int BlocksHeightDown;

    public Transform BonusSpawnPositionsParent;

	private void Start ()
	{
	}

    public void SetBlockSprites(Sprite sprite)
    {
        var blocks= transform.GetChildsWithTag(Tags.FLOOR);
        blocks.ForEach(x => x.GetComponentInChildren<SpriteRenderer>().sprite = sprite);
    }

    public List<Vector3> GetRandomPointsToSpawnObjects(int maxPointsCount)
    {
        if (maxPointsCount == 0) return new List<Vector3>();

        var bonusSpawnPositions = BonusSpawnPositionsParent.GetAllChilds().Select(x => x.transform.position).ToList();
        if (bonusSpawnPositions.Count > maxPointsCount)
        {
            var list = new List<Vector3>(bonusSpawnPositions);
            bonusSpawnPositions.Clear();
            for (int i = 0; i < maxPointsCount; i++)
            {
                var rand = Random.Range(0, list.Count);
                bonusSpawnPositions.Add(list[rand]);
                list.Remove(list[rand]);
            }
        }

        return bonusSpawnPositions;
    }
}
