using ChessLogics;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ChessGUI
{
    public static class Images
    {
        private static readonly Dictionary<PieceType, ImageSource> whiteSources = new()
        {
            {PieceType.Pawn,LoadImage("ChessAssets/PawnW.png") },
            {PieceType.Bishop,LoadImage("ChessAssets/BishopW.png") },
            {PieceType.Knight,LoadImage("ChessAssets/KnightW.png") },
            {PieceType.Rook,LoadImage("ChessAssets/RookW.png") },
            {PieceType.Queen,LoadImage("ChessAssets/QueenW.png") },
            {PieceType.King,LoadImage("ChessAssets/KingW.png") }

        };

        private static readonly Dictionary<PieceType, ImageSource> BlackSources = new()
        {
            {PieceType.Pawn,LoadImage("ChessAssets/PawnB.png") },
            {PieceType.Bishop,LoadImage("ChessAssets/BishopB.png") },
            {PieceType.Knight,LoadImage("ChessAssets/KnightB.png") },
            {PieceType.Rook,LoadImage("ChessAssets/RookB.png") },
            {PieceType.Queen,LoadImage("ChessAssets/QueenB.png") },
            {PieceType.King,LoadImage("ChessAssets/KingB.png") }

        };

        private static ImageSource LoadImage(string path)
        {
            return new BitmapImage(new Uri(path,UriKind.Relative));
        }

        public static ImageSource GetImage(Player color,PieceType type)
        {
            return color switch
            {
                Player.White => whiteSources[type],
                Player.Black => BlackSources[type],
                _ => null
            };
        }

        public static ImageSource GetImage(Piece piece)
        {
            if (piece == null)
            {
                return null;
            }

            return GetImage(piece.Color, piece.Type);
        }
    }
}
