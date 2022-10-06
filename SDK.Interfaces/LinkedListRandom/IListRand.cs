using System.Collections.Generic;
using System.IO;

using SDK.LinkedListRandom.Data;

namespace SDK.LinkedListRandom.Interfaces
{
    /// <summary>
    /// Presents ListRand linked list functionality interface
    /// 
    /// 2017/10/20 - Created, VTyagunov
    /// </summary>
    public interface IListRand
    {
        /// <summary>Head Node</summary>
        ListNode Head { get; }
        /// <summary>Tail Node</summary>
        ListNode Tail { get; }
        /// <summary>Count</summary>
        int Count { get; }

        /// <summary>
        /// Use for Add at Tail new Node
        /// </summary>
        /// <param name="data">Node data</param>
        void AddAtTail(string data);

        /// <summary>
        /// Use for Remove Node from Tail 
        /// </summary>
        void RemoveFromTail();

        /// <summary>
        /// Use for Add at Head new Node 
        /// </summary>
        /// <param name="data">Node data</param>
        void AddAtHead(string data);

        /// <summary>
        /// Use for Remove Node from Tail 
        /// </summary>
        void RemoveFromHead();

        /// <summary>
        /// Use for Get List of ListNodes
        /// </summary>
        /// <returns></returns>
        List<ListNode> GetNodesList();

        /// <summary>
        /// Use for Serialize current object
        /// </summary>
        /// <param name="s">File stream</param>
        void Serialize(FileStream s);

        /// <summary>
        /// Use for Deserialize current object
        /// </summary>
        /// <param name="s">File stream</param>
        void Deserialize(FileStream s);
    }
}