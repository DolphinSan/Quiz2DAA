using System;
using System.Collections.Generic;
using System.Linq;

namespace Slidingbox.Core {
    public class PuzzleBoard {
        public int Size { get; }
        public int[] Tiles { get; private set; }
        private readonly Random rng = new Random();

        public PuzzleBoard(int size = 3) {
            Size = size;
            Tiles = SolvedTiles();
        }

        public static int[] SolvedTiles(int size = 3) {
            var t = new int[size * size];
            for (int i = 0; i < t.Length - 1; i++) t[i] = i + 1;
            t[^1] = 0;
            return t;
        }

        public void ResetSolved() {
            Tiles = SolvedTiles(Size);
        }

        public int IndexOf(int value) => Array.IndexOf(Tiles, value);

        public bool IsSolved() {
            var solved = SolvedTiles(Size);
            return Tiles.SequenceEqual(solved);
        }

        public bool IsAdjacentIndex(int a, int b) {
            int ra = a / Size, ca = a % Size;
            int rb = b / Size, cb = b % Size;
            return Math.Abs(ra - rb) + Math.Abs(ca - cb) == 1;
        }

        public bool MoveTileAt(int tileIndex) {
            int zero = IndexOf(0);
            if (!IsAdjacentIndex(zero, tileIndex)) return false;
            Tiles[zero] = Tiles[tileIndex];
            Tiles[tileIndex] = 0;
            return true;
        }

        public bool MoveBlank(char dir) {
            int zero = IndexOf(0);
            int r = zero / Size, c = zero % Size;
            int nr = r, nc = c;
            switch (dir) {
                case 'U': nr = r - 1; break;
                case 'D': nr = r + 1; break;
                case 'L': nc = c - 1; break;
                case 'R': nc = c + 1; break;
                default: return false;
            }
            if (nr < 0 || nr >= Size || nc < 0 || nc >= Size) return false;
            int ni = nr * Size + nc;
            Tiles[zero] = Tiles[ni];
            Tiles[ni] = 0;
            return true;
        }

        public void Scramble(int moves = 30) {
            Tiles = SolvedTiles(Size);
            char last = '\0';
            for (int i = 0; i < moves; i++) {
                var movesList = ValidBlankMoves(last).ToList();
                char mv = movesList[rng.Next(movesList.Count)];
                MoveBlank(mv);
                last = mv;
            }
        }

        private IEnumerable<char> ValidBlankMoves(char lastMove) {
            int zero = IndexOf(0);
            int r = zero / Size, c = zero % Size;
            var res = new List<char>();
            if (r > 0) res.Add('U');
            if (r < Size - 1) res.Add('D');
            if (c > 0) res.Add('L');
            if (c < Size - 1) res.Add('R');

            if (lastMove != '\0') {
                char undo = lastMove switch { 'U' => 'D', 'D' => 'U', 'L' => 'R', 'R' => 'L', _ => '\0' };
                if (res.Count > 1) res.RemoveAll(m => m == undo);
            }
            return res;
        }
    }
}