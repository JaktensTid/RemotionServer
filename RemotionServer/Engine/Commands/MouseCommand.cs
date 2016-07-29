using Engine;

namespace RemotionServer.Engine.Commands
{
    /// <summary>
    /// Инкапсулирует поведение для событий мыши, таких как ЛМБ, РМБ и т.д
    /// </summary>
    internal class MouseCommand : ICommand
    {
        private const string LMBCLICK = "10";
        private const string RMBCLICK = "11";
        private const string MMBUP = "12";
        private const string MMBDOWN = "13";
        private const string LMBDOWN = "14";
        private const string LMBUP = "15";
        private const string RMBDOWN = "16";
        private const string RMBUP = "17";

        public void Execute(string parameter)
        {
            switch (parameter)
            {
                case (LMBCLICK):
                    {
                        ControllerEngine.DoLeftButtonMouseClick();
                    }
                    break;
                case (RMBCLICK):
                    {
                        ControllerEngine.DoRightButtonMouseClick();
                    }
                    break;
                case (LMBDOWN):
                    {
                        ControllerEngine.DoLeftButtonMouseDown();
                    }
                    break;
                case (LMBUP):
                    {
                        ControllerEngine.DoLeftButtonMouseUp();
                    }
                    break;
                case (RMBDOWN):
                    {
                        ControllerEngine.DoRightButtonMouseDown();
                    }
                    break;
                case (RMBUP):
                    {
                        ControllerEngine.DoRightButtonMouseUp();
                    }
                    break;
                case (MMBUP):
                    {
                        ControllerEngine.MouseWheelUp();
                    }
                    break;
                case (MMBDOWN):
                    {
                        ControllerEngine.MouseWheelDown();
                    }
                    break;
            }
        }
    }
}
