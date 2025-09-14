using System;
using UnityEngine;

namespace UI.RoomPanel.Presenters
{
    public interface IRoomAvatarPresenter
    {
        event Action<string> NickNameChanged;
        event Action<string> ConnectionInfoChanged;
        event Action<Sprite> AvatarChanged ;
    }
}