using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class MoveGenerator
{
    public static List<int> possibleMoves(Piece piece, ChessBoard board)
    {
        if (piece.getType().Equals(Piece.TypeOfPiece.PAWN)) return pawnMoves(piece.getPos(), piece.getTeam(), board);
        if (piece.getType().Equals(Piece.TypeOfPiece.ROOK)) return rookMoves(piece.getPos(), board); 
        if (piece.getType().Equals(Piece.TypeOfPiece.KNIGHT)) return knightMoves(piece.getPos(), board);
        if (piece.getType().Equals(Piece.TypeOfPiece.BISHOP)) return bishopMoves(piece.getPos(), board); 
        if (piece.getType().Equals(Piece.TypeOfPiece.QUEEN)) return queenMoves(piece.getPos(), board);
        if (piece.getType().Equals(Piece.TypeOfPiece.KING)) return kingMoves(piece.getPos(), board);


        return new List<int>();
    }

    public static List<int> queenMoves(int pos, ChessBoard board)
    {
        return Join<int>(rookMoves(pos, board), bishopMoves(pos, board));
    }

    public static List<int> rookMoves(int pos)
    {
        List<int> results = new List<int>();
        int line = pos / 8;
        int col = pos % 8;
        for (int i = 0; i < 8; i++)
        {
            results.Add(i*8+col);
            results.Add(line*8 + i);
        }
        return results;
    }

    public static List<int> rookMoves(int pos, ChessBoard board)
    {
        List<int> moves = new List<int>();
        int line = pos / 8;
        int col = pos % 8;
        moves.Add(pos);
        for (int i = pos+1;  i < (line+1)*8; i++)
        {
            if (board.GetPiece(i) != null && board.GetPiece(i).getTeam().Equals(board.GetPiece(pos).getTeam())) break;
            moves.Add(i);
            if(!board.isFree(i)) break;
        }
        for (int i = pos - 1; i >= line*8; i--)
        {
            if (board.GetPiece(i) != null && board.GetPiece(i).getTeam().Equals(board.GetPiece(pos).getTeam())) break;
            moves.Add(i);
            if (!board.isFree(i)) break;
        }
        for (int i = pos + 8; i < 64; i+=8) 
        {
            if (board.GetPiece(i) != null && board.GetPiece(i).getTeam().Equals(board.GetPiece(pos).getTeam())) break;
            moves.Add(i);
            if (!board.isFree(i)) break;
        }
        for (int i = pos - 8; i >= 0; i -= 8)
        {
            if (board.GetPiece(i) != null && board.GetPiece(i).getTeam().Equals(board.GetPiece(pos).getTeam())) break;
            moves.Add(i);
            if (!board.isFree(i)) break;
        }
        return moves;
    }

    public static List<int> kingMoves(int pos, ChessBoard board)
    {
        List<int> moves = new List<int>();
        moves.Add(pos+1);
        moves.Add(pos-1);
        moves.Add(pos+8);
        moves.Add(pos-8);
        moves.Add(pos+7);
        moves.Add(pos-7);
        moves.Add(pos+9);
        moves.Add(pos-9);

        List<int> results = new List<int>();

        foreach (var item in moves)
        {
            if (item >= 0 && item <= 63)
            {
                if (board.isFree(item) || board.GetPiece(item).getTeam() != board.GetPiece(pos).getTeam())
                    results.Add(item);
            }
        }
        results.Add(pos);

        return results;
    }

    public static List<int> bishopMoves(int pos, ChessBoard board)
    {
        List<int> allMoves = new List<int>();
        List<int> digMoves1 = openDiagonalMoves(pos, board);
        List<int> digMoves2 = closedDiagonalMoves(pos, board);
        allMoves = Join<int>(digMoves1, digMoves2);
        return allMoves;
    }

    public static List<int> openDiagonalMoves(int pos, ChessBoard board)
    {
        List<int> results = new List<int>();
        results.Add(pos);
        for(int i = pos + 9; i<64; i+=9)
        {
            if (pos % 8 == 7) break;
            if (!board.isFree(i) && board.GetPiece(i).getTeam() == board.GetPiece(pos).getTeam()) break;
            results.Add(i);
            if(!board.isFree(i)) break;
            if(i%8 == 7)
            {
                break;
            }
        }
        for(int i = pos - 9; i>=0; i-=9)
        {
            if (pos % 8 == 0) break;
            if (!board.isFree(i) && board.GetPiece(i).getTeam() == board.GetPiece(pos).getTeam()) break;
            results.Add(i);
            if (!board.isFree(i)) break;
            results.Add(i);
            if(i%8 == 0)
            {
                break;
            }
        }
        return results;
    }

    public static List<int> knightMoves(int pos, ChessBoard board)
    {
        List<int> possibleRessults = new List<int> ();
        if(pos%8 > 1 && pos/8 != 7)  possibleRessults.Add(pos+6);
        if (pos % 8 < 6 && pos / 8 != 0) possibleRessults.Add(pos-6);
        if (pos % 8 < 6 && pos / 8 != 7) possibleRessults.Add(pos+10);
        if (pos % 8 > 1 && pos / 8 != 0) possibleRessults.Add(pos-10);
        if (pos % 8 != 0 && pos / 8 < 6) possibleRessults.Add(pos+15);
        if (pos % 8 != 7 && pos / 8 > 1) possibleRessults.Add(pos-15);
        if (pos % 8 != 7 && pos / 8 < 6) possibleRessults.Add(pos+17);
        if (pos % 8 != 0 && pos / 8 > 1) possibleRessults.Add(pos-17);

        List<int> moves = new List<int>();

        foreach (var item in possibleRessults)
        {
            if (item >= 0 && item <= 63)
            {
                if (board.isFree(item) || board.GetPiece(item).getTeam() != board.GetPiece(pos).getTeam())
                    moves.Add(item);
            }
        }
        moves.Add(pos);

        return moves;
    }

    public static List<int> closedDiagonalMoves(int pos, ChessBoard board)
    {
        List<int> results = new List<int>();
        results.Add(pos);
        for (int i = pos +7; i < 64; i += 7)
        {
            if (pos % 8 == 0) break;
            if (!board.isFree(i) && board.GetPiece(i).getTeam() == board.GetPiece(pos).getTeam()) break;
            results.Add(i);
            if (!board.isFree(i)) break;
            if (i % 8 == 0)
            {
                break;
            }
        }
        for (int i = pos - 7; i >= 0; i -= 7)
        {
            if (pos % 8 == 7) { break; }
            if (!board.isFree(i) && board.GetPiece(i).getTeam() == board.GetPiece(pos).getTeam()) break;
            results.Add(i);
            if (!board.isFree(i)) break;
            if (i % 8 == 7)
            {
                break;
            }
        }
        return results;
    }

    public static List<int> pawnMoves(int pos, Piece.Team team, ChessBoard board)
    {
        if(pos/8 == 0 || pos/8 == 7)
        {
            board.GetPiece(pos).setType(Piece.TypeOfPiece.QUEEN);
            return queenMoves(pos, board);
        }

        List<int > results = new List<int>();
        if (team.Equals(Piece.Team.WHITE)) results = whitePawnMoves(pos, board);
        if (team.Equals(Piece.Team.BLACK)) results = blackPawnMoves(pos, board);


        if (pos%8 == 0)
        {
            if (results.Contains(pos + 7)) { results.Add(pos+7); }
            if (results.Contains(pos - 9)) { results.Add(pos-9); }
        }else if (pos % 8 == 7)
        {
            if (results.Contains(pos + 9)) { results.Add(pos + 9); }
            if (results.Contains(pos - 7)) { results.Add(pos - 7); }
        }


        results.Add(pos);

        return results;
    }

    public static List<int> whitePawnMoves(int pos,  ChessBoard board)
    {
        List<int> results = new List<int>();
        if (pos / 8 == 6)
        {
            if (board.isFree(pos - 16) && board.isFree(pos - 8)) results.Add(pos - 16);
        }
        if (board.isFree(pos - 8)) results.Add(pos - 8);
        if (!board.isFree(pos - 7) && board.GetPiece(pos - 7).getTeam() != Piece.Team.WHITE) results.Add(pos - 7);
        if (!board.isFree(pos - 9) && board.GetPiece(pos - 9).getTeam() != Piece.Team.WHITE) results.Add(pos - 9);

        return results;
    }
    public static List<int> blackPawnMoves(int pos, ChessBoard board)
    {
        List<int> results = new List<int>();
        if(pos/8 == 1)
        {
            if(board.isFree(pos + 16) && board.isFree(pos+8) ) results.Add(pos+16);
        }
        if (board.isFree(pos + 8)) results.Add(pos + 8);
        if(!board.isFree(pos + 7) && board.GetPiece(pos+7).getTeam() != Piece.Team.BLACK) results.Add(pos+7);
        if(!board.isFree(pos + 9) && board.GetPiece(pos+9).getTeam() != Piece.Team.BLACK) results.Add(pos+9);

        return results;
    }


    public static List<T> Join<T>(this List<T> first, List<T> second)
    {
        if (first == null)
        {
            return second;
        }
        if (second == null)
        {
            return first;
        }

        return first.Concat(second).ToList();
    }
}