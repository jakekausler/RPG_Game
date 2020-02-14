using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
 
public class DungeonGeneration : MonoBehaviour {

	[SerializeField]
	private int numberOfRooms;

	[SerializeField]
	private float connectionChance;
	
	[SerializeField]
	private Tilemap tileMap;

	[SerializeField]
	private Tile floorTile;

	[SerializeField]
	private Tile wallTile;

	[SerializeField]
	private string floorTileName;

	[SerializeField]
	private GameObject line;

	[SerializeField]
	private GameObject circle;

	private Room startRoom;

	private List<Room> rooms = new List<Room>();

	void Start () {
		numberOfRooms = Random.Range(5, 25) - 1;
		startRoom = GenerateDungeon();

		GenerateRooms();
		GenerateCorridors();
		FillWalls();
	}

	private Room GenerateDungeon() {
		Room startRoom = new Room(new Vector2Int(0, 0));

		rooms.Add(startRoom);

		int step = 0;
		while (step < numberOfRooms) {
			int idx = Random.Range(0, rooms.Count);
			Room chosenRoom = rooms[idx];
			int available = chosenRoom.AvailableNeighbors();
			if (available == 0) {
				continue;
			}
			int neighborIndex = chosenRoom.FindNeighbor(Random.Range(0, available));
			int x;
			int y;
			Room existingRoom = null;
			switch (neighborIndex) {
			//North
			case 0:
				x = chosenRoom.roomCoordinate.x;
				y = chosenRoom.roomCoordinate.y + 1;
				foreach (Room r in rooms) {
					if (r.roomCoordinate.x == x && r.roomCoordinate.y == y) {
						existingRoom = r;
						break;
					}
				}
				if (existingRoom != null && Random.value < connectionChance) {
					existingRoom.SetNeighbor(1, chosenRoom);
					chosenRoom.SetNeighbor(0, existingRoom);
				} else if (existingRoom == null) {
					Room newRoom = new Room(new Vector2Int(x, y));
					rooms.Add(newRoom);
					newRoom.SetNeighbor(1, chosenRoom);
					chosenRoom.SetNeighbor(0, newRoom);
					step++;
				}
				break;
			//South
			case 1:
				x = chosenRoom.roomCoordinate.x;
				y = chosenRoom.roomCoordinate.y - 1;
				foreach (Room r in rooms) {
					if (r.roomCoordinate.x == x && r.roomCoordinate.y == y) {
						existingRoom = r;
						break;
					}
				}
				if (existingRoom != null && Random.value < connectionChance) {
					existingRoom.SetNeighbor(0, chosenRoom);
					chosenRoom.SetNeighbor(1, existingRoom);
				} else if (existingRoom == null) {
					Room newRoom = new Room(new Vector2Int(x, y));
					rooms.Add(newRoom);
					newRoom.SetNeighbor(0, chosenRoom);
					chosenRoom.SetNeighbor(1, newRoom);
					step++;
				}
				break;
			//West
			case 2:
				x = chosenRoom.roomCoordinate.x - 1;
				y = chosenRoom.roomCoordinate.y;
				foreach (Room r in rooms) {
					if (r.roomCoordinate.x == x && r.roomCoordinate.y == y) {
						existingRoom = r;
						break;
					}
				}
				if (existingRoom != null && Random.value < connectionChance) {
					existingRoom.SetNeighbor(3, chosenRoom);
					chosenRoom.SetNeighbor(2, existingRoom);
				} else if (existingRoom == null) {
					Room newRoom = new Room(new Vector2Int(x, y));
					rooms.Add(newRoom);
					newRoom.SetNeighbor(3, chosenRoom);
					chosenRoom.SetNeighbor(2, newRoom);
					step++;
				}
				break;
			//East
			case 3:
				x = chosenRoom.roomCoordinate.x + 1;
				y = chosenRoom.roomCoordinate.y;
				foreach (Room r in rooms) {
					if (r.roomCoordinate.x == x && r.roomCoordinate.y == y) {
						existingRoom = r;
						break;
					}
				}
				if (existingRoom != null && Random.value < connectionChance) {
					existingRoom.SetNeighbor(2, chosenRoom);
					chosenRoom.SetNeighbor(3, existingRoom);
				} else if (existingRoom == null) {
					Room newRoom = new Room(new Vector2Int(x, y));
					rooms.Add(newRoom);
					newRoom.SetNeighbor(2, chosenRoom);
					chosenRoom.SetNeighbor(3, newRoom);
					step++;
				}
				break;
			}
		}
		return startRoom;
	}

	private void GenerateRooms() {
		foreach(Room room in rooms) {
			room.Generate(tileMap, floorTile);
		}
		tileMap.RefreshAllTiles();
	}

	static List<Vector2Int> Bresenham(int x0, int y0, int x1, int y1) {
		int dx = (int)(Mathf.Abs(x1-x0)), sx = x0<x1 ? 1 : -1;
		int dy = (int)(-Mathf.Abs(y1-y0)), sy = y0<y1 ? 1 : -1;
		int err = dx+dy, e2;

		List<Vector2Int> retVal = new List<Vector2Int>();

		for (;;) {
			retVal.Add(new Vector2Int(x0, y0));
			if (x0==x1 && y0==y1) break;
			e2 = 2*err;
			if (e2 >= dy) {err += dy; x0 += sx; }
			if (e2 <= dx) {err += dx; y0 += sy; }
		}
		return retVal;
	}

	private void GenerateCorridors() {
		foreach(Room room in rooms) {
			if (room.northExit > -9999) {
				int startX = room.northExit;
				int startY = room.roomCoordinate.y * Room.max_height + room.height;
				int endX = room.neighbors[0].southExit;
				int endY = room.neighbors[0].roomCoordinate.y * Room.max_height - 1;
				List<Vector2Int> values = Bresenham(startX, startY, endX, endY);
				foreach (Vector2Int v in values) {
					tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
				}
				if (endX-startX !=0 && ((float)(endY-startY))/(endX-startX)<1) {
					values = Bresenham(startX, startY-1, endX, endY-1);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
					values = Bresenham(startX, startY+1, endX, endY+1);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
				} else {
					values = Bresenham(startX-1, startY, endX-1, endY);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
					values = Bresenham(startX+1, startY, endX+1, endY);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
				}
			}
			if (room.eastExit > -9999) {
				int startX = room.roomCoordinate.x * Room.max_width + room.width;
				int startY = room.eastExit;
				int endX = room.neighbors[3].roomCoordinate.x * Room.max_width - 1;
				int endY = room.neighbors[3].westExit;
				List<Vector2Int> values = Bresenham(startX, startY, endX, endY);
				foreach (Vector2Int v in values) {
					tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
				}
				if (endX-startX !=0 && ((float)(endY-startY))/(endX-startX)<1) {
					values = Bresenham(startX, startY-1, endX, endY-1);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
					values = Bresenham(startX, startY+1, endX, endY+1);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
				} else {
					values = Bresenham(startX-1, startY, endX-1, endY);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
					values = Bresenham(startX+1, startY, endX+1, endY);
					foreach (Vector2Int v in values) {
						tileMap.SetTile(new Vector3Int(v.x, v.y, 0), floorTile);
					}
				}
			}
		}
	}

	private void FillWalls() {
		int xMin = tileMap.cellBounds.xMin - 25;
		int yMin = tileMap.cellBounds.yMin - 25;
		int xMax = tileMap.cellBounds.xMax + 25;
		int yMax = tileMap.cellBounds.yMax + 25;
		for (int x = xMin; x < xMax + 1; x++) {
			for (int y = yMin; y < yMax + 1; y++) {
				if (tileMap.GetTile(new Vector3Int(x, y, 0)) == null) {
					tileMap.SetTile(new Vector3Int(x, y, 0), wallTile);
				}
			}
		}

	}

	private void PrintRooms(Room startRoom) {
		List<string> traversed = new List<string>();
		Queue<Room> toExplore = new Queue<Room>();
		toExplore.Enqueue(startRoom);
		while (toExplore.Count > 0) {
			Room current = toExplore.Dequeue();
			Instantiate(circle, new Vector3(current.roomCoordinate.x, current.roomCoordinate.y, 0), Quaternion.Euler(90f, 0f, 0f));
			for (int i=0; i<current.neighbors.Length; i++) {
				Room neighbor = current.neighbors[i];
				if (neighbor != null) {
					string edgeName = "";
					if (i == 0 || i == 3) {
						edgeName = neighbor.roomCoordinate.x + "x" + neighbor.roomCoordinate.y + "_" + current.roomCoordinate.x + "x" + current.roomCoordinate.y;
					} else {
						edgeName = current.roomCoordinate.x + "x" + current.roomCoordinate.y + "_" + neighbor.roomCoordinate.x + "x" + neighbor.roomCoordinate.y;
					}
					if (!traversed.Contains(edgeName)) {
						toExplore.Enqueue(neighbor);
						traversed.Add(edgeName);
						if (i == 0) {
							Instantiate(line, new Vector3(current.roomCoordinate.x, current.roomCoordinate.y + 0.5f, 0), Quaternion.Euler(0f, 0f, 90f));
						} else if (i == 1) {
							Instantiate(line, new Vector3(current.roomCoordinate.x, current.roomCoordinate.y - 0.5f, 0), Quaternion.Euler(0f, 0f, 90f));
						} else if (i == 2) {
							Instantiate(line, new Vector3(current.roomCoordinate.x - 0.5f, current.roomCoordinate.y, 0), Quaternion.Euler(0f, 0f, 0f));
						} else {
							Instantiate(line, new Vector3(current.roomCoordinate.x + 0.5f, current.roomCoordinate.y, 0), Quaternion.Euler(0f, 0f, 0f));
						}
					}
				}
			}
		}
	}

}
