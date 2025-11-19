namespace Slidingbox.Core {
    public class PuzzleSolver {
        private readonly int size;
        public PuzzleSolver(int size = 3) {
            this.size = size;
        }
        
        private class PuzzleState : IEquatable<PuzzleState> {
            public readonly int[] Tiles;
            private readonly int size;
            private readonly int hash;

            public PuzzleState(int[] tiles, int size) {
                this.size = size;
                Tiles = new int[tiles.Length];
                Array.Copy(tiles, Tiles, tiles.Length);
                int h = 17;
                for (int i = 0; i < Tiles.Length; i++) h = h * 31 + Tiles[i] + 1;
                hash = h;
            }

            public int ManhattanDistance() {
                int sum = 0;
                for (int i = 0; i < Tiles.Length; i++) {
                    int v = Tiles[i];
                    if (v == 0) continue;
                    int targetIdx = v - 1;
                    int r1 = i / size, c1 = i % size;
                    int r2 = targetIdx / size, c2 = targetIdx % size;
                    sum += Math.Abs(r1 - r2) + Math.Abs(c1 - c2);
                }
                return sum;
            }

            public IEnumerable<(PuzzleState neighbor, char move)> Neighbors() {
                int zero = Array.IndexOf(Tiles, 0);
                int r = zero / size, c = zero % size;

                if (r > 0) { yield return (CreateSwapped(zero, zero - size), 'U');}
                if (r < size - 1) { yield return (CreateSwapped(zero, zero + size), 'D'); }
                if (c > 0) { yield return (CreateSwapped(zero, zero - 1), 'L'); }
                if (c < size - 1) { yield return (CreateSwapped(zero, zero + 1), 'R'); }
            }

            private PuzzleState CreateSwapped(int i, int j) {
                int[] copy = new int[Tiles.Length];
                Array.Copy(Tiles, copy, Tiles.Length);
                copy[i] = copy[j];
                copy[j] = 0;
                return new PuzzleState(copy, size);
            }

            public override int GetHashCode() => hash;
            public bool Equals(PuzzleState? other) {
                if (other == null) return false;
                if (other.hash != hash) return false;
                for (int i = 0; i < Tiles.Length; i++) if (Tiles[i] != other.Tiles[i]) return false;
                return true;
            }
            public override bool Equals(object? obj) => Equals(obj as PuzzleState);
        }
    }
}