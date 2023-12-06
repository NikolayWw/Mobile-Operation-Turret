using CodeBase.StaticData.Bullet;
using CodeBase.StaticData.Car;
using CodeBase.StaticData.Enemy;
using CodeBase.StaticData.Finish;
using CodeBase.StaticData.Level;
using CodeBase.StaticData.Windows;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CodeBase.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string WindowStaticDataPath = "Windows/WindowStaticData";
        private const string CarStaticDataPath = "Car/CarStaticData";
        private const string BulletStaticDataPath = "Bullet/BulletStaticData";
        private const string EnemyStaticDataPath = "Enemy/EnemyStaticData";
        private const string LevelStaticDataPath = "Level/LevelStaticData";
        private const string FinishStaticDataPath = "Finish/FinishStaticData";

        public CarStaticData CarStaticData { get; private set; }
        public WindowStaticData WindowData { get; private set; }
        public EnemyStaticData EnemyStaticData { get; private set; }
        public LevelStaticData LevelData { get; private set; }
        public FinishStaticData FinishStaticData { get; private set; }
        public BulletStaticData BulletStaticData { get; private set; }
        private Dictionary<BulletId, BulletConfig> _bulletConfigs;
        private Dictionary<EnemyId, EnemyConfig> _enemyConfigs;

        public void Load()
        {
            CarStaticData = Resources.Load<CarStaticData>(CarStaticDataPath);
            LevelData = Resources.Load<LevelStaticData>(LevelStaticDataPath);
            FinishStaticData = Resources.Load<FinishStaticData>(FinishStaticDataPath);
            WindowData = Resources.Load<WindowStaticData>(WindowStaticDataPath);
            LoadBullet();
            LoadEnemy();
        }

        public BulletConfig ForBullet(BulletId id) =>
            _bulletConfigs.TryGetValue(id, out BulletConfig cfg) ? cfg : null;

        public EnemyConfig ForEnemy(EnemyId id) =>
            _enemyConfigs.TryGetValue(id, out EnemyConfig cfg) ? cfg : null;

        private void LoadEnemy()
        {
            EnemyStaticData = Resources.Load<EnemyStaticData>(EnemyStaticDataPath);
            _enemyConfigs = EnemyStaticData.Configs.ToDictionary(x => x.Id, x => x);
        }

        private void LoadBullet()
        {
            BulletStaticData = Resources.Load<BulletStaticData>(BulletStaticDataPath);
            _bulletConfigs = BulletStaticData.Configs.ToDictionary(x => x.Id, x => x);
        }
    }
}