using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SearchTree;
using Books;
using System.Linq;

namespace BinarySearchTests
{
    [TestClass]
    public class BinaryTreeTest
    {
        #region int
        [TestMethod]
        public void InsertTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            tree.Insert(9);
            tree.Insert(11);

            CollectionAssert.AreEqual(new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11 }, tree.ToArray());

        }

        [TestMethod]
        public void InOrderTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);
            tree.Delete(12);

            CollectionAssert.AreEqual(new int[]{3,4,5,6,7,8,10},tree.ToArray());
        }

        [TestMethod]
        public void MinMaxCustomComparerTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>((x,y)=>y-x);

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            Assert.AreEqual(3,tree.Max());
            Assert.AreEqual(10, tree.Min());
        }

        [TestMethod]
        public void InOrderCustomCamparerTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>((x, y) => y - x);

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            CollectionAssert.AreEqual(new int[] { 10, 8, 7, 6, 5, 4, 3 }, tree.ToArray());
        }

        [TestMethod]
        public void SubTreeCustomCamparerTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>((x, y) => y - x);

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            CollectionAssert.AreEqual(new int[] { 6, 8, 7, 4, 5, 3 }, tree.SubTree(6));
            CollectionAssert.AreEqual(new int[] { 4, 5, 3 }, tree.SubTree(4));
        }

        [TestMethod]
        public void PreOrderTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            CollectionAssert.AreEqual(new int[] { 10, 6, 4, 3, 5, 8, 7 }, tree.PreOrderWalk().ToArray());
        }

        [TestMethod]
        public void PostOrderTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            CollectionAssert.AreEqual(new int[] { 3, 5, 4, 7, 8, 6, 10 }, tree.PostOrderWalk().ToArray());
        }

        [TestMethod]
        public void DeleteWithOneSonTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            tree.Delete(8);

            CollectionAssert.AreEqual(new int[] { 3, 4, 5, 6, 7, 10 }, tree.ToArray());
        }

        [TestMethod]
        public void DeleteWithTwoSonsTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            tree.Delete(6);

            CollectionAssert.AreEqual(new int[] { 3, 4, 5, 7, 8, 10 }, tree.ToArray());
        }

        [TestMethod]
        public void DeleteWithNoSonsTest()
        {
            BinarySearchTree<int> tree = new BinarySearchTree<int>();

            tree.Insert(10);
            tree.Insert(6);
            tree.Insert(4);
            tree.Insert(8);
            tree.Insert(5);
            tree.Insert(3);
            tree.Insert(7);

            tree.Delete(5);

            CollectionAssert.AreEqual(new int[] { 3, 4, 6, 7, 8, 10 }, tree.ToArray());
        }
        #endregion

        #region string

        [TestMethod]
        public void InOrderCustomStringTest()
        {
            BinarySearchTree<string> tree = new BinarySearchTree<string>((x, y) => x.CompareTo(y) * -1);

            tree.Insert("Author");
            tree.Insert("Bart");
            tree.Insert("Jon");
            tree.Insert("Marry");
            tree.Insert("Barry");

            CollectionAssert.AreEqual((new string[] { "Author", "Barry", "Bart", "Jon", "Marry" }).Reverse().ToArray(), tree.ToArray());
        }

        [TestMethod]
        public void InOrderStringTest()
        {

            BinarySearchTree<string> tree = new BinarySearchTree<string>();

            tree.Insert("Author");
            tree.Insert("Bart");
            tree.Insert("Jon");
            tree.Insert("Marry");
            tree.Insert("Barry");

            CollectionAssert.AreEqual(new string[] { "Author", "Barry", "Bart", "Jon", "Marry" }, tree.ToArray());
        }

        #endregion

        #region Book

        [TestMethod]
        public void InOrderCustomBookTest()
        {
            Comparison<Book> comparer = (x, y) => (x.Author + x.Title).CompareTo(y.Author + y.Title);
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>(comparer);

            Book[] books = new Book[]
            {
                new Book {Author = "Joan Roling", PageCount = 133, Price = 10000, Title = "King of the kingdom"},
                new Book {Author = "Jon Skeet", PageCount = 614, Price = 1500, Title = "C# in Depth"},
                new Book {Author = "Jon Skeet", PageCount = 644, Price = 1504, Title = "C# in Depth. Third edition"},
                new Book {Author = "Jeffrey Richter", PageCount = 896, Price = 98768, Title = "CLR via C#"}
            };

            int i = 0;
            for (i = 0; i < books.Length; i++)
                tree.Insert(books[i]);            

            for (i = 0; i < books.Length; i++ )
                for (int j = 0; j < books.Length-1-i; j++)
                if (comparer(books[j],books[j+1])>0)
                {
                    Book temp = books[j];
                    books[j] = books[j + 1];
                    books[j + 1] = temp;
                }

                CollectionAssert.AreEqual(books, tree.ToArray());
        }

        [TestMethod]
        public void InOrderBookTest()
        {            
            BinarySearchTree<Book> tree = new BinarySearchTree<Book>();

            Book[] books = new Book[]
            {
                new Book {Author = "Joan Roling", PageCount = 133, Price = 10000, Title = "King of the kingdom"},
                new Book {Author = "Jon Skeet", PageCount = 614, Price = 1500, Title = "C# in Depth"},
                new Book {Author = "Jon Skeet", PageCount = 644, Price = 1504, Title = "C# in Depth. Third edition"},
                new Book {Author = "Jeffrey Richter", PageCount = 896, Price = 98768, Title = "CLR via C#"}
            };

            int i = 0;
            for (i = 0; i < books.Length; i++)
                tree.Insert(books[i]);

            for (i = 0; i < books.Length; i++)
                for (int j = 0; j < books.Length - 1 - i; j++)
                    if (books[j].CompareTo(books[j + 1]) > 0)
                    {
                        Book temp = books[j];
                        books[j] = books[j + 1];
                        books[j + 1] = temp;
                    }

            CollectionAssert.AreEqual(books, tree.ToArray());
        }

        #endregion

        #region Point2D

        [TestMethod]
        public void InOrderCustomPoint2DTest()
        {
            BinarySearchTree<Point2D> tree = new BinarySearchTree<Point2D>((x, y) => - x.x - x.y + y.x + y.y);

            Point2D[] points = new Point2D[]
            {
             new Point2D { x = 0, y = 0 },
             new Point2D { x = 1, y = 1 },
             new Point2D { x = 2, y = 2 },
             new Point2D { x = 3, y = 3 },
             new Point2D { x = 4, y = 4 }
            };

            for (int i = 0; i < points.Length; i++) tree.Insert(points[i]);

                CollectionAssert.AreEqual(points.Reverse().ToArray(), tree.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void InOrderPoint2DTest()
        {
            BinarySearchTree<Point2D> tree = new BinarySearchTree<Point2D>();

            Point2D[] points = new Point2D[]
            {
             new Point2D { x = 0, y = 0 },
             new Point2D { x = 1, y = 1 },
             new Point2D { x = 2, y = 2 },
             new Point2D { x = 3, y = 3 },
             new Point2D { x = 4, y = 4 }
            };

            for (int i = 0; i < points.Length; i++) tree.Insert(points[i]);

            CollectionAssert.AreEqual(points.Reverse().ToArray(), tree.ToArray());
        }


        private struct Point2D
        {
            public int x;
            public int y;
        }
        #endregion
    }
}
