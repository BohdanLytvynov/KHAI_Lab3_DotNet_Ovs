using System.Collections.Generic;
using System.Windows;
using Microsoft.Win32;
using System.IO;
using System;
using System.Text;

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

        string m_fileName;
        #endregion


        public MainWindow()
        {
            InitializeComponent();

            m_array1 = new List<double>();
            m_array2 = new List<double>();
            Calculate.IsEnabled = false;
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(task, caption, MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void OpenInputFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();

            if (fd.ShowDialog() ?? false)
            {
                string inpFilepath = fd.FileName;
                var splitString = inpFilepath.Split('\\');
                string last = splitString[splitString.Length - 1];

                m_fileName = last.Split('.')[0];

                if (string.IsNullOrWhiteSpace(inpFilepath))
                {
                    return;
                }
                Calculate.IsEnabled = false;
                m_array1.Clear();
                m_array2.Clear();

                DownloadResultTxtBx.Text = "";

                StreamReader sr = new StreamReader(inpFilepath, true);
                int row = 1;
                while (true)
                {
                    string line = sr.ReadLine();

                    if (string.IsNullOrEmpty(line))
                        break;

                    var array = line.Split(' ');
                    int len = array.Length;
                    if (len == 2)
                    {
                        for (int i = 0; i < len; i++)
                        {
                            double res = double.MaxValue;

                            var trimmed = array[i].Trim(' ');

                            if (trimmed.Contains("."))
                            {
                                trimmed = trimmed.Replace('.', ',');
                            }

                            if (!double.TryParse(trimmed, out res))//Fail To Parse
                            {
                                MessageBox.Show($"Помилка парсінгу рядка!\n ->Файл: \n\t{inpFilepath}\n\t->Рядок: {row} \n\t->Стовпчик: {i}"
                                    , caption, MessageBoxButton.OK, MessageBoxImage.Error);
                                return;
                            }

                            if (i == 0)
                            {
                                m_array1.Add(res);
                            }
                            else
                            {
                                m_array2.Add(res);
                            }
                        }


                    }

                    DownloadResultTxtBx.Text += array[0] + "   " + array[1] + "\n";

                    ++row;
                }

                if (m_array1.Count > 0 && m_array2.Count > 0 && m_array1.Count == m_array2.Count)
                    Calculate.IsEnabled = true;

                sr.Close();
                sr.Dispose();
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            int count1 = m_array1.Count;
            int count2 = m_array2.Count;

            if (count1 > 0 && count2 > 0 && count1 == count2)
            {
                ShowResultTxtBx.Text = "";

                for (int i = 0; i < count1; i++)
                {
                    double n1 = m_array1[i];
                    double n2 = m_array2[i];

                    if (m_array2[i] == 0)
                    {
                        MessageBox.Show($"Ділення на 0! \nРядок: {i + 1}", caption, MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                    ShowResultTxtBx.Text += n1.ToString() + " / " + n2.ToString() + " = " + (n1 / n2).ToString() + "\n";
                }
            }
        }

        private void SaveResultsButton_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fd = new System.Windows.Forms.FolderBrowserDialog();

            using (var fbd = new System.Windows.Forms.FolderBrowserDialog())
            {
                var result = fbd.ShowDialog();

                if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {                     
                    FileStream fs = File.Open(fbd.SelectedPath + Path.DirectorySeparatorChar + m_fileName + "_Обчислення.txt",
                        FileMode.Create);

                    StreamWriter sw = new StreamWriter(fs);

                    sw.Write(ShowResultTxtBx.Text);

                    sw.Close();
                    sw.Dispose();

                    fs.Close();
                    fs.Dispose();
                }
            }
        }
    }
}
