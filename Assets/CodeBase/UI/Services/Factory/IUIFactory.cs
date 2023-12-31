﻿using CodeBase.Services;

namespace CodeBase.UI.Services.Factory
{
    public interface IUIFactory : IService
    {
        void CreateUIRoot();
        void CreateHUD();
        void CreateLoseWindow();
        void CreateWinWindow();
        void CreateStartGameWindow();
    }
}