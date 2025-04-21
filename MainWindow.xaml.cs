using System;
using System.Collections.Generic;
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

namespace Lab_Work_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields

        const string caption = "Lab Work 3";

        const string task = 
            "\tЛабораторна робота номер 3, варіант 9\n" +
            "\tВиконав: Литвинов Б Ю гр 125\n" +
            "\tНеобхідно розробити додаток який зчитує файл з цифрами сумує їх та записує результат в інший файл.\n" +
            "\tЗа варіантом 9 операція '/'\n" +
            "\tТЗ:\n" +
    "\t• запитує користувача ім'я файла;\n" +
    "\t• відкриває та читає його записи до масивів(перше число в один масив, а друге – відповідно до другого);\n" +
    "\t• виводить ці числа, суворо почергово, в елементи textBox1 і textBox2 на формі по натисканню якого-небудь кнопки, що заваляє, клавіші або будь-яким іншим зрозумілим навіть освіченому користувачеві способом;\n" +
    "\t• сама натискає кнопку складання чисел;\n" +
    "\t• у процесі підсумовування чисел програма формує результуючий масив;\n" +
    "\t• за командою FileSave as програма зберігає результати обчислень у вихідному файлі.\n";

        List<double> m_array1;
        List<double> m_array2;

        #endregion


        public MainWindow()
        {
            InitializeComponent();

            m_array1 = new List<double>();
            m_array2 = new List<double>();
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(task, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
