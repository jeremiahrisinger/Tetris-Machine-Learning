using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGameFlow : MonoBehaviour
{
    public GameObject[] piecePrefab;
    public GameObject[] board;
    public GameObject[] triggers;
    public GameObject blockPrefab;
    public GameObject emptyPrefab;
    public GameObject endScreen;
    public Transform startPosition;
    public Transform levelStart;
    public int height = 2;
    // Start is called before the first frame update
    void Start()
    {
        board = new GameObject[height * 12];
        //triggers for every spot on the board
        triggers = new GameObject[20 * 12];
        
		for (int i = 0; i < 20; i++)
		{
			for (int j = 0; j < 12; j++)
			{
				triggers[i * 12 + j] = Instantiate(emptyPrefab,
							new Vector3(levelStart.position.x + j, levelStart.position.y + i, levelStart.position.z),
							levelStart.rotation);
                triggers[i * 12 + j].name = (i * 12 + j).ToString(); 
			}
		}
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < 12; j++)
            {
                int blockId = Random.Range(0, 3);
                if (blockId != 0)
                {
                    board[i * 12 + j] = Instantiate(blockPrefab,
                                new Vector3(levelStart.position.x + j, levelStart.position.y + i, levelStart.position.z),
                                levelStart.rotation);
                    triggers[i * 12 + j].GetComponent<Data>().filler = board[i * 12 + j];
                    triggers[i * 12 + j].GetComponent<Data>().filled = 1;
                }
            }
        }
        StartCoroutine("SpawnBlock");
    }

    // Update is called once per frame
    void Update()
    {
        IEnumerator coroutine;
		for (int i = 0; i < 20; i++)
		{
            coroutine = checkLine(i * 12, (i + 1) * 12);
            StartCoroutine(coroutine);
		}
        if(board[0] == null)
		{
            print("you win " + board[0]);
            //Time.timeScale = 0;
            StartCoroutine("EndGame");
		}
    }
    IEnumerator checkLine(int a, int b) {
        int linetotal = 0;
        for (int i = b - 1; i >= a; i--)
        {
            linetotal += triggers[i].GetComponent<Data>().filled;
        }
        if (linetotal == 12)
        {
            yield return new WaitForSeconds(.1f);
            for (int i = a; i < b; i++)
            {
                triggers[i].GetComponent<Data>().deleteObject();
                triggers[i].GetComponent<Data>().filled = 0;
                triggers[i].GetComponent<Data>().filler = null;
            }
        }
    }
    IEnumerator Pause()
	{
        yield return new WaitForSeconds(2.0f);
	}
    IEnumerator SpawnBlock() {
        yield return new WaitForSeconds(2.0f);
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
    IEnumerator EndGame()
	{
        yield return new WaitForSeconds(2.0f);
        endScreen.SetActive(true);

	}
}
