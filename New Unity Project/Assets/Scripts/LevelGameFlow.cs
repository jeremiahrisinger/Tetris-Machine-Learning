using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameFlow : MonoBehaviour
{
    public GameObject[] piecePrefab;
    public GameObject[] board;
    public GameObject blockPrefab;
    public GameObject emptyPrefab;
    public Transform startPosition;
    public Transform levelStart;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                int blockId = Random.Range(0,3);
				if (blockId != 0)
				{
                    board[i * 12 + j] = Instantiate(blockPrefab,
                                new Vector3(levelStart.position.x + j, levelStart.position.y + i, levelStart.position.z),
                                levelStart.rotation);
				}
				else
				{
                    board[i * 12 + j] = Instantiate(emptyPrefab,
                                new Vector3(levelStart.position.x + j, levelStart.position.y + i, levelStart.position.z),
                                levelStart.rotation);
                }
            }
        }
        StartCoroutine("SpawnBlock");
    }

    // Update is called once per frame
    void Update()
    {

		for (int i = 0; i < 5; i++)
		{
            checkLine(i * 12, (i + 1) * 12);
		}

    }
    void checkLine(int a, int b) {
        int linetotal = 0;
        for (int i = b - 1; i >= a; i--)
        {
            linetotal += board[i].GetComponent<Data>().filled;
        }
        if (linetotal == 12)
        {
            for (int i = a; i < b; i++)
            {
                board[i].SetActive(false);
            }
        }
    }
    IEnumerator SpawnBlock() {
        int pieceId = Random.Range(0, piecePrefab.Length);
        GameObject current = Instantiate(piecePrefab[pieceId], startPosition); ;
        while (true) {
            if (current.GetComponent<PieceControl>().active)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                pieceId = Random.Range(0, piecePrefab.Length);
                current = Instantiate(piecePrefab[pieceId], startPosition);
            }
        }
    }
}
