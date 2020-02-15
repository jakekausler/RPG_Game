class Path {
	public int g; //Steps from A to this
	public int h; //Steps from this to B
	public Path parent;
	public float x;
	public float y;

	public Path(int _g, int _h, Path _parent, float _x, float _y) {
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