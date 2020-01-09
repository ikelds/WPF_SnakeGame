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
        public int CountFoodOnField { get; set; }
        public int SnakeSpeed { get; set; }
        public static ObservableCollection<Figure> elements { get; set; }

        System.Windows.Threading.DispatcherTimer gameTickTimer;

        int snakeHeadX = 5;
        int snakeHeadY = 5;
        
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
            CanvasWidth = 400;
            CanvasHeight = 400;
            currPosX = 0;
            currPosY = 0;
            sizeRect = 20;
            currColor = "Black";
            directionSnake = DirectionSnake.Right;
            backgroundOk = false;
            CountFoodOnField = 3;
            SnakeSpeed = 500;

            numberCellsHoriz = CanvasWidth / sizeRect;
            numberCellsVert = CanvasHeight / sizeRect;

            randomPos = new Random();

            elements = new ObservableCollection<Figure>();
            gameTickTimer = new System.Windows.Threading.DispatcherTimer();

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
                    IsHead = false,
                    ElementType = "Snake"
                });

                currSnakeLength++;
            }

            // Добавим заданное кол-во еды на игровое поле.
            for (int i = 0; i < CountFoodOnField; i++)
            {
                AddFood();
            }

            // Запустим таймер, который будет периодически вызывать метод DrawPartSnake - для движения змейки.
            gameTickTimer.Tick += DrawPartSnake;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(SnakeSpeed);
            gameTickTimer.IsEnabled = true;
        }

        // Метод для увеличения скорости змейки.
        void IncreaseSpeedSnake()
        {
            SnakeSpeed -= 10;
            gameTickTimer.Interval = TimeSpan.FromMilliseconds(SnakeSpeed);
        }

        // Если поймали еду, увеличить змейку на один элемент.
        void AddPartSnake()
        {
            elements.Add(new SnakePart
            {
                Left = snakeHeadX * sizeRect,
                Top = snakeHeadY * sizeRect,
                Color = "DeepGreen"
            });
        }

        void CheckToCatchFood()
        {
            int tempPosSnakeX;
            int tempPosSnakeY;

            int tempPosFoodX;
            int tempPosFoodY;

            for (int i = 0; i < elements.Count(); i++)
            {
                if (elements[i].GetType() == typeof(SnakePart))
                {
                    SnakePart sp = (SnakePart)elements[i];

                    if (sp.IsHead == true)
                    {
                        tempPosSnakeX = elements[i].Left / sizeRect;
                        tempPosSnakeY = elements[i].Top / sizeRect;

                        for (int j = 0; j < elements.Count(); j++)
                        {
                            if (elements[j].GetType() == typeof(Food))
                            {
                                Food fd = (Food)elements[j];

                                tempPosFoodX = elements[j].Left / sizeRect;
                                tempPosFoodY = elements[j].Top / sizeRect;

                                if (tempPosSnakeX == tempPosFoodX && tempPosSnakeY == tempPosFoodY)
                                {
                                    //MessageBox.Show("Наехали на еду");
                                    AddPartSnake();
                                    // Удалим найденную еду.
                                    elements.Remove(fd);
                                    // Добавим новую еду.
                                    AddFood();
                                    // Увеличим скорость змейки. 
                                    IncreaseSpeedSnake();
                                }
                            }
                        }
                    }
                }
            }
        }

        void AddFood()
        {
            foodPosX = randomPos.Next(0, numberCellsHoriz);
            foodPosY = randomPos.Next(0, numberCellsVert);

            elements.Add(new Food { Left = foodPosX * sizeRect, Top = foodPosY * sizeRect, Color = "Red" });
            //foodAdded = true;

            //while (!foodAdded)
            //{

            //}
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

            // Добавив новый элемент в начало змейки с цветом Темно зеленый.
            elements.Add(new SnakePart
            {
                Left = snakeHeadX * sizeRect,
                Top = snakeHeadY * sizeRect,
                Color = "DarkGreen",
                ElementType = "Snake",
                IsHead = true

            }); ;

            // Проверим не наехали ли на еду.
            CheckToCatchFood();
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
