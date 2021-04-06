using System;
using System.Linq;
// This is an example of the CommandPattern
namespace CommandPattern
{
    public interface ICommand
    {
        void Execute();
    }

    /* The Invoker class */
    public class Switch
    {
        ICommand _closedCommand;
        ICommand _openedCommand;
        ICommand _dimCommand;

        public Switch(ICommand closedCommand, ICommand openedCommand, ICommand dimCommand)
        {
            this._closedCommand = closedCommand;
            this._openedCommand = openedCommand;
            this._dimCommand = dimCommand;
        }

        // Close the circuit / power on
        public void Close()
        {
            this._closedCommand.Execute();
        }

        // Open the circuit / power off
        public void Open()
        {
            this._openedCommand.Execute();
        }

        public void Dim()
        {
            this._dimCommand.Execute();
        }
    }

    /* An interface that defines actions that the receiver can perform */
    public interface ISwitchable
    {
        void PowerOn();
        void PowerOff();
        void SetDimLevel(int dimLevel);
    }

    /* The Receiver class */
    public class Light : ISwitchable
    {
        public void PowerOn()
        {
            Console.WriteLine("The light is on");
        }

        public void PowerOff()
        {
            Console.WriteLine("The light is off");
        }

        public void SetDimLevel(int dimLevel)
        {
            Console.WriteLine($"The dim level was set to {dimLevel}");
        }
    }

    /* The Command for turning off the device - ConcreteCommand #1 */
    public class CloseSwitchCommand : ICommand
    {
        private ISwitchable _switchable;

        public CloseSwitchCommand(ISwitchable switchable)
        {
            _switchable = switchable;
        }

        public void Execute()
        {
            _switchable.PowerOff();
        }
    }

    /* The Command for turning on the device - ConcreteCommand #2 */
    public class OpenSwitchCommand : ICommand
    {
        private ISwitchable _switchable;

        public OpenSwitchCommand(ISwitchable switchable)
        {
            _switchable = switchable;
        }

        public void Execute()
        {
            _switchable.PowerOn();
        }
    }

    /* The test class or client */
    internal class Program
    {
        public static void Main(string[] arguments)
        {
            ISwitchable lamp = new Light();

            // Pass reference to the lamp instance to each command
            ICommand switchClose = new CloseSwitchCommand(lamp);
            ICommand switchOpen = new OpenSwitchCommand(lamp);
            ICommand switchDim = new DimSwitchCommand(lamp, 10);

            // Pass reference to instances of the Command objects to the switch
            Switch switchen = new Switch(switchClose, switchOpen, switchDim);

            bool toggle = true;

            foreach (var i in Enumerable.Range(1,10))
            {
                if (toggle)
                {
                    switchen.Close();
                    toggle = false;
                }
                else 
                {
                    switchen.Open();
                    switchen.Dim();
                    toggle = true;
                }
            }
        }
    }

    internal class DimSwitchCommand : ICommand
    {
        private ISwitchable _switchable;
        private int _dimLevel;

        public DimSwitchCommand(ISwitchable switchable, int dimLevel)
        {
            this._switchable = switchable;
            this._dimLevel = dimLevel;
        }

        public void Execute()
        {
            _switchable.SetDimLevel(_dimLevel);
        }
    }
}
