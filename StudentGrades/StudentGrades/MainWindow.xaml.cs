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

namespace Student_Grade
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {


        // Global Variables

        // Create a List
        List<int> gradesList = new List<int>()
        {

        };

        // List Remove
        List<Rectangle> itemstoremove = new List<Rectangle>();

        // Create Random Object
        Random randomGrade = new Random();

        // Number of Grades
        const int grades = 50;

        // Max grade value
        const int maxGrade = 100;

        // Run Code

        public MainWindow()
        {
            InitializeComponent();

            // Add 100 blocks
            for (int i = 0; i < grades; i++)
            {
                gradesList.Add(randomGrade.Next(0, maxGrade));
                displayMarks.Text += $"{gradesList[i]}, ";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            // Create Class
            generateAllGrades displayAllGrades = new generateAllGrades();
            createRectangle makeGradeBlock = new createRectangle();
            removeAllGrades removeGradeBlock = new removeAllGrades();
            checkGrades averageGrades = new checkGrades();

            if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Display All Grades")
            {
                displayAllGrades.makeGradesList(grades, gradesList, makeGradeBlock, holdGrades, GradeSpace, randomGrade);
            }
            else if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Randomize Grades")
            {
                // Remove Grades from canvas
                removeGradeBlock.removeGradesList(GradeSpace);

                // Clear numbers in grade list
                displayMarks.Text = "";

                // Enter new values
                for (int i = 0; i < grades; i++)
                {
                    gradesList[i] = randomGrade.Next(0, 100);
                    displayMarks.Text += $"{gradesList[i]}, ";
                }


                // Redisplay all grades
                displayAllGrades.makeGradesList(grades, gradesList, makeGradeBlock, holdGrades, GradeSpace, randomGrade);
            }
            else if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Stats")
            {
                averageGrades.stats(grades, gradesList);
            }
            else if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Count Honours")
            {
                averageGrades.countHonors(grades, gradesList);
            }
            else if (e.OriginalSource.ToString() == "System.Windows.Controls.Button: Exit")
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
    }

    class generateAllGrades
    {
        public void makeGradesList(int grades, List<int> gradesList, createRectangle makeGradeBlock, Rectangle holdGrades, Canvas GradeSpace, Random randomGrade)
        {
            for (int i = 0; i < grades; i++)
            {
                makeGradeBlock.makeRectangle(gradesList, holdGrades, i, GradeSpace);
            }
        }
    }

    class checkGrades
    {
        public void stats(int grades, List<int> gradesList)
        {
            // Get max grade
            int averageGrade = 0;
            int lowGrade = gradesList.Min();
            int maxGrade = gradesList.Max();

            for (int i = 0; i < grades; i++)
            {
                averageGrade += gradesList[i];
            }

            averageGrade /= grades;

            MessageBox.Show($"The highest grade is {maxGrade}% and the lowest grade {lowGrade}%! The average is {averageGrade}%!");
        }

        public void countHonors(int grades, List<int> gradesList)
        {
            int aboveEighty = 0;

            for (int i = 0; i < grades; i++)
            {
                if (gradesList[i] > 80)
                {
                    aboveEighty++;
                }
            }

            MessageBox.Show($"There are {aboveEighty} grades that are above 80%!");
        }
    }

    class createRectangle
    {
        public void makeRectangle(List<int> gradesList, Rectangle holdGrades, int i, Canvas GradeSpace)
        {
            Rectangle newGradeBlock = new Rectangle
            {
                Tag = "gradeBlock",
                Height = (gradesList[i] * ((holdGrades.Height) / 100)),
                Width = holdGrades.Width / gradesList.Count,
                Fill = Brushes.Orange,
                Stroke = Brushes.Black
            };

            Canvas.SetTop(newGradeBlock, Canvas.GetTop(holdGrades));
            Canvas.SetLeft(newGradeBlock, Canvas.GetLeft(holdGrades) + (i * newGradeBlock.Width));

            GradeSpace.Children.Add(newGradeBlock);
        }
    }

    class removeAllGrades
    {
        public void removeGradesList(Canvas GradeSpace)
        {
            // Delete Rectangle
            foreach (var z in GradeSpace.Children.OfType<Rectangle>())
            {
                if (z is Rectangle && (string)z.Tag == "gradeBlock")
                {
                    z.Width = 0;
                    z.Height = 0;
                }

            }
        }
    }
}
