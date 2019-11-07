using System;
using Windows.Storage;

namespace MyEvents.Client.Organizer.Helper
{
    public static class TileHelper
    {
        public enum TileType { Event, Session};

        public static string SetEventTileId(int id)
        {
            return string.Format("MyEvents_Event{0}", id);
        }

        public static string SetEventTileActivationArguments(int id)
        {
            return string.Format("Event={0}", id);
        }

        public static string SetSessionTileId(int id)
        {
            return string.Format("MyEvents_Session{0}", id);
        }

        public static string SetSessionTileActivationArguments(int id)
        {
            return string.Format("Session={0}", id);
        }

        public static int GetTileId()
        {
            var args = ApplicationData.Current.LocalSettings.Values["TileArgs"].ToString().Split('=');
            return Int32.Parse(args[1]);   
        }

        public static string GetTileType()
        {
            var args = ApplicationData.Current.LocalSettings.Values["TileArgs"].ToString().Split('=');
            return args[0];
        }
    }
}
