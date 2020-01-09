using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfSnake.Model;

namespace WpfSnake.ViewModel
{
    class GameViewModel
    {
        public int CanvasWidth { get; set; }
        public int CanvasHeight { get; set; }
        public static ObservableCollection<Figure> elements { get; set; }

        int snakeHeadX = 5;
        int snakeHeadY = 5;
        const int SnakeStartSpeed = 500;
        int sizeRect;
        int currPosX;
        int currPosY;
        int foodPosX;
        int foodPosY;
        String currColor;
        bool backgroundOk;
        int numberCellsHoriz;
        int numberCellsVert;
        bool foodAdded = false;
        Random randomPos;


        int currSnakeLength;
        enum DirectionSnake { Left, Right, Up, Down };
        DirectionSnake directionSnake;



        public RelayCommand UpCommand { get; set; }
        public RelayCommand DownCommand { get; set; }
        public RelayCommand LeftCommand { get; set; }
        public RelayCommand RightCommand { get; set; }
        public RelayCommand TestСommand { get; set; }

        public GameViewModel()
        {
            CanvasWidth = 600;
            CanvasHeight = 600;
            currPosX = 0;
            currPosY = 0;
            sizeRect = 20;
            currColor = "Black";
            directionSnake = DirectionSnake.Right;
            backgroundOk = false;

            numberCellsHoriz = CanvasWidth / sizeRect;
            numberCellsVert = CanvasHeight / sizeRect;

            randomPos = new Random();


            elements = new ObservableCollection<Figure>();
            System.Windows.Threading.DispatcherTimer gameTickTimer = new System.Windows.Threading.DispatcherTimer();

            UpCommand = new RelayCommand(new Action<object>(btn_UpPressed));
            DownCommand = new RelayCommand(new Action<object>(btn_DownPressed));
            LeftCommand = new RelayCommand(new Action<object>(btn_LeftPressed));
            RightCommand = new RelayCommand(new Action<object>(btn_RightPressed));
            TestСommand = new RelayCommand(new Action<object>(Test));

            // Риусем фон.
            while (!backgroundOk)
            {
                while (currPosY < CanvasHeight)
                {
                    if (currColor == "Black")
                        currColor = "Yellow";
                    else currColor = "Black";

                    while (currPosX < CanvasWidth)
                    {
                        elements.Add(new RectField { Left = currPosX, Top = currPosY, Color = currColor });

                        currPosX += sizeRect;

                        if (currColor == "Black")
                            currColor = "Yellow";
                        else currColor = "Black";
                    }
                    currPosX = 0;
                    currPosY += sizeRect;

                }

                backgroundOk = true;
            }

            // Нарисуем первоначальное положение змейки в начале игры.
            for (int i = 0; i < 3; i++)
            {
                snakeHeadX++;

                elements.Add(new SnakePart
                {
                    Left = snakeHeadX * sizeRect,
                    Top = snakeHeadY * sizeRect,
                    Color = "Green",
                    isHead = false,
                    ElementType = "Snake"
                });

                currSnakeLength++;
            }



            // Запустим таймер, который будет периодически вызывать метод DrawPartSnake - для движения змейки.
            gameTickTimer.Tick += DrawPartSnake;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(SnakeStartSpeed);
            gameTickTimer.IsEnabled = true;

            AddFood();
        }

        void AddPartSnake()
        {
            switch (directionSnake)
            {
                case DirectionSnake.Right:
                    snakeHeadX++;
                    break;
                case DirectionSnake.Left:
                    snakeHeadX--;
                    break;
                case DirectionSnake.Down:
                    snakeHeadY++;
                    break;
                case DirectionSnake.Up:
                    snakeHeadY--;
                    break;
            }

            elements.Add(new SnakePart
            {
                Left = snakeHeadX * sizeRect,
                Top = snakeHeadY * sizeRect,
                Color = "Purple"
            });
            //elements.Add(new SnakePart { Left = foodPosX * sizeRect, Top = foodPosY * sizeRect, Color = "Purple" });
        }

        void CheckToCatchFood()
        {
            int tempPosX;
            int tempPosY;

            for (int i = 0; i < elements.Count(); i++)
            {
                if (elements[i].GetType() == typeof(SnakePart))
                {
                    tempPosX = elements[i].Left / sizeRect;
                    tempPosY = elements[i].Top / sizeRect;

                    //MessageBox.Show(tempPosX.ToString() + " " + tempPosY.ToString());
                    //MessageBox.Show("В цикле");
                    if (tempPosX == foodPosX && tempPosY == foodPosY)
                    {
                        //MessageBox.Show("Наехали на еду");
                        AddPartSnake();

                    }


                }
            }
        }


        void AddFood()
        {
            while (!foodAdded)
            {
                foodPosX = randomPos.Next(0, numberCellsHoriz);
                foodPosY = randomPos.Next(0, numberCellsVert);

                elements.Add(new Food { Left = foodPosX * sizeRect, Top = foodPosY * sizeRect, Color = "Red" });
                foodAdded = true;
            }
        }

        private void Test(object sender)
        {
            MessageBox.Show(foodPosX.ToString() + " " + foodPosY.ToString());
        }


        // Метод который удаляет последний элемент змейки и добавляет новый елемнт в начало
        // в зависимости от направления.
        void DrawPartSnake(object sender, EventArgs e)
        {
            switch (directionSnake)
            {
                case DirectionSnake.Right:
                    snakeHeadX++;
                    break;
                case DirectionSnake.Left:
                    snakeHeadX--;
                    break;
                case DirectionSnake.Down:
                    snakeHeadY++;
                    break;
                case DirectionSnake.Up:
                    snakeHeadY--;
                    break;
            }

            // Удалим последний элемент змейки.
            for (int i = 0; i < elements.Count(); i++)
            {
                if (elements[i].GetType() == typeof(SnakePart))
                {
                    elements.RemoveAt(i);

                    break;
                }
            }

            // Установим цвет всех частей змейки в зеленый.
            for (int i = 0; i < elements.Count(); i++)
            {
                if (elements[i].GetType() == typeof(SnakePart))
                {

                    elements[i].Color = "LightGreen";
                }
            }

            //elements.FindAll()

            // Добавив новый элемент в начало змейки с цветом Темно зеленый.
            elements.Add(new SnakePart
            {
                Left = snakeHeadX * sizeRect,
                Top = snakeHeadY * sizeRect,
                Color = "DarkGreen",
                ElementType = "Snake"
            });

            CheckToCatchFood();
        }

        private void btn_DelFood(object sender)
        {
            //essageBox.Show("Вверх");
            //directionSnake = DirectionSnake.Up;

            foreach (Figure f in elements)
            {
                MessageBox.Show((f.GetType() == typeof(SnakePart)).ToString());
            }

        }



        private void btn_UpPressed(object sender)
        {
            //essageBox.Show("Вверх");
            directionSnake = DirectionSnake.Up;
        }

        private void btn_DownPressed(object sender)
        {
            //MessageBox.Show("Вниз");
            directionSnake = DirectionSnake.Down;
        }

        private void btn_LeftPressed(object sender)
        {
            //MessageBox.Show("Влево");
            directionSnake = DirectionSnake.Left;
        }

        private void btn_RightPressed(object sender)
        {
            //MessageBox.Show("Вправо");
            directionSnake = DirectionSnake.Right;
        }

    }
}
