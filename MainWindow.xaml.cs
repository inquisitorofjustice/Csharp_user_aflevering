
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Net;

namespace Csharp_User_Aflevering
{

    public class User
    {
        private string id;
        private string name;
        private int age;
        private int score;

        public User(string s)
        {
            var txt = s.Split(';');
            id = txt[0];
            name = txt[1];
            int.TryParse(txt[2], out age);
            int.TryParse(txt[3], out score);
        }

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public override string ToString()
        {
            return $"\tId: {id}\tName: {name}";
        }

        public string ListBoxToString {
            get
            {
                return Id + ": " + Name;
            }
        }

        public static List<User> ReadCSVFile(string Filename)
        {
            List<User> personList = new List<User>();
            using (var file = new StreamReader(Filename))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    var u = new User(line);
                    personList.Add(u);
                }
            }
            return personList;
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<User> data;

        private string[] lines;

        public MainWindow()
        {
            InitializeComponent();

        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            string filePath = string.Empty;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = @"C:\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if(openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    filePath = openFileDialog.FileName;
                    data = User.ReadCSVFile(filePath);

                    Userbox.ItemsSource = data;
                    GridOuter.DataContext = data;

                    lblLines.Content = data.Count() + ";";
                    lblTime.Content = DateTime.Now + ";";

                }
            }   
        }
    }
}
