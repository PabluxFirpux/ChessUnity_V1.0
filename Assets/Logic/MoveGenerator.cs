using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class MoveGenerator
{
    public static List<int> possibleMoves(Piece piece)
    {
        if (piece.getType().Equals(Piece.TypeOfPiece.PAWN)) return pawnMoves(piece.getPos(), piece.getTeam()); //Needs enemy checking
        if (piece.getType().Equals(Piece.TypeOfPiece.ROOK)) return rookMoves(piece.getPos()); //Needs enemy checking
        if (piece.getType().Equals(Piece.TypeOfPiece.KNIGHT)) return knightMoves(piece.getPos());
        if (piece.getType().Equals(Piece.TypeOfPiece.BISHOP)) return bishopMoves(piece.getPos()); //Needs enemy checking
        if (piece.getType().Equals(Piece.TypeOfPiece.QUEEN)) return queenMoves(piece.getPos()); //Needs enemy checking, can streamline from rook and bishop checking
        if (piece.getType().Equals(Piece.TypeOfPiece.KING)) return kingMoves(piece.getPos()); //Needs king enemy checking


        return new List<int>();
    }

    public static List<int> queenMoves(int pos)
    {
        return Join<int>(rookMoves(pos), bishopMoves(pos));
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

    public static List<int> kingMoves(int pos)
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
                results.Add(item);
            }
        }

        return results;
    }

    public static List<int> bishopMoves(int pos)
    {
        List<int> allMoves = new List<int>();
        List<int> digMoves1 = openDiagonalMoves(pos);
        List<int> digMoves2 = closedDiagonalMoves(pos);
        allMoves = Join<int>(digMoves1, digMoves2);
        return allMoves;
    }

    public static List<int> openDiagonalMoves(int pos)
    {
        List<int> results = new List<int>();
        for(int i = pos; i<64; i+=9)
        {
            results.Add(i);
            if(i%8 == 7)
            {
                break;
            }
        }
        for(int i = pos; i>=0; i-=9)
        {
            results.Add(i);
            if(i%8 == 0)
            {
                break;
            }
        }
        return results;
    }

    public static List<int> knightMoves(int pos)
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
                moves.Add(item);
            }
        }

        return moves;
    }

    public static List<int> closedDiagonalMoves(int pos)
    {
        List<int> results = new List<int>();
        for (int i = pos; i < 64; i += 7)
        {
            results.Add(i);
            if (i % 8 == 0)
            {
                break;
            }
        }
        for (int i = pos; i >= 0; i -= 7)
        {
            results.Add(i);
            if (i % 8 == 7)
            {
                break;
            }
        }
        return results;
    }

    public static List<int> pawnMoves(int pos, Piece.Team team)
    {
        List<int > results = new List<int>();
        if(team.Equals(Piece.Team.WHITE)) { 
          List<int> starterPawnMoves = new List<int>();
            for(int i = 48;  i <=55;  i++)
            {
                starterPawnMoves.Add(i);
            }
            if(starterPawnMoves.Contains(pos))
            {
                results.Add(pos-16);
            } 
            results.Add(pos-8); 
        } else
        {
            List<int> starterPawnMoves = new List<int>();
            for (int i = 8; i <= 15; i++)
            {
                starterPawnMoves.Add(i);
            }
            if (starterPawnMoves.Contains(pos))
            {
                results.Add(pos + 16);
            }
            results.Add(pos + 8); 
        }
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