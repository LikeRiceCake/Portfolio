using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularList<T>
{
    List<T> myList;

    int _current;

    public CircularList()
    {
        myList = new List<T>();

        _current = 0;
    }

    public void Add(T _contents)
    {
        myList.Add(_contents);
    }

    public void Previous()
    {
        _current--;
        if (_current < 0)
            _current = myList.Count - 1;
    }

    public void Next()
    {
        _current++;
        if(_current == myList.Count)
            _current = 0;
    }

    public T GetCurrent { get { return myList[_current]; } }
    public int SetCurrentIndex { set { _current = value; } }
}
