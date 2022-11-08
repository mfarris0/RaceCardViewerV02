using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace RaceCardViewer.CoreConsole.Classes
{
    public class Menu
    {
        public Menu(IEnumerable<string> items)
        {
            Items = items.ToArray();
        }

        public Menu(IEnumerable<FileInfo> files)
        {
            var names = from file in files
                        select file.Name;
            Items = names.ToArray();
        }


        public IReadOnlyList<string> Items { get; }

        public int SelectedIndex { get; private set; } = -1; // nothing selected

        public string SelectedOption => SelectedIndex == -1 ? null : Items[SelectedIndex];


        public void MoveUp() => SelectedIndex = Math.Max(SelectedIndex - 1, 0);

        public void MoveDown() => SelectedIndex = Math.Min(SelectedIndex + 1, Items.Count - 1);
    }

}
