using System;

namespace RaceCardViewer.CoreConsole.Classes
{
    public class MenuPainter
    {
        readonly Menu menu;

        public MenuPainter(Menu menu)
        {
            this.menu = menu;
        }

        public void Paint(int x, int y)
        {
            for (int i = 0; i < menu.Items.Count; i++)
            {
                Console.SetCursorPosition(x, y + i);

                var color = menu.SelectedIndex == i ? ConsoleColor.DarkBlue : ConsoleColor.Gray;

                Console.ForegroundColor = color;
                Console.WriteLine(menu.Items[i]);
            }
        }
    }

}
