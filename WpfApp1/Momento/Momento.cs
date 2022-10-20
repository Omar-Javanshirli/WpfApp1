using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WpfApp1.Momento
{
    public class Originator
    {
        private string _state;

        public Originator(string state)
        {
            _state = state;
        }

        public void DoSomething(string filename)
        {
            this._state = filename;
        }
        public IMemento Save()
        {
            return new TextMemento(this._state);
        }

        public void Restore(IMemento memento)
        {
            if (!(memento is TextMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }
            this._state = memento.GetState();
        }
    }
    public interface IMemento
    {
        string GetState();
    }
    public class TextMemento : IMemento
    {
        private string _state;
        private DateTime _date;
        public TextMemento(string state)
        {
            this._state = state;
            this._date = DateTime.Now;
        }

        public string GetState()
        {
            return _state;
        }
    }
    public class CareTaker
    {
        public List<IMemento> _mementos = new List<IMemento>();
        private Originator _originator = null;

        public CareTaker(Originator originator)
        {
            _originator = originator;
        }

        public void Undo()
        {
            if (_mementos.Count == 0)
            {
                return;
            }
            var memento = _mementos[Index];
            Index--;
            try
            {
                _originator.Restore(memento);
            }
            catch (Exception)
            {
                this.Undo();
            }

        }
        public void Redo()
        {
            var memento = _mementos[Index];
            Index++;
        }
        public int Index { get; set; } = -1;
        public void BackUp()
        {
            this._mementos.Add(_originator.Save());
            Index++;
        }

    }
}
