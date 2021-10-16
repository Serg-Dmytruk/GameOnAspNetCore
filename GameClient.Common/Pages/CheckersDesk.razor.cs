﻿using Game.Common.ModelsDto;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;

namespace GameClient.Common.Pages
{
    [Route("desk")]
    public partial class CheckersDesk
    {
        private List<CheckerDto> _blackCheckers { get; set; } = new();
        private List<CheckerDto> _whiteCheckers { get; set; } = new();
        private List<(int row, int column)> _cellsPossible { get; set; } = new();
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
                    _whiteCheckers.Add(new CheckerDto
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
            _cellsPossible.Clear();

            if (_activeChecker != null)
            {
                List<int> rowsPossible = new();

                if(_activeChecker.Direction == CheckerDirection.Down || _activeChecker.Direction == CheckerDirection.Both)
                {
                    rowsPossible.Add(_activeChecker.Row + 1);
                }

                if (_activeChecker.Direction == CheckerDirection.Up || _activeChecker.Direction == CheckerDirection.Both)
                {
                    rowsPossible.Add(_activeChecker.Row - 1);
                }

                foreach (var row in rowsPossible)
                {
                    EvaluateSpot(row, _activeChecker.Column - 1);
                    EvaluateSpot(row, _activeChecker.Column + 1);
                }
            }
        }

        private void EvaluateSpot(int row, int column)
        {
            var blackChecker = _blackCheckers.FirstOrDefault(x => x.Row == row && x.Column == column);
            var whiteChecker = _whiteCheckers.FirstOrDefault(x => x.Row == row && x.Column == column);
            if(blackChecker == null && whiteChecker == null)
            {
                _cellsPossible.Add((row, column));
            }
        }

        private void MoveChecker(int row, int column)
        {
            _canMove = _cellsPossible.Contains((row, column));
            if (!_canMove)
                return;

            _activeChecker.Column = column;
            _activeChecker.Row = row;

            if (_activeChecker.Row == 0 && _activeChecker.Color == "white")
                _activeChecker.Direction = CheckerDirection.Both;

            if (_activeChecker.Row == 7 && _activeChecker.Color == "black")
                _activeChecker.Direction = CheckerDirection.Both;

            _activeChecker = null;
            EvaluateCheckerSpots();
        }

    }
}
