using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridManager : MonoBehaviour
{
    public static GridManager Instance;

    public List<Vector2> _grid;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void CreateGrid(int x , int y)
    {
        _grid = new List<Vector2>();

        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                _grid.Add(new Vector2(i * 2, j * 2.5f));
            }
        }
    }

    public void FillGrid(Stack<Card> cards)
    {
        Debug.Log(_grid.Count);
        foreach (var cell in _grid)
        {
            Card card = cards.Pop();
            card.transform.position = cell;
        }
    }
}