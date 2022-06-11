// See https://aka.ms/new-console-template for more information

using GameObjects;

namespace Main
{
    static class Program
    {
        static void Main()
        {
            Game game = new();
            game.GameLoop(ref game);
        }
    }
}