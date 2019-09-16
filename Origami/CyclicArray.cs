using System;
using System.Collections;
using System.Collections.Generic;

namespace Origami {
    public class CyclicArray<T> : IEnumerable<T> {
        public int Length => _size;

        private readonly T[] _array;
        private readonly int _size;
        private int _index;

        public CyclicArray(int size) {
            _size = size;
            _array = new T[size];
        }

        public CyclicArray(T[] array) {
            _size = array.Length;
            _array = array;
        }

        private int ClampedIndex(int i) {
            return (i % _size + _size) % _size;
        }

        public T PopAndPushBack() {
            var prev = _array[_index];
            _index = ClampedIndex(_index + 1);
            return prev;
        }

        public T Head() {
            return this[0];
        }

        public T Last() {
            return this[_size - 1];
        }

        public T this[int i] {
            get => _array[ClampedIndex(_index + i)];
            set => _array[ClampedIndex(_index + i)] = value;
        }

        public bool Equals(CyclicArray<T> other) {
            for (int i = 0; i < Length; i++) {
                if (other[i].Equals(this[0])) {
                    bool succeed = true;
                    for (int j = 0; j < Length; j++) {
                        if (!other[i + j].Equals(this[j])) {
                            succeed = false;
                            break;
                        }
                    }

                    if (succeed) {
                        return true;
                    }
                }
            }

            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() {
            return new CyclicEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return new CyclicEnumerator(this);
        }

        class CyclicEnumerator : IEnumerator<T> {
            private CyclicArray<T> array;
            private int index;

            public CyclicEnumerator(CyclicArray<T> value) {
                array = value;
                index = -1;
            }

            bool IEnumerator.MoveNext() {
                return ++index < array._size;
            }

            void IEnumerator.Reset() {
                index = -1;
            }

            T IEnumerator<T>.Current => array[index];

            object IEnumerator.Current => array[index];

            void IDisposable.Dispose() {
            }
        }
    }
}