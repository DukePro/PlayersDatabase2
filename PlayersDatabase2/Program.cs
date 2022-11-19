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
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Level { get; private set; }
        public bool IsBanned { get; private set; }

        public Player(int id, string name, int level = 0, bool isBanned = false)
        {
            Id = id;
            Name = name;
            Level = level;
            IsBanned = isBanned;
        }

        public void Ban()
        {
            IsBanned = true;
            Console.WriteLine("Игрок забанен");
        }

        public void Unban()
        {
            IsBanned = false;
            Console.WriteLine("Игрок разбанен");
        }
    }

    class Database
    {
        private List<Player> _players = new List<Player>();
        private int _lastId;

        public void AddPlayer()
        {
            ++_lastId;
            Console.WriteLine("Введите имя игрока");
            string name = Console.ReadLine();
            _players.Add(new Player(_lastId, name));
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
            Console.WriteLine($"Id: {player.Id} | Name: {player.Name} | Level: {player.Level} | Ban status: {player.IsBanned}");
        }

        private int GetNumber()
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

        private bool TryGetPlayer(out Player player)
        {
            player = null;
            Console.WriteLine("Введите ID игрока:");
            int id = GetNumber();

            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].Id == id)
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
            if (TryGetPlayer(out Player player))
            {
                player.Ban();
            }
        }

        public void UnbanPlayer()
        {
            if (TryGetPlayer(out Player player))
            {
                player.Unban();
            }
        }
    }
}