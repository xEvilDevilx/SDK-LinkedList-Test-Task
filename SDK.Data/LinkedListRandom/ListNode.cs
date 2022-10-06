namespace SDK.LinkedListRandom.Data
{
    /// <summary>
    /// Presents Node for ListRand linked list
    /// 
    /// 2017/10/20 - Created, VTyagunov
    /// </summary>
    public class ListNode
    {
        /// <summary>Previous node</summary>
        public ListNode Prev { get; set; }
        /// <summary>Next node</summary>
        public ListNode Next { get; set; }
        /// <summary>Random node</summary>
        public ListNode Rand { get; set; }
        /// <summary>Node data</summary>
        public string Data { get; set; }
    }
}