using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DelegateTEST
{
    public class CustomEventArgs : EventArgs
    {
        private string message; 

        public CustomEventArgs(string s)
        {
            message = s;    
        }

        public string Message
        {
            get { return message; }
            set { message = value; }
        }
    }

    class Susscriber
    {
        private string id;
        public Susscriber(string ID, Publisher pub)
        {
            id = ID;
            pub.RaiseCustomEvent += HandleCustomEvent;
        }

        private void HandleCustomEvent(object sender, CustomEventArgs e)
        {
            Console.WriteLine(id = " received this message {0} ", e.Message);
        }

    }
    class Publisher
    {
        public event EventHandler<CustomEventArgs> RaiseCustomEvent;

        public void DoSomething()
        {
            OnRaiseCustomEvent(new CustomEventArgs("Did Something"));
        }

        protected virtual void OnRaiseCustomEvent(CustomEventArgs e)
        {
            EventHandler<CustomEventArgs> handler = RaiseCustomEvent;

            if (handler != null)
            {
                e.Message += String.Format(" at {0}", DateTime.Now.ToString());

                handler(this, e);
            }
        }
        
    }


}
