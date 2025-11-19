using System;
using System.Collections.Generic;

namespace Slidingbox.Core {
    public class GameManager {
        public PuzzleBoard Board { get; }
        public PuzzleSolver Solver { get; }
        public int UserMoves { get; private set; }
        public List<char>? LastSolution { get; private set; }

        public GameManager(int size = 3) {
            Board = new PuzzleBoard(size);
            Solver = new PuzzleSolver(size);
            UserMoves = 0;
            LastSolution = null;
        }

        public void NewGame(int scrambleMoves = 30) {
            Board.Scramble(scrambleMoves);
            UserMoves = 0;
            LastSolution = Solver.Solve((int[])Board.Tiles.Clone());
        }

        public bool ClickTileAt(int index) {
            bool moved = Board.MoveTileAt(index);
            if (moved) UserMoves++;
            return moved;
        }

        public List<char>? ComputeSolution() {
            LastSolution = Solver.Solve((int[])Board.Tiles.Clone());
            return LastSolution;
        }

        public bool IsSolved() => Board.IsSolved();
        public void IncrementMoves() {
            UserMoves++;
        }
    }
}