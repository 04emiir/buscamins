using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum BlockType {
    Block = 0,
    Empty = 1,
    Flag = 2,
    Q1 = 3,
    Q2 = 4,
    Mine = 5,
    MineExplode = 6,
    MineDelete = 7,
    N1 = 8,
    N2 = 9,
    N3 = 10,
    N4 = 11,
    N5 = 12,
    N6 = 13,
    N7 = 14,
    N8 = 15
}

public class Block : MonoBehaviour
{
    [SerializeField] private List<Sprite> blockSprites;
    public BlockType blocktype = 0;
    public int i = 0;
    public int j = 0;

    public bool show = false;
    public bool flag = false;
    private SpriteRenderer sb;

    private void Awake() {
        sb = GetComponent<SpriteRenderer>();
    }

    private void OnMouseOver() {
        if (Input.GetMouseButtonDown(0)) {
            Clicked();
        }

        if (Input.GetMouseButtonDown(1)) {
            ShowFlag();
        }
    }

    public void Clicked() {
        if (blocktype == BlockType.Mine) {
            blocktype = BlockType.MineExplode;
            FindObjectOfType<Board>().LoseGame();
        } else if (blocktype == BlockType.Empty) {
            FindObjectOfType<Board>().RevealEmptyBox(i, j); ;
        }
        Revealed();
    }

    public void Revealed() {
        if (!show) {
            show = true;
            sb.sprite = blockSprites[(int)blocktype];
        }
    }

    public void ShowFlag() {
        if (!flag && !show) {
            flag = true;
            sb.sprite = blockSprites[(int)BlockType.Flag];
        } else if (flag) {
            flag = false;
            sb.sprite = blockSprites[(int)BlockType.Block];
        }


    }
}
