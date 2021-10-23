using Game.Common.ModelsDto;
using GameClient.Common.Services.HubServices;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.SignalR.Client;
using Blazored.SessionStorage;
using GameClient.Common.Services.ApiServices;
using System.Threading.Tasks;

namespace GameClient.Common.Pages
{
    [Route("desk")]
    public partial class CheckersDesk
    {
        [Parameter] public bool IsWhitePlayer { get; set; }
        [Parameter] public HubConnection HubConnection { get; set; }
        [Parameter] public string TableId { get; set; }
        [Inject] private ISessionStorageService _sessionStorageService { get; set; }
        [Inject] private IApiService _apiService { get; set; }
        private List<CheckerDto> _blackCheckers { get; set; } = new();
        private List<CheckerDto> _whiteCheckers { get; set; } = new();
        private List<(int row, int column)> _cellsPossible { get; set; } = new();
        private CheckerDto _activeChecker { get; set; }
        private bool _canMove { get; set; } = false;
        private bool _whiteTurn { get; set; } = true;
        private string _whoJoined { get; set; }
        private bool _someononeJoined { get; set; } = false;
        private bool _gameEnd { get; set; } = false;
        private string _endMess { get; set; }
        private int _looseCount { get; set; } = 11;
        private string _userLogin { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _userLogin = (await _sessionStorageService.GetItemAsync<LoginModelDto>("User")).Login;
            SetBlackChackers();
            SetWhiteChackers();
            HubConnection.On<string>("TableJoined", TableJoin);
            HubConnection.On<int, int, int, int>("Move", ServerMove);
        }

        private void TableJoin(string login)
        {
            _whoJoined = login;
            _someononeJoined = true;
            StateHasChanged();
        }

        private async Task ServerMove(int prevCol, int prevRow, int newCol, int newRow)
        {
            var checker = _blackCheckers.FirstOrDefault(c => c.Column == prevCol && c.Row == prevRow);
            if (checker == null)
                checker = _whiteCheckers.FirstOrDefault(c => c.Column == prevCol && c.Row == prevRow);
            _activeChecker = checker;
            EvaluateCheckerSpots();
            MoveChecker(newRow, newCol);
            StateHasChanged();
            if (_blackCheckers.Count == _looseCount || _whiteCheckers.Count == _looseCount)
            {
                await WinCheck();
            }
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

                if (_activeChecker.Direction == CheckerDirection.Down || _activeChecker.Direction == CheckerDirection.Both)
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

        private void EvaluateSpot(int row, int column, bool firstTime = true)
        {
            var blackChecker = _blackCheckers.FirstOrDefault(x => x.Row == row && x.Column == column);
            var whiteChecker = _whiteCheckers.FirstOrDefault(x => x.Row == row && x.Column == column);

            if (blackChecker == null && whiteChecker == null)
            {
                _cellsPossible.Add((row, column));
            }
            else if (firstTime)
            {
                if ((_whiteTurn && blackChecker != null) || (!_whiteTurn && whiteChecker != null))
                {
                    int columnDifference = column - _activeChecker.Column;
                    int rowDifference = row - _activeChecker.Row;

                    EvaluateSpot(row + rowDifference, column + columnDifference, false);
                }
            }
        }

        private void MoveChecker(int row, int column)
        {
            _canMove = _cellsPossible.Contains((row, column));
            if (!_canMove)
                return;

            if (Math.Abs(_activeChecker.Column - column) == 2)
            {
                int jumpetColumn = (_activeChecker.Column + column) / 2;
                int jumpetRow = (_activeChecker.Row + row) / 2;

                var blackChecker = _blackCheckers.FirstOrDefault(x => x.Row == jumpetRow && x.Column == jumpetColumn);
                if (blackChecker != null)
                    _blackCheckers.Remove(blackChecker);

                var whiteChecker = _whiteCheckers.FirstOrDefault(x => x.Row == jumpetRow && x.Column == jumpetColumn);
                if (whiteChecker != null)
                    _whiteCheckers.Remove(whiteChecker);


            }

            HubConnection.SendAsync("Move", TableId, _activeChecker.Column, _activeChecker.Row, column, row);

            _activeChecker.Column = column;
            _activeChecker.Row = row;

            if (_activeChecker.Row == 0 && _activeChecker.Color == "white")
                _activeChecker.Direction = CheckerDirection.Both;

            if (_activeChecker.Row == 7 && _activeChecker.Color == "black")
                _activeChecker.Direction = CheckerDirection.Both;

            _activeChecker = null;
            _whiteTurn = !_whiteTurn;

            EvaluateCheckerSpots();
        }

        private async Task WinCheck()
        {
            await HubConnection.DisposeAsync();
     
            bool res = false;

            if (IsWhitePlayer && _blackCheckers.Count == _looseCount)
            {
                res = true;
                 _endMess = "You Win!";
            }
            else if(IsWhitePlayer && _whiteCheckers.Count == _looseCount)
            {
                res = false;
                _endMess = "You Loose!";
            }

            if (!IsWhitePlayer && _whiteCheckers.Count == _looseCount)
            {
                res = true;
                _endMess = "You Win!";
            }
            else if(!IsWhitePlayer && _blackCheckers.Count == _looseCount)
            {
                res = false;
               _endMess = "You Loose!";
            }

            var result = new GameResultDto { Login = _userLogin, IsWin = res };
            await _apiService.ExecuteRequest(() => _apiService.ApiMethods.Update(result));
            _gameEnd = true;
            StateHasChanged();

        }

        private void CheckerClick(CheckerDto checker)
        {
            if (_whiteTurn != IsWhitePlayer)
                return;

            if (!_whiteTurn && IsWhitePlayer)
                return;

            if (_whiteTurn && checker.Color == "black")
                return;

            if (!_whiteTurn && checker.Color == "white")
                return;

            _activeChecker = checker;
            EvaluateCheckerSpots();
        }

    }
}
