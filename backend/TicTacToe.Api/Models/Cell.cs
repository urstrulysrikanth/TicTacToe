using TicTacToe.Api.Enums;

namespace TicTacToe.Api.Models
{
    public class Cell
    {
        public int Row { get; set; }

        public int Column { get; set; }

        public Player Value { get; set; } = Player.None;
    }
}
