namespace Restaurante.Services
{
    public static class AppSettings
    {
        #region private
        private static bool? _useSqlite;
        private static bool? _alreadyMigratedSqlite;
        #endregion

        public static bool UseSqlite 
        { 
            get { if (_useSqlite.HasValue) return _useSqlite.Value; else return false; } 
            set { if (!_useSqlite.HasValue) _useSqlite = value; } 
        }
        public static bool AlreadyMigratedSqlite
        {
            get { if (_alreadyMigratedSqlite.HasValue) return _alreadyMigratedSqlite.Value; else return false; }
            set { if (!_alreadyMigratedSqlite.HasValue) _alreadyMigratedSqlite = value; }
        }
        public static string JWTSecretKey { get; set; }
        public static int JWTHourExpirationTime { get; set; }
    }
}
