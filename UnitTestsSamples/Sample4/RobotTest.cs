using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace UnitTestsSamples.Sample4
{
    [TestFixture]
    public class RobotTest
    {
        [TestCase]
        public void test_init_robot()
        {
            Robot robot = new Robot();
            Assert.AreEqual(robot.PosX, 0);
            Assert.AreEqual(robot.PosY, 0);
        }

        [TestCase]
        public void test_move_robot_x()
        {
            Robot robot = new Robot();
            robot.MoveX(10);
            Assert.AreEqual(robot.PosX, 10);
            Assert.AreEqual(robot.PosY, 0);
        }

        [TestCase]
        public void test_move_robot_y()
        {
            Robot robot = new Robot();
            robot.MoveY(20);
            Assert.AreEqual(robot.PosX, 0);
            Assert.AreEqual(robot.PosY, 20);
        }

        [TestCase]
        public void test_move_x_and_y()
        {
            Robot robot = new Robot();
            robot.Move(10, 20);
            Assert.AreEqual(robot.PosX, 10);
            Assert.AreEqual(robot.PosY, 20);
        }
    }

    internal class Robot
    {
        public Robot()
        {
            PosX = 0;
            PosY = 0;
        }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public void Move(int x, int y)
        {
            MoveX(x);
            MoveY(y);
        }

        public void MoveX(int i)
        {
            PosX += i;
        }

        public void MoveY(int i)
        {
            PosY += i;
        }
    }
}
