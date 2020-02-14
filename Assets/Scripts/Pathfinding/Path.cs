class Path {
	public int g; //Steps from A to this
	public int h; //Steps from this to B
	public Path parent;
	public int x;
	public int y;

	public Path(int _g, int _h, Path _parent, int _x, int _y) {
		g = _g;
		h = _h;
		parent = _parent;
		x = _x;
		y = _y;
	}

	//Total score
	public int f {
		get {
			return g+h;
		}
	}
}