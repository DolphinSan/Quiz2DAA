using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Slidingbox.Core;

namespace Slidingbox {
    public partial class Form1 : Form {
        private readonly int size = 3;
        private readonly int tileSize = 100;
        private Button[,] tileButtons = new Button[3, 3];
        private GameManager game;
        private Queue<char>? solutionQueue;
        private bool animating = false;

        public Form1() {
            InitializeComponent();
            InitializeBoardUI();
            StartNewGame();
        }

        private void InitializeBoardUI() {
            panelBoard.Width = size * tileSize;
            panelBoard.Height = size * tileSize;
            panelBoard.BackColor = Color.FromArgb(52, 73, 94); 

            for (int r = 0; r < size; r++) {
                for (int c = 0; c < size; c++) {
                    var btn = new Button();
                    btn.Font = new Font("Segoe UI", 32, FontStyle.Bold);
                    btn.Size = new Size(tileSize - 6, tileSize - 6);
                    btn.Location = new Point(c * tileSize + 3, r * tileSize + 3);
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    btn.Cursor = Cursors.Hand;
                    btn.Click += TileButton_Click;
                    btn.Tag = r * size + c;
                    
                    btn.MouseEnter += (s, e) => {
                        if (game.Board.Tiles[(int)btn.Tag] != 0 && !animating) {
                            var currentColor = btn.BackColor;
                            if (currentColor != Color.FromArgb(52, 73, 94)) {
                                btn.BackColor = ControlPaint.Light(currentColor, 0.2f);
                            }
                        }
                    };
                    
                    btn.MouseLeave += (s, e) => {
                        UpdateTileColor(btn);
                    };
                    
                    panelBoard.Controls.Add(btn);
                    tileButtons[r, c] = btn;
                }
            }
        }

        private void UpdateTileColor(Button btn) {
            int index = (int)btn.Tag;
            int value = game.Board.Tiles[index];
            
            if (value == 0) {
                btn.BackColor = Color.FromArgb(52, 73, 94); 
                btn.ForeColor = Color.FromArgb(52, 73, 94);
            } else {
                Color[] tileColors = {
                    Color.FromArgb(46, 204, 113),  // Hijau
                    Color.FromArgb(52, 152, 219),  // Biru
                    Color.FromArgb(155, 89, 182),  // Ungu
                    Color.FromArgb(241, 196, 15),  // Kuning
                    Color.FromArgb(230, 126, 34),  // Oranye
                    Color.FromArgb(231, 76, 60),   // Merah
                    Color.FromArgb(26, 188, 156),  // Tosca
                    Color.FromArgb(52, 73, 94)     // Abu-abu gelap
                };
                
                btn.BackColor = tileColors[(value - 1) % tileColors.Length];
                btn.ForeColor = Color.White;
            }
        }

        private void StartNewGame() {
            game = new GameManager(size);
            game.NewGame(scrambleMoves: 30);
            UpdateUI();
            UpdateLabels();
            solutionQueue = null;
            animating = false;
            animationTimer.Enabled = false;
        }

        private void UpdateLabels() {
            lblMoves.Text = $"Moves: {game.UserMoves}";
            var sol = game.LastSolution ?? game.ComputeSolution();
            lblOptimal.Text = sol == null ? "Optimal: N/A" : $"Optimal: {sol.Count}";
        }

        private void UpdateUI() {
            int[] board = game.Board.Tiles;
            for (int i = 0; i < board.Length; i++) {
                int r = i / size, c = i % size;
                int v = board[i];
                var btn = tileButtons[r, c];
                
                if (v == 0) {
                    btn.Text = "";
                    btn.Enabled = true;
                } else {
                    btn.Text = v.ToString();
                    btn.Enabled = true;
                }
                
                UpdateTileColor(btn);
            }
        }

        private void TileButton_Click(object? sender, EventArgs e) {
            if (animating) return;
            var btn = sender as Button;
            if (btn == null) return;

            int index = (int)btn.Tag;
            bool moved = game.ClickTileAt(index);
            
            if (moved) {
                UpdateUI();
                UpdateLabels();
                if (game.IsSolved()) {
                    animationTimer.Enabled = false;
                    MessageBox.Show($"🎉 Selamat! Puzzle terpecahkan!\n\nJumlah gerakan: {game.UserMoves}\nGerakan optimal: {(game.LastSolution?.Count ?? 0)}", 
                                    "Berhasil!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } else {
                btn.BackColor = Color.FromArgb(231, 76, 60); // Merah
                var t = new System.Windows.Forms.Timer { Interval = 120 };
                t.Tick += (s, ev) => { 
                    UpdateTileColor(btn);
                    t.Stop(); 
                    t.Dispose(); 
                };
                t.Start();
            }
        }

        private void BtnNew_Click(object sender, EventArgs e) {
            StartNewGame();
        }

        private void BtnShuffle_Click(object sender, EventArgs e) {
            if (animating) return;
            game.NewGame(scrambleMoves: 30);
            UpdateUI();
            UpdateLabels();
        }

        private void BtnSolve_Click(object sender, EventArgs e) {
            if (animating) return;

            var moves = game.ComputeSolution();
            if (moves == null) {
                MessageBox.Show("Tidak ada solusi yang ditemukan.", "Solver");
                return;
            }

            solutionQueue = new Queue<char>(moves);
            animating = true;
            animationTimer.Interval = 300;
            animationTimer.Enabled = true;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e) {
            if (solutionQueue == null || solutionQueue.Count == 0) {
                animationTimer.Enabled = false;
                animating = false;
                UpdateLabels();
                if (game.IsSolved()) {
                    MessageBox.Show($"Puzzle berhasil dipecahkan secara otomatis!\n\nTotal gerakan: {game.UserMoves}", 
                                    "Auto-Solve Selesai", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return;
            }

            char mv = solutionQueue.Dequeue();
            game.Board.MoveBlank(mv);
            game.IncrementMoves(); 
            
            UpdateUI();
            UpdateLabels();
        }
    }
}