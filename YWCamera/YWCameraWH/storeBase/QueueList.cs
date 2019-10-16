using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YWCameraWH.storeBase
{
    public class QueueList : BaseSortedList<int, QueueItem>
    {

        public QueueItem Find(int key)
        {
            if (base.m_List.ContainsKey(key))
            {
                return base.m_List[key];
            }
            return null;
        }

        public QueueItem GetTopOutQueue()
        {
            for (int i = 0; i < base.m_List.Count; i++)
            {
                QueueItem item = base.m_List.Values[i];
                if (item.usedState == 0)
                {
                    lock (this)
                    {//标记为已经使用
                        item.usedState = 1;
                    }
                    return item;
                }
            }
            return null;
        }
        public QueueItem GetTopQueue1to2()
        {
            for (int i = 0; i < base.m_List.Count; i++)
            {
                QueueItem item = base.m_List.Values[i];
                if (item.usedState == 1)
                {
                    lock (this)
                    {//标记为已经使用
                        item.usedState = 2;
                    }
                    return item;
                }
            }
            return null;
        }
        public QueueItem[] GetAllQueue()
        {
            try
            {
                IList<QueueItem> item = base.m_List.Values;
                return item.ToArray();
            }
            catch
            {
                return null;
            }
        }
    }
}
