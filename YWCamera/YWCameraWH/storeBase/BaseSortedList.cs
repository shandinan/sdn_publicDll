using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YWCameraWH.storeBase
{
    public class BaseSortedList<K, V>
    {
        protected SortedList<K, V> m_List;
        protected object m_Lock;

        public BaseSortedList()
        {
            this.m_List = new SortedList<K, V>();
            this.m_Lock = new object();
        }

        /// <summary>
        /// 添加键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="Obj"></param>
        public void Add(K key, V Obj)
        {
            if (!this.m_List.ContainsKey(key))
            {
                lock (this.m_Lock)
                {
                    this.m_List.Add(key, Obj);
                }
            }
        }

        /// <summary>
        /// 移除键值对
        /// </summary>
        /// <param name="key"></param>
        public void Remove(K key)
        {
            if (this.m_List.ContainsKey(key))
            {
                lock (this.m_Lock)
                {
                    this.m_List.Remove(key);
                }
            }
        }

        /// <summary>
        /// 队列内的数量
        /// </summary>
        public int Count
        {
            get
            {
                return this.m_List.Count;
            }
        }

        public V this[K key]
        {
            get
            {
                if (this.m_List.ContainsKey(key))
                {
                    return this.m_List[key];
                }
                return default(V);
            }
            set
            {
                lock (this.m_Lock)
                {
                    if (this.m_List.ContainsKey(key))
                    {
                        this.m_List[key] = value;
                    }
                }
            }
        }

        public SortedList<K, V> List
        {
            get
            {
                return this.m_List;
            }
        }
    }
}
