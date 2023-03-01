using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{

    [SerializeField] private Block block;
    private int w;
    private int h;
    private int bombCount;
    public GameObject canvas;
    public GameObject GameCanvas;

    private Block[,] blocks;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Init()
    {
        w = GameController.width;
        h = GameController.height;
        bombCount = GameController.bomb;
        GameCanvas.SetActive(true);
        canvas.SetActive(false);
        blocks = new Block[w, h];
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) { 
                var blockType = GetBlockType();
                blocks[i, j] = Instantiate(block, new Vector2(i, j), Quaternion.identity);
                blocks[i, j].blocktype= blockType;
                blocks[i, j].i= i;
                blocks[i, j].j= j;
            }
        }
        UpdateNextBlocks();
    }

    void UpdateNextBlocks() {
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                int n = 0;
                if (blocks[i, j].blocktype == BlockType.Mine)
                    continue;
                var list = GetAllNearbyBlock(i, j);

                foreach (var block in list ) {
                    if (block.blocktype == BlockType.Mine)
                        n++;
                }

                if (n > 0)
                    blocks[i, j].blocktype = (BlockType)(n + 7);
            }
        }
    }

    BlockType GetBlockType() {
        if (Random.Range(1, 1000) % 5 == 0 && bombCount > 0) {
            bombCount--;
            return BlockType.Mine;
        } else {
            return BlockType.Empty;
        }
    }

    Block GetBlock(int i, int j) {
        if (i < 0 || j < 0 || i >= w || j >= h)
            return null;
        return blocks[i, j];
    }

    List<Block> GetAllNearbyBlock(int i, int j, bool includeCorner = true) {
        List<Block> list = new List<Block>();
        list.Add(GetBlock(i + 1, j));
        list.Add(GetBlock(i, j + 1));
        list.Add(GetBlock(i - 1, j));
        list.Add(GetBlock(i, j - 1));

        if (includeCorner) {
            list.Add(GetBlock(i + 1, j + 1));
            list.Add(GetBlock(i - 1, j - 1));
            list.Add(GetBlock(i + 1, j - 1));
            list.Add(GetBlock(i - 1 , j + 1));
        }

        return list
            .Where(x => x != null)
            .ToList();
    }

    void RevealAll() {
        for (int i = 0; i < w; i++) {
            for (int j = 0; j < h; j++) {
                blocks[i, j].Revealed();
            }
        }
    }

    public void RevealEmptyBox(int i, int j) {
        List<Block> list = GetAllNearbyBlock(i, j, false);
        list = list.Where(x => !x.show).ToList();
        foreach (var block in list) {
            if (block.blocktype == BlockType.Empty && !block.show) {
                block.Revealed();   
                RevealEmptyBox(block.i, block.j);   
            }
        }
    }

    public void LoseGame() { 
        RevealAll();
    }
    
}
