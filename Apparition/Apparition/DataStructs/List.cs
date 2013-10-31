using System;
using System.Collections;
using System.Collections.Generic;

namespace DesktopBattle
{
    /// <summary>
    /// List of objects</summary>
    /// <typeparam name="T">
    /// Type of objects stored</typeparam>
    public class List<T> : IEnumerable<T>
    {
        /// <summary>First item in list</summary>
        private Node<T> head;
        /// <summary>Last item in list</summary>
        private Node<T> tail;
        /// <summary>Number of items in list</summary>
        private int count;

        /// <summary>Number of Nodes in list</summary>
        public int Count { get { return count; } }

        /// <summary>
        /// Indexer for List</summary>
        /// <param name="index">
        /// Index of List item to get or set</param>
        /// <returns>
        /// get: Data at index set: nothing</returns>
        public T this[int index]
        {
            get
            {
                Node<T> temp = head;

                try
                {
                    for (int ii = 0; ii < index; ii++)
                    {
                        temp = temp.Next;
                    }
                }
                catch (Exception)
                {
                    throw new IndexOutOfRangeException("index is not in List");
                }

                try
                {
                    return temp.Data;
                }
                catch
                {
                    return default(T);
                }
            }

            set
            {
                Node<T> temp = head;

                try
                {
                    for (int ii = 0; ii < index; ii++)
                    {
                        temp = temp.Next;
                    }
                }
                catch (Exception)
                {
                    throw new IndexOutOfRangeException("index is not in List");
                }

                temp.Data = value;
            }
        }

        /// <summary>
        /// Enumerator for foreach loops</summary>
        /// <returns></returns>
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> temp = head;

            for (int ii = 0; ii < count; ii++)
            {
                yield return this[ii];
            }
        }

        ///<summary>
        /// Returns other enumerator</summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Default Constructor, sets default values</summary>
        public List()
        {
            count = 0;
            head = null;
            tail = null;
        }

        /// <summary>
        /// Adds item to end of list</summary>
        /// <param name="data">
        /// Data to add to list</param>
        public void Add(T data)
        {
            Node<T> toBeAdded = new Node<T>(data, count);        // Node to be put into array

            if (head == null)
            {
                //Console.WriteLine( "First Add" );
                head = toBeAdded;
                tail = head;
            }
            else
            {
                //Console.WriteLine( "Not First Add" );
                tail.Next = toBeAdded;
                tail = tail.Next;
            }

            count++;
        }

        /// <summary>
        /// Adds Item to front of List</summary>
        /// <param name="data">
        /// Item to add to front of List</param>
        public void AddFront(T data)
        {
            Node<T> toBeAdded = new Node<T>(data, 0, head);
            Node<T> temp = head;

            head = toBeAdded;
            count++;

            for (int ii = 1; ii < count; ii++)
            {
                temp.Index++;
                temp = temp.Next;
            }
        }

        /// <summary>
        ///<author>Eric Christenson</author>
        ///<contributer>Daniel Jost (made compatible with the rest of the List class)</contributer>
        ///<contributer>Brandon Littell (fixed various bugs with references and the Indexer)</contributer>
        /// remove first node with data
        /// </summary>
        /// <param name="data">object of type T to add</param>

        public void Remove(T data)
        {
            Node<T> current = head;
            Node<T> prev = new Node<T>(data, count);

            while (current != null)// && current.Next != null)
            {
                // The following 2 lines are what I changed to make it work
                // EqualityComparer uses the default .Equals of object, comparing the hash code 
                // of the two objects which will be unique for each instance of an object
                if (EqualityComparer<T>.Default.Equals(data, current.Data))
                //if (current.Data.Equals(data))
                {
                    if (current == head) // if current is head, it has no previous, need to do something different
                    {
                        head = head.Next;
                        count--;
                    }
                    else if (current == tail)
                    {
                        prev.Next = current.Next;
                        tail = prev;
                        current = null;
                        count--;
                    }
                    else
                    {
                        prev.Next = current.Next;
                        current = null;
                        count--;
                    }
                    ReIndex();
                    return;
                }
                else
                {
                    prev = current;
                    current = current.Next;
                }
            }
        }

        /// <summary>
        /// Sets index of each node to its correct value
        /// </summary>
        ///<author>Eric Christenson</author>
        private void ReIndex()
        {
            Node<T> temp = head;
            for (int i = 0; i < count; i++)
            {
                temp.Index = i;
                temp = temp.Next;
            }
        }


        /// <summary>
        /// Removes an item at the specified 0-based index
        /// </summary>
        /// <author>Colden C</author>
        /// <contributor>Zachary Behrmann (index updating code)</contributor>
        /// <contributor>Brandon Littell (fixed for GetEnumerator)</contributor>
        /// <param name="index">The index of the item you wish to remove</param>
        /// <returns>A boolean value indicating whether an item was removed</returns>
        public void RemoveAt(int index)
        {
            //Calls the Remove function using the index
            Remove(this[index]);
        }


        /// <summary>
        /// Gets the index of the node containing T data
        /// </summary>
        /// <author>Brandon Littell</author>
        /// <param name="data">The data to look for in the list</param>
        /// <returns>Returns the index of the node containing data, or -1 if there is no node containing data</returns>
        public int IndexOf(T data)
        {
            //The current node we are looking at
            Node<T> current = head;

            try
            {
                for (int ii = 0; ii < Count; ii++)
                {
                    if (current.Data.Equals(data))
                        return current.Index;
                    else
                        current = current.Next;
                }

                //If nothing has been returned, return -1 to signal that data is not in the list
                return -1;

            }
            catch (Exception)
            {
                return -1;
            }
        }

        /// <summary>
        /// Returns whether or not the list is empty</summary>
        /// <returns>
        /// Emptyness of list</returns>
        public bool IsEmpty()
        {
            return head == null;
        }

        /// <summary>
        /// Returns all items in List</summary>
        /// <returns>
        /// A string of all items</returns>
        public override string ToString()
        {
            string returnString = "";
            if (head != null)
            {
                returnString = head.Index + ": " + head.ToString();      // String to add elements to
                Node<T> temp = head;                        // Current Node to be added to string

                for (int ii = 0; ii < count - 1; ii++)
                {
                    temp = temp.Next;                       // Set temp as next
                    returnString += ", " + temp.Index + ": " + temp.ToString(); // Add temp to string
                }
            }

            return returnString;
        }



        /// <summary>	USE AT YOUR OWN RISK
        /// Checks whether the list contains the specified item
        /// </summary>
        /// <author>Zachary Behrmann</author>
        /// <param name="data">The item to check against the list</param>
        /// <returns>A boolean value indicating whether item is in the list</returns>
        public bool Contains(T data)
        {
            Node<T> current = head;
            while (!current.Data.Equals(data))
            {
                if (current.Next != null)
                {
                    current = current.Next;
                }
                else
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// Inserts an item at the specified 0-based index
        /// </summary>
        /// <author>Zachary Behrmann</author>
        /// <param name="index">The index you wish to insert it at</param>
        /// <param name="data">The item to insert</param>
        public void Insert(int index, T data)
        {
            Node<T> current = head;
            if (index == 0)
            {
                head = new Node<T>(data, 0, current);
                for (int i = 0; i < count; i++)
                {
                    current.Index++;
                    if (current.Next != null)
                    {
                        current = current.Next;
                    }
                }
                count++;
            }
            else if (index == count)
            {
                tail.Next = new Node<T>(data, count);
                count++;
            }
            else if (index > count || index < 0)
            {
                throw new Exception("index is outside the bounds of the list");
            }
            else
            {
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }

                Node<T> temp = current.Next;
                current.Next = new Node<T>(data, index, temp);
                for (int i = index; i < count; i++)
                {
                    temp.Index++;
                    if (temp.Next != null)
                    {
                        temp = temp.Next;
                    }
                }
                count++;
            }
        }

    }
}