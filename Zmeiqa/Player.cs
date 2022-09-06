using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Zmeiqa
{
    public class Player
    {
        public enum Direction
        {
            Right,
            Left,
            Down,
            Up
        }

        public LinkedList<Cell> PlayerParts { get; private set; }

        public Direction PlayerDirection { get; set; }

        public Player(LinkedList<Cell> playerParts)
        {
            PlayerParts = playerParts;
            PlayerDirection = Direction.Down;
        }

        public void DeleteLast()
        {
            PlayerParts.Remove(PlayerParts.Last());
        }

        public void AddFirst()
        {
            switch (PlayerDirection)
            {
                case Direction.Up:
                    PlayerParts.AddFirst(
                        new Cell(20, PlayerParts.First.Value.x, PlayerParts.First.Value.y - 1));
                    break;
                case Direction.Down:
                    PlayerParts.AddFirst(
                        new Cell(20, PlayerParts.First.Value.x, PlayerParts.First.Value.y + 1));
                    break;
                case Direction.Right:
                    PlayerParts.AddFirst(
                        new Cell(20, PlayerParts.First.Value.x + 1, PlayerParts.First.Value.y));
                    break;
                default:
                {
                    if (PlayerDirection == Direction.Left)
                    {
                        PlayerParts.AddFirst(
                            new Cell(20, PlayerParts.First.Value.x - 1, PlayerParts.First.Value.y));
                    }

                    break;
                }
            }
        }

        public void MakeStep()
        {
            AddFirst();
            DeleteLast();
        }
    }
}