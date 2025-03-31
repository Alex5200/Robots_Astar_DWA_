using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Text.RegularExpressions;
using MongoDB.Bson.IO;
using MongoDB.Bson;
using System.Diagnostics;
using System.Drawing.Drawing2D;

namespace IoTRobotWorldUDPServer
{
    public partial class Form : System.Windows.Forms.Form
    {
        bool Automatization = false;

        const int CMaxVisibleLogLines = 10;

        string UDPReceiveBuffer = "";

        string remoteAddress; // хост для отправки данных
        int remotePort; // порт для отправки данных
        int localPort; // локальный порт для прослушивания входящих подключений
        int currentCommanNumber = 0;
        int lastCommanNumber = 0;
        public delegate void ShowUDPMessage(string message);
        public ShowUDPMessage myDelegate;
        UdpClient udpClient; // = new UdpClient(11000);
        Thread thread;

        string[] dataForSave = { "az", "d1", "d2", "d3", "d4", "d5", "d6", "d7" };
        int d0, d1, d2, d3, d4, d5, d6, d7, le = 0;
        float x, y, re = 0;
        string t;
        int az = 0;
        int globalTickCounter = 0;
        Bitmap Main;
        Bitmap mapBitmap;

        private List<PointF> routePoints = new List<PointF>();
        private int currentPointIndex = 0;
        private float robotX, robotY, robotCourse;
        private float radiusOfAchievement = 10f; // Значение по умолчанию
        public Form()
        {
            InitializeComponent();
            timer2.Stop();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Создадим делегата метода распечатки сообщения от удаленного сервера
            myDelegate = new ShowUDPMessage(ShowUDPMessageMethod);
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            StopUDPClient();
        }

        private void PrintLog(string s)
        {
            // CMaxVisibleLogLines
            ReportListBox.Items.Add(s);
            while (ReportListBox.Items.Count > CMaxVisibleLogLines)
            {
                ReportListBox.Items.RemoveAt(0);
            }
            ReportListBox.SelectedIndex = ReportListBox.Items.Count - 1;
            ReportListBox.SelectedIndex = -1;
        }

        private void CheckStartStopUDPClient()
        {
            if (udpClient != null)
            {
                StartStopUDPClientButton.Text = "Stop";
                RemoteIPTextBox.Enabled = false;
                RemoteIPTextBox.BackColor = Color.LightGray;
                RemotePortTextBox.Enabled = false;
                RemotePortTextBox.BackColor = Color.LightGray;
                LocalIPTextBox.Enabled = false;
                LocalIPTextBox.BackColor = Color.LightGray;
                LocalPortTextBox.Enabled = false;
                LocalPortTextBox.BackColor = Color.LightGray;
            }
            else
            {
                StartStopUDPClientButton.Text = "Start";
                RemoteIPTextBox.Enabled = true;
                RemoteIPTextBox.BackColor = Color.White;
                RemotePortTextBox.Enabled = true;
                RemotePortTextBox.BackColor = Color.White;
                LocalIPTextBox.Enabled = true;
                LocalIPTextBox.BackColor = Color.White;
                LocalPortTextBox.Enabled = true;
                LocalPortTextBox.BackColor = Color.White;
            }
        }

        private void StopUDPClient()
        {
            if ((thread != null) && (udpClient != null))
            {
                thread.Abort();
                udpClient.Close();
                thread = null;
                udpClient = null;
            }
            PrintLog("UDPClient stopped");
            CheckStartStopUDPClient();
        }

        private void StartUDPClient()
        {
            if (thread != null)
            {
                thread.Abort();
            }
            if (udpClient != null)
            {
                udpClient.Close();
            }

            localPort = Int32.Parse(LocalPortTextBox.Text);
            int localPortLidar = Int32.Parse(PortLidar.Text);

            try
            {
                udpClient = new UdpClient(localPort);
                UdpClient udpClient2 = new UdpClient(localPortLidar);
                thread = new Thread(new ThreadStart(ReceiveUDPMessage));
                thread.IsBackground = true;
                thread.Start();
                PrintLog("UDPClient started");
            }
            catch
            {
                PrintLog("UDPClient's start failed");
            }
            CheckStartStopUDPClient();
        }

        private void StartStopUDPClientButton_Click(object sender, EventArgs e)
        {
            timer1.Start();
            if (udpClient == null)
            {
                StartUDPClient();
            }
            else
            {
                StopUDPClient();
            }
        }

        private void ShowUDPMessageMethod(string message)
        {
            PrintLog("Remote >" + message);
        }
        string message;
        private void ReceiveUDPMessage()
        {
            string oldError = "";
            string oldMassage = null;
            while (true)
            {
                try
                {
                    string[] valuesToChek = textBox1.Text.Split(',');
                    IPEndPoint remoteIPEndPoint = new IPEndPoint(IPAddress.Any, 0); // port);
                    byte[] content = udpClient.Receive(ref remoteIPEndPoint);
                    if (content.Length > 0)
                    {
                        message = Encoding.ASCII.GetString(content);
                        string[] allMassage = message.Split(',');
                        string correctMassage = string.Empty;
                        string outMassage = string.Empty;
                        string pattern = "\"t\":(\\f+)";
                        BsonDocument bsonDoc = BsonDocument.Parse(message);

                        if (bsonDoc.Contains("t"))
                        {
                            t = (bsonDoc["t"].AsString);
                        }
                        else
                        {
                            throw new Exception("Значение t не найдено");
                        }
                        for (int i = 0; i <= allMassage.Length; i++)
                        {

                            foreach (string values in valuesToChek)
                            {

                                if (allMassage[i].Contains("az"))
                                    az = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d0"))
                                    d0 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d1"))
                                    d1 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d2"))
                                    d2 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d3"))
                                    d3 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d4"))
                                    d4 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d5"))
                                    d5 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d6"))
                                    d6 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("d7"))
                                    d7 = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("x"))
                                    x = float.Parse(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }), CultureInfo.InvariantCulture.NumberFormat);
                                else if (allMassage[i].Contains("y"))
                                    y = float.Parse(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }), CultureInfo.InvariantCulture.NumberFormat);
                                //if (allMassage[i].Contains("t"))
                                //    t = float.Parse(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }), CultureInfo.InvariantCulture.NumberFormat);
                                if (allMassage[i].Contains("le"))
                                    le = Convert.ToInt32(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));
                                else if (allMassage[i].Contains("T"))
                                    re = float.Parse(allMassage[i].Split(':')[1].Trim(new Char[] { '"' }));


                                if (allMassage[i].Contains('n'))
                                {
                                    var getData = allMassage[i].Split(':');
                                    var commandNumber = getData[1].Trim(new Char[] { '"' });
                                    lastCommanNumber = Int32.Parse(commandNumber);
                                    currentCommanNumber = lastCommanNumber;
                                }
                                if (allMassage[i].Contains(values))
                                {
                                    correctMassage += String.Join(" ", allMassage[i]);
                                    if (correctMassage.Length >= (textBox1.Text.Length * 1))
                                    {
                                        this.Invoke(myDelegate, new object[] { correctMassage + "\n" });
                                    }
                                }
                            }
                            this.Invoke(new Action(() => UpdateListBox(outMassage)));


                        }

                    }
                }
                catch (Exception ex)
                {
                    if (ex.ToString() != oldError)
                    {
                        MessageBox.Show(ex.ToString());
                        this.Invoke(myDelegate, new object[] { ex.ToString() });
                        oldError = ex.ToString();
                    }
                }
            }
        }
        private void UpdateListBox(string message)
        {
            d_0.Text = "d0: " + Convert.ToString(d0);
            d_1.Text = "d1: " + Convert.ToString(d1);
            d_2.Text = "d2: " + Convert.ToString(d2);
            d_3.Text = "d3: " + Convert.ToString(d3);
            d_4.Text = "d4: " + Convert.ToString(d4);
            d_5.Text = "d5: " + Convert.ToString(d5);
            d_6.Text = "d6: " + Convert.ToString(d6);
            d_7.Text = "d7: " + Convert.ToString(d7);
        }
        private void SendUDPMessage(string s)
        {
            if (udpClient != null)
            {
                Int32 port = Int32.Parse(RemotePortTextBox.Text);
                IPAddress ip = IPAddress.Parse(RemoteIPTextBox.Text.Trim());
                IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
                byte[] content = Encoding.ASCII.GetBytes(s);
                try
                {
                    int count = udpClient.Send(content, content.Length, ipEndPoint);
                    if (count > 0)
                    {
                        PrintLog("Message has been sent.");
                    }
                }
                catch
                {
                    PrintLog("Error occurs.");
                }

            }
        }
        private void SendUDPMessageWithoutPrint(string s)
        {
            if (udpClient != null)
            {
                Int32 port = Int32.Parse(RemotePortTextBox.Text);
                IPAddress ip = IPAddress.Parse(RemoteIPTextBox.Text.Trim());
                IPEndPoint ipEndPoint = new IPEndPoint(ip, port);
                byte[] content = Encoding.ASCII.GetBytes(s);
                try
                {
                    int count = udpClient.Send(content, content.Length, ipEndPoint);
                    if (count > 0)
                    {
                        Debug.WriteLine("Message has been sent.");
                    }
                }
                catch
                {
                    Debug.WriteLine("Error occurs.");
                }

            }
        }
        PointF target = new PointF(8, 10); // Целевая точка в точке (8, 10)

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (lastFoundPath != null && lastFoundPath.Count > 0)
            {
                for (int i = 0; i < lastFoundPath.Count - 1; i++)
                {
                    Debug.WriteLine(lastFoundPath[i].Y + "   :RobotY::  " + robotY);

                    while (lastFoundPath[i + 1].Y < robotY)
                    {
                        Debug.WriteLine(lastFoundPath[i].Y + "   :RobotY::  " + robotY);
                        SendUDPMessageWithoutPrint(@"{ ""N"":" + (currentCommanNumber + 1) + @", ""M"":0, ""F"":" + 20 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        SendUDPMessageWithoutPrint(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 20 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        Thread.Sleep(1000);
                    }
                    //if (lastFoundPath[i].X == robotX)
                    //{
                    //    SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                    //    SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                    //}
                }
            }
        }
        private bool IsPositionValid(Point center)
        {
            // Размеры ячейки в пикселях
            const int cellWidth = 10;
            const int cellHeight = 10;

            // Переводим координаты центра в координаты карты
            int col = center.X / cellWidth;
            int row = (pictureBox1.Height - center.Y) / cellHeight; // Инвертируем Y

            // Проверка выхода за границы карты
            if (row < 0 || row >= 40 || col < 0 || col >= 64)
                return false;



            // Проверяем область 3x3 ячейки вокруг целевой позиции
            for (int dy = -1; dy <= 1; dy++)
            {
                for (int dx = -1; dx <= 1; dx++)
                {
                    int checkRow = row + dy;
                    int checkCol = col + dx;

            


                    // Выводим результат в консоль или отображаем другим способом
            

                    // Проверка границ массива
                    if (checkRow < 0 || checkRow >= 40 || checkCol < 0 || checkCol >= 64)
                        return false;

                    // Проверка содержимого ячейки
                    if (mapData[checkRow, checkCol] == '1')
                        return false;
                }
            }

            return true;
        }

        // Функция для чтения цвета пикселя (осталась без изменений)
        Color GetPixelColor(PictureBox pictureBox, int x, int y)
        {
            if (x < 0 || x >= pictureBox.Width || y < 0 || y >= pictureBox.Height)
                return Color.White; // Возвращаем белый, если координаты вне границ
            // Считываем файл и рисуем карту
            if (mapBitmap != null)
            {
                pictureBox.Image = mapBitmap;
                Bitmap bitmap2 = new Bitmap(pictureBox.Image);
                return bitmap2.GetPixel(x, y);
            }
            else
            {
                return Color.White;
            }



        }
        private Point? startPosition = null; // Стартовая позиция
        private Point? endPosition = null;   // Конечная позиция

        private Point? startPositionNOEDIT = null; // Стартовая позиция
        private Point? endPositionNOEDIT = null;   // Конечная позиция
        private void AnimateStep(Point center)
        {

            if ((startPosition != null && center == startPosition.Value) ||
        (endPosition != null && center == endPosition.Value))
            {
                return;
            }
            if (pictureBox1.Image != null)
            {
                Bitmap bitmap2 = new Bitmap(pictureBox1.Width, pictureBox1.Height); // Копируем текущее изображение
                using (Graphics g = Graphics.FromImage(bitmap2))
                {
                    // Вычисляем область 3x3 клеток
                    Rectangle cellRect = new Rectangle(center.X * 10 - 10, center.Y * 10 - 10, 30, 30);
                    using (Brush brush = new SolidBrush(Color.LightGray)) // Цвет посещённой клетки
                    {
                        g.FillRectangle(brush, cellRect);
                    }
                }

                pictureBox1.Image = bitmap2;
                pictureBox1.Refresh(); // Обновляем отображение
                Thread.Sleep(0); // Задержка для анимации
            }
        }
        private Random _random = new Random();

        private Color GetRandomColor()
        {
            return Color.FromArgb(_random.Next(256), _random.Next(256), _random.Next(256));
        }
        private void DrawPath(List<Point> points)
        {
            // Проверяем, что список точек не пуст
            if (points == null || points.Count == 0)
            {
                pictureBox2.Image = null; // Очистка pictureBox, если список точек пуст
                pictureBox2.Refresh();
                return;
            }

            // Создаем новое изображение, если его нет, или используем существующее
            Bitmap bitmap = pictureBox2.Image as Bitmap;
            if (bitmap == null || bitmap.Width != pictureBox2.Width || bitmap.Height != pictureBox2.Height)
            {
                bitmap = new Bitmap(pictureBox2.Width, pictureBox2.Height);
            }

            using (Graphics g = Graphics.FromImage(bitmap))
            {

                using (Pen pen = new Pen(GetRandomColor(), 2)) // Цвет и толщина линии
                {
                    for (int i = 0; i < points.Count - 1; i++)
                    {
                        g.DrawLine(pen, points[i], points[i + 1]);
                    }
                }

                // Отрисовка точек
                using (Brush brush = new SolidBrush(GetRandomColor())) // Цвет точек
                {
                    foreach (Point point in points)
                    {
                        g.FillEllipse(brush, point.X - 2, point.Y - 2, 4, 4); // Размер точки 4x4 пикселя
                    }
                }
            }

            pictureBox2.Image = bitmap; // Установка нового изображения
            pictureBox2.Refresh(); // Обновляем PictureBox для отображения результата
        }
        private List<Point> ReconstructPath(Point[,] parents, Point start, Point end)
        {
            List<Point> path = new List<Point>();
            Point current = end;

            while (current != start)
            {
                path.Add(current);
                current = parents[current.Y, current.X];
            }

            path.Add(start);
            path.Reverse(); // Путь строится от конца к началу, поэтому разворачиваем
            return path;
        }
        private List<Point> BreadthFirstSearch(Point start, Point end)
        {
            int rows = 40, cols = 64;
            Queue<Point> queue = new Queue<Point>();
            bool[,] visited = new bool[rows, cols];
            Point[,] parents = new Point[rows, cols]; // Для восстановления пути

            queue.Enqueue(start);
            visited[start.Y, start.X] = true;

            int[][] directions = new int[][]
            {
                new int[] { 0, -1 }, // Вверх
                new int[] { 0, 1 },  // Вниз
                new int[] { -1, 0 }, // Влево
                new int[] { 1, 0 }   // Вправо
            };

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();

                // Если достигли конца
                if (current == end)
                {
                    return ReconstructPath(parents, start, end);
                }

                foreach (var dir in directions)
                {
                    int newX = current.X + dir[0];
                    int newY = current.Y + dir[1];
                    Point next = new Point(newX, newY);

                    // Проверяем доступность позиции для робота
                    if (newX >= 0 && newX < cols && newY >= 0 && newY < rows &&
                        !visited[newY, newX] && IsPositionValid(next))
                    {
                        queue.Enqueue(next);
                        visited[newY, newX] = true;
                        parents[newY, newX] = current;
                        AnimateStep(next); // Анимация
                    }
                }
            }

            // Если путь не найден
            MessageBox.Show("Путь не найден!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return null;
        }
        List<PointF> obstacles = new List<PointF>();
        List<PointF> path = new List<PointF>();
        private List<Point> lastFoundPath = null; // Глобальная переменная для хранения последнего найденного пути

        int countsRobot = 1;
        int goCommand = 0;
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (currentPointIndex >= routePoints.Count)
            {
                timer3.Stop();
                return;
            }
            if (lastFoundPath != null)
                Debug.WriteLine($"Координаты последнего найденного пути: {string.Join(", ", lastFoundPath.Select(p => $"({p.X}, {p.Y})"))}");

            PointF nextPoint = routePoints[points];
            float targetX = nextPoint.X;
            float targetY = nextPoint.Y;

            float robotX = (x / 10) * pictureBox1.Width;
            float robotY = pictureBox1.Height - (y / 10) * pictureBox1.Height;

            float dx = targetX - robotX;
            float dy = targetY - robotY;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            TargetX.Text = targetX.ToString();
            TargetY.Text = targetY.ToString();
            PointX.Text = robotX.ToString();
            PointY.Text = robotY.ToString();
            startPosition = new Point((int)robotX, (int)robotY);
            endPosition = new Point((int)targetX, (int)targetY);
            if (distance < 10) // Если робот близко к целевой точке
            {
                SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                currentPointIndex++;
                return;
            }
            if (lastFoundPath != null && lastFoundPath.Count > 0)
            {
                DrawPath(lastFoundPath); // Отрисовка пути
            }
            else
            {
                Debug.WriteLine("Путь не найден!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (lastFoundPath != null)
            {
                Debug.WriteLine($"Координаты последнего найденного пути: {lastFoundPath[countsRobot].X + " : " + lastFoundPath[countsRobot].Y}");
                if (lastFoundPath[countsRobot].Y >= robotX + 5 && lastFoundPath[countsRobot].Y <= robotX - 5)
                {
                    goCommand = 1;
                }
                if (goCommand == 0)
                {
                    if (lastFoundPath[countsRobot].Y > robotY)
                    {
                        countsRobot += 1;
                    }
                    if (robotY > lastFoundPath[countsRobot].Y)
                    {
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 30 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 30 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                    }
                    else if (robotX <= lastFoundPath[countsRobot].X)
                    {
                        // поворот 
                        if (float.Parse(t, NumberStyles.Float, CultureInfo.InvariantCulture) >= 89 && float.Parse(t, NumberStyles.Float, CultureInfo.InvariantCulture) <= 92)
                        {
                            SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                            SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                            goCommand = 1;
                        }
                        else if (float.Parse(t, NumberStyles.Float, CultureInfo.InvariantCulture) < 90)
                        {
                            SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + -10 + @", ""T"":0}" + "\n");
                            SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + -10 + @", ""T"":0}" + "\n");
                        }
                        else if (float.Parse(t, NumberStyles.Float, CultureInfo.InvariantCulture) > 90)
                        {
                            SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 1 + @", ""T"":0}" + "\n");
                            SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 1 + @", ""T"":0}" + "\n");
                        }

                    }
                    else
                    {
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        countsRobot += 1;
                    }
                }
                else if (goCommand == 1)
                {
                    if (lastFoundPath[countsRobot].X > robotX)
                    {
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 30 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 30 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");

                    }
                    else if (lastFoundPath[countsRobot].X < robotX)
                    {

                        countsRobot += 1;
                    }
                    else
                    {
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + 0 + @", ""B"":" + 0 + @", ""T"":0}" + "\n");
                        countsRobot += 1;
                    }
                }
            }

            // Выводим значения для отладки
            Debug.WriteLine($"robotX: {robotX}" + $"robotY:{robotY}");
            timer2.Enabled = false;

            //SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + F + @", ""B"":" + B + @", ""T"":0}" + "\n");
            //SendUDPMessage(@"{ ""N"":" + (currentCommanNumber += 1) + @", ""M"":0, ""F"":" + F + @", ""B"":" + B + @", ""T"":0}" + "\n");

        }
        void GoRobot()
        {



        }
        private List<Point> ConstrainedBreadthFirstSearch(Point start, Point end)
        {
            int rows = pictureBox1.Height, cols = pictureBox1.Width;
            Queue<Point> queue = new Queue<Point>();
            bool[,] visited = new bool[rows, cols];
            Point[,] parents = new Point[rows, cols];
            pictureBox1.Invalidate();

            queue.Enqueue(start);
            visited[start.Y, start.X] = true;

            int[][] directions = new int[][]
            {
                new int[] { 0, -1 }, // Вверх
                new int[] { 0, 1 },  // Вниз
                new int[] { -1, 0 }, // Влево
                new int[] { 1, 0 }   // Вправо
            };

            while (queue.Count > 0)
            {
                Point current = queue.Dequeue();

                if (current == end)
                {
                    return ReconstructPath(parents, start, end);
                }

                foreach (var dir in directions)
                {
                    int newX = current.X + dir[0];
                    int newY = current.Y + dir[1];
                    Point next = new Point(newX, newY);
                    //Debug.WriteLine($"Рассматриваем точку: {next.X}, {next.Y}");



                    if (newX >= 0 && newX < cols && newY >= 0 && newY < rows &&
                !visited[newY, newX] && IsPositionValid(next))
                    {
                        //Debug.WriteLine($"Точка в границах и не посещена: {next.X}, {next.Y}");

                        if (IsPositionValid(next))
                        {

                            queue.Enqueue(next);
                            visited[newY, newX] = true;
                            parents[newY, newX] = current;
                        }
                        else
                        {

                            //Debug.WriteLine($"Точка НЕ доступна: {next.X}, {next.Y}");

                        }
                    }
                    else
                    {
                        //Debug.WriteLine($"Точка вне границ или уже посещена: {next.X}, {next.Y}");
                    }
                }
            }


            return null;
        }

        // Новый метод для проверки ограничения под 90 градусов
        private bool IsValidTurn(Point current, Point next, Point start, Point end)
        {
            if (current == start) return true; // Из начальной позиции можно двигаться в любом направлении

            int deltaXStartToCurrent = current.X - start.X;
            int deltaYStartToCurrent = current.Y - start.Y;

            int deltaXCurrentToNext = next.X - current.X;
            int deltaYCurrentToNext = next.Y - current.Y;

            // Проверяем, что движения являются прямыми линиями или поворотами на 90 градусов
            bool isStraightLine = (deltaXStartToCurrent == 0 && deltaYCurrentToNext == 0) ||
                                (deltaYStartToCurrent == 0 && deltaXCurrentToNext == 0);

            bool isRightAngleTurn = (deltaXStartToCurrent != 0 && deltaYStartToCurrent == 0 &&
                                     deltaXCurrentToNext == 0 && deltaYCurrentToNext != 0) ||
                                   (deltaXStartToCurrent == 0 && deltaYStartToCurrent != 0 &&
                                    deltaXCurrentToNext != 0 && deltaYCurrentToNext == 0);

            return isStraightLine || isRightAngleTurn;
        }

        private float NormalizeAngle360(float angle)
        {
            angle = angle % 360;
            if (angle < 0)
            {
                angle += 360;
            }
            return angle;
        }

        // Нормализация угла в диапазоне от -180 до 180 градусов
        private float NormalizeAngle(float angle)
        {
            angle = angle % 360;
            if (angle > 180)
            {
                angle -= 360;
            }
            else if (angle < -180)
            {
                angle += 360;
            }
            return angle;
        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Bitmap _bufferBitmap = new Bitmap(pictureBox1.Width, pictureBox1.Height);

            e.Graphics.Clear(Color.White);
            if (mapData != null)
            {
                for (int row = 0; row < 40; row++)
                {
                    for (int col = 0; col < 64; col++)
                    {
                        char cell = mapData[row, col];
                        Rectangle cellRect = new Rectangle(col * 6, row * 6, 10, 10);

                        // Определяем цвет клетки с использованием метода GetColorForCell
                        Color cellColor = GetColorForCell(cell);

                        using (Brush brush = new SolidBrush(cellColor))
                        {
                            e.Graphics.FillRectangle(brush, cellRect);
                        }


                    }
                }
            }
            if (routePoints.Count == 0)
            {
                return; // Если список точек маршрута пуст, ничего не рисуем
            }

            // Отрисовка робота
            float robotX = (x / 10) * pictureBox1.Width;
            float robotY = pictureBox1.Height - (y / 10) * pictureBox1.Height;

            // Отрисовка круга робота
            e.Graphics.FillEllipse(Brushes.Green, robotX - 5, robotY - 5, 10, 10);

            // Конвертация строки t в число с плавающей запятой
            if (float.TryParse(t, System.Globalization.NumberStyles.Float, System.Globalization.CultureInfo.InvariantCulture, out float angleDegrees))
            {
                // Вычисление направления робота
                float directionLength = 15; // Длина вектора направления
                float angleInRadians = (angleDegrees - 90) * (float)Math.PI / 180; // Конвертация градусов в радианы и смещение на 90 градусов

                // Вычисление конечной точки вектора направления
                float endX = robotX + directionLength * (float)Math.Cos(angleInRadians);
                float endY = robotY + directionLength * (float)Math.Sin(angleInRadians);

                // Отрисовка вектора направления
                using (Pen directionPen = new Pen(Color.Blue, 2))
                {
                    e.Graphics.DrawLine(directionPen, robotX, robotY, endX, endY);
                }
            }
            else
            {
                // Обработка ошибки конвертации
                Console.WriteLine("Не удалось преобразовать строку t в число с плавающей запятой.");
            }

            // Отрисовка маршрута от робота к точкам
            using (Pen pen = new Pen(Color.Red, 2))
            {
                if (routePoints.Count > 0)
                {
                    e.Graphics.DrawLine(pen, robotX, robotY, routePoints[0].X, routePoints[0].Y);

                    for (int i = 0; i < routePoints.Count - 1; i++)
                    {
                        e.Graphics.DrawLine(pen, routePoints[i], routePoints[i + 1]);
                    }
                }
            }


            // Отрисовка точек маршрута с разными цветами и номерами
            using (Font font = new Font("Arial", 10))
            {
                for (int i = 0; i < routePoints.Count; i++)
                {
                    // Выбор цвета для каждой точки
                    Color pointColor = Color.FromArgb(255, (i * 30) % 256, (i * 50) % 256, (i * 70) % 256);
                    using (Brush brush = new SolidBrush(pointColor))
                    {
                        e.Graphics.FillEllipse(brush, routePoints[i].X - 5, routePoints[i].Y - 5, 10, 10);
                    }

                    // Отрисовка номера точки
                    using (Brush textBrush = new SolidBrush(Color.Blue))
                    {
                        e.Graphics.DrawString((i + 1).ToString(), font, textBrush, routePoints[i].X + 5, routePoints[i].Y - 15);
                    }
                }
            }
        }

        private void txtRadius_TextChanged(object sender, EventArgs e)
        {
            if (float.TryParse("30", out float radius))
            {
                radiusOfAchievement = radius;
            }
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            X_textbox.Text = "X: " + x.ToString();
            Y_textbox.Text = "Y: " + y.ToString();
            if (t != null)
            {
                T_textbox.Text = "T: " + t.ToString() + " : " + re;
            }
            //if (message != null) {
            //    MessageBox.Show(message);
            //}
        }
        int points = 0;
        List<List<Point>> allFoundPaths = new List<List<Point>>();

        private void button3_Click(object sender, EventArgs e)
        {
            PointF nextPoint = routePoints[points];
            float targetX = nextPoint.X;
            float targetY = nextPoint.Y;

            float robotX = (x / 10) * pictureBox1.Width;
            float robotY = pictureBox1.Height - (y / 10) * pictureBox1.Height;

            float dx = targetX - robotX;
            float dy = targetY - robotY;
            float distance = (float)Math.Sqrt(dx * dx + dy * dy);
            TargetX.Text = targetX.ToString();
            TargetY.Text = targetY.ToString();
            PointX.Text = robotX.ToString();
            PointY.Text = robotY.ToString();
            startPosition = new Point((int)robotX, (int)robotY);
            endPosition = new Point((int)targetX, (int)targetY);
            startPositionNOEDIT = startPosition;
            endPositionNOEDIT = endPosition;
            lastFoundPath = ConstrainedBreadthFirstSearch(startPosition.Value, endPosition.Value);

            if (lastFoundPath != null && lastFoundPath.Count > 0)
            {
                DrawPath(lastFoundPath); // Отрисовка пути
                Debug.WriteLine(lastFoundPath);
                Debug.WriteLine($"Координаты последнего найденного пути: {string.Join(", ", lastFoundPath.Select(p => $"({p.X}, {p.Y})"))}");
            }
            else
            {
                Debug.WriteLine("Путь не найден!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (ConstrainedBreadthFirstSearch(endPosition.Value, new Point((int)routePoints[points + 1].X, (int)routePoints[points + 1].Y)) != null)
            {
                DrawPath(ConstrainedBreadthFirstSearch(endPosition.Value, new Point((int)routePoints[points + 1].X, (int)routePoints[points + 1].Y))); // Отрисовка пути
            }
            else
            {
                Debug.WriteLine("Путь не найден!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if (ConstrainedBreadthFirstSearch(new Point((int)routePoints[points + 1].X, (int)routePoints[points + 1].Y), startPosition.Value) != null)
            {
                DrawPath(ConstrainedBreadthFirstSearch(new Point((int)routePoints[points + 1].X, (int)routePoints[points + 1].Y), startPosition.Value)); // Отрисовка пути
            }
            else
            {
                Debug.WriteLine("Путь не найден!", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void LoadMAP_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Text Files (*.txt)|*.txt";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = openFileDialog.FileName;
                    mapBitmap = LoadMapFromFile(filePath);
                    // Считываем файл и рисуем карту
                    if (mapBitmap != null)
                    {
                        pictureBox1.Image = mapBitmap;
                        pictureBox2.Image = mapBitmap;
                    }
                    pictureBox1.Invalidate();
                    pictureBox2.Invalidate();

                }
            }
        }
        private char[,] mapData = new char[40, 64]; // 40 строк, 64 столбца

        private Bitmap LoadMapFromFile(string filePath)
        {
            try
            {
                string[] lines = File.ReadAllLines(filePath);

                if (!ValidateMapFile(lines))
                    return null;

                // Сохраняем карту в массив
                for (int row = 0; row < 40; row++)
                {
                    for (int col = 0; col < 64; col++)
                    {
                        mapData[row, col] = lines[row][col];
                    }
                }


                return null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки карты: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }
        private bool ValidateMapFile(string[] lines)
        {
            if (lines.Length != 40)
            {
                MessageBox.Show("Файл должен содержать ровно 40 строк.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length != 64)
                {
                    MessageBox.Show($"Строка {i + 1} должна содержать ровно 64 символа.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            return true;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "CSV files (*.csv)|*.csv";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    LoadRouteFromFile(openFileDialog.FileName);
                    pictureBox1.Invalidate();
                }
            }
        }

        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show(GetPixelColor(pictureBox1, e.X, e.Y).ToString());
        }

        private void LocalPortTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void LoadRouteFromFile(string filePath)
        {
            routePoints.Clear();
            using (StreamReader reader = new StreamReader(filePath))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] parts = line.Split(',');
                    if (parts.Length == 2 && float.TryParse(parts[0], out float x) && float.TryParse(parts[1], out float y))
                    {
                        routePoints.Add(new PointF(x, y));
                    }
                }
            }

            if (routePoints.Count > 0)
            {
                currentPointIndex = 0;
                robotX = routePoints[currentPointIndex].X;
                robotY = routePoints[currentPointIndex].Y;
                robotCourse = 0;
                timer3.Start();
                timer3.Interval = 1000;
            }
            else
            {
                MessageBox.Show("Файл не содержит координат маршрута.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                timer3.Stop();
            }

        }
        private void SendUDPMessageButton_Click(object sender, EventArgs e)
        {
            string s = UDPMessageTextBox.Text;
            if (AppendLFSymbolCheckBox.Checked) { s += "\n"; };
            SendUDPMessage(s);
        }

        private void RegularUDPSendCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (RegularUDPSendCheckBox.Checked)
            {
                UDPRegularSenderTimer.Enabled = true;
            }
            else
            {
                UDPRegularSenderTimer.Enabled = false;
            }
        }

        private void UDPRegularSenderTimer_Tick(object sender, EventArgs e)
        {
            SendUDPMessage(UDPMessageTextBox.Text);
        }

        private void RemotePortTextBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Automatization == false)
            {
                Automatization = true;

            }
            else
            {
                Automatization = false;

            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        int counter = 0;
        void rotate(int rotate)
        {

        }
        int comandCounters = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {

        }
        private Color GetColorForCell(char cell)
        {
            Color cellColor;

            switch (cell)
            {
                case '#':
                    cellColor = Color.Black; // Стена
                    break;
                case '.':
                    cellColor = Color.White; // Свободная клетка
                    break;
                case '0':
                    cellColor = Color.Gray;
                    break;
                case '1':
                    cellColor = Color.Orange;
                    break;
                case '2':
                    cellColor = Color.Orange;
                    break;
                case '3':
                    cellColor = Color.Orange;
                    break;
                case '4':
                    cellColor = Color.Orange;
                    break;
                case '5':
                    cellColor = Color.Orange;
                    break;
                case '6':
                    cellColor = Color.Orange;
                    break;
                case '7':
                    cellColor = Color.Orange;
                    break;
                case '8':
                    cellColor = Color.Orange;
                    break;
                case '9':
                    cellColor = Color.Pink;
                    break;
                default:
                    cellColor = Color.Transparent; // Пропускаем символы, которые не являются стенами или цифрами
                    break;
            }

            return cellColor;
        }
    }
}