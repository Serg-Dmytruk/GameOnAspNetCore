using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Game.Common.ModelsDto;

namespace GameClient.Common.Pages
{
    [Route("desk")]
    public partial class CheckersDesk
    {
        private List<CheckerDto> _blackCheckers { get; set; } = new();
        private List<CheckerDto> _whiteCheckers { get; set; } = new();
        private List<int> _rowPossible { get; set; } = new();
        private List<int> _colomnsPossible { get; set; } = new ();
        private CheckerDto _activeChecker { get; set; }
        private bool _canMove { get; set; } = false;

        protected override void OnInitialized()
        {
            SetBlackChackers();
            SetWhiteChackers();
        }

        private void SetBlackChackers()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = (i + 1) % 2; j < 8; j += 2)
                {
                    _blackCheckers.Add(new CheckerDto
                    {
                        Color = "black",
                        Column = j,
                        Row = i,
                        Direction = CheckerDirection.Down
                    });
                }
            }
        }
        private void SetWhiteChackers()
        {
            for (int i = 5; i < 8; i++)
            {
                for (int j = (i + 1) % 2; j < 8; j += 2)
                {
                    _blackCheckers.Add(new CheckerDto
                    {
                        Color = "white",
                        Column = j,
                        Row = i,
                        Direction = CheckerDirection.Up
                    });
                }
            }
        }

        private void EvaluateCheckerSpots()
        {
            _rowPossible.Clear();
            _colomnsPossible.Clear();

            if (_activeChecker != null)
                {
                _rowPossible.Add(_activeChecker.Row + (1 * (_activeChecker.Direction == CheckerDirection.Down ? 1 : -1)));
                _colomnsPossible.Add(_activeChecker.Column - 1);
                _colomnsPossible.Add(_activeChecker.Column + 1);
            }
        }

        private void MoveChecker(int row, int column)
        {
            _canMove = _rowPossible.Contains(row) && _colomnsPossible.Contains(column);
            if (!_canMove)
                return;

            _activeChecker.Column = column;
            _activeChecker.Row = row;
            _activeChecker = null;
            EvaluateCheckerSpots();
        }

    }
}
