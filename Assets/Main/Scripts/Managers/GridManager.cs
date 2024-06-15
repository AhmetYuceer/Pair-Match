using UnityEngine;
using System.Collections.Generic;
using Mirror; 

public class GridManager : NetworkBehaviour
{
    public static GridManager Instance;

    [SyncVar] public List<Vector2> _grid = new List<Vector2>();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    [Command(requiresAuthority = false)]
    public void CMDCreateGrid(int x, int y)
    {
        RPCCreateGrid(x, y);
    } 
    
    [ClientRpc]
    public void RPCCreateGrid(int x, int y)
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                _grid.Add(new Vector2(i * 2, j * 2.5f));
            }
        }
    } 

    [Command(requiresAuthority = false)] 
    public void FillGrid(List<Card> cards)
    {
        RPCFillGrid(cards);
    }

    [ClientRpc]
    public void RPCFillGrid(List<Card> cards)
    {
        for (int i = 0; i < _grid.Count; i++)
        {
            cards[i].transform.position = _grid[i];
        }
    }
}