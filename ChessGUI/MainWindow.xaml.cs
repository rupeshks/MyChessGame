using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ChessLogics;
using System.Windows.Shapes;

namespace ChessGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Image[,] pieceImage = new Image[8, 8];
        private readonly Rectangle[,] highlights = new Rectangle[8, 8];
        private readonly Dictionary<Position,Move> moveCache = new Dictionary<Position,Move>();

        private GameState gameState;
        private Position selectedPos = null;

        public MainWindow()
        {
            InitializeComponent();
            InitializeBoard();

            gameState = new GameState(Player.White, Board.Initial());
            DrawBoard(gameState.Board);
        }

        private void InitializeBoard()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    Image image = new Image();
                    pieceImage[i, j] = image;
                    PieceGrid.Children.Add(image);

                    Rectangle highlight=new Rectangle();
                    highlights[i, j] = highlight;
                    HighlightGrid.Children.Add(highlight);
                }
            }
        }

        private void DrawBoard(Board board)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Piece piece = board[i, j];
                    pieceImage[i, j].Source = Images.GetImage(piece);
                }
            }
        }

        private void BoardGrid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Point point=e.GetPosition(BoardGrid);

            Position pos=ToSquarePosition(point);

            if(selectedPos==null)
            {
                OnFromPositionSelected(pos);
            }
            else
            {
                OnToPositionSelected(pos);
            }
        }

        private void OnToPositionSelected(Position pos)
        {
            selectedPos=null;
            HideHighlights();

            if(moveCache.TryGetValue(pos,out Move move))
            {
                HandleMove(move);
            }
        }

        private void HandleMove(Move move)
        {
            gameState.MakeMove(move);
            DrawBoard(gameState.Board);
        }

        private void OnFromPositionSelected(Position pos)
        {
            IEnumerable<Move> moves=gameState.LegalMovesForPiece(pos);

            if(moves.Any())
            {
                selectedPos=pos;
                CacheMoves(moves);
                ShowHighlights();
            }
        }

        private Position ToSquarePosition(Point point)
        {
            double squareSize = BoardGrid.ActualWidth / 8;
            int row=(int)(point.Y / squareSize);
            int col = (int)(point.X / squareSize);

            return new Position(row, col);  
        }

        private void CacheMoves(IEnumerable<Move> moves)
        {
            foreach (Move move in moves)
            {
                moveCache.Clear();

                moveCache[move.ToPos]=move;
            }
        }

        private void ShowHighlights()
        {
            Color color = Color.FromArgb(150, 125, 255, 125);

            foreach(Position to in moveCache.Keys)
            {
                highlights[to.Row,to.Column].Fill=new SolidColorBrush(color);
            }
        }

        private void HideHighlights()
        {
            foreach (Position to in moveCache.Keys)
            {
                highlights[to.Row, to.Column].Fill = Brushes.Transparent;
            }
        }
    }
}