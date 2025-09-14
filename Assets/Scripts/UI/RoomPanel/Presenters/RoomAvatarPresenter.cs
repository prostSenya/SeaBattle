using System;
using UnityEngine;

namespace UI.RoomPanel.Presenters
{
    public class RoomAvatarPresenter : IRoomAvatarPresenter
    {
        public event Action<string> NickNameChanged;
        public event Action<string> ConnectionInfoChanged;
        public event Action<Sprite> AvatarChanged;
    }
}