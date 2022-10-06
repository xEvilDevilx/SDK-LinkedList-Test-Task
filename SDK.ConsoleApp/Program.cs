using System;
using System.IO;

using SDK.LinkedListRandom;
using SDK.LinkedListRandom.Interfaces;

namespace SDK.LinkedList.ConsoleApp
{
    /// <summary>
    /// Presents Main class in project with Main Entry Point method
    /// 
    /// 2017/10/20 - Created, VTyagunov
    /// </summary>
    class Program
    {
        #region Variables

        /// <summary>Linked list for Serialize</summary>
        private static IListRand _linkedlist;
        /// <summary>Linked list for Deserialize</summary>
        private static IListRand _deserializedLinkedList;
        /// <summary>Path to file for Serialize and Deserialize</summary>
        private static string _filePath;

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        static Program()
        {
            _linkedlist = new ListRand();
            _deserializedLinkedList = new ListRand();
            _filePath = "LinkedListRandomTest.dat";
        }

        #endregion

        #region Methods

        /// <summary>
        /// The Main Entry Point of App
        /// </summary>
        /// <param name="args">Arguments array</param>
        static void Main(string[] args)
        {
            _linkedlist.AddAtHead("50");
            _linkedlist.AddAtTail("0");
            _linkedlist.AddAtHead("40");
            _linkedlist.AddAtTail("1");
            _linkedlist.AddAtHead("30");
            _linkedlist.AddAtTail("20");
            
            _linkedlist.AddAtHead("60");
            _linkedlist.AddAtHead("70");
            _linkedlist.AddAtHead("80");

            _linkedlist.RemoveFromTail();
            _linkedlist.RemoveFromTail();

            _linkedlist.RemoveFromHead();
            _linkedlist.RemoveFromHead();
            _linkedlist.RemoveFromHead();

            _linkedlist.AddAtTail("1000001");
            _linkedlist.AddAtTail("2000001");
            _linkedlist.AddAtTail("3020001");

            _linkedlist.AddAtHead("6000001");
            _linkedlist.AddAtHead("7000001");

            Console.WriteLine("Before Serialize we have this Nodes in our Linked List from Head to Tail:\n");
            var nodesList = _linkedlist.GetNodesList();
            foreach(var node in nodesList)
                Console.WriteLine("Node Data = [{0}], Node Rand = [{1}]", node.Data, node.Rand.Data);

            using (var stream = new FileStream(_filePath, FileMode.OpenOrCreate))
                _linkedlist.Serialize(stream);
            
            using (var stream = new FileStream(_filePath, FileMode.Open))
                _deserializedLinkedList.Deserialize(stream);

            Console.WriteLine("\n\nAfter Deserialize:\n");
            var resultNodesList = _deserializedLinkedList.GetNodesList();
            foreach (var node in resultNodesList)
                Console.WriteLine("Node Data = [{0}], Node Rand = [{1}]", node.Data, node.Rand.Data);

            Console.ReadLine();
        }

        #endregion
    }
}