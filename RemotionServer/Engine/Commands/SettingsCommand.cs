using System;
using System.Collections.Generic;
using System.Windows;
using Engine;
using RemotionServer.Properties;

namespace RemotionServer.Engine.Commands
{
    /// <summary>
    /// Инкапсулирует контроллер юзер-настроек для кнопок быстрых действий
    /// </summary>
    internal class SettingsCommand : ICommand
    {
        public const int TURN_OFF_SOUND = 1;
        public const int MIMIMIZE_ALL = 2;
        public const int CLOSE_ACTIVE_WINDOW = 4;
        public const int EXPAND_ALL = 7;
        public const int BROWSER_BACKWARD = 8;
        public const int BROWSER_FORWARD = 9;
        public const int BROWSER_REFRESH_PAGE = 10;
        public const int COPY_SELECTION = 11;
        public const int PASTE = 12;
        public const int CUT = 13;
        public const int VOLUMEUP = 14;
        public const int VOLUMEDOWN = 15;
        public const int ZOOMIN = 16;
        public const int ZOOMOUT = 17;
        public const int SCROLLUP = 18;
        public const int SCROLLDOWN = 19;
        public const int NOTHING = 0;

        static SettingsCommand()
        {
            _action1 = action1;
            _action2 = action2;
        }

        private static int _action1, _action2;
        public static int action1
        {
            get
            {
                _action1 = UserSettings.Default.action1;
                return _action1;
            }
            set
            {
                _action1 = value;
                UserSettings.Default.action1 = value;
                UserSettings.Default.Save();
            }
        }

        public static int action2
        {
            get
            {
                _action2 = UserSettings.Default.action2;
                return _action2;
            }
            set
            {
                _action2 = value;
                UserSettings.Default.action2 = value;
                UserSettings.Default.Save();
            }
        }

        /// <summary>
        /// Получает видимость скролл-интерфейса в зависимости от пользовательского выбора действий
        /// </summary>
        public static Visibility GetScrollInterfaceVisibility
        {
            get
            {
                if (_action1 == SCROLLUP ||
                    _action1 == SCROLLDOWN ||
                   _action2 == SCROLLUP ||
                    _action2 == SCROLLDOWN)
                {
                    return Visibility.Visible;
                }
                return Visibility.Hidden;
            }
        }

        /// <summary>
        /// Локализованная строка юзер-действия --- соответствующий Int32 флаг
        /// </summary>
        public static Dictionary<string, int> name_flag_dictionary
        {
            get
            {
                return new Dictionary<string, int>
                {
                    { Resources.TurnOffSoundMenu, TURN_OFF_SOUND },
                    { Resources.MinimizeAllMenu, MIMIMIZE_ALL },
                    { Resources.CloseActiveWindowMenu, CLOSE_ACTIVE_WINDOW },
                    { Resources.ExpandAllMenu, EXPAND_ALL },
                    { Resources.BrowserBackwardMenu, BROWSER_BACKWARD },
                    { Resources.BrowserForwardMenu, BROWSER_FORWARD },
                    { Resources.BrowserRefreshPageMenu, BROWSER_REFRESH_PAGE },
                    { Resources.CopyMenu, COPY_SELECTION },
                    { Resources.PasteMenu, PASTE },
                    { Resources.CutMenu, CUT },
                    {Resources.VolumeUpMenu, VOLUMEUP},
                    {Resources.VolumeDownMenu, VOLUMEDOWN },
                    {Resources.ZoomInMenu, ZOOMIN },
                    {Resources.ZoomOutMenu, ZOOMOUT },
                    { Resources.ScrollUpMenu, SCROLLUP },
                    { Resources.ScrollDownMenu, SCROLLDOWN },
                    { Resources.NothingMenu, NOTHING }
                };
            }
        }
        /// <summary>
        /// Cоответствующий Int32 флаг --- kокализованная строка юзер-действия
        /// </summary>
        public static Dictionary<int, string> flag_name_dictionary
        {
            get
            {
                return new Dictionary<int, string>
                {
                    {TURN_OFF_SOUND, Resources.TurnOffSoundMenu  },
                    {MIMIMIZE_ALL, Resources.MinimizeAllMenu  },
                    {CLOSE_ACTIVE_WINDOW, Resources.CloseActiveWindowMenu  },
                    {EXPAND_ALL, Resources.ExpandAllMenu  },
                    {BROWSER_BACKWARD, Resources.BrowserBackwardMenu  },
                    {BROWSER_FORWARD, Resources.BrowserForwardMenu  },
                    {BROWSER_REFRESH_PAGE, Resources.BrowserRefreshPageMenu  },
                    {COPY_SELECTION, Resources.CopyMenu  },
                    {PASTE, Resources.PasteMenu  },
                    {CUT, Resources.CutMenu  },
                    {VOLUMEUP,Resources.VolumeUpMenu },
                    {VOLUMEDOWN,Resources.VolumeDownMenu  },
                    {ZOOMIN, Resources.ZoomInMenu  },
                    {ZOOMOUT, Resources.ZoomOutMenu },
                    {SCROLLUP, Resources.ScrollUpMenu },
                    {SCROLLDOWN, Resources.ScrollDownMenu },
                    {NOTHING, Resources.NothingMenu }
                };
            }
        }
        /// <summary>
        /// Int32 флаг --- действие, связанное с флагом
        /// </summary>
        private static Dictionary<int, Action> flag_action_dictionary
        {
            get
            {
                return new Dictionary<int, Action>
                {
                    { TURN_OFF_SOUND,ControllerEngine.Mute },
                    { MIMIMIZE_ALL, ControllerEngine.MinimizeAll},
                    { CLOSE_ACTIVE_WINDOW, ControllerEngine.CloseActiveWindow },
                    { EXPAND_ALL, ControllerEngine.ExpandAll },
                    { BROWSER_BACKWARD,  ControllerEngine.BrowserBackward},
                    { BROWSER_FORWARD, ControllerEngine.BrowserForward},
                    { BROWSER_REFRESH_PAGE, ControllerEngine.RefreshPage },
                    { COPY_SELECTION,  ControllerEngine.CopySelection},
                    { PASTE, ControllerEngine.Paste },
                    { CUT, ControllerEngine.Cut},
                    {VOLUMEUP, ControllerEngine.VolUp  },
                    {VOLUMEDOWN, ControllerEngine.VolDown },
                    {ZOOMIN, ControllerEngine.ZoomIn },
                    {ZOOMOUT, ControllerEngine.ZoomOut },
                    {SCROLLUP, ControllerEngine.MouseWheelUp },
                    {SCROLLDOWN, ControllerEngine.MouseWheelDown },
                    { NOTHING, ()=> { } }
                };
            }
        }

        public void Execute(string parameter)
        {
            if (parameter == "1")
            {
                flag_action_dictionary[_action1].Invoke();
            }
            if (parameter == "2")
            {
                flag_action_dictionary[_action2].Invoke();
            }
        }
    }
}
