using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using SDK.LinkedListRandom.Data;
using SDK.LinkedListRandom.Interfaces;

namespace SDK.LinkedListRandom
{
    /// <summary>
    /// Implements ListRand linked list functionality
    /// 
    /// 2017/10/20 - Created, VTyagunov
    /// </summary>
    public class ListRand : IListRand
    {
        #region Properties

        /// <summary>Head Node</summary>
        public ListNode Head { get; private set; }
        /// <summary>Tail Node</summary>
        public ListNode Tail { get; private set; }
        /// <summary>Count</summary>
        public int Count { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ListRand()
        {
            Head = new ListNode();
        }

        #endregion

        #region Methods

        #region IListRand

        /// <summary>
        /// Use for Add at Tail new Node
        /// </summary>
        /// <param name="data">Node data</param>
        public void AddAtTail(string data)
        {
            var newNode = new ListNode()
            {
                Data = data                
            };
            newNode.Rand = GetRandomNode();
            if (newNode.Rand == null)
                newNode.Rand = newNode;

            if (Tail != null)
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
                Tail = newNode;
            }
            else
            {
                Tail = newNode;
                Head.Next = Tail;
            }

            Count++;
        }

        /// <summary>
        /// Use for Remove Node from Tail 
        /// </summary>
        public void RemoveFromTail()
        {
            if (Tail == null)
            {
                // Log: Can not remove node from Tail. Tail is null
                return;
            }

            if (Count > 0)
            {
                Tail = Tail.Prev;
                Count--;
            }
        }

        /// <summary>
        /// Use for Add at Head new Node 
        /// </summary>
        /// <param name="data">Node data</param>
        public void AddAtHead(string data)
        {
            var newNode = new ListNode()
            {
                Data = data                
            };
            newNode.Rand = GetRandomNode();
            if (newNode.Rand == null)
                newNode.Rand = newNode;

            if (Head.Next != null)
            {
                Head.Next.Prev = newNode;
                newNode.Next = Head.Next;
            }
            else Tail = newNode;
            Head.Next = newNode;

            Count++;
        }

        /// <summary>
        /// Use for Remove Node from Tail 
        /// </summary>
        public void RemoveFromHead()
        {
            if (Head.Next == null)
            {
                // Log: Can not remove node from Head. Head is null
                return;
            }

            if (Count > 0)
            {
                Head.Next = Head.Next.Next;
                Head.Next.Prev = Head;
                Count--;
            }
        }

        /// <summary>
        /// Use for Get List of ListNodes
        /// </summary>
        /// <returns></returns>
        public List<ListNode> GetNodesList()
        {
            var nodeList = new List<ListNode>();
            var node = Head.Next;

            while (node != null)
            {
                nodeList.Add(node);
                node = node.Next;
            }

            return nodeList;
        }

        #endregion

        /// <summary>
        /// Use for Get Random Node
        /// </summary>
        /// <returns></returns>
        private ListNode GetRandomNode()
        {
            try
            {
                if (Count > 0)
                {
                    var rnd = new Random();
                    var id = rnd.Next(1, Count);
                    int tempID = 1;
                    var tempNode = Head.Next;
                    while (tempID < id)
                    {
                        tempNode = tempNode.Next;
                        tempID++;
                    }
                    return tempNode;
                }
            }
            catch(ArgumentOutOfRangeException ex)
            {
                // Log exception                
            }

            return null;
        }

        #region Serialize

        /// <summary>
        /// Use for Serialize current object
        /// </summary>
        /// <param name="s">File stream</param>
        public void Serialize(FileStream s)
        {
            var nodeList = GetNodesList();

            WriteInt32(s, Count);
            WriteListNode(s, Head.Next, nodeList);
        }
        
        /// <summary>
        /// Use for Write ListNode to Stream
        /// </summary>
        /// <param name="s">Stream</param>
        /// <param name="node">ListNode object</param>
        /// <param name="nodesList">List of ListNode object</param>
        private void WriteListNode(Stream s, ListNode node, List<ListNode> nodesList)
        {
            if (node == null)
                return;

            WriteListNode(s, node.Next, nodesList);

            try
            {
                if (node.Rand != null)
                {
                    var randNodeID = nodesList.IndexOf(node.Rand);
                    WriteInt32(s, randNodeID);
                }
                else WriteEmpty(s);

                WriteString(s, node.Data);
            }
            catch(Exception ex)
            {
                // Log exception
                Environment.Exit(0);
            }
        }
        
        /// <summary>
        /// Use for Write Empty to Stream
        /// </summary>
        /// <param name="s">Stream</param>
        private void WriteEmpty(Stream s)
        {
            try
            {
                s.Write(BitConverter.GetBytes(-1), 0, sizeof(Int32));
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }

        /// <summary>
        /// Use for Write String to Stream
        /// </summary>
        /// <param name="s">Stream</param>
        /// <param name="s">String value</param>
        private void WriteString(Stream s, string str)
        {
            try
            {
                if (string.IsNullOrEmpty(str))
                {
                    WriteEmpty(s);
                    return;
                }
                var bytesArray = Encoding.BigEndianUnicode.GetBytes(str);
                WriteInt32(s, bytesArray.Length);
                s.Write(bytesArray, 0, bytesArray.Length);
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }

        /// <summary>
        /// Use for Write Int32 to Stream
        /// </summary>
        /// <param name="s">Stream</param>
        /// <param name="i">Int32 value</param>
        private void WriteInt32(Stream s, Int32 i)
        {
            try
            {
                s.Write(BitConverter.GetBytes(i), 0, sizeof(Int32));
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }

        #endregion

        #region Deserialize

        /// <summary>
        /// Use for Deserialize current object
        /// </summary>
        /// <param name="s">File stream</param>
        public void Deserialize(FileStream s)
        {
            try
            {
                Count = ReadInt32(s);
                var nodesList = new List<ListNode>();
                var node = ReadListNode(s, null, Count, nodesList);
                Head.Next = node;

                var tempNode = node;
                while (tempNode != null)
                {
                    Tail = tempNode;
                    tempNode = tempNode.Next;
                }
            }
            catch(Exception ex)
            {
                // Log exception
                Environment.Exit(0);
            }
        }

        /// <summary>
        /// Use for Write ListNode to Stream
        /// </summary>
        /// <param name="s">Stream</param>
        /// <param name="prevNode">Previous node</param>
        /// <param name="nodesCount">Count of nodes</param>
        /// <param name="nodesList">List of nodes</param>
        /// <returns></returns>
        private ListNode ReadListNode(Stream s, ListNode prevNode, int nodesCount, List<ListNode> nodesList)
        {
            if (nodesCount == 0)
                return null;
            
            nodesCount--;
            var node = new ListNode();
            node.Prev = prevNode;
            nodesList.Add(node);

            node.Next = ReadListNode(s, node, nodesCount, nodesList);

            try
            {
                int randID = ReadInt32(s);
                if (randID != -1)
                    node.Rand = nodesList[randID];

                node.Data = ReadString(s);

                return node;
            }
            catch(Exception ex)
            {
                // Log exception                
                throw;
            }
        }

        /// <summary>
        /// Use for Read String
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        private string ReadString(Stream stream)
        {
            try
            {
                int length = ReadInt32(stream);
                if (length == -1)
                    return string.Empty;

                var bytesArray = new byte[length];
                int bytesRead = 0;

                while (bytesRead < length)
                {
                    var count = stream.Read(bytesArray, bytesRead, bytesArray.Length - bytesRead);
                    if (count == 0)
                        throw new Exception("Unexpected disconnect");

                    bytesRead += count;
                }
                
                return Encoding.BigEndianUnicode.GetString(bytesArray, 0, bytesArray.Count());
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }

        /// <summary>
        /// Use for Read Int32
        /// </summary>
        /// <param name="stream">Stream</param>
        /// <returns></returns>
        private Int32 ReadInt32(Stream stream)
        {
            try
            {
                var bytesArray = new byte[sizeof(Int32)];
                stream.Read(bytesArray, 0, sizeof(Int32));
                
                return BitConverter.ToInt32(bytesArray, 0);
            }
            catch (Exception ex)
            {
                // Log exception
                throw;
            }
        }

        #endregion

        #endregion
    }
}