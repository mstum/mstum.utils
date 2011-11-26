#region License
/* The MIT License (MIT)
 * Copyright (c) 2011 Michael Stum, http://www.Stum.de <opensource@stum.de>
 * 
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime;

namespace mstum.utils
{
    /// <summary>
    /// A Circular Buffer holds as many elements as the capacity it was created with. Adding more items overwrites the oldest items.
    /// </summary>
    /// <remarks>
    /// A CircularBuffer{T} can support multiple readers concurrently, as long as the collection is not modified.
    /// Enumerating through a collection is intrinsically not a thread-safe procedure.
    /// In the rare case where an enumeration contends with one or more write accesses, the only way to ensure thread safety is to lock the collection during the entire enumeration.
    /// To allow the collection to be accessed by multiple threads for reading and writing, you must implement your own synchronization.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class CircularBuffer<T> : ICollection<T>
    {
        // Todo: Optimize Contains and CopyTo for Speed
        // Todo: Implement IList<T>
        // Todo: Get rid of _index since _capacity, _start and _size are enough to calculate the Index

        private int _start;
        private int _size;
        private readonly T[] _store;
        private readonly int _capacity;
        private readonly object _lockObj;
        private int _version;
        private int _index;

        public CircularBuffer(int capacity)
        {
            _lockObj = new Object();
            _capacity = capacity;
            _store = new T[capacity];
        }

        private int CalculateIndex(int currentIndex, int steps)
        {
            // ToDo: Handle Negative steps
            return (currentIndex + steps) % _capacity;
        }

        public void Add(T item)
        {
            _store[_index] = item;
            _index = CalculateIndex(_index,1);
            _size++;
            if (_size > _capacity)
            {
                _start = CalculateIndex(_start,1);
                _size = _capacity;
            }
            this._version++;
        }

        public void Clear()
        {
            if (this._size > 0)
            {
                Array.Clear(this._store, 0, this._size);
                this._size = 0;
                this._start = 0;
                this._index = 0;
            }
            this._version++;

        }

        public bool Contains(T item)
        {
            var tempArray = new T[_size];
            CopyTo(tempArray, 0);
            return tempArray.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            var tempArray = new T[_size];
            var i = 0;
            using (var en = GetEnumerator())
            {
                while (en.MoveNext())
                {
                    tempArray[i] = en.Current;
                    i++;
                }
            }            

            Array.Copy(tempArray, 0, array, arrayIndex, _size);
        }

        public int Count
        {
            get { return _size; }
        }

        public bool IsReadOnly
        {
            [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
            get { return false; }
        }

        public bool Remove(T item)
        {
            throw new InvalidOperationException("Removing of Elements is not possible.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CircularBufferEnumerator<T>(this);
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private class CircularBufferEnumerator<T> : IEnumerator<T>
        {
            private int _version;
            private CircularBuffer<T> _buffer;
            private T _current;
            private int? _index;
            private int _numSteps;

            public CircularBufferEnumerator(CircularBuffer<T> buffer)
            {
                _buffer = buffer;
                _version = buffer._version;
                _current = default(T);
                _index = null;
            }

            public T Current
            {
                get { return _current; }
            }

            object System.Collections.IEnumerator.Current
            {
                get
                {
                    if (_numSteps == 0 || _numSteps > _buffer._size)
                    {
                        throw new InvalidOperationException("Enumeration has either not started or has already finished.");
                    }
                    return _current;
                }
            }

            public void Reset()
            {
                if (_buffer._version != _version)
                {
                    throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
                }
                _numSteps = 0;
                _index = null;
                _current = default(T);
            }

            public bool MoveNext()
            {
                if (_buffer._version != _version)
                {
                    throw new InvalidOperationException("Collection was modified; enumeration operation may not execute.");
                }
                _numSteps++;
                if (_numSteps > _buffer._size)
                {
                    return false;
                }

                if (!_index.HasValue) _index = _buffer._start;

                _current = _buffer._store[_index.Value];

                _index = _buffer.CalculateIndex(_index.Value,1);

                return true;
            }



            public void Dispose()
            {
            }
        }
    }
}
