using System.Collections.Generic;
using System.Numerics;

namespace MyProgramm
{
    class Programm
    {
        static void Main()
        {
            Menu menu = new Menu();
            menu.ShowMenu();
        }
    }

    class Menu
    {
        const string MenuAddPlayer = "1";
        const string MenuRemovePlayer = "2";
        const string MenuShowAllPlayers = "3";
        const string MenuBanPlayer = "4";
        const string MenuUnbanPlayer = "5";
        const string MenuExit = "6";

        bool isExit = false;
        string userInput;
        Database database = new Database();

        public void ShowMenu()
        {
            while (isExit == false)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine(MenuAddPlayer + " - Добавить игрока");
                Console.WriteLine(MenuRemovePlayer + " - Удалить игрока");
                Console.WriteLine(MenuShowAllPlayers + " - Показать всех игроков");
                Console.WriteLine(MenuBanPlayer + " - Забанить игрока");
                Console.WriteLine(MenuUnbanPlayer + " - Разбанить игрока");
                Console.WriteLine(MenuExit + " - Выход");
                userInput = Console.ReadLine();

                switch (userInput)
                {
                    case MenuAddPlayer:
                        database.AddPlayer();
                        break;
                    case MenuRemovePlayer:
                        database.RemovePlayer();
                        break;
                    case MenuShowAllPlayers:
                        database.ShowAllRecords();
                        break;
                    case MenuBanPlayer:
                        database.BanPlayer();
                        break;
                    case MenuUnbanPlayer:
                        database.UnbanPlayer();
                        break;
                    case MenuExit:
                        isExit = true;
                        break;
                }
            }
        }
    }

    class Player
    {
        public int PlayerId { get; private set; }
        public string PlayerName { get; private set; }
        public int PlayerLevel { get; private set; }
        public bool IsPlayerBanned { get; private set; }

        public Player(int playerId, string playerName, int playerLevel = 0, bool isPlayerBanned = false)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            PlayerLevel = playerLevel;
            IsPlayerBanned = isPlayerBanned;
        }

        public void Ban()
        {
            IsPlayerBanned = true;
            Console.WriteLine("Игрок забанен");
        }

        public void Unban()
        {
            IsPlayerBanned = false;
            Console.WriteLine("Игрок разбанен");
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        int LastId = 0;

        public void AddPlayer()
        {
            int id = LastId++;
            Console.WriteLine("Введите имя игрока");
            string name = Console.ReadLine();
            _players.Add(new Player(id, name));
        }

        public void RemovePlayer()
        {
            Player player;

            if (TryGetPlayer(out player))
            {
                _players.Remove(player);
                Console.WriteLine("Игрок удалён");
            }
        }

        public void ShowAllRecords()
        {
            foreach (var playerField in _players)
            {
                PlayerInfo(playerField);
            }
        }

        public void PlayerInfo(Player player)
        {
            Console.WriteLine($"Id: {player.PlayerId} | Name: {player.PlayerName} | Level: {player.PlayerLevel} | Ban status: {player.IsPlayerBanned}");
        }

        public int GetNumber()
        {
            int parsedNumber = 0;
            bool isParsed = false;

            while (isParsed == false)
            {
                string userInput = Console.ReadLine();
                isParsed = int.TryParse(userInput, out parsedNumber);

                if (isParsed == false)
                {
                    Console.WriteLine("Введите целое число:");
                }
            }

            return parsedNumber;
        }

        public bool TryGetPlayer(out Player player)
        {
            player = null;
            Console.WriteLine("Введите ID игрока:");
            int id = GetNumber();

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].PlayerId == id)
                {
                    player = _players[i];
                    Console.WriteLine("Игрок найден");
                    return true;
                }
            }

            Console.WriteLine("Игрока с таким ID не найдено");
            return false;
        }

        public void BanPlayer()
        {
            Player player;

            if (TryGetPlayer(out player))
            {
                player.Ban();
            }
        }

        public void UnbanPlayer()
        {
            Player player;

            if (TryGetPlayer(out player))
            {
                player.Unban();
            }
        }
    }
}