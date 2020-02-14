using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
 
public class Room {
	public Vector2Int roomCoordinate;

	public Room[] neighbors = new Room[4];
	public int northExit = -9999;
	public int southExit = -9999;
	public int eastExit = -9999;
	public int westExit = -9999;

	public int width;
	public int height;

	public static int max_width = 20;
	public static int max_height = 20;

	public static int min_width = 8;
	public static int min_height = 8;

	public Room(Vector2Int roomCoordinate) {
		this.roomCoordinate = roomCoordinate;
	}

	public int AvailableNeighbors() {
		int a = 0;
		foreach (Room neighbor in neighbors) {
			if (neighbor == null) {
				a += 1;
			}
		}
		return a;
	}

	public int FindNeighbor(int next_i) {
		for (int i=0; i<neighbors.Length; i++) {
			if (neighbors[i] == null) {
				if (next_i == 0) {
					return i;
				}
				next_i--;
			}
		}
		return -1;
	}

	public void SetNeighbor(int idx, Room r) {
		neighbors[idx] = r;
	}

	public void Generate(Tilemap tileMap, Tile floorTile) {
		width = Random.Range(min_width, max_width - 2);
		height = Random.Range(min_height, max_height - 2);

		int baseX = Room.max_width * roomCoordinate.x;
		int baseY = Room.max_height * roomCoordinate.y;

		for (int x=0; x<width; x++) {
			for (int y=0; y<height; y++) {
				tileMap.SetTile(new Vector3Int(baseX+x, baseY+y, 0), floorTile);
			}
		}

		for (int i=0; i<neighbors.Length; i++) {
			if (neighbors[i] != null) {
				switch (i) {
				case 0:
					northExit = Random.Range(0, width) + baseX;
					break;
				case 1:
					southExit = Random.Range(0, width) + baseX;
					break;
				case 2:
					westExit = Random.Range(0, height) + baseY;
					break;
				case 3:
					eastExit = Random.Range(0, height) + baseY;
					break;
				}
			}
		}
	}
}