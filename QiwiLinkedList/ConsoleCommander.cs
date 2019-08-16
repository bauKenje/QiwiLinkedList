using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QiwiLinkedList
{
    public static class ConsoleCommander
    {
        public const string EXIT_COMMAND = "q";
        public const string HELP_COMMAND = "h";
        public const string GET_COMMAND = "get";
        public const string ADD_COMMAND = "add";
        public const string DELETE_COMMAND = "delete";
        public const string COUNT_COMMAND = "count";
        public const string INTERFACE_COMMAND = "i";

        public static QiwiLinkedList<int> list = new QiwiLinkedList<int>();

        public static void Start()
        {
            ConsoleCommander.WriteHelpConsole();
            string command;
            do
            {
                command = Console.ReadLine();
                Read(command);
            }
            while (command != EXIT_COMMAND);
        }

        static void Read(string command)
        {
            string[] commandElements = command.Split(" ".ToCharArray());
            try
            {
                switch (commandElements[0])
                {
                    case HELP_COMMAND:
                        WriteHelpConsole();
                        break;
                    case GET_COMMAND:
                        uint index = 0;
                        if (commandElements.Length == 1)
                            GetAllElements();
                        else if (commandElements.Length == 2 && uint.TryParse(commandElements[1], out index))
                            GetElement(index);
                        else
                            Console.WriteLine("Неверный формат команды " + GET_COMMAND);
                        break;
                    case ADD_COMMAND:
                        int element = 0;
                        index = 0;
                        if (commandElements.Length == 2 && int.TryParse(commandElements[1], out element))
                            AddElement(element);
                        else if (commandElements.Length == 3
                            && int.TryParse(commandElements[1], out element)
                            && uint.TryParse(commandElements[2], out index))
                            AddElement(element, index);
                        else
                            Console.WriteLine("Неверный формат команды " + ADD_COMMAND);
                        break;
                    case DELETE_COMMAND:
                        index = 0;
                        if (commandElements.Length == 2 && uint.TryParse(commandElements[1], out index))
                            DeleteElement(index);
                        else
                            Console.WriteLine("Неверный формат команды " + DELETE_COMMAND);
                        break;
                    case COUNT_COMMAND:
                        GetElementsCount();
                        break;
                    case INTERFACE_COMMAND:
                        CheckInterfaces();
                        break;
                    default:
                        Console.WriteLine("Неверная команда");
                        break;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Индекс вне границ списка");
            }
        }

        static void WriteHelpConsole()
        {
            StringBuilder helpOutput = new StringBuilder();
            helpOutput.Append(HELP_COMMAND);
            helpOutput.Append(": Помощь\n");
            helpOutput.Append(GET_COMMAND);
            helpOutput.Append(": Вывести все\n");
            helpOutput.Append(GET_COMMAND);
            helpOutput.Append(" ИНДЕКС");
            helpOutput.Append(": Вывести элемент по индексу\n");
            helpOutput.Append(ADD_COMMAND);
            helpOutput.Append(" ЭЛЕМЕНТ");
            helpOutput.Append(": Добавить элемент в конец списка\n");
            helpOutput.Append(ADD_COMMAND);
            helpOutput.Append(" ЭЛЕМЕНТ ИНДЕКС");
            helpOutput.Append(": Добавить элемент в позицию по индексу\n");
            helpOutput.Append(COUNT_COMMAND);
            helpOutput.Append(": Вывести количество элементов в списке\n");
            helpOutput.Append(INTERFACE_COMMAND);
            helpOutput.Append(": Проверить работу интерфейсов\n");
            helpOutput.Append(EXIT_COMMAND);
            helpOutput.Append(": Выход\n");
            Console.Write(helpOutput.ToString());
        }

        static void GetAllElements()
        {
            Console.WriteLine(list.Get());
        }

        static void GetElement(uint index)
        {
            Console.WriteLine(list.Get(index));
        }

        static void AddElement(int element)
        {
            list.Add(element);
        }

        static void AddElement(int element, uint index)
        {
            list.Add(element, index);
        }

        static void DeleteElement(uint index)
        {
            list.Delete(index);
        }

        static void CheckInterfaces()
        {
            Console.WriteLine("Положительнвые элементы списка");
            var positiveList = list.Where(x => x > 0);
            foreach (int item in positiveList)
            {
                Console.WriteLine(item);
            }

        }

        static void GetElementsCount()
        {
            Console.WriteLine(list.Count());
        }
    }
}
